using System.Xml.Linq;

namespace CapstoneG14.Utilities.Response
{
    public class GenericResponse<TObject>
    {
        public bool Estado { get; set; }
        public string? Mensaje { get; set; }
        public TObject? Objeto { get; set; }
        public List<XObject>? ListaObjeto { get; set; }
    }
}