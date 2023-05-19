
using CleanArchitecture.WebApi1.Application.Features.Insurances.Commands.CreateInsurance;
using CleanArchitecture.WebApi1.Application.Features.Insurances.Commands.UpdateInsurance;
using CleanArchitecture.WebApi1.Application.Features.Insurances.Queries.GatAllInsurances;
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
    public static class InsuranceEndpoints
    {
        public static string GetAll = "api/v1/insurance/getAll";

     

        public static string Create = "api/v1/insurance";

        public static string Update(int Id)
        {
            return $"api/v1/insurance/{Id}";
        }
    

        public static string Delete = "api/v1/insurance";

    }
    public class InsuranceManager : IInsuranceManager
    {
        private readonly HttpClient _httpClient;



        public InsuranceManager()
        {
            _httpClient = new HttpClient();

        }

       
        public async Task<IResult<List<GetAllInsuranceViewModel>>> GetAllAsync()
        {
            
            var response = await _httpClient.GetAsync(InsuranceEndpoints.GetAll.ToFullUrl());
            return await response.ToResult<List<GetAllInsuranceViewModel>>();
        }

        public async Task<IResult<int>> CreateAsync(CreateInsuranceCommand request)
        {
           // _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.PostAsJsonAsync(InsuranceEndpoints.Create.ToFullUrl(), request);
            return await response.ToResult<int>();
        }

        public async Task<IResult<int>> UpdateAsync(UpdateInsuranceCommand request)
        {
            var response = await _httpClient.PutAsJsonAsync(InsuranceEndpoints.Update(request.Id).ToFullUrl(), request);
            return await response.ToResult<int>();
        }

        public async Task<IResult<int>> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{InsuranceEndpoints.Delete.ToFullUrl()}/{id}");
            return await response.ToResult<int>();
        }
    }
}
