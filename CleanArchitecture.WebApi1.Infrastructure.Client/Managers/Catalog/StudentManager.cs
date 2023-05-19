
using CleanArchitecture.WebApi1.Application.Features.Students.Commands.CreateStudent;
using CleanArchitecture.WebApi1.Application.Features.Students.Commands.UpdateStudent;
using CleanArchitecture.WebApi1.Application.Features.Students.Queries.GatAllStudent;
using CleanArchitecture.WebApi1.Application.Wrappers;
using CleanArchitecture.WebApi1.Infrastructure.Client.Extensions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi1.Infrastructure.Client.Managers.Catalog
{
    public static class StudentEndpoints
    {
        public static string GetAll = "api/v1/student/getAll";

     

        public static string Create = "api/v1/student";

        public static string Update(int Id)
        {
            return $"api/v1/student/{Id}";
        }
    

        public static string Delete = "api/v1/student";

    }
    public class StudentManager : IStudentManager
    {
        private readonly HttpClient _httpClient;



        public StudentManager()
        {
            _httpClient = new HttpClient();

        }

       
        public async Task<IResult<List<GetAllStudentViewModel>>> GetAllAsync()
        {
            
            var response = await _httpClient.GetAsync(StudentEndpoints.GetAll.ToFullUrl());
            return await response.ToResult<List<GetAllStudentViewModel>>();
        }

        public async Task<IResult<int>> CreateAsync(CreateStudentCommand request)
        {
           // _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.PostAsJsonAsync(StudentEndpoints.Create.ToFullUrl(), request);
            return await response.ToResult<int>();
        }

        public async Task<IResult<int>> UpdateAsync(UpdateStudentCommand request)
        {
            var response = await _httpClient.PutAsJsonAsync(StudentEndpoints.Update(request.Id).ToFullUrl(), request);
            return await response.ToResult<int>();
        }

        public async Task<IResult<int>> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{StudentEndpoints.Delete.ToFullUrl()}/{id}");
            return await response.ToResult<int>();
        }
    }
}
