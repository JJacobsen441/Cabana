using System.Collections.Generic;
using System.Net.Http.Formatting;
using Umbraco.Core;
using Umbraco.Web.Actions;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Mvc;
using Umbraco.Web.Trees;

namespace Cabana.App_Plugins.AdminSection
{
    [Tree("myAdminSection", "treeHeaders", TreeTitle = "Admin", TreeGroup = "adminGroup", SortOrder = 5)]
    [PluginController("adminSection")]
    public class AdminTreeController : TreeController
    {
        // GET: Admin
        protected override TreeNodeCollection GetTreeNodes(string id, FormDataCollection queryStrings)
        {
            var nodes = new TreeNodeCollection();

            // check if we're rendering the root node's children
            if (id == Constants.System.Root.ToInvariantString())
            {
                // you can get your custom nodes from anywhere, and they can represent anything...
                Dictionary<int, string> layers = new Dictionary<int, string>();
                layers.Add(1, "Members");
                //layers.Add(2, "Whiskers on Kittens");
                //layers.Add(3, "Skys full of Stars");
                //layers.Add(4, "Warm Woolen Mittens");
                //layers.Add(5, "Cream coloured Unicorns");
                //layers.Add(6, "Schnitzel with Noodles");

                // loop through our favourite things and create a tree item for each one
                foreach (var layer in layers)
                {
                    // add each node to the tree collection using the base CreateTreeNode method
                    // it has several overloads, using here unique Id of tree item, -1 is the Id of the parent node to create, eg the root of this tree is -1 by convention - the querystring collection passed into this route - the name of the tree node -  css class of icon to display for the node - and whether the item has child nodes
                    var node = CreateTreeNode(layer.Key.ToString(), "-1", queryStrings, layer.Value, "icon-presentation", false);
                    nodes.Add(node);
                }
            }

            return nodes;
        }

        protected override MenuItemCollection GetMenuForNode(string id, FormDataCollection queryStrings)
        {
            // create a Menu Item Collection to return so people can interact with the nodes in your tree
            var menu = new MenuItemCollection();
            string _c = Constants.System.Root.ToInvariantString();
            switch (id)
            {
                case "-1":
                    // root actions, perhaps users can create new items in this tree, or perhaps it's not a content tree, it might be a read only tree, or each node item might represent something entirely different...
                    // add your menu item actions or custom ActionMenuItems
                    menu.Items.Add(new CreateChildEntity(Services.TextService));
                    // add refresh menu item (note no dialog)
                    menu.Items.Add(new RefreshNode(Services.TextService, true));
                    break;
                case "1":
                    // add a delete action to each individual item
                    menu.Items.Add<ActionBrowse>(Services.TextService, true, opensDialog: true);
                    //menu.Items.Add<ActionBrowse>(Services.TextService, true, opensDialog: true);
                    //menu.Items.Add<ActionDelete>(Services.TextService, true, opensDialog: true);
                    break;
            }

            return menu;
        }

        protected override TreeNode CreateRootNode(FormDataCollection queryStrings)
        {
            var root = base.CreateRootNode(queryStrings);

            // set the icon
            root.Icon = "icon-hearts";
            // could be set to false for a custom tree with a single node.
            root.HasChildren = true;
            //url for menu
            root.MenuUrl = null;

            return root;
        }
    }
}