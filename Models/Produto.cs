using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndDataTech.Models
{
    
        
    [Table("tb_produto")]
    public class Produto
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string Nome { get; set; } = string.Empty;

        public int preco { get; set; }
        public int EmpresaId { get; set; }
        public required Empresa Empresa { get; set; }
    }
    }
