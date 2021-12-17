using System;
using System.Collections.Generic;

#nullable disable

namespace RocketElevatorsCustomerPortal.Models
{
    public class Intervention
    {
        public long Id { get; set; }
        public long? UserId { get; set; }
        public long? CustomerId { get; set; }
        public long? BuildingId { get; set; }
        public long? BatteryId { get; set; }
        public long? ColumnId { get; set; }
        public long? ElevatorId { get; set; }
        public long? EmployeeId { get; set; }
        public DateTime? InterventionStart { get; set; }
        public DateTime? InterventionEnd { get; set; }
        public string Result { get; set; }
        public string Report { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Battery Battery { get; set; }
        public virtual Building Building { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual User User { get; set; }
    }
}
