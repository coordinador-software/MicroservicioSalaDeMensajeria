using Microsoft.AspNetCore.Mvc;
using RHAPI.Helpers;

namespace RHAPI.Interfaces
{
    public interface IGRAPI
    {
        /// <summary>
        /// Carga un documento en el sistema.
        /// </summary>
        /// <param name="cARGA_GRAPI">Datos del documento a cargar.</param>
        /// <returns>Devuelve un objeto RESULT_GRAPI con información sobre el resultado de la carga del documento.</returns>
        public Task<RESULT_GRAPI> UploadDocument(CARGA_GRAPI cARGA_GRAPI);
        public Task<RESULT_GRAPI> Certificar(CARGA_GRAPI cARGA_GRAPI);
        public Task<FileContentResult> GetDocumentoPDF(Guid? documentoId);
        public Task<string> GetDocumentoPDFB64(Guid? documentoId);
        public Task<RESULT_IMAGEN> UploadImage(CARGA_IMAGEN cARGA_IMAGEN);
        public Task<RESULT_IMAGEN> GetImage(Guid? imagenId);
        public Task<bool> DeleteImage(Guid? imagenId);
        public Task<bool> DeleteDocumento(Guid? documentoId);
        public Task<bool> UpdateDocumento(UPDATE_GRAPI uPDATE_GRAPI);
        /// <summary>
        /// Envía una notificación a través de Firebase Cloud Messaging a los correos electrónicos especificados.
        /// </summary>
        /// <param name="emails">Lista de correos electrónicos separados por punto y coma.</param>
        /// <param name="asunto">Asunto del correo electrónico.</param>
        /// <param name="cuerpo">Cuerpo del correo electrónico.</param>
        /// <param name="email">Correo electrónico del remitente. Si no se especifica, se utilizará el correo electrónico predeterminado.</param>
        /// <returns>Devuelve verdadero si la notificación se envió correctamente, de lo contrario, falso.</returns>
        public Task<bool> SendMail(string emails ,string asunto,string cuerpo,string? email);
        
        /// <summary>
        /// Envía un mensaje de WhatsApp al número de teléfono especificado.
        /// </summary>
        /// <param name="telefono">Número de teléfono al que se enviará el mensaje. Debe incluir el código de país y el número de teléfono sin espacios ni guiones.</param>
        /// <param name="mensaje">Mensaje que se enviará por WhatsApp.</param>
        /// <returns>Devuelve verdadero si el mensaje se envió correctamente, de lo contrario, falso.</returns>
        public Task<bool> SendWhatsapp(string mensaje, string telefono);

        /// <summary>
        /// Envía una notificación push a través de Firebase Cloud Messaging a los tokens de dispositivos especificados.
        /// </summary>
        /// <param name="title">Título de la notificación push.</param>
        /// <param name="body">Cuerpo de la notificación push.</param>
        /// <param name="tokens">Lista de tokens de dispositivos a los que se enviará la notificación push.</param>
        /// <returns>Devuelve verdadero si la notificación push se envió correctamente, de lo contrario, falso.</returns>
        public Task<bool> SendFirebase(string title, string body, string[] tokens);
        /// <summary>
        /// Envía una notificación push a través de Firebase Cloud Messaging a los tokens de dispositivos especificados.
        /// </summary>
        /// <param name="title">Título de la notificación push.</param>
        /// <param name="body">Cuerpo de la notificación push.</param>
        /// <param name="tokens">Lista de tokens de dispositivos a los que se enviará la notificación push.</param>
        /// <param name="data">Datos adicionales que se enviarán con la notificación push.</param>
        /// <returns>Devuelve verdadero si la notificación push se envió correctamente, de lo contrario, falso.</returns>
        public Task<bool> SendFirebase(string title, string body, string[] tokens, Dictionary<string, string> data);
        /// <summary>
        /// Envía una notificación push a través de Firebase Cloud Messaging al token de dispositivo especificado.
        /// </summary>
        /// <param name="title">Título de la notificación push.</param>
        /// <param name="body">Cuerpo de la notificación push.</param>
        /// <param name="token">Token del dispositivo al que se enviará la notificación push.</param>
        /// <returns>Devuelve verdadero si la notificación push se envió correctamente, de lo contrario, falso.</returns>
        public Task<bool> SendFirebase(string title, string body, string token);

        /// <summary>
        /// Envía una notificación push a través de Firebase Cloud Messaging al token de dispositivo especificado.
        /// </summary>
        /// <param name="title">Título de la notificación push.</param>
        /// <param name="body">Cuerpo de la notificación push.</param>
        /// <param name="token">Token del dispositivo al que se enviará la notificación push.</param>
        /// <param name="data">Datos adicionales que se enviarán con la notificación push.</param>
        /// <returns>Devuelve verdadero si la notificación push se envió correctamente, de lo contrario, falso.</returns>
        public Task<bool> SendFirebase(string title, string body, string token,Dictionary<string, string> data);

        public Task<bool> SendNotifications(List<USUARIO_INFO> usersList, List<string> tokensFB, string asunto, string body);
        public Task<bool> SendNotifications(USUARIO_INFO user, List<string> tokensFB, string asunto, string body);
    }
}
