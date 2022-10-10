using Cabana.Controllers;
using System.Web.Mvc;
using System.Web.Routing;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Core.Configuration;

namespace Cabana.Migrations
{

    /*public class AttributeRoutingComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Components().Append<AttributeRoutingComponent>(); ;
        }
    }

    public class AttributeRoutingComponent : IComponent
    {
        public void Initialize()
        {
            GlobalConfiguration.Configuration.MapHttpAttributeRoutes();
        }

        public void Terminate()
        {

        }
    }/**/

    public class RegisterDeleteMvcRouteComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Register<DeleteController>(Lifetime.Request);
            composition.Components().Append<RegisterDeleteMvcRouteComponent>();

        }
    }

    public class RegisterDeleteMvcRouteComponent : IComponent
    {
        private readonly IGlobalSettings _globalSettings;

        public RegisterDeleteMvcRouteComponent(IGlobalSettings globalSettings)
        {
            _globalSettings = globalSettings;
        }

        public void Initialize()
        {
            /*
             * I need some guidance on this matter :)
             * */
            ;

            RouteTable.Routes.MapRoute("DeleteMovie",
                "Delete/{action}/{movie_id}",
                new
                {
                    controller = "Delete",
                    action = "Delete",
                    movie_id = UrlParameter.Optional
                }
                //constraints: new { controller = "ApiAdmin" }
                );

            ;
        }

        public void Terminate()
        {
            // Nothing to terminate
        }
    }
}