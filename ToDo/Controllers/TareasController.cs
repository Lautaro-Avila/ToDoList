using Microsoft.AspNetCore.Mvc;
using ToDo;
using ToDo.Domain.dto;
using ToDo.Servicios.InterfazServicio;

[ApiController]
[Route("api/tareas")]
public class TareasController : ControllerBase
{
    private readonly ITareasService _tareasService;

    public TareasController(ITareasService tareasService)
    {
        _tareasService = tareasService;
    }

    [HttpGet("Listar todas las tareas")]
    public async Task<ActionResult<List<Tarea>>> GetAllTareas()
    {
     
            var tareas = await _tareasService.GetAllTareasAsync();
            return Ok(tareas);
  
    }

    [HttpPost("Agregar nueva tarea")]
    public async Task<ActionResult<bool>> AddNewTarea([FromBody] TareasDTO tareaDTO)
    {
     
            var result = await _tareasService.AddNewTareaAsync(tareaDTO);
            if (result)
            {
                return Ok("La tarea se agrego correctamente");
            }
            else
            {
                return BadRequest("No se pudo agregar la tarea. Verifica el estado proporcionado.");
            }
 
    }
    
    [HttpPut("{id} | Modificar tarea")]
    public async Task<ActionResult<bool>> UpdateTareas(int id, [FromBody] TareasDTO tareaDTO)
    {
      
            var result = await _tareasService.UpdateTareasAsync(id, tareaDTO);
            if (result)
            {
                return Ok("La tarea se modifico correctamente");
            }
            else
            {
                return NotFound("Error al modificar la tarea.Verifique que el id es correcto y recuerde que los estados validos son \"en curso\" \"pendiente \" \"finalizado\"");
            }
 
    }

    [HttpPut("{id} | Modificar estado de una tarea")]
    public async Task<ActionResult<bool>> UpdateEstado(int id, [FromBody] string nuevoEstado)
    {
        
            var tareaDTO = new TareasDTO
            {
                Estado = nuevoEstado
            };

            var result = await _tareasService.UpdateEstadoAsync(id, tareaDTO);

            if (result)
            {
                return Ok("El estado se modifico correctamente");
            }
            else
            {
                return NotFound("Error al modificar el estado. Verifique que el id es correcto y recuerde que los estados validos son \"en curso\" \"pendiente \" \"finalizado\"");
            }
        
  
    }

    [HttpDelete("{id} | Eliminar tarea")]
    public async Task<ActionResult<bool>> DeleteTareas(int id)
    {
     
            var result = await _tareasService.DeleteTareasAsync(id);
            if (result)
            {
                return Ok("La tarea se elimino correctamente");
            }
            else
            {
                return NotFound($"No se encontró la tarea con ID {id}");
            }
        
 
    }




    [HttpGet("Listar tareas activas")]

    public async Task<ActionResult<List<Tarea>>> GetAllNoDeleteAsync()
    {
        
            var tareas = await _tareasService.GetAllNoDeleteAsync();
            return Ok(tareas);
        

    }

    [HttpGet("Listar tareas eliminadas")]
    public async Task<ActionResult<List<Tarea>>> GetAllDeleteAsync()
    {
      
            var tareas = await _tareasService.GetAllDeleteAsync();
            return Ok(tareas);
        
  
    }

}
