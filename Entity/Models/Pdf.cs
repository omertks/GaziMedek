using Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Models
{
    public class Pdf: BaseEntity
    {

        public string Name { get; set; }

        public string Description { get; set; }
        
        public PdfTag MyProperty { get; set; } // BU DB DE STRİNG OLARAK DURACAK

        public string Path { get; set; }

    }
}
