using CleanArchitecture.WebApi1.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.WebApi1.Domain.Entities
{
    public class Insurance : AuditableBaseEntity
    {
      
        public string ExternalId { get; set; }
        public string CompanyName { get; set; }
        public string PolicyNumber { get; set; }
        public string CompanyPhone { get; set; }
    }
}
