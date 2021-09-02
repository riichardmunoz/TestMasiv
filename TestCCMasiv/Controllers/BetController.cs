using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestCCMasiv.Data;
using TestCCMasiv.Models;
using TestCCMasiv.Services;

namespace TestCCMasiv.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BetController : ControllerBase
    {
        private IBetService betService;
        public BetController(IBetService betService)
        {
            this.betService = betService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Bet newBet,
             [FromHeader(Name = "userId")] string UserId)
        {
            try
            {
                Bet successBet = new Bet();
                if (string.IsNullOrEmpty(UserId))
                    return BadRequest(Utils.Utils.CreateMessageError(message: "No se encontró {userId} válido en la cabecera."));
                newBet.Id = Guid.NewGuid().ToString();
                newBet.IdUsuario = UserId;
                successBet = betService.CreateBet(newBet);

                return Ok(successBet);
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }
        }
        [HttpPost("{rouletteId}/close")]
        public IActionResult CloseBets(string rouletteId)
        {
            try
            {
                ResultBet result = null;
                result = betService.CloseBet(rouletteId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
