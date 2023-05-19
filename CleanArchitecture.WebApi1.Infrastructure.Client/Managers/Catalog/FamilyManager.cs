using CleanArchitecture.WebApi1.Application.Enums;
using CleanArchitecture.WebApi1.Application.Features.Families.Commands.CreateFamily;
using CleanArchitecture.WebApi1.Application.Features.Families.Commands.UpdateFamily;
using CleanArchitecture.WebApi1.Application.Features.Families.Queries.GatAllFamilies;
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
    public static class FamilyEndpoints
    {
        public static string GetAll = "api/v1/family/getAll";

     

        public static string Create = "api/v1/family";

        public static string Update(int Id)
        {
            return $"api/v1/family/{Id}";
        }
    

        public static string Delete = "api/v1/family";

    }
    public class FamilyManager : IFamilyManager
    {
        private readonly HttpClient _httpClient;



        public FamilyManager()
        {
            _httpClient = new HttpClient();

        }

       
        public async Task<IResult<List<GetAllFamiliesViewModel>>> GetAllAsync()
        {
            
            var response = await _httpClient.GetAsync(FamilyEndpoints.GetAll.ToFullUrl());
            return await response.ToResult<List<GetAllFamiliesViewModel>>();
        }

        public async Task<IResult<int>> CreateAsync(CreateFamilyCommand request)
        {
           // _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.PostAsJsonAsync(FamilyEndpoints.Create.ToFullUrl(), request);
            return await response.ToResult<int>();
        }

        public async Task<IResult<int>> UpdateAsync(UpdateFamilyCommand request)
        {
            var response = await _httpClient.PutAsJsonAsync(FamilyEndpoints.Update(request.Id).ToFullUrl(), request);
            return await response.ToResult<int>();
        }

        public async Task<IResult<int>> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{FamilyEndpoints.Delete.ToFullUrl()}/{id}");
            return await response.ToResult<int>();
        }
    }
}
