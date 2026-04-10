using GestaoPatrimonios.Applications.Services;
using GestaoPatrimonios.DTOs.PatrimonioDto;
using GestaoPatrimonios.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [Authorize]
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

        [Authorize]
        [HttpGet("{id}")]
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

        [Authorize(Roles = "Coordenador")]
        [HttpPost("importar-csv")]
        public ActionResult Adicionar(IFormFile arquivoCsv)
        {
            try
            {
                string usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrWhiteSpace(usuarioIdClaim))
                {
                    return Unauthorized("Usuário não autenticado.");
                }

                Guid usuarioId = Guid.Parse(usuarioIdClaim);

                _service.Adicionar(arquivoCsv, usuarioId);

                return Created();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Coordenador")]
        [HttpPatch("{id}/status")]
        public ActionResult AtualizarStatus(Guid id, AtualizarStatusPatrimonioDto dto)
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
