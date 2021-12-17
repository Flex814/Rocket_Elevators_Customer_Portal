using System;
using System.Collections.Generic;

#nullable disable

namespace RocketElevatorsCustomerPortal.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Buildings = new HashSet<Building>();
            Interventions = new HashSet<Intervention>();
        }

        public long Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string CompanyDescription { get; set; }
        public string ServiceTechName { get; set; }
        public string ServiceTechPhone { get; set; }
        public string ServiceTechEmail { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public long? AddressId { get; set; }
        public long? UserId { get; set; }

        public virtual Address Address { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Building> Buildings { get; set; }
        public virtual ICollection<Intervention> Interventions { get; set; }
    }
}
