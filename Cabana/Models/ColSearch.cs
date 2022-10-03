using Cabana.Models.DTO;
using System.Collections.Generic;
using Umbraco.Core.Models.PublishedContent;

namespace Cabana.Models
{
    public class ColSearch : Umbraco.Web.PublishedModels.Search
    {
        public ColSearch(IPublishedContent _c) : base(_c)
        {

        }

        public ColSearch(IPublishedContent _c, IEnumerable<DtoMovie> _m) : base(_c)
        {
            this.content = _c;
            this.movies = _m;
        }

        public IPublishedContent content { get; set; }
        public IEnumerable<DtoMovie> movies { get; set; }
    }
}
