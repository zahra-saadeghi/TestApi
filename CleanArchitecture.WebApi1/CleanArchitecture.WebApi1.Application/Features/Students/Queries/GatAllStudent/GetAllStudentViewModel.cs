using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.WebApi1.Application.Features.Students.Queries.GatAllStudent
{
    public class GetAllStudentViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string DateOfBirth { get; set; }
        public string GradeLevel { get; set; }
        public int? ContactInfoId { get; set; }

        public int FamilyId { get; set; }


    }
}
