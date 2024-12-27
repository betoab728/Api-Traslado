using Microsoft.AspNetCore.Mvc;
using ApiGrupoOptico.Services;
using ApiGrupoOptico.Models;

namespace ApiGrupoOptico.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TiendaController : ControllerBase
    {
        private readonly ITiendaService _tiendaService;
        
        public TiendaController(ITiendaService tiendaService)
        {
            _tiendaService = tiendaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTiendas()
        {
            try
            {
                var tiendas = await _tiendaService.ListarTiendasAsync();
                return Ok(tiendas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("{idAlmacenOrigen}/almacenes-destino")]

        public async Task<IActionResult> GetAlmacenesDestino(int idAlmacenOrigen)
        {
            try
            {
                var almacenesDestino = await _tiendaService.ListarAlmacenesDestinoAsync(idAlmacenOrigen);
                return Ok(almacenesDestino);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        //listar vitrinas de una tienda
        [HttpGet("{idAlmacenDestino}/vitrinas")]
        public async Task<IActionResult> GetVitrinas(int idAlmacenDestino)
        {
            try
            {
                var vitrinas = await _tiendaService.ListarVitrinasAsync(idAlmacenDestino);
                return Ok(vitrinas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

       

    }
}
