﻿using ControlePontoAM.Models.entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ControlePontoAM.Repositorio
{

    public class cadastrohora
    {
        [Key]
        public int codigo { get; set; }
        public int codigo_usuario { get; set; }
        public string horaEntradaInicio { get; set; }
        public string horaSaidaInicio { get; set; }
        public string horaEntradaTarde { get; set; }
        public string horaSaidaTarde { get; set; }
        public string observacao { get; set; }
        public Nullable<System.DateTime> dataAlteracao { get; set; }
        public string dia { get; set; }
        public string mes { get; set; }
        public string ano { get; set; }

        public virtual usuario usuarios { get; set; }
    }
}