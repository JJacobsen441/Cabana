using Cabana.Models.DTO;
using System.Collections.Generic;
using Umbraco.Core.Models.PublishedContent;

namespace Cabana.Models
{
    public class ColSave : Umbraco.Web.PublishedModels.Save
    {
        public ColSave(IPublishedContent _c) : base(_c)
        {

        }

        public ColSave(IPublishedContent _c, List<DtoMovie> _m) : base(_c)
        {
            this.content = _c;
            this.movies = _m;
        }

        public IPublishedContent content { get; set; }
        public List<DtoMovie> movies { get; set; }
    }
}
