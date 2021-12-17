using System;
using System.Collections.Generic;

#nullable disable

namespace RocketElevatorsCustomerPortal.Models
{
    public partial class Quote
    {
        public long Id { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string BuildingType { get; set; }
        public string Apartments { get; set; }
        public string Floors { get; set; }
        public string Basements { get; set; }
        public string Elevators { get; set; }
        public string Companies { get; set; }
        public string ParkingSpots { get; set; }
        public string Corporations { get; set; }
        public string MaximumOccupancy { get; set; }
        public string BusinessHours { get; set; }
        public string ServicesType { get; set; }
        public string AmountOfElevator { get; set; }
        public string PricePerElevator { get; set; }
        public string Installation { get; set; }
        public string PriceElevatorTotal { get; set; }
        public string TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
