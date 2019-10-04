using System;
using System.Collections.Generic;
using System.Net;

namespace MindStone.Models
{
    public class RestApiResponse
    {
        public IReadOnlyDictionary<string, string> FilteredParams { get; set; }
        public HttpStatusCode HttpResponseCode { get; set; }
        
    }
}
