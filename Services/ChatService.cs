using ChatAPI.Interfaces;
using ChatAPI.Models.DB;
using Microsoft.EntityFrameworkCore;
using static ChatAPI.Models.Clases;

namespace ChatAPI.Services
{
    public class ChatService : IChat
    {
        private readonly DBCHAT _db;

        public ChatService(DBCHAT context)
        {
            _db = context;
        }
        public async Task<bool> verificarPermisoEliminacion(TipoEliminacion tipoEliminacion, Guid salaId)
        {
            SALAS? sala = await _db.SALAS.Include(s => s.SISTEMA).SingleOrDefaultAsync(s => s.SALA_ID == salaId);

            if (sala == null)
            {
                throw new Exception("ERROR: ID DE LA SALA INEXISTENTE");
            }

            return tipoEliminacion switch
            {
                TipoEliminacion.Sala => sala.SISTEMA.ELIMINAR_SALAS && sala.SISTEMA.ELIMINAR_MENSAJES && sala.SISTEMA.ELIMINAR_ARCHIVOS,
                TipoEliminacion.Mensaje => sala.SISTEMA.ELIMINAR_MENSAJES,
                TipoEliminacion.Archivo => sala.SISTEMA.ELIMINAR_ARCHIVOS,   
                _ => throw new ArgumentException("ERROR: TIPO DE ELIMINACIÓN INVALIDO.", nameof(tipoEliminacion))
            };
        }
    }
}
