using Ribbit_API.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace Ribbit_API.Models
{
    public class Product : IEntityBase
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage ="Este campo es requerido")]
        [MinLength(3, ErrorMessage = "Minimo 3 caracteres")]
        [MaxLength(100, ErrorMessage = "Maximo 100 caracteres")]
        public string Nombre { get; set; }

        [Display(Name = "Description")]
        [MaxLength(500, ErrorMessage = "Maximo 500 caracteres")]
        public string? Descripcion { get; set; }

        [Display(Name = "Precio")]
        [Required(ErrorMessage = "Este campo es requerido")]
        [Range(0.01 , double.MaxValue, ErrorMessage = "Solo numeros positivos")]
        public decimal Precio { get; set; }

        [Display(Name = "Stock")]
        [Required(ErrorMessage = "Este campo es requerido")]
        [Range(1, int.MaxValue, ErrorMessage ="Solo numeros enteros positivos")]
        public int Stock { get; set; }

        [Display(Name = "Fecha de creacion")]
        public DateTime FechaCreacion { get; set; }
    }
}
