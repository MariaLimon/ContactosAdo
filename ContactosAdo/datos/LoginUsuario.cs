using System.Data.SqlClient;
using System.Data;
using CrudAdoNet.Models;
using CrudAdoNet.datos;

namespace ContactosAdo.datos
{
    public class LoginUsuario
    {
        //registro
        public bool Registro(ContactoModel model)
        {
            bool respuesta;
            if (existeCorreo(model.Correo))
            {
                try
                {
                    var cn = new Conexion();
                    using (var conexion = new SqlConnection(cn.getCadenaSql()))
                    {
                        conexion.Open();
                        SqlCommand cmd = new SqlCommand("SP_guardar", conexion);
                        cmd.Parameters.AddWithValue("nombre", model.Nombre);
                        cmd.Parameters.AddWithValue("telefono", model.Telefono);
                        cmd.Parameters.AddWithValue("correo", model.Correo);
                        cmd.Parameters.AddWithValue("clave", model.Clave);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();
                    }
                    respuesta = true;
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    respuesta = false;
                }


            }
            else
            {
                respuesta = false;
            }
        } 
        //existe correo
        public class LoginRegistrer
        {
            public bool existeCorreo(string correo)
            {
                string eCorreo = "";
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSql()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_ValidarCorreo", conexion);
                    cmd.Parameters.AddWithValue("Correo", correo);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            eCorreo = dr["Correo"].ToString();
                        }

                    }
                }
                if (eCorreo == "")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        //validar usuario



        //cambiar clave
        public bool CambiarClave(string correo, string clave)
        {
            bool respuesta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSql()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_CambiarContrasena", conexion);
                    cmd.Parameters.AddWithValue("Correo", correo);
                    cmd.Parameters.AddWithValue("Contrasena", clave);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                respuesta = true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                respuesta = false;
            }
            return respuesta;
        }
    }
}
