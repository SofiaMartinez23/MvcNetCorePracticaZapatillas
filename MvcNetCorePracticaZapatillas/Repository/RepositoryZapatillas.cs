using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcNetCorePracticaZapatillas.Data;
using MvcNetCorePracticaZapatillas.Models;
using System.Data;

namespace MvcNetCorePracticaZapatillas.Repository
{
#region
/*
     create or alter procedure SP_ZAPATILLAS_IMAGENES_OUT
     (@posicion int, @idproducto int, @registros int out)
     as
     --todos los registro de un departamento
     select @registros = COUNT(IDIMAGEN) from IMAGENESZAPASPRACTICA
     where IDPRODUCTO = @idproducto

     SELECT IDIMAGEN, IMAGEN , IDPRODUCTO from
     (select CAST(ROW_NUMBER() over(order by IMAGEN) as int) AS POSICION,
     IDIMAGEN, IMAGEN, IDPRODUCTO
     from IMAGENESZAPASPRACTICA
     where IDPRODUCTO = @idproducto) as query
     where query.POSICION = @posicion
     go

  */
#endregion
    public class RepositoryZapatillas
    {
        private ZapatillasContext context;
        public RepositoryZapatillas(ZapatillasContext context)
        {
            this.context = context;
        }

        public async Task<List<Zapatilla>> GetZapatillasAsync()
        {
            var zapatillas = await this.context.Zapatillas.ToListAsync();
            return zapatillas;
        }

        public async Task<Zapatilla> FindZapatillasAsync(int idProducto)
        {
            var zapatillas = await this.context.Zapatillas
                .Where(x => x.IdProducto == idProducto)
                .FirstOrDefaultAsync();
            return zapatillas;
        }

        public async Task<ImagenesZapatillas> FindImagensZapatillasAsync(int idImagen)
        {
            ImagenesZapatillas imgZapa = await this.context.ImagenesZapatillas
                .Where(x => x.IdImagen == idImagen)
                .FirstOrDefaultAsync();
            return imgZapa;
        }

        public async Task<ModelZapatillasImagenes> GetZapatillasImagenesOutAsync(int? posicion, int idproducto)
        {
            string sql = "SP_ZAPATILLAS_IMAGENES_OUT @posicion, @idproducto, @registros OUT";
            SqlParameter pamPosicion = new SqlParameter("@posicion", posicion);
            SqlParameter pamIdProducto = new SqlParameter("@idproducto", idproducto);
            SqlParameter pamRegistros = new SqlParameter("@registros", 0);
            pamRegistros.Direction = ParameterDirection.Output;

            var consulta =
                this.context.ImagenesZapatillas.FromSqlRaw(sql, pamPosicion, pamIdProducto, pamRegistros);

            List<ImagenesZapatillas> imagenzapatillas = await consulta.ToListAsync();
            int registro = int.Parse(pamRegistros.Value.ToString());
            return new ModelZapatillasImagenes
            {
                NumeroRegistro = registro,
                ImagenesZapatillas = imagenzapatillas
            };
        }
    }
}
