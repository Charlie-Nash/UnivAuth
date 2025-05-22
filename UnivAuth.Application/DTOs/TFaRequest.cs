namespace UnivAuth.Application.DTOs
{
    public class TFaRequest
    {
        public string Usuario { get; set; } = "";
        public string Codigo { get; set; } = "";
        public string Secreto { get; set; } = "";
    }
}
