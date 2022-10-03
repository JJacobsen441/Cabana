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

    public class RegisterCustomBackofficeMvcRouteComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Register<ApiAdminController>(Lifetime.Request);
            composition.Components().Append<RegisterCustomBackofficeMvcRouteComponent>();

        }
    }

    public class RegisterCustomBackofficeMvcRouteComponent : IComponent
    {
        private readonly IGlobalSettings _globalSettings;

        public RegisterCustomBackofficeMvcRouteComponent(IGlobalSettings globalSettings)
        {
            _globalSettings = globalSettings;
        }

        public void Initialize()
        {
            /*
             * I need some guidance on this matter :)
             * */
            ;

            RouteTable.Routes.MapRoute("MembersMovies",
                _globalSettings.GetUmbracoMvcArea() + "/backoffice/api/{controller}/{action}/{name}/movies",
                //_globalSettings.GetUmbracoMvcArea() + "/backoffice/api/{controller}/{action}/{name}",
                new
                {
                    controller = "ApiAdmin",
                    action = "GetMembersMovies",
                    name = UrlParameter.Optional
                },
                constraints: new { controller = "ApiAdmin" });

            ;
        }

        public void Terminate()
        {
            // Nothing to terminate
        }
    }
}