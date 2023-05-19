using CleanArchitecture.WebApi1.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.WebApi1.Domain.Entities
{
    public class ContactInfo : AuditableBaseEntity
    {
       
        public string PrimaryPhone { get; set; }
        public string AlternatePhone { get; set; }

        public int AddressId { get; set; }

        public virtual Address Address { get; set; }

    }
}
