using System.Net;

namespace UnivAuth.Domain.Entities
{
    public class User
    {
        public int id { get; set; } = 0;
        public int persona_id { get; set; } = 0;
        public string usr { get; set; } = "";
        public string pwd { get; set; } = "";
        public string secreto { get; set; } = "";
        public bool activo { get; set; } = false;
        public int rol_id { get; set; } = 0;
        public bool tfa { get; set; } = false;
        public string nombre_usuario { get; set; } = "";
        public string doc_id { get; set; } = "";

        public HttpStatusCode status { get; set; }
        public string mensaje { get; set; } = "";
    }
}
