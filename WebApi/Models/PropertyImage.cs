using System;
using System.Collections.Generic;

#nullable disable

namespace WebApi.Models
{
    public partial class PropertyImage
    {
        public int IdPropertyImage { get; set; }
        public int IdProperty { get; set; }
        public byte[] File { get; set; }
        public bool Enabled { get; set; }

        public virtual Property IdPropertyNavigation { get; set; }
    }
}
