using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using timezone.Api.Models;
using timezone.DataBase;
using timezone.Models;
using timezone.Services;

namespace timezone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private readonly TimeDBContext _dbContext;
        private readonly TimeZoneConversion _timeZoneConversion;
        private readonly OfferValidCheck _offerValidCheck;

        public OfferController(TimeDBContext dbContext, TimeZoneConversion timeZoneConversion, OfferValidCheck offerValidCheck)
        {
            _dbContext = dbContext;
            _timeZoneConversion = timeZoneConversion;
            _offerValidCheck = offerValidCheck;
        }

        [HttpPost]
        public async Task<IActionResult> AddOffer([FromBody] OfferModel offerRequest)
        {
            var inTimeZoneId = "Europe/London";
            offerRequest.Id = Guid.NewGuid();
   
            offerRequest.StartTime = _timeZoneConversion.ToUctTime(offerRequest.StartTime,inTimeZoneId);
            offerRequest.EndTime = _timeZoneConversion.ToUctTime(offerRequest.EndTime,inTimeZoneId);
            await _dbContext.AddAsync(offerRequest);
            await _dbContext.SaveChangesAsync();
            return Ok(offerRequest);
        }

        [HttpGet]
        public async Task<IActionResult> GetOffer()
        {
            var outTimeZoneId = "Europe/London";
            var offers = await _dbContext.OfferModels.ToListAsync();
            var offerResponseList = new List<OfferResponseModel>();
            foreach (var item in offers)
            {
                var msg = _offerValidCheck.OfferMessage(item.StartTime,item.EndTime);
                item.StartTime = _timeZoneConversion.ToOutTime(item.StartTime,outTimeZoneId);
                item.EndTime = _timeZoneConversion.ToOutTime(item.EndTime,outTimeZoneId);


                var offerResponse = new OfferResponseModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    StartTime = item.StartTime,
                    EndTime = item.EndTime,
                    validOfferMsg = msg,
                    currentTime = DateTime.UtcNow
                };
                offerResponseList.Add(offerResponse);
                
            }
            return Ok(offerResponseList);
        }

    }
}
