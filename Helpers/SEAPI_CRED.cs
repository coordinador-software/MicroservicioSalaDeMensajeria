namespace RHAPI.Helpers
{
    public class SEAPI_CRED
    {
        //public string URL_SEAPI = "https://sistema.prosur.com.mx:8819/api";
        public string URL_SEAPI = "http://prosur.zapto.org:8819/api";
        //public string URL_SEAPI = "http://192.168.6.152:2724/api";
        //public string URL_SEAPI = "http://192.168.50.102:2724/api/";
        //public string URL_SEAPI = "http://192.168.1.62:2724/api/";
        //public string URL_SEAPI = "http://192.168.6.2:8819/api";
        //public string URL_SEAPI = "http://192.168.6.98:45457/api";
        public string API_KEY = "34E6B85D-933A-4B7A-8A0E-D573B672974B:2666206BB7AEED34B04449F2F3E759E1C500F6FD5AA9BAC0B719C21E55B0135F";
        public string MESES = "3";
        public string GET_FBTOKEN => "Seguridad/GetFireBaseTokens/{0}";
        public string GET_JWT => "seguridad/gettoken";
        public string GET_FBTOKENS_SISTEMA => "seguridad/GetFireBaseTokensSistema";
        public string DELETE_TOKENS => "Seguridad/DeleteTokens";
        public string GET_INFO_USUARIO => "Seguridad/GetUsuario/{0}";

    }
    public class USUARIO_INFO
    {
        public Guid? USUARIO_ID { get; set; }
        public string? NOMBRE { get; set; }
        public string? EMAIL { get; set; }
        public string? CELULAR { get; set; }
        public List<string>? FB_TOKENS { get; set; }
    }
}
