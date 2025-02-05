﻿using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace NikeFrontend.Services
{
    public class ValidateHeaderHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!request.Headers.Contains("Authorization"))
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}