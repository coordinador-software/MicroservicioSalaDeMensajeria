using RHAPI.Helpers;
using static ChatAPI.Models.Classes;

namespace RHAPI.Interfaces
{
    public interface ISeguridad
    {
        public Task<string[]> GetFBTokens();
        public Task<string[]> GetFBTokens(Guid USUARIO_ID);
        public Task<bool> DeleteTokens();
        public Task<REQUEST_TOKEN> GetJWT(Guid USUARIO_ID, string USERNAME);
        public Task<USUARIO_INFO> GetInfoUsuario(Guid? USUARIO_ID);
    }
}
