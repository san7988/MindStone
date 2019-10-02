using System;
using System.Collections.Generic;

namespace MindStone.Models
{
    public class ImageExtract
    {
        public string Id { get; set; }
        public List<ExtractedField> Extraction { get; set; }
    }
}
