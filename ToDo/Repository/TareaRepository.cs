using Microsoft.EntityFrameworkCore;
using ToDo.Domain.dto;
using ToDo.Repository.Interfaces;
using ToDo.Servicios;
using ToDo.Servicios.Extensions;

namespace ToDo.Repository
{
    public class TareaRepository : ITareaRepository 
    {
        private readonly ToDoContext _todoContext;

        public TareaRepository(ToDoContext context)
        {
            _todoContext = context;
        }

        public async Task<List<Tarea>> GetTareasAsync()
        {
            return await _todoContext.Tareas.ToListAsync();
        }

        public async Task<bool> AddTareaAsync(TareasDTO tareaDTO)
        {
            bool isValid = ValidateIntegrity.ValidateIntegrityTarea(tareaDTO);
            if (!isValid) return false;

            bool isValidTituloEstado = ValidateTituloEstado.ValidateTituloEstadoTarea(tareaDTO);
            if (!isValidTituloEstado) return false;



            var tarea = new Tarea();
            tarea.Estado = tareaDTO.Estado.ToCapitalizate();
            tarea.Titulo = tareaDTO.Titulo.ToCapitalizate();
            tarea.Descripcion = tareaDTO.Descripcion;
            tarea.Activo = true;
            tarea.FechaAlta = DateTime.Now;
            tarea.FechaModificacion = DateTime.Now;

            await  _todoContext.Tareas.AddAsync(tarea);

            int cambios = await _todoContext.SaveChangesAsync();

            return cambios > 0;
        }
        public async Task<bool> UpdateTareaAsync(int id, TareasDTO tarea)
        {
      
            var tareaMatch = await _todoContext.Tareas.FirstOrDefaultAsync(f => f.Id == id);
            if (tareaMatch == null) return false;

           bool isValid = ValidateIntegrity.ValidateIntegrityTarea(tarea);

            if (!isValid) return false;

            tareaMatch.Estado = tarea.Estado.ToCapitalizate();
            tareaMatch.Titulo = tarea.Titulo.ToCapitalizate();
            tareaMatch.Descripcion = tarea.Descripcion;
            tareaMatch.FechaModificacion = DateTime.Now;

            int cambios = await _todoContext.SaveChangesAsync();

            return cambios > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var tareaMatch = await _todoContext.Tareas.FirstOrDefaultAsync(f => f.Id == id);
            if (tareaMatch == null) return false;

            tareaMatch.Activo = false;
            tareaMatch.FechaModificacion = DateTime.Now;

            int cambios = await _todoContext.SaveChangesAsync();

            return cambios > 0;
        }

        public async Task<bool> UpdateEstadoTareaAsync(int id, TareasDTO tarea)
        {
            var tareaMatch = await _todoContext.Tareas.FirstOrDefaultAsync(f => f.Id == id);
            if (tareaMatch == null) return false;

            bool isValid = ValidateIntegrity.ValidateIntegrityTarea(tarea);
            if (!isValid) return false;

            tareaMatch.Estado = tarea.Estado.ToCapitalizate();
            tareaMatch.FechaModificacion = DateTime.Now;

            int cambios = await _todoContext.SaveChangesAsync();

            return cambios > 0;
        }

        public async Task<List<Tarea>> GetInactiveAsync()
        {
            return await _todoContext.Tareas.Where(w => w.Activo == false).ToListAsync();
        }

        public async Task<List<Tarea>> GetActiveAsync()
        {
            return await _todoContext.Tareas.Where(w => w.Activo == true).ToListAsync();
        }
            


       

    }
}
