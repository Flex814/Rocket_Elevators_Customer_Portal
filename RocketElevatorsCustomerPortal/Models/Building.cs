using System;
using System.Collections.Generic;

#nullable disable

namespace RocketElevatorsCustomerPortal.Models
{
    public partial class Building
    {
        public Building()
        {
            Batteries = new HashSet<Battery>();
            BuildingDetails = new HashSet<BuildingDetail>();
            Interventions = new HashSet<Intervention>();
        }

        public long Id { get; set; }
        public string AdminName { get; set; }
        public string AdminEmail { get; set; }
        public string AdminPhoneNumer { get; set; }
        public string TechFullName { get; set; }
        public string TechEmail { get; set; }
        public string TechPhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public long? AddressId { get; set; }
        public long? CustomerId { get; set; }

        public virtual Address Address { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Battery> Batteries { get; set; }
        public virtual ICollection<BuildingDetail> BuildingDetails { get; set; }
        public virtual ICollection<Intervention> Interventions { get; set; }
    }
}
