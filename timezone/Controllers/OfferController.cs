using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using timezone.DataBase;
using timezone.Models;

namespace timezone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private readonly TimeDBContext _dbContext;

        public OfferController(TimeDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddOffer([FromBody] OfferModel offerRequest)
        {
            offerRequest.Id = Guid.NewGuid();
            DateTime inTime = offerRequest.Time;
            //TimeZoneInfo convertTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/London");
            TimeZoneInfo convertTimeZone = TimeZoneInfo.Local;

            DateTimeOffset targetDateTimeOffset = new DateTimeOffset(inTime, TimeSpan.Zero);
            DateTime utcDateTime = TimeZoneInfo.ConvertTimeToUtc(targetDateTimeOffset.DateTime, convertTimeZone);
            offerRequest.Time = utcDateTime;
            await _dbContext.AddAsync(offerRequest);
            await _dbContext.SaveChangesAsync();
            return Ok(offerRequest);

        }

        [HttpGet]
        public async Task<IActionResult> GetOffer()
        {
            //DateTime utcTime = new DateTime(2023, 05, 15, 10, 0, 0);
            //TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/London");
            //if (targetTimeZone.IsDaylightSavingTime(utcTime))
            //{
            //    return Ok("YES DST ZONE");
            //}
            //return Ok("NO");

            var offers = await _dbContext.OfferModels.ToListAsync();

            foreach (var item in offers)
            {
                var utcTime = item.Time;

                //TimeZoneInfo convertTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/London");
                TimeZoneInfo convertTimeZone = TimeZoneInfo.Local;

                DateTimeOffset storedDateTimeOffset = new DateTimeOffset(utcTime, TimeSpan.Zero);
                DateTimeOffset targetDateTimeOffset = TimeZoneInfo.ConvertTime(storedDateTimeOffset, convertTimeZone);
                DateTime outTime = targetDateTimeOffset.DateTime;
                item.Time = outTime;
            }

            return Ok(offers);
        }


    }
}
