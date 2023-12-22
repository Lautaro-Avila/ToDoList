using ToDo.Domain.dto;

namespace ToDo.Repository.Interfaces
{
    public interface ITareaRepository
    {
        public Task<List<Tarea>> GetTareasAsync();

        public Task<bool> AddTareaAsync(TareasDTO tarea);

        public Task<bool> UpdateTareaAsync(int id, TareasDTO tarea);

        public Task<bool> UpdateEstadoTareaAsync(int id, TareasDTO tarea);

        public Task<List<Tarea>> GetInactiveAsync();

        public Task<List<Tarea>> GetActiveAsync();

        public Task<bool> DeleteAsync(int id);
    }
}




