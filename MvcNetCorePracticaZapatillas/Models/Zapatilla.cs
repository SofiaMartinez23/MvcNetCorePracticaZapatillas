using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcNetCorePracticaZapatillas.Models
{
    [Table("ZAPASPRACTICA")]
    public class Zapatilla
    {
        [Key]
        [Column("IDPRODUCTO")]
        public int IdProducto { get; set; }

        [Column("NOMBRE")]
        public string Nombre { get; set; }

        [Column("DESCRIPCION")]
        public string Descripcion { get; set; }

        [Column("PRECIO")]
        public int Precio { get; set; }


    }
}
