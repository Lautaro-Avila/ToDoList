using ToDo.Domain.dto;

namespace ToDo.Servicios.InterfazServicio
{
    public interface ITareasService
    {
        public Task<List<Tarea>> GetAllTareasAsync();

        public Task<bool> AddNewTareaAsync(TareasDTO tarea);

        public Task<bool> UpdateTareasAsync(int id, TareasDTO tareas);

        public Task<bool> DeleteTareasAsync(int id);

        public Task<bool> UpdateEstadoAsync(int id, TareasDTO tarea);

        public Task<List<Tarea>> GetAllDeleteAsync();

        public Task<List<Tarea>> GetAllNoDeleteAsync();

    }
}
