using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenStatesApi.Tests.Fakes
{
    internal class FakeHttpMessageHandler : HttpMessageHandler
    {
        private HttpResponseMessage response;

        public HttpRequestMessage Request { get; private set; }

        public FakeHttpMessageHandler(HttpResponseMessage response)
        {
            this.response = response;
        }

        protected override Task<HttpResponseMessage>SendAsync(HttpRequestMessage request, 
            CancellationToken cancellationToken)
        {
            // Set the Request property so it can be inspected
            Request = request;
            // Return the canned response
            var responseTask =
                new TaskCompletionSource<HttpResponseMessage>();
            responseTask.SetResult(response);
            return responseTask.Task;
        }
    }
}
