// Mohammed Osama Mohammed
//Windows now Browser 2018
//Email : mohmamed.osama914@gmail.com
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace MicrosoftWindowsManagerBrowser
{
    public partial class OrganizeFavorites : Form
    {
        TreeView tree;
        ToolStrip linkbar;
        String favXml = WindowsNowBrowser.favXml, linksXml = WindowsNowBrowser.linksXml;
        ContextMenuStrip linkContext, favContext;
        public OrganizeFavorites(TreeView tree, ToolStrip linkbar, ContextMenuStrip linkContext, ContextMenuStrip favContext)
        {
            this.tree = tree;
            this.linkbar = linkbar;
            this.linkContext = linkContext;
            this.favContext = favContext;
            InitializeComponent();
        }

        private void OrganizeFavorites_Load(object sender, EventArgs e)
        {
            organizeFavTreeView.ImageList = tree.ImageList;
            organizeFavTreeView.Nodes[0].ImageIndex = 0;

            XmlDocument myXml = new XmlDocument();

            if (File.Exists(favXml))
            {
                myXml.Load(favXml);

                foreach (XmlElement el in myXml.DocumentElement.ChildNodes)
                {
                    Uri url = new Uri(el.GetAttribute("url"));
                    TreeNode node = new TreeNode(el.InnerText, tree.ImageList.Images.IndexOfKey(url.Host.ToString()), tree.ImageList.Images.IndexOfKey(url.Host.ToString()));
                    node.ToolTipText = el.GetAttribute("url");
                    node.Name = el.GetAttribute("url");
                    node.ContextMenuStrip = organizeContextMenu;
                    organizeFavTreeView.Nodes.Add(node);
                }

            }

            if (File.Exists(linksXml))
            {
                myXml.Load(linksXml);

                foreach (XmlElement el in myXml.DocumentElement.ChildNodes)
                {
                    Uri url = new Uri(el.GetAttribute("url"));
                    TreeNode node = new TreeNode(el.InnerText, tree.ImageList.Images.IndexOfKey(url.Host.ToString()), tree.ImageList.Images.IndexOfKey(url.Host.ToString()));
                    node.ToolTipText = el.GetAttribute("url");
                    node.Name = el.GetAttribute("url");
                    node.ContextMenuStrip = organizeContextMenu;
                    organizeFavTreeView.Nodes[0].Nodes.Add(node);
                }

            }


        }
        //rename method
        private void rename()
        {
            if (organizeFavTreeView.SelectedNode.Index >= 0)
            {
                String file = "";
                RenameLink rl = new RenameLink(organizeFavTreeView.SelectedNode.Text);
                TreeNode node = organizeFavTreeView.SelectedNode;

                if (rl.ShowDialog() == DialogResult.OK)
                {
                    node.Text = rl.newName.Text;

                    if (organizeFavTreeView.Nodes[0].Nodes.Contains(node))
                    {
                        if (tree.Visible == true)
                            tree.Nodes[0].Nodes[node.Name].Text = rl.newName.Text;
                        file = linksXml;
                        if (linkbar.Visible == true)
                            linkbar.Items[node.Name].Text = rl.newName.Text;
                    }
                    else
                    {
                        if (tree.Visible == true)
                            tree.Nodes[node.Name].Text = rl.newName.Text;
                        file = favXml;
                    }
                }

                XmlDocument myXml = new XmlDocument();
                myXml.Load(file);
                foreach (XmlElement x in myXml.DocumentElement.ChildNodes)
                {
                    if (x.GetAttribute("url").Equals(node.Name))
                    {
                        x.InnerText = rl.newName.Text;
                        break;
                    }
                }

                myXml.Save(file);

                rl.Close();
            }
        }
        //delete method       
        private void delete()
        {
            if (organizeFavTreeView.SelectedNode.Index >= 0)
            {
                String file = "";
                TreeNode node = organizeFavTreeView.SelectedNode;

                if (organizeFavTreeView.Nodes[0].Nodes.Contains(node))
                {
                    if (tree.Visible == true)
                        tree.Nodes[0].Nodes[node.Name].Remove();
                    file = linksXml;
                    if (linkbar.Visible == true)
                        linkbar.Items[node.Name].Dispose();
                }
                else
                {
                    if (tree.Visible == true)
                        tree.Nodes[node.Name].Remove();
                    file = favXml;
                }

                node.Remove();
                XmlDocument myXml = new XmlDocument();
                myXml.Load(file);
                XmlElement root = myXml.DocumentElement;
                foreach (XmlElement x in root.ChildNodes)
                {
                    if (x.GetAttribute("url").Equals(node.Name))
                    {
                        root.RemoveChild(x);
                        break;
                    }
                }

                myXml.Save(file);
            }
        }
        public void move()
        {
            if (organizeFavTreeView.SelectedNode.Index >= 0)
            {
                String dest = "", source = "", element = "";
                TreeNode node = organizeFavTreeView.SelectedNode;

                if (organizeFavTreeView.Nodes[0].Nodes.Contains(node))
                {
                    organizeFavTreeView.SelectedNode.Remove();
                    organizeFavTreeView.Nodes.Add(node);

                    dest = favXml;
                    source = linksXml;
                    element = "favorit";

                    if (tree.Visible == true)
                    {
                        tree.Nodes[0].Nodes.RemoveByKey(node.Name);
                        node.ContextMenuStrip = favContext;
                        tree.Nodes.Add(node);
                    }

                    if (linkbar.Visible == true)
                        linkbar.Items.RemoveByKey(node.Name);

                }
                else
                {
                    dest = linksXml;
                    source = favXml;
                    element = "link";

                    organizeFavTreeView.SelectedNode.Remove();
                    organizeFavTreeView.Nodes[0].Nodes.Add(node);
                    if (tree.Visible == true)
                    {
                        tree.Nodes.RemoveByKey(node.Name);
                        node.ContextMenuStrip = linkContext;
                        tree.Nodes[0].Nodes.Add(node);
                    }
                    //if (linkbar.Visible == true)


                }

                XmlDocument sourceXml = new XmlDocument();
                XmlDocument destXml = new XmlDocument();
                sourceXml.Load(source);
                destXml.Load(dest);
                XmlElement el = destXml.CreateElement(element);
                el.SetAttribute("url", node.ToolTipText);
                el.InnerText = node.Text;
                destXml.DocumentElement.AppendChild(el);

                XmlElement root = sourceXml.DocumentElement;

                foreach (XmlElement elem in root.ChildNodes)
                {
                    if (elem.GetAttribute("url").Equals(node.ToolTipText))
                    {
                        root.RemoveChild(elem);
                        break;
                    }
                }
                destXml.Save(dest);
                sourceXml.Save(source);
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            rename();
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rename();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            delete();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            delete();
        }

        private void organizeFavTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                organizeFavTreeView.SelectedNode = e.Node;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            move();
        }
    }
}
