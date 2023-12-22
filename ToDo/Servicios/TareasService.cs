
using ToDo.Domain.dto;
using ToDo.Repository.Interfaces;
using ToDo.Servicios.InterfazServicio;

namespace ToDo.Servicios.Extensions
{
    public class TareasService : ITareasService
    {

        private readonly ITareaRepository _tareaRepository;

        public TareasService(ITareaRepository tareaRepository)
        {
            _tareaRepository = tareaRepository;
        }

        public async Task<bool> AddNewTareaAsync(TareasDTO tarea)
        {
            var result = await  _tareaRepository.AddTareaAsync(tarea);
            
            return result;
        }

        public async Task<bool> DeleteTareasAsync(int id)
        {
           var result = await _tareaRepository.DeleteAsync(id);

            return result;
        }

        public async Task<List<Tarea>> GetAllDeleteAsync()
        {
           var result = await _tareaRepository.GetInactiveAsync();

            return result;
        }

        public async Task<List<Tarea>> GetAllNoDeleteAsync()
        {
            var result = await _tareaRepository.GetActiveAsync();
            
            return result;

        }

        public Task<List<Tarea>> GetAllTareasAsync()
        {
            var result = _tareaRepository.GetTareasAsync();

            return result;  
        }

        public async Task<bool> UpdateEstadoAsync(int id, TareasDTO tarea)
        {
            var result = await _tareaRepository.UpdateEstadoTareaAsync(id, tarea);

            return result;
        }

        public async Task<bool> UpdateTareasAsync(int id, TareasDTO tareas)
        {
           var result = await _tareaRepository.UpdateTareaAsync(id, tareas);

            return result;
        }
    }
} 

    