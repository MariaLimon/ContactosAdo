
using System.Data.SqlClient;
using System.Data;
using CrudAdoNet.Models;
using CrudAdoNet.datos;
using static System.Runtime.InteropServices.JavaScript.JSType;

//archivo de acceso a los datos
namespace ContactoAdo.datos
{
    public class ContactoDatos
    {
        //listar contactos
        public List<ContactoModel> Listar()
        {
            List<ContactoModel> oLista = new List<ContactoModel>();
            
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
                        oLista.Add(new ContactoModel  
                        {
                            //esta creando los objetos que esta guardando en la lista
                            IdContactos = Convert.ToInt32(dr["IdContactos"]),
                            Nombre = dr["nombre"].ToString(),
                            Telefono = dr["telefono"].ToString(),
                            Correo = dr["correo"].ToString(),
                            Clave = dr["clave"].ToString()
                        }) ;
                    }
                }
            }
            return oLista;
        }


        //obteber contactos
        public ContactoModel ObtenerContacto(int idContacto) //estamos declarando un metodo publico
        {
            ContactoModel oContacto=new ContactoModel(); //crea un nuevo objeto de tipo contacto porque es lo que tiene que debolver
           
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
                            oContacto.IdContactos = Convert.ToInt32(dr["IdContactos"]);
                            oContacto.Nombre = dr["nombre"].ToString();
                            oContacto.Telefono = dr["telefono"].ToString();
                            oContacto.Correo = dr["correo"].ToString();
                            oContacto.Clave = dr["clave"].ToString();
                        }
                    }
                };
            
            return oContacto;
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
                    cmd.Parameters.AddWithValue("nombre", model.Nombre);
                    cmd.Parameters.AddWithValue("telefono", model.Telefono);
                    cmd.Parameters.AddWithValue("correo", model.Correo);
                    cmd.Parameters.AddWithValue("clave", model.Clave);
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
                    cmd.Parameters.AddWithValue("idContactos", model.IdContactos);
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