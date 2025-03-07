using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MvcNetCorePracticaZapatillas.Models
{
    [Table("IMAGENESZAPASPRACTICA")]
    public class ImagenesZapatillas
    {
        [Key]
        [Column("IDIMAGEN")]
        public int IdImagen { get; set; }

        [Column("IDPRODUCTO")]
        public int IdProducto { get; set; }

        [Column("IMAGEN")]
        public string Imagen { get; set; }

    }
}
