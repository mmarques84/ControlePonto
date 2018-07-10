using ControlePontoAM.Models.entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace ControlePontoAM.Models.repositorio
{
    public class Contexto : DbContext
    {
        public Contexto() : base("DefaultConnection")
        {

        }

        public virtual DbSet<cadastrohora> cadastrohora { get; set; }
        public virtual DbSet<usuario> usuario { get; set; }

       // public System.Data.Entity.DbSet<cadastrohora> cadastrohoras { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<cadastrohora>();
            //modelBuilder.Entity<usuario>();


        }

      
    }
}