using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace TreeSample.adjacentListModel
{
    public class AdjacentListTree
    {
        public AdjacentListTree()
        {

        }

        public void insert(Tree element)
        {
            try 
            {
                using (var ctx = new adjacentListContext())
                {
                    ctx.Tree.Add(element);
                    ctx.SaveChanges();
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }

        protected void navigate(adjacentListContext ctx, Tree el, Action<Tree> f)
        {
            f.Invoke(el);
            ctx.Tree.Where(t => t.Parent == el.Id).ToList().ForEach(cl => {
                this.navigate(ctx, cl, f);
            });
        }

        public void ForEach(Action<Tree> f)
        {
            try
            {
                using (var ctx = new adjacentListContext())
                {
                    var tr = ctx.Tree.Find(0L);
                    if (tr != null)
                        this.navigate(ctx, tr, f);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public int getDepth(long Id)
        {
            int depth = 0;

            using (var ctx = new adjacentListContext())
            {
                long parentId = 0;
                long idtoSearch = Id;

                //meglio usare la reduce!!!

                /*while (true) {
                    var t = ctx.Tree.Where(e => e.Id == Id).Select()
                    if (t.Parent!= null)
                }*/
            }

            return depth;
        }
    }

    public class AdjacentiListTreeFactory
    {
        protected static void parse(JObject node, AdjacentListTree tree, long parentId = -1)
        {
            Tree elem = new Tree();
            foreach (var jt in node) {
                if (jt.Key == "id")
                    elem.Id = Int32.Parse(jt.Value.ToString());
                if (parentId > -1)
                    elem.Parent = parentId;

                if (jt.Key == "children") {
                    if (jt.Value.Type == JTokenType.Array) {
                        var children = (JArray)jt.Value;
                        if (children.Count > 0) {
                            elem.IsLeaf = 0;
                            //giro per parsare tutti i children
                            foreach (var child in children) {
                                if (child.Type == JTokenType.Object) {
                                    JObject childNode = (JObject)child;
                                    parse(childNode, tree, elem.Id);
                                }
                            }
                        }
                        else {
                            elem.IsLeaf = 1;
                        }
                    }
                }
                else
                    elem.IsLeaf = 1;
            }

            tree.insert(elem);
        }
        public static AdjacentListTree fromFile(string fileName)
        {
            var str = System.IO.File.ReadAllText(fileName);
            JObject json = JObject.Parse(str);

            var ret = new AdjacentListTree();
            parse(json, ret);
            
            return ret;
        }
    }
}