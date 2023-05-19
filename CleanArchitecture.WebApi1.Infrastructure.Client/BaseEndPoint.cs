using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi1.Infrastructure.Client
{
    public  class BaseEndPoint
    {
        public  string BaseUrl { get; set; } = "https://localhost:4221/";

      
        public  override string ToString()
        {
            
            return BaseUrl + base.ToString();
        }
    }
}
