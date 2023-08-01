using ContactosAdo.Models;
using System.Data.SqlClient;
using System.Data;

namespace ContactosAdo.datos
{
    public class contacto_datos
    {
        public List<ContactoModel> Listar()
        {
            List<ContactoModel> Lista = new List<ContactoModel>();
            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSql())) ;
            {
                conexion.open
            }
        }
           
        
    }
}
