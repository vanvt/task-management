using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    internal class TestUploadFunction
    {
        [Test]
        public async Task UploadAsync() {
            var options = new RestClientOptions("http://localhost:7238");
            var client = new RestClient(options);
            var request = new RestRequest("/api/FileUpload", Method.Post);
            request.AlwaysMultipartFormData = true;
            request.AddFile("File", "./image/micky.png");
            RestResponse response = await client.ExecuteAsync(request);
            var expectString = "https://vanvt.blob.core.windows.net/task-image/micky.png";
            Assert.IsTrue(response.Content.Contains(expectString));
        }
    }
}
