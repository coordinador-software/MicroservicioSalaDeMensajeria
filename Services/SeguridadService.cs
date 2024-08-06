using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RestSharp;
using RHAPI.Helpers;
using RHAPI.Interfaces;
using static ChatAPI.Models.Classes;

namespace RHAPI.Services
{
    public class SeguridadService : ISeguridad
    {
        private readonly SEAPI_CRED _seapi = new();
        //restclient
        private RestClient client;
        private RestClientOptions clientOptions;
        public SeguridadService()
        {
            clientOptions = new RestClientOptions(_seapi.URL_SEAPI)
            {
                MaxTimeout = -1,
               //Timeout = TimeSpan.MaxValue
            };
            client = new RestClient(clientOptions);
        }
        public async Task<string[]> GetFBTokens(Guid USUARIO_ID)
        {
            try
            {
                var url = String.Format(_seapi.GET_FBTOKEN, USUARIO_ID);

                RestResponse response = await GetRequest(url);

                string[]? tokens = Array.Empty<string>();
                if (response.IsSuccessStatusCode)
                {
                    //convertir respuesta en un array de string
                    tokens = JsonConvert.DeserializeObject<string[]>(response!.Content!);
                }

                //si no hay tokens retornamos un array vacio
                if (tokens.IsNullOrEmpty())
                {
                    return Array.Empty<string>();
                }

                return tokens!;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<REQUEST_TOKEN> GetJWT(Guid USUARIO_ID, string USERNAME)
        {
            try
            {

                //crear lista de parametros
                var parameters = new Dictionary<string, string>
                {
                    { "usuario_id", USUARIO_ID.ToString() },
                    { "username", USERNAME }
                };


                RestResponse response = await GetRequest(_seapi.GET_JWT, parameters);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("No se pudo obtener los tokens");
                }

                if (string.IsNullOrEmpty(response.Content))
                {
                    throw new InvalidOperationException("La respuesta del servidor está vacía");
                }

                //convertir respuesta en un array de string
                var result = JsonConvert.DeserializeObject<REQUEST_TOKEN>(response.Content);


                return result ?? throw new InvalidOperationException("No se pudo deserializar los tokens");

            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al obtener los tokens", ex);
            }
        }

        public async Task<string[]> GetFBTokens()
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "MESES", _seapi.MESES },
                };

                RestResponse response = await GetRequest(_seapi.GET_FBTOKENS_SISTEMA, parameters);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("No se pudo obtener los tokens");
                }
                if (string.IsNullOrEmpty(response.Content))
                {
                    throw new InvalidOperationException("La respuesta del servidor está vacía");
                }

                //convertir respuesta en un array de string
                string[]? tokens = JsonConvert.DeserializeObject<string[]>(response.Content);

                //si no hay tokens retornamos un array vacio

                if (tokens == null || tokens.Length == 0)
                {
                    return Array.Empty<string>();
                }

                return tokens;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> DeleteTokens()
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "MESES", _seapi.MESES },
                };
                RestResponse response = await DeleteRequest(_seapi.DELETE_TOKENS, parameters);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("No se pudo obtener los tokens");
                }


                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        private async Task<RestResponse> PostRequest(string url, object model)
        {
            try
            {
                var request = new RestRequest(url, Method.Post);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("X-API-Key", _seapi.API_KEY);

                request.AddJsonBody(model, ContentType.Json);
                RestResponse response = await client.ExecuteAsync(request);
                return response;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public async Task<USUARIO_INFO> GetInfoUsuario(Guid? USUARIO_ID)
        {
            try
            {
                var URL = String.Format(_seapi.GET_INFO_USUARIO, USUARIO_ID);
                var request = await GetRequest(URL);
                USUARIO_INFO? result = null;
                if (request.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<USUARIO_INFO>(request.Content!);
                }
                return result!;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private async Task<RestResponse> GetRequest(string url)
        {
            try
            {

                var request = new RestRequest(url, Method.Get);

                request.AddHeader("X-API-Key", _seapi.API_KEY);
                RestResponse response = await client.ExecuteAsync(request);
                return response;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        //SOBRECARGA PARA AGREGAR PARAMETROS
        private async Task<RestResponse> GetRequest(string url, Dictionary<string, string> parameters)
        {
            try
            {

                var request = new RestRequest(url, Method.Get);
                //PARAMETROS agregarlos a la url
                foreach (var item in parameters)
                {
                    request.AddParameter(item.Key, item.Value);
                }

                request.AddHeader("X-API-Key", _seapi.API_KEY);
                RestResponse response = await client.ExecuteAsync(request);
                return response;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private async Task<RestResponse> DeleteRequest(string url)
        {
            try
            {
                var request = new RestRequest(url, Method.Delete);
                request.AddHeader("X-API-Key", _seapi.API_KEY);
                RestResponse response = await client.ExecuteAsync(request);
                return response;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //sobrecarga del metodo DeleteRequest para agregar parametros
        private async Task<RestResponse> DeleteRequest(string url, Dictionary<string, string> parameters)
        {
            try
            {
                var request = new RestRequest(url, Method.Delete);

                foreach (var item in parameters)
                {
                    request.AddParameter(item.Key, item.Value);
                }
                request.AddHeader("X-API-Key", _seapi.API_KEY);
                RestResponse response = await client.ExecuteAsync(request);
                return response;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
