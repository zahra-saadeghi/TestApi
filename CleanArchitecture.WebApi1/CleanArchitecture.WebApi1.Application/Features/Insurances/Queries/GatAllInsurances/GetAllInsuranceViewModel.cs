using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.WebApi1.Application.Features.Insurances.Queries.GatAllInsurances
{
    public class GetAllInsuranceViewModel
    {
        public int Id { get; set; }

        public string ExternalId { get; set; }
        public string CompanyName { get; set; }
        public string PolicyNumber { get; set; }
        public string CompanyPhone { get; set; }


    }
}
