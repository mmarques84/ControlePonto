using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ControlePontoAM.Models.entidades
{
    [Table("usuario")]
    public class usuario
    {
      [Key]
        public int codigo { get; set; }
        public string nome { get; set; }
        public string login { get; set; }
        public string senha { get; set; }
        public Boolean status { get; set; }
        public virtual ICollection<cadastrohora> cadastrohoras { get; set; }
    }
}