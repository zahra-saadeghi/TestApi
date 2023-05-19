using CleanArchitecture.WebApi1.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.WebApi1.Domain.Entities
{
    public class Family : AuditableBaseEntity
    {
        public Family()
        {
            Parents = new HashSet<Parent>();
            Students = new HashSet<Student>();
        }
        public int? BusinessId { get; set; }
        public string Email { get; set; }
        public string ExternalId { get; set; }
        public int? InsuranceId { get; set; }
        public int? ContactInfoId { get; set; }

        public virtual ICollection<Parent> Parents { get; set; }

        public virtual ICollection<Student> Students { get; set; }


    }
}
