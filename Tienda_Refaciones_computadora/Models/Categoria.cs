using System.ComponentModel.DataAnnotations;

namespace Tienda_Refaciones_computadora.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50)]
        public string Nombre { get; set; } = string.Empty;

        public List<Producto> Productos { get; set; } = new();
    }
}