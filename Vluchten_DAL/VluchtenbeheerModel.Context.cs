﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Vluchten_DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class VluchtenbeheerEntities : DbContext
    {
        public VluchtenbeheerEntities()
            : base("name=VluchtenbeheerEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Klasse> Klasse { get; set; }
        public virtual DbSet<Luchthaven> Luchthaven { get; set; }
        public virtual DbSet<Passagier> Passagier { get; set; }
        public virtual DbSet<Reservering> Reservering { get; set; }
        public virtual DbSet<Reserveringvlucht> Reserveringvlucht { get; set; }
        public virtual DbSet<Vlucht> Vlucht { get; set; }
    }
}