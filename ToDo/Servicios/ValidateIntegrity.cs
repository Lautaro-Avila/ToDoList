using ToDo.Domain.dto;

namespace ToDo.Servicios
{
    public class ValidateIntegrity
    {
        public static bool ValidateIntegrityTarea(TareasDTO tarea)
        {
            if (string.IsNullOrEmpty(tarea.Estado))
            {
                tarea.Estado = "pendiente";
            }
            if (string.IsNullOrEmpty(tarea.Titulo) || (string.IsNullOrEmpty(tarea.Descripcion)))
            {
                return false;
            }
            if (tarea.Estado.ToLower() != "pendiente" && tarea.Estado.ToLower() != "en curso" && tarea.Estado.ToLower() != "finalizado")
            {
                return false;
            }

            return true;


            
        }   
    }
}
