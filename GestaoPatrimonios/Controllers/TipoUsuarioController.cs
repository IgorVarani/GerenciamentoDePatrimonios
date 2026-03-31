using GestaoPatrimonios.Applications.Services;
using GestaoPatrimonios.DTOs.LocalizacaoDto;
using GestaoPatrimonios.DTOs.TipoUsuarioDto;
using GestaoPatrimonios.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestaoPatrimonios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoUsuarioController : ControllerBase
    {
        private readonly TipoUsuarioService _service;

        public TipoUsuarioController(TipoUsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ListarTipoUsuarioDto>> Listar()
        {
            List<ListarTipoUsuarioDto> tipos = _service.Listar();
            return Ok(tipos);
        }

        [HttpGet("{id}")]
        public ActionResult<ListarTipoUsuarioDto> BuscarPorId(Guid id)
        {
            try
            {
                ListarTipoUsuarioDto tipos = _service.BuscarPorId(id);
                return Ok(tipos);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Adicionar(CriarTipoUsuarioDto dto)
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
        public ActionResult Atualizar(Guid id, CriarTipoUsuarioDto dto)
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
