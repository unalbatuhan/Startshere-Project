using System;
using AppStudio.DataProviders;

namespace StartsHere.Sections
{
    /// <summary>
    /// Implementation of the AgYazlm1Schema class.
    /// </summary>
    public class AgYazlm1Schema : SchemaBase
    {

        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public string Thumbnail { get; set; }
    }
}
