using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.WebApi1.Application.Features.Families.Queries.GatAllFamilies
{
    public class GetAllFamiliesViewModel
    {
        public int Id { get; set; }
        public int BusinessId { get; set; }
        public string Email { get; set; }
        public string ExternalId { get; set; }
        public string InsuranceId { get; set; }
        public int ContactInfoId { get; set; }

       
    }
}
