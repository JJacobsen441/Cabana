using Cabana.Models.DTO;
using Umbraco.Core.Models.PublishedContent;

namespace Cabana.Models
{
    public class ColShow : Umbraco.Web.PublishedModels.Show
    {
        public ColShow(IPublishedContent _c) : base(_c)
        {

        }

        public ColShow(IPublishedContent _c, DtoMovie _m) : base(_c)
        {
            this.content = _c;
            this.movie = _m;
        }

        public IPublishedContent content { get; set; }
        public DtoMovie movie { get; set; }
    }
}
