﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Project2016.Data_Tier
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Project2016Entities : DbContext
    {
        public Project2016Entities()
            : base("name=Project2016Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<album> albums { get; set; }
        public virtual DbSet<artist> artists { get; set; }
        public virtual DbSet<track> tracks { get; set; }
        public virtual DbSet<user> users { get; set; }
        public virtual DbSet<played> playeds { get; set; }
    }
}
