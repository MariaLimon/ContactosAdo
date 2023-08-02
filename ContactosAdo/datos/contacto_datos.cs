
using System.Data.SqlClient;
using System.Data;
using ContactosAdo.Models;
using ContactosAdo.datos;
using static System.Runtime.InteropServices.JavaScript.JSType;

//archivo de acceso a los datos
namespace ContactoAdo.datos
{
    public class ContactoDatos
    {
        //listar contactos
        public List<ContactoModel> Listar()
        {
            List<ContactoModel> Lista = new List<ContactoModel>();
            
            var cn = new Conexion();//creado instancia para acceder a la clase conexxion
            using (var conexion = new SqlConnection(cn.getCadenaSql())) //devuelve la cadena de conexion para acceder a la base de datos
            {
                conexion.Open(); //abre la conexion
                SqlCommand cmd = new SqlCommand("SP_listar", conexion);//crea un nuevo comando
                cmd.CommandType = CommandType.StoredProcedure;//esta diciendo el tipo de comando

                using (var dr = cmd.ExecuteReader())//ejecuta la lectura de la base de datos
                {
                    while (dr.Read())//recorre el archivo el elemento por elemento
                    {
                        Lista.Add(new ContactoModel  
                        {
                            //esta creando los objetos que esta guardando en la lista
                            idContactos = Convert.ToInt32(dr["IdContactos"]),
                            nombre = dr["nombre"].ToString(),
                            telefono = dr["telefono"].ToString(),
                            correo = dr["correo"].ToString(),
                            clave = dr["clave"].ToString()
                        }) ;
                    }
                }
            }
            return Lista;
        }


        //obteber contactos
        public ContactoModel ObtenerContacto(int idContacto) //estamos declarando un metodo publico
        {
            ContactoModel _contacto=new ContactoModel(); //crea un nuevo objeto de tipo contacto porque es lo que tiene que debolver
           
                var cn = new Conexion();
                using (var conexion= new SqlConnection(cn.getCadenaSql()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_obtener", conexion);
                    cmd.Parameters.AddWithValue("idContacto", idContacto); //pasa el parametro
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = cmd.ExecuteReader())
                    {
                        while(dr.Read())
                        {
                            _contacto.idContactos = Convert.ToInt32(dr["IdContactos"]);
                            _contacto.nombre = dr["nombre"].ToString();
                            _contacto.telefono = dr["telefono"].ToString();
                            _contacto.correo = dr["correo"].ToString();
                            _contacto.clave = dr["clave"].ToString();
                        }
                    }
                };
            
            return _contacto;
        }

        //guardar contacto
        public bool GuardarContacto(ContactoModel model) //crea un metodo para crear un nuevo contacto en la base de datos
        {
            bool respuesta; //crea una variable 
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSql()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_guardar", conexion); //crea el nuevo comando
                                             //nombre de nuetra columna en base de datos tal cual
                    cmd.Parameters.AddWithValue("nombre", model.nombre);
                    cmd.Parameters.AddWithValue("telefono", model.telefono);
                    cmd.Parameters.AddWithValue("correo", model.correo);
                    cmd.Parameters.AddWithValue("clave", model.clave);
                    cmd.CommandType = CommandType.StoredProcedure; //tipo de comando 
                    cmd.ExecuteNonQuery(); //ejecuta
                }
                respuesta = true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                respuesta=false;
            }
            return respuesta;
        }

        //editar contacto
        public bool EditarContacto(ContactoModel model)
        {
            bool respuesta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSql()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_editar", conexion); //pasa el procedimiento amacenado y la conexion
                    cmd.Parameters.AddWithValue("idContactos", model.idContactos);
                    cmd.Parameters.AddWithValue("nombre", model.nombre);
                    cmd.Parameters.AddWithValue("telefono", model.telefono);
                    cmd.Parameters.AddWithValue("correo", model.correo);
                    cmd.Parameters.AddWithValue("clave", model.clave);
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


        //eliminar contacto
        public bool EliminarContacto(int idContacto)
        {
            bool respuesta;
            try
            {
                var cn = new Conexion();//
                using (var conexion = new SqlConnection(cn.getCadenaSql()))// intancia de conexion
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_eliminar", conexion);
                    cmd.Parameters.AddWithValue("idContacto", idContacto);//pasa el parametro para que el procedimiento pueda funcionar
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                };
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
}﻿