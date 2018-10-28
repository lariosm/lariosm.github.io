using Apartment.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Apartment.DAL
{
    public class TenantContext : DbContext
    {
        public TenantContext() : base("name=TenantForms")
        {

        }

        public virtual DbSet<Tenant> Tenants { get; set; }
    }
}