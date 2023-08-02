
using System.Data.SqlClient;
using System.Data;
using ContactosAdo.Models;
using ContactosAdo.datos;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ContactoAdo.datos
{
    public class ContactoDatos
    {
        //listar contactos
        public List<ContactoModel> Listar()
        {
            List<ContactoModel> Lista = new List<ContactoModel>();
            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSql()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("SP_listar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Lista.Add(new ContactoModel
                        {
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

        //odteber contactos
        public ContactoModel ObtenerContacto(int idContacto)
        {
            ContactoModel _contacto=new ContactoModel();
           
                var cn = new Conexion();
                using (var conexion= new SqlConnection(cn.getCadenaSql()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_obtener", conexion);
                    cmd.Parameters.AddWithValue("idContacto", idContacto);
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
        public bool GuardarContacto(ContactoModel model)
        {
            bool respuesta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSql()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_guardar", conexion);
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
                    SqlCommand cmd = new SqlCommand("SP_editar", conexion);
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
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSql()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_eliminar", conexion);
                    cmd.Parameters.AddWithValue("idContacto", idContacto);
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