using GestaoPatrimonios.Applications.Services;
using GestaoPatrimonios.DTOs.LocalizacaoDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GestaoPatrimonios.Exceptions;

namespace GestaoPatrimonios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalizacaoController : ControllerBase
    {
        private readonly LocalizacaoService _service;

        public LocalizacaoController(LocalizacaoService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ListarLocalizacaoDto>> Listar()
        {
            List<ListarLocalizacaoDto> localizacoes = _service.Listar();
            return Ok(localizacoes);
        }

        [HttpGet("{id}")]
        public ActionResult<ListarLocalizacaoDto> BuscarPorId(Guid id)
        {
            try
            {
                ListarLocalizacaoDto localizacao = _service.BuscarPorId(id);
                return Ok(localizacao);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Adicionar(CriarLocalizacaoDto dto)
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
        public ActionResult Atualizar(Guid id, CriarLocalizacaoDto dto)
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
