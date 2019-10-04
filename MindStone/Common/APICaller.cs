using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MindStone.Models;
using System.Net.Http;
using System.Linq;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Linq;

namespace MindStone.Common
{
    public class APICaller
    {
        private HttpResponseMessage _response;
        public APICaller()
        {
            _response = new HttpResponseMessage();
        }

        public async Task<RestApiResponse> CallRestApiAsync(RestApiSettings restApiSettings)
        {
            
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(restApiSettings.EndPoint);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(
                    restApiSettings.MediaType
                    ));
            foreach (KeyValuePair<string, string> entry in restApiSettings.Headers)
            {
                client.DefaultRequestHeaders.Add(entry.Key, entry.Value);
            }

            if (restApiSettings.Method.ToLowerInvariant().Equals("get"))
            {
                _response = await client.GetAsync("");
            }
            else if (restApiSettings.Method.ToLowerInvariant().Equals("post"))
            {
                var paramDict = restApiSettings.RestApiInputParameters.Select
                    (p => new { key = p.ParamName, val = p.ParamMetaValue })
                    .ToDictionary(_x => _x.key, _x => _x.val);
                var serializedInputParams = JsonConvert.SerializeObject(paramDict);
                var httpPostContent = new StringContent(
                                                serializedInputParams,
                                                Encoding.UTF8,
                                                restApiSettings.MediaType);
                var fragments = client.BaseAddress.Segments;
                string subAddress = null;
                foreach (var fragment in fragments)
                {
                    subAddress += fragment;
                }
                _response = await client.PostAsync(subAddress, httpPostContent);
            }

            return await ParseAPIResponse(restApiSettings);

        }

        private async Task<RestApiResponse> ParseAPIResponse(RestApiSettings restApiSettings)
        {

            var statusCode = _response.StatusCode;
            if (_response.IsSuccessStatusCode)
            {
                return await ParseFromSuccessfullApiResponse(_response, restApiSettings);
            }
            else if (((int)statusCode >= 300) && ((int)statusCode <= 399))
            {
                throw new Exception($"API received a {statusCode} status code.Please check the API parameters." +
                    $"Client might need to take some additional action in order to complete the request.");
            }
            else if (((int)statusCode >= 400) && ((int)statusCode <= 499))
            {
                throw new Exception($"API received a {statusCode} status code.");
            }
            else
            {
                throw new Exception($"API received a {statusCode} status code");
            }

        }

        private async Task<RestApiResponse> ParseFromSuccessfullApiResponse(HttpResponseMessage response, RestApiSettings restApiSettings)
        {
            RestApiResponse apiResponse = new RestApiResponse();
            apiResponse.HttpResponseCode = response.StatusCode;
            
            Dictionary<string, string> filteredParams = new Dictionary<string, string>();
            
            if (response.Content.Headers.ContentType.MediaType.ToLowerInvariant().Equals("text/plain") ||
                response.Content.Headers.ContentType.MediaType.ToLowerInvariant().Equals("text/html"))
            {
                var parsedResponse = await response.Content.ReadAsStringAsync();
                filteredParams.Add("parsedValue", parsedResponse);
                
                apiResponse.FilteredParams = filteredParams;
            }
            else if (response.Content.Headers.ContentType.MediaType.ToLowerInvariant().Equals("application/json"))
            {
                var parsedResponse = await response.Content.ReadAsStringAsync();
                filteredParams.Add("parsedValue", parsedResponse);

                apiResponse.FilteredParams = filteredParams;
                //dynamic deserializedResponse = JsonConvert.DeserializeObject(parsedResponse);
                //foreach (var item in restApiSettings.HttpJsonResponseParserSettings)
                //{
                //    JToken filteredParamValue = deserializedResponse.SelectToken(item.JsonFilterToApply);
                //    filteredParams.Add(item.TargetVariable, filteredParamValue.ToString());
                //}
                //apiResponse.FilteredParams = filteredParams;
            }
            else
            {
                throw new Exception($"Could not parse the media type {response.Content.Headers.ContentType.MediaType}");
            }
            return apiResponse;
        }
    }
}
