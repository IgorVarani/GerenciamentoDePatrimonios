using GestaoPatrimonios.Applications.Services;
using GestaoPatrimonios.DTOs.LogPatrimonioDto;
using GestaoPatrimonios.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestaoPatrimonios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogPatrimonioController : ControllerBase
    {
        public readonly LogPatrimonioService _service;

        public LogPatrimonioController(LogPatrimonioService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<List<ListarLogPatrimonioDto>> Listar()
        {
            List<ListarLogPatrimonioDto> logs = _service.Listar();
            return Ok(logs);
        }

        [Authorize]
        [HttpGet("patrimonio/{id}")]
        public ActionResult<List<ListarLogPatrimonioDto>> BuscarPorPatrimonio(Guid id)
        {
            try
            {
                List<ListarLogPatrimonioDto> logs = _service.BuscarPorPatrimonio(id);
                return Ok(logs);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
