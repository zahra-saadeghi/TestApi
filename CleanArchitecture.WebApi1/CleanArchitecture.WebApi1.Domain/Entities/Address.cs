using CleanArchitecture.WebApi1.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.WebApi1.Domain.Entities
{
    public class Address : AuditableBaseEntity
    {
        public Address()
        {
            ContactInfos = new HashSet<ContactInfo>();
        }
        public int Lat { get; set; }
        public int Lng { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }

        public virtual ICollection<ContactInfo> ContactInfos { get; set; }

    }
}
