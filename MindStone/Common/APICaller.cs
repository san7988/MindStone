using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MindStone.Models;

namespace MindStone.Common
{
    public class APICaller
    {
        public APICaller()
        {
        }

        public async Task<ImageExtract> CallApi()
        {
            return await Task.FromResult<ImageExtract>(GetDummyData());

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
