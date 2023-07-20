using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using System;
using System.Diagnostics;
using System.Net.Http;

namespace TranslationManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        private const int MaxRetries = 5;//should be in app config
        protected readonly AsyncRetryPolicy _retryPolicy;
        public BaseController()
        {
            _retryPolicy = Policy.Handle<ApplicationException>().WaitAndRetryAsync(
                MaxRetries,
                retryAttempt => TimeSpan.FromSeconds(3),
                (exception, timespan) =>
                {
                    Debug.WriteLine($"Retrying api call at {DateTime.UtcNow}");
                });
        }
    }
}
