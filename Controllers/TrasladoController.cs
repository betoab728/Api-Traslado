using Microsoft.AspNetCore.Mvc;
using ApiGrupoOptico.Services;  
using ApiGrupoOptico.Models;

namespace ApiGrupoOptico.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrasladoController : ControllerBase
    {
        private readonly ITrasladoService _trasladoService;

        public TrasladoController(ITrasladoService trasladoService)
        {
            _trasladoService = trasladoService;
        }

        [HttpPost]
        public async Task<IActionResult> RealizarTraslado([FromBody] Traslado traslado)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                await _trasladoService.RealizarTrasladoAsync(traslado);
                return Ok(new { message = "Traslado realizado correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }

        }

        [HttpGet("{codigoBarras}/{idAlmacen}")]
        public async Task<IActionResult> BuscarProducto(string codigoBarras, int idAlmacen)
        {
            try
            {
                var producto = await _trasladoService.BuscarProductoAsync(codigoBarras, idAlmacen);
                if (producto == null)
                    return NotFound(new { message = "Producto no encontrado." });
                return Ok(producto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

    }
}
