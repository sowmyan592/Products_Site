﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Products_Site.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class testEntities1 : DbContext
    {
        public testEntities1()
            : base("name=testEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<RegisterUser> RegisterUsers { get; set; }

        public System.Data.Entity.DbSet<Products_Site.Models.Registration_Model> Registration_Model { get; set; }

        public System.Data.Entity.DbSet<Products_Site.Models.Login_Model> Login_Model { get; set; }
    }
}
