using System.Data.SqlClient;
namespace CrudAdoNet.datos
{
    public class  Conexion
    {
        private string cadenaSql=string.Empty;
        public Conexion()
        {   //para obtener la cedena de conexion que esta en appsettings.json
            var builder =new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
            //para obtener el valor de la cadena
            cadenaSql = builder.GetSection("ConnectionString.cadenaSql").Value;
        }
        //metodo para devolver
        public string getCadenaSql()
        {
            return cadenaSql;
        }
    }
}
