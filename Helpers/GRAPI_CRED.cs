using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RHAPI.Helpers
{
    public class GRAPI_CRED
    {
        //public string URL_GRAPI = "https://sistema.prosur.com.mx:8822/api/";
        public string URL_GRAPI = "http://prosur.zapto.org:8822/api";
        //public string URL_GRAPI = "http://192.168.6.152:8822/api/";
        //public string URL_GRAPI = "http://192.168.50.102:8822/api/";
        //public string URL_GRAPI = "http://192.168.1.62:8822/api/";
        //public string URL_GRAPI = "http://192.168.6.98:8822/api/";
        public string API_KEY = "34E6B85D-933A-4B7A-8A0E-D573B672974B:90A6E9844196CD27A366529237C9BE88E3F05B636E1B3B2600D55D710FAC3225";
        public string CARGA_ARCHIVO => "MDocumentos/UploadDocument";
        public string UPDATE_ARCHIVO => "MDocumentos/UpdateDocument";
        public string CONSULTA_ARCHIVO_PDF => "MDocumentos/PDF/";
        public string CONSULTA_ARCHIVO_B64 => "MDocumentos/PDFB64/";
        public string CERTIFICAR => "MDocumentos/CertificarNom151";
        public string GET_IMAGEN => "MImagenes/";
        public string UPLOAD_IMAGEN => "MImagenes";
        public string DELETE_IMAGEN => "MImagenes/Delete/";
        public string DELETE_DOCUMENTO => "MDocumentos/Delete/{0}";
        public string NOTIFICAION_FIREBASE => "Notificaciones/SendFirebase";
        public string NOTIFICAION_FIREBASE_DATA => "Notificaciones/SendFirebaseWithData";
        public string NOTIFICAION_FIREBASE_SINGLE => "Notificaciones/SendFirebaseSingleToken";
        public string NOTIFICAION_FIREBASE_SINGLE_DATA => "Notificaciones/SendFirebaseSingleTokenWithData";
        public string NOTIFICAION_EMAIL => "Notificaciones/SendMail";
        public string NOTIFICAION_WHATSAPP => "Notificaciones/SendWhatsapp";



    }


    public class CARGA_GRAPI
    {
        public Guid SISTEMA_ID { get; set; } = new Guid("34E6B85D-933A-4B7A-8A0E-D573B672974B");
        public string DOCUMENTO { get; set; } = null!;
        public string USER_CREATED { get; set; } = "SISTEMA RRHH";
        public string DOC_BASE64 { get; set; } = null!;
        public string? NOMPSC { get; set; } = "EDICOM";
        public bool? ORDEN { get; set; }
        public string? N_EMAILS { get; set; }
        public string? JSON_NOTI { get; set; }
    }

    public class UPDATE_GRAPI
    {
        public Guid M_DOCUMENTO_ID { get; set; }
        public string DOC_BASE64 { get; set; } = null!;
        public bool DELETE { get; set; } = true;
    }

    public class RESULT_GRAPI
    {
        public Guid M_DOCUMENTO_ID { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string DOCUMENTO { get; set; } = null!;
        [Precision(0)]
        public DateTime FECHA_REG { get; set; }
        public string ESTATUS { get; set; } = null!;
        public DateTime? FECHA_ACT { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string PSC { get; set; } = null!;
    }

    public class CARGA_IMAGEN
    {
        public string IMAGENB64 { get; set; } = null!;
        [StringLength(100)]
        [Unicode(false)]
        public string? NOMBRE { get; set; }

        [StringLength(100)]
        [Unicode(false)]
        public string? USUARIO_REG { get; set; } = "SISTEMA RRHH";
    }


    public class RESULT_IMAGEN
    {
        public Guid? M_IMAGEN_ID { get; set; }
        [Precision(0)]
        public DateTime? FECHA_REG { get; set; }
        public string IMAGENB64 { get; set; } = null!;
        [StringLength(100)]
        [Unicode(false)]
        public string? NOMBRE { get; set; }
    }

    public class SendFirebase
    {
        public string title { get; set; } = null!;
        public string body { get; set; } = null!;
        public string[] tokens { get; set; } = null!;
        public string? data { get; set; }
    }

    public class SendFirebaseSingleToken
    {
        public string title { get; set; } = null!;
        public string body { get; set; } = null!;
        public string token { get; set; } = null!;
        public string? data { get; set; }
    }

}
