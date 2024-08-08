

using static ChatAPI.Models.Clases;

namespace ChatAPI.Interfaces
{
    public interface IChat
    {

        public Task<bool> verificarPermisoEliminacion(TipoEliminacion tipoEliminacion, Guid salaId);
    }
}
