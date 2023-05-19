
using CleanArchitecture.WebApi1.Application.Features.Parents.Commands.CreateParent;
using CleanArchitecture.WebApi1.Application.Features.Parents.Commands.UpdateParent;
using CleanArchitecture.WebApi1.Application.Features.Parents.Queries.GatAllParent;
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
    public static class ParentEndpoints
    {
        public static string GetAll = "api/v1/parent/getAll";

     

        public static string Create = "api/v1/parent";

        public static string Update(int Id)
        {
            return $"api/v1/parent/{Id}";
        }
    

        public static string Delete = "api/v1/parent";

    }
    public class ParentManager : IParentManager
    {
        private readonly HttpClient _httpClient;



        public ParentManager()
        {
            _httpClient = new HttpClient();

        }

       
        public async Task<IResult<List<GetAllParentViewModel>>> GetAllAsync()
        {
            
            var response = await _httpClient.GetAsync(ParentEndpoints.GetAll.ToFullUrl());
            return await response.ToResult<List<GetAllParentViewModel>>();
        }

        public async Task<IResult<int>> CreateAsync(CreateParentCommand request)
        {
           // _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.PostAsJsonAsync(ParentEndpoints.Create.ToFullUrl(), request);
            return await response.ToResult<int>();
        }

        public async Task<IResult<int>> UpdateAsync(UpdateParentCommand request)
        {
            var response = await _httpClient.PutAsJsonAsync(ParentEndpoints.Update(request.Id).ToFullUrl(), request);
            return await response.ToResult<int>();
        }

        public async Task<IResult<int>> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{ParentEndpoints.Delete.ToFullUrl()}/{id}");
            return await response.ToResult<int>();
        }
    }
}
