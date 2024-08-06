//using DevExpress.ClipboardSource.SpreadsheetML;
//using DevExpress.XtraSpreadsheet.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RestSharp;
using RHAPI.Helpers;
using RHAPI.Interfaces;
using System.Text.Json;

namespace RHAPI.Services
{
    public class GRAPIService : IGRAPI
    {
        private readonly GRAPI_CRED _grapi = new GRAPI_CRED();
        //restclient
        private RestClient client;
        private RestClientOptions clientOptions;
        public GRAPIService()
        {
            clientOptions = new RestClientOptions(_grapi.URL_GRAPI)
            {
                MaxTimeout = -1
            };
            client = new RestClient(clientOptions);

        }
        public async Task<RESULT_GRAPI> Certificar(CARGA_GRAPI cARGA_GRAPI)
        {
            try
            {

                RestResponse response = await PostRequest(_grapi.CERTIFICAR, cARGA_GRAPI);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Error al certificar");
                }

                if (string.IsNullOrEmpty(response.Content))
                {
                    throw new InvalidOperationException("La respuesta del servidor está vacía");
                }
                //convertimos la respuesta en RESULT_GRAPI
                RESULT_GRAPI rESULT_GRAPI = JsonConvert.DeserializeObject<RESULT_GRAPI>(response.Content)!;

                return rESULT_GRAPI ?? throw new InvalidOperationException("No se pudo deserializar la respusta de Grapi");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> DeleteImage(Guid? imagenId)
        {
            try
            {
                string url = _grapi.DELETE_IMAGEN + imagenId;

                RestResponse response = await DeleteRequest(url);
                return response.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public async Task<FileContentResult> GetDocumentoPDF(Guid? documentoId)
        {
            try
            {
                string url = _grapi.CONSULTA_ARCHIVO_PDF + documentoId;
                RestResponse response = await GetRequest(url);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("No se pudo obtener el archivo PDF");
                }

                if (string.IsNullOrEmpty(response.Content))
                {
                    throw new InvalidOperationException("La respuesta del servidor está vacía");
                }
                //convertimos la application/pdf a FILE
                byte[] bytes = Convert.FromBase64String(response.Content);
                FileContentResult file = new FileContentResult(bytes, "application/pdf");
                // Devolvemos el archivo creado
                return file;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }
        }

        public async Task<string> GetDocumentoPDFB64(Guid? documentoId)
        {
            try
            {
                string result = "";
                string url = _grapi.CONSULTA_ARCHIVO_B64 + documentoId;
                RestResponse response = await GetRequest(url);
                if (!response.IsSuccessStatusCode)
                {

                    return result;
                }
                //convertimos la respuesta en RESULT_GRAPI
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                if (string.IsNullOrEmpty(response.Content))
                {
                    throw new InvalidOperationException("La respuesta del servidor está vacía");
                }

                var rESULT_GRAPI = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(response.Content, options);

                if (rESULT_GRAPI.GetProperty("status").ToString() == "success" && rESULT_GRAPI.TryGetProperty("data", out var res))
                {
                    result = res.ToString();
                }
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }
        }

        public async Task<RESULT_IMAGEN> GetImage(Guid? imagenId)
        {
            try
            {
                var url = _grapi.GET_IMAGEN + imagenId;

                RestResponse response = await GetRequest(url);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("No se pudo obtener la imagen");

                }
                if (string.IsNullOrEmpty(response.Content))
                {
                    throw new InvalidOperationException("La respuesta del servidor está vacía");
                }
                var result = JsonConvert.DeserializeObject<RESULT_IMAGEN>(response.Content);
                // Devolvemos el archivo creado
                return result ?? throw new InvalidOperationException("No se pudo deserializar la imagen");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }


        }

        public async Task<RESULT_GRAPI> UploadDocument(CARGA_GRAPI cARGA_GRAPI)
        {
            try
            {

                RestResponse response = await PostRequest(_grapi.CARGA_ARCHIVO, cARGA_GRAPI);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Error al subir el documento");

                }

                if (string.IsNullOrEmpty(response.Content))
                {
                    throw new InvalidOperationException("La respuesta del servidor está vacía");
                }
                //convertimos la respuesta en RESULT_GRAPI
                RESULT_GRAPI rESULT_GRAPI = JsonConvert.DeserializeObject<RESULT_GRAPI>(response.Content)!;
                return rESULT_GRAPI ?? throw new InvalidOperationException("No se pudo deserializar la respuesta de Grapi");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }
        }

        public async Task<RESULT_IMAGEN> UploadImage(CARGA_IMAGEN cARGA_IMAGEN)
        {
            try
            {
                RestResponse response = await PostRequest(_grapi.UPLOAD_IMAGEN, cARGA_IMAGEN);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Error al subir la imagen");
                }

                if (string.IsNullOrEmpty(response.Content))
                {
                    throw new InvalidOperationException("La respuesta del servidor está vacía");
                }
                //convertimos la respuesta en RESULT_GRAPI
                RESULT_IMAGEN rESULT_IMAGEN = JsonConvert.DeserializeObject<RESULT_IMAGEN>(response.Content)!;
                return rESULT_IMAGEN ?? throw new InvalidOperationException("No se pudo deserializar la respuesta de Grapi");

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> DeleteDocumento(Guid? documentoId)
        {
            try
            {
                var url = String.Format(_grapi.DELETE_DOCUMENTO, documentoId);
                RestResponse response = await DeleteRequest(url);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Error al eliminar el documento");
                }
                //convertimos la respuesta en RESULT_GRAPI


                return true;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<bool> UpdateDocumento(UPDATE_GRAPI uPDATE_GRAPI)
        {

            try
            {
                RestResponse response = await PostRequest(_grapi.UPDATE_ARCHIVO, uPDATE_GRAPI);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Error al actualizar el documento");
                }
                //convertimos la respuesta en RESULT_GRAPI
                return true;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<bool> SendMail(string emails, string asunto, string cuerpo, string? email)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "emails", emails },
                    { "asunto", asunto },
                    { "cuerpo", cuerpo },

                };

                if (email != null)
                {
                    parameters.Add("email", email);
                }
                RestResponse response = await PostRequest(_grapi.NOTIFICAION_EMAIL, parameters);
                return response.IsSuccessStatusCode;


            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> SendWhatsapp(string mensaje, string telefono)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "telefono", telefono },
                    { "mensaje", mensaje }
                };
                RestResponse response = await PostRequest(_grapi.NOTIFICAION_WHATSAPP, parameters);
                return response.IsSuccessStatusCode;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> SendFirebase(string title, string body, string[] tokens)
        {
            try
            {
                SendFirebase sendFirebase = new()
                {
                    title = title,
                    body = body,
                    tokens = tokens
                };


                RestResponse response = await PostRequest(_grapi.NOTIFICAION_FIREBASE, sendFirebase);
                return response.IsSuccessStatusCode;


            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> SendFirebase(string title, string body, string[] tokens, Dictionary<string, string> data)
        {
            try
            {
                //convertir el diccionario en json

                SendFirebase sendFirebase = new()
                {
                    title = title,
                    body = body,
                    tokens = tokens,
                    data = JsonConvert.SerializeObject(data)
                };
                RestResponse response = await PostRequest(_grapi.NOTIFICAION_FIREBASE_DATA, sendFirebase);
                return response.IsSuccessStatusCode;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> SendFirebase(string title, string body, string token)
        {
            try
            {
                SendFirebaseSingleToken sendFirebase = new()
                {
                    title = title,
                    body = body,
                    token = token
                };

                RestResponse response = await PostRequest(_grapi.NOTIFICAION_FIREBASE_SINGLE_DATA, sendFirebase);
                return response.IsSuccessStatusCode;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> SendFirebase(string title, string body, string token, Dictionary<string, string> data)
        {
            try
            {
                //convertir el diccionario en json
                var json = JsonConvert.SerializeObject(data);

                SendFirebaseSingleToken sendFirebase = new()
                {
                    title = title,
                    body = body,
                    token = token,
                    data = json
                };
                RestResponse response = await PostRequest(_grapi.NOTIFICAION_FIREBASE_SINGLE, sendFirebase);
                return response.IsSuccessStatusCode;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //funcion para cetralizar el envio de archivos

        private async Task<RestResponse> PostRequest(string url, object model)
        {
            try
            {
                var request = new RestRequest(url, Method.Post);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("X-API-Key", _grapi.API_KEY);

                request.AddJsonBody(model, ContentType.Json);
                RestResponse response = await client.ExecuteAsync(request);
                return response;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        private async Task<RestResponse> PostRequest(string url, Dictionary<string, string> parameters)
        {
            try
            {
                var request = new RestRequest(url, Method.Post);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("X-API-Key", _grapi.API_KEY);
                //PARAMETROS agregarlos a la url
                foreach (var item in parameters)
                {
                    request.AddQueryParameter(item.Key, item.Value);
                }
                RestResponse response = (RestResponse)await client.ExecuteAsync(request);
                return response;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private async Task<RestResponse> PostRequest(string url, Dictionary<string, Object> parameters)
        {
            try
            {
                var request = new RestRequest(url, Method.Post);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("X-API-Key", _grapi.API_KEY);
                //serializar el diccionario
                //agregar parametros al cuerpo  
                request.AddJsonBody(parameters, ContentType.Json);

                RestResponse response = (RestResponse)await client.ExecuteAsync(request);
                return response;
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
                request.AddHeader("X-API-Key", _grapi.API_KEY);
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
                request.AddHeader("X-API-Key", _grapi.API_KEY);
                RestResponse response = await client.ExecuteAsync(request);
                return response;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        //convertir el diccionario a un objeto
        //recibe un diccionario y lo convierte en un objeto el diccionare debe de ser string, object
        private object ConvertDictionaryToObject(Dictionary<string, object> dictionary)
        {
            try
            {
                var json = JsonConvert.SerializeObject(dictionary);
                var result = JsonConvert.DeserializeObject(json);
                return result ?? throw new InvalidOperationException("No se pudo deserializar la respuesta de Grapi"); ;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> SendNotifications(List<USUARIO_INFO> usersList, List<string> tokensFB, string asunto, string body)
        {
            //Envio de correos electronicos
            try
            {
                if (usersList.Count > 0)
                {
                    //agrupar los correos en un solo string divididos por ;
                    var mails = string.Join(";", usersList.Select(x => x.EMAIL));
                    await SendMail(mails, asunto, body, null);
                }
            }
            catch (Exception) { }

            //Envio de mensajes de Whatsapp
            try
            {
                foreach (var user in usersList)
                {
                    if (!user.CELULAR.IsNullOrEmpty())
                    {
                        await SendWhatsapp(body, user.CELULAR!);
                    }
                }
            }
            catch (Exception) { }

            //Envio de notificaciones push
            try
            {
                if (!tokensFB.IsNullOrEmpty())
                {
                    await SendFirebase(asunto, body, tokensFB.ToArray());
                }
            }
            catch (Exception) { }

            return true;
        }

        public async Task<bool> SendNotifications(USUARIO_INFO user, List<string> tokensFB, string asunto, string body)
        {
            //Envio de correos electronicos
            try
            {
                if (user.EMAIL != null)
                {
                    await SendMail(user.EMAIL, asunto, body, null);
                }
            }
            catch (Exception) { }

            //Envio de mensajes de Whatsapp
            try
            {
                if (!user.CELULAR.IsNullOrEmpty())
                {
                    await SendWhatsapp(body, user.CELULAR!);
                }
            }
            catch (Exception) { }

            //Envio de notificaciones push
            try
            {
                if (!tokensFB.IsNullOrEmpty())
                {
                    await SendFirebase(asunto, body, tokensFB.ToArray());
                }
            }
            catch (Exception) { }

            return true;
        }

    }



}
