using GestaoPatrimonios.Applications.Services;
using GestaoPatrimonios.DTOs.Bairro;
using GestaoPatrimonios.DTOs.StatusPatrimonioDto;
using GestaoPatrimonios.DTOs.StatusTransferenciaDto;
using GestaoPatrimonios.DTOs.TipoPatrimonioDto;
using GestaoPatrimonios.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestaoPatrimonios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusPatrimonioController : ControllerBase
    {
        private readonly StatusPatrimonioService _service;

        public StatusPatrimonioController(StatusPatrimonioService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ListarStatusPatrimonioDto>> Listar()
        {
            return Ok(_service.Listar());
        }

        [HttpGet("{id}")]
        public ActionResult<ListarStatusPatrimonioDto> BuscarPorId(Guid id)
        {
            try
            {
                return Ok(_service.BuscarPorId(id));
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Adicionar(CriarStatusPatrimonioDto dto)
        {
            try
            {
                _service.Adicionar(dto);
                return Created();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Atualizar(Guid id, CriarStatusPatrimonioDto dto)
        {
            try
            {
                _service.Atualizar(id, dto);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
