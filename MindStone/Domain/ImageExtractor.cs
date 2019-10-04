using System;
using System.Collections.Generic;
using MindStone.Models;

namespace MindStone.Domain
{
    public class ImageExtractor
    {
        private string _base64String;
        private string _id;
        private string _templateName;
        private string _ocrEngine;

        public ImageExtractor(string base64String,
            string id="string",
            string templateName="akshay_sg_id",
            string ocrEngine="TESSERACT")
        {
            _base64String = base64String;
            _ocrEngine = ocrEngine;
            _id = id;
            _templateName = templateName;
        }

        public ImageExtract GetImageExtract()
        {
            //Calling dummy method for now. Will be replaced
            //with API call
            return GetDummyData();
        }

        private RestApiSettings GetRestAPISettings()
        {
            var restAPISettings = new RestApiSettings();
            restAPISettings.EndPoint = "some url";
            restAPISettings.Method = "POST";
            restAPISettings.MediaType = "application/json";

            restAPISettings.RestApiInputParameters = new List<RestApiInputParameter>();
            restAPISettings.RestApiInputParameters.Add(new RestApiInputParameter("id", _id));
            restAPISettings.RestApiInputParameters.Add(new RestApiInputParameter("templateName", _templateName));
            restAPISettings.RestApiInputParameters.Add(new RestApiInputParameter("ocrEngine", _ocrEngine));
            restAPISettings.RestApiInputParameters.Add(new RestApiInputParameter("base64String", _base64String));
            return restAPISettings;
        }

        private ImageExtract GetDummyData()
        {
            var dummyExtract = new ImageExtract()
            {
                Id = "string",
                Extraction = new List<ExtractedField>()
            };
            var extract1 = new ExtractedField()
            {
                FieldName = "Name",
                FieldValue = "AIYAZAN",
                FieldColor = null,
                FieldCoordinates = new Coordinates()
                {
                    X1 = 0.03272,
                    Y1 = 0.4315,
                    X2 = 0.21909,
                    Y2 = 0.52005
                }
            };
            dummyExtract.Extraction.Add(extract1);

            var extract2 = new ExtractedField()
            {
                FieldName = "Name",
                FieldValue = "MOHAMMAD",
                FieldColor = null,
                FieldCoordinates = new Coordinates()
                {
                    X1 = 0.24636,
                    Y1 = 0.43568,
                    X2 = 0.60727,
                    Y2 = 0.50207
                }
            };
            dummyExtract.Extraction.Add(extract2);
            return dummyExtract;
        }
    }
}
