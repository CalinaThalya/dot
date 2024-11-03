using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEndDataTech.Models
{
    [Table("tb_cliente")]
    public class Cliente
    {
        public Cliente()
        {
            Nome = string.Empty;
            email = string.Empty;
            senha = string.Empty;
        }

        [Key]
        public int id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string email { get; set; }

        [Required(ErrorMessage = "The senha field is required.")]
        public string senha { get; set; }
    }
}
