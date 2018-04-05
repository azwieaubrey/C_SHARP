﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GymManagementSystem
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class GMS_DBEntities : DbContext
    {
        public GMS_DBEntities()
            : base("name=GMS_DBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerMembership> CustomerMemberships { get; set; }
        public virtual DbSet<FitnessMeasurement> FitnessMeasurements { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Membership> Memberships { get; set; }
        public virtual DbSet<OrderedProduct> OrderedProducts { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductSold> ProductSolds { get; set; }
        public virtual DbSet<Registration> Registrations { get; set; }
        public virtual DbSet<Stock> Stocks { get; set; }
        public virtual DbSet<SubCategory> SubCategories { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Temp_Stock> Temp_Stock { get; set; }
    }
}
