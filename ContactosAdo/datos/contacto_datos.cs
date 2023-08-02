using ContactoAdo.Models;
using System.Data.SqlClient;
using System.Data;
using ContactosAdo.Models;
using ContactosAdo.datos;

namespace ContactoAdo.datos
{
    public class ContactoDatos
    {
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
                            IdContacto = Convert.ToInt32(dr["IdContactos"]),
                            Nombre = dr["nombre"].ToString(),
                            Telefono = dr["telefono"].ToString(),
                            Correo = dr["correo"].ToString(),
                            Clave = dr["clave"].ToString()
                        });
                    }
                }
            }
            return Lista;
        }

        public ContactoModel ObtenerContacto(int idContacto)
        {
            ContactoModel _contacto=new ContactoModel();
           
                var cn = new Conexion();
                using (var conexion= new SqlConnection(cn.getCadenaSql()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_obtener", conexion)
                }
            
            return _contacto;
        }

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
                    cmd.Parameters.AddWithValue("nombre",model.nombre)
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

        public ContactoModel EliminarContacto(int idContacto)
        {
            ContactoModel _contacto = new ContactoModel();

            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSql()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("SP_eliminar", conexion)
                }

            return _contacto;
        }
    }
}﻿