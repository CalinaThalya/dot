using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndDataTech.Models
{
        
    [Table("tb_empresa")]
    public class Empresa
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string Nome { get; set; } = string.Empty;
        public string CNPJ { get; set; } = string.Empty;

    }
    }
