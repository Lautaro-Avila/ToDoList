using ToDo.Domain.dto;

namespace ToDo.Servicios
{
    public static class ValidateTituloEstado
    {
        public static bool ValidateTituloEstadoTarea(TareasDTO tarea)
        {
            if ((string.IsNullOrEmpty(tarea.Titulo)) || (string.IsNullOrEmpty(tarea.Descripcion)))
            {
                return false;
            }   
            return true;
    }
}

            }
