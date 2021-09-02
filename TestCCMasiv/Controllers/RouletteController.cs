using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCCMasiv.Models;
using TestCCMasiv.Repositories.Roulette;
using TestCCMasiv.Services;

namespace TestCCMasiv.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouletteController : ControllerBase
    {
        private IRouletteService rouletteService;

        public RouletteController(IRouletteService rouletteService)
        {
            this.rouletteService = rouletteService;
        }

        [HttpPost]
        public IActionResult NewRoulette()
        {
            Roulette roulette = rouletteService.create();

            return Ok(roulette);
        }
        [HttpPut("{id}/open")]
        public IActionResult OpenRoulette([FromRoute(Name = "id")] string id)
        {
            try
            {
                rouletteService.OpenRoulette(id);

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(405);
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(rouletteService.GetAll());
        }

    }
}
