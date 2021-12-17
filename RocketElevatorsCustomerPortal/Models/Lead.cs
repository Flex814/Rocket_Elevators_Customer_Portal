using System;
using System.Collections.Generic;

#nullable disable

namespace RocketElevatorsCustomerPortal.Models
{
    public partial class Lead
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public string Department { get; set; }
        public string Message { get; set; }
        public byte[] Attachment { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string Filename { get; set; }
    }
}
