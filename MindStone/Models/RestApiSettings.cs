using System;
using System.Collections.Generic;

namespace MindStone.Models
{
    public class RestApiSettings
    {
        public string EndPoint { get; set; }
        public string Method { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public List<RestApiInputParameter> RestApiInputParameters { get; set; }
        public string MediaType { get; set; }
        public List<HttpJsonParserSettings> HttpJsonResponseParserSettings { get; set; }
    }


}
