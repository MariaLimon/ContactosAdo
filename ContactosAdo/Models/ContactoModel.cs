using System.ComponentModel.DataAnnotations;
namespace CrudAdoNet.Models
{
    public class ContactoModel
    {
        public int IdContactos { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Correo { get; set;}
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Clave { get; set; }
    }
}
