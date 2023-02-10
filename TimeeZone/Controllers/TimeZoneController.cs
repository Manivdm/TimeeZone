using Microsoft.AspNetCore.Mvc;
using NodaTime;
using NodaTime.Extensions;
using NodaTime.TimeZones;
using TimeeZone.Model;

namespace TimeeZone.Controllers
{


    [Route("api/[controller]")]
    public class TimeZoneController : Controller
    {
        [HttpGet("{city}")]
        public ActionResult<TimeZoneModel> Get(string city)
        {
            try
            {
                var decodedId = System.Uri.UnescapeDataString(city);
                var timeZone = DateTimeZoneProviders.Tzdb[decodedId];
                var currentTime = SystemClock.Instance.GetCurrentInstant().InZone(timeZone).ToDateTimeOffset();

                return new TimeZoneModel
                {
                    TimeZoneId = decodedId,
                    CurrentTime = currentTime
                };
            }
            catch (DateTimeZoneNotFoundException)
            {
                return BadRequest($"The time zone '{city}' was not found in the TZDB database.");
            }
        }
    }




}
