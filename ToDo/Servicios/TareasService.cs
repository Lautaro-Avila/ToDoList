using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using ToDo.Domain.dto;
using ToDo.Repository;
using ToDo.Servicios.InterfazServicio;

namespace ToDo.Servicios.Extensions
{
    public class TareasService : ITareasService

    {
        private readonly ToDoContext _todoContext;

        public TareasService(ToDoContext context)
        {
            _todoContext = context;
        }

        public async Task<List<Tarea>> GetAllTareasAsync()
        {
            return await _todoContext.Tareas.ToListAsync();
        }

        public async Task<bool> AddNewTareaAsync(TareasDTO tareaDTO)
        {
            bool isValid = ValidateIntegrity.ValidateIntegrityTarea(tareaDTO);

            if (!isValid) return false;

            var tarea = new Tarea();
            tarea.Estado = tareaDTO.Estado.ToCapitalizate();
            tarea.Titulo = tareaDTO.Titulo.ToCapitalizate();
            tarea.Descripcion = tareaDTO.Descripcion;
            tarea.Activo = true;
            tarea.FechaAlta = DateTime.UtcNow;
            tarea.FechaModificacion = DateTime.UtcNow;

            await  _todoContext.Tareas.AddAsync(tarea);

            int cambios = await _todoContext.SaveChangesAsync();

            return cambios > 0;
        }


        public async Task<bool> UpdateTareasAsync(int id, TareasDTO tarea)
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

        public async Task<bool> DeleteTareasAsync(int id)
        {
           var tarea = await _todoContext.Tareas.FirstOrDefaultAsync(f => f.Id == id);
            if (tarea == null) return false;


            tarea.Activo = false;
            int cambios = await _todoContext.SaveChangesAsync();

            return cambios > 0;
        }


        public async Task<bool> UpdateEstadoAsync(int id, TareasDTO tarea)
        {
            if (tarea.Estado.ToLower() != "pendiente" && tarea.Estado.ToLower() != "en curso" && tarea.Estado.ToLower() != "finalizado")
                return false;

            var tareaMatch = await _todoContext.Tareas.FirstOrDefaultAsync(f => f.Id == id);

            if (tareaMatch == null) return false;

            tareaMatch.Estado = char.ToUpper(tarea.Estado[0]) + tarea.Estado.Substring(1).ToLower();


            int cambios= await _todoContext.SaveChangesAsync();

            return cambios > 0;
        }

        public async Task<List<Tarea>> GetAllDeleteAsync()
        {
            return await _todoContext.Tareas.Where(w => w.Activo == false).ToListAsync();
        }

        public async Task<List<Tarea>> GetAllNoDeleteAsync()
        {
            return await _todoContext.Tareas.Where(w => w.Activo == true).ToListAsync();
        }

    }
}

    