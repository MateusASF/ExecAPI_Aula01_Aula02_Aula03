using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace ApiAula01.Filters
{
    public class LogResourceFilterTimer : IResourceFilter
    {
        Stopwatch relogio = new();
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            relogio.Stop();
            Console.WriteLine(relogio.Elapsed);
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            relogio.Start();
        }
    }
}
