using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JPApi.Models;
using JPApi.Services.Interfaces;
using Lib;
using Lib.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JPApi.Controllers
{
    [Produces("application/json")]
    [Route("api/")]
    public class ApiController : Controller
    {

        private readonly IEventHubService _eventHubService;

        public ApiController(IEventHubService eventHubService)
        {
            _eventHubService = eventHubService;
        }


        //POST CAMBIO DI DISPOSITIVO VEICOLO
        [HttpPost]
        [Route("assignDevice/{raptorId}")]
        public async Task<IActionResult> SendGeoTracking([FromRoute] int raptorId, [FromBody] int geneticMutationId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var msg = JsonConvert.SerializeObject(new
                    {
                        Type = "AssignGeneticMutation",
                        Msg = new { RaptorId = raptorId, GeneticMutationId = geneticMutationId }
                    });
                    await _eventHubService.SendMsg(msg);

                    return Ok();
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }


        //POST INVIO DATI PORTA APERTA
        [HttpPost]
        [Route("signalDoor")]
        public async Task<IActionResult> SendGeoTracking([FromBody] GeoTrackingMawDtp obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var msg = JsonConvert.SerializeObject(new
                    {
                        Type = "MawTelemetry",
                        Msg = JsonConvert.SerializeObject(new
                        {
                            Tracking = JsonConvert.SerializeObject(new GeoTracking
                            {
                                IdRaptor = obj.IdRaptor,
                                IdWinningGeneticMutation = obj.IdWinningGeneticMutation,
                                Tempo = obj.Tempo,
                                X = obj.X,
                                Y = obj.Y
                            }),
                            Data = JsonConvert.SerializeObject(new RaptorMetaData
                            {
                                FauciAperte = obj.FauciAperte,
                                PersoneMangiate = obj.PersoneMangiate
                            })
                        })
                    });
                    await _eventHubService.SendMsg(msg);

                    return Ok();
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        //POST DATI TELEMETRICI [INVIA OK]
        [HttpPost]
        [Route("sendTracking")]
        public async Task<IActionResult> SendGeoTracking([FromBody] GeoTracking gt)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var msg = JsonConvert.SerializeObject(new
                    {
                        Type = "Telemetry",
                        Msg = JsonConvert.SerializeObject(gt)
                    });
                    await _eventHubService.SendMsg(msg);

                    return Ok();
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }



    }
}