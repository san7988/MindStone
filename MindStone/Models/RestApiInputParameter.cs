using System;
namespace MindStone.Models
{
    public class RestApiInputParameter
    {
        public string ParamName { get; }
        public dynamic ParamMetaValue { get; }

        public RestApiInputParameter(string paramName, dynamic paramMetaValue)
        {
            ParamName = paramName;
            ParamMetaValue = paramMetaValue;
        }
    }
}
