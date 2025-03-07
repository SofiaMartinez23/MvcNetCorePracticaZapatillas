using Microsoft.EntityFrameworkCore;
using MvcNetCorePracticaZapatillas.Models;

namespace MvcNetCorePracticaZapatillas.Data
{
    public class ZapatillasContext : DbContext
    {

        public ZapatillasContext(DbContextOptions<ZapatillasContext> options)
            : base(options) { }

        public DbSet<Zapatilla> Zapatillas { get; set; }

        public DbSet<ImagenesZapatillas> ImagenesZapatillas { get; set;}
    }
}
