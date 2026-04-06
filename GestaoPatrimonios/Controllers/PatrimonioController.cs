using GestaoPatrimonios.Applications.Services;
using GestaoPatrimonios.DTOs.PatrimonioDto;
using GestaoPatrimonios.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestaoPatrimonios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatrimonioController : ControllerBase
    {
        private readonly PatrimonioService _service;

        PatrimonioController(PatrimonioService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ListarPatrimonioDto>> Listar()
        {
            List<ListarPatrimonioDto> listar = _service.Listar();
            if (listar == null)
            {
                return NotFound(listar);
            }

            return Ok(listar);
        }

        [HttpGet("id/{id}")]
        public ActionResult<ListarPatrimonioDto> BuscarPorId(Guid id)
        {
            try
            {
                ListarPatrimonioDto dto = _service.BuscarPorId(id);
                return Ok(dto);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("id/{numeroPatrimonio}/{patrimonioId?}")]
        public ActionResult<ListarPatrimonioDto> BuscarPorNumeroPatrimonio(string numeroPatrimonio, Guid? patrimonioId = null)
        {
            try
            {
                ListarPatrimonioDto dto = _service.BuscarPorNumeroPatrimonio(numeroPatrimonio, patrimonioId);
                return Ok(dto);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<CriarPatrimonioDto> Adicionar(ListarPatrimonioDto dto)
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

        [HttpPut]
        public ActionResult<ListarPatrimonioDto> Atualizar(Guid id, CriarPatrimonioDto dto)
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

        [HttpPatch("tipoPatrimonio/{id}")]
        public ActionResult<AtualizarStatusPatrimonioDto> AtualizarStatus(Guid id, AtualizarStatusPatrimonioDto dto)
        {
            try
            {
                _service.AtualizarStatus(id, dto);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
