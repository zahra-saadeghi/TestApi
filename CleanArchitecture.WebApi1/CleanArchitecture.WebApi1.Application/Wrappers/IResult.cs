using System.Collections.Generic;

namespace CleanArchitecture.WebApi1.Application.Wrappers
{
    public interface IResult
    {
        List<string> Messages { get; set; }

        bool Succeeded { get; set; }
    }

    public interface IResult<out T> : IResult
    {
        T Data { get; }
    }
}