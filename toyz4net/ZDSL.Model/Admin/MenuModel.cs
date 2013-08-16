using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toyz4net.Core.Util;
using Toyz4net.Core.Model;


namespace ZDSL.Model.Admin
{
    public class MenuModel:BaseModel
    {

        public static string MENU_ID_ROOT = "root";

        public string id { get; set; }
        public string text { get; set; }
        public string iconCls { get; set; }
        public string parentId { get; set; }
        public string state { get; set; }
        public string url { get; set; }
        public int orderNum { get; set; }
        public string parma { get; set; }



        public override object getPk()
        {
            return this.id;
        }

        public override void setPk(object pk)
        {
            this.id = pk.ToString();
        }

        public TreeNodeObject toTreeNode() {
            TreeNodeObject treeNode = new TreeNodeObject();
            treeNode.id = this.id;
            treeNode.text = this.text;
            treeNode.iconCls = this.iconCls;
            treeNode.state = this.state;
            treeNode.attributes.Add("url",this.url);
            treeNode.attributes.Add("parma", this.parma);
            return treeNode;
        }

        public static IList<TreeNodeObject> getTreeNode(IList<MenuModel> menus) {
            return getTreeNode(menus, MENU_ID_ROOT);
        }

        public static IList<TreeNodeObject> getTreeNode(IList<MenuModel> menus, string rootId) {
            IList<TreeNodeObject> treeNodes = new List<TreeNodeObject>();
            foreach (MenuModel menu in menus) {
                if (menu.parentId!=rootId) {
                    continue;
                }
                TreeNodeObject tempNode = menu.toTreeNode();
                tempNode.children=getTreeNode(menus,menu.id);
                treeNodes.Add(tempNode);
            }
            return treeNodes;

        }
    }
}
