using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace treeDb.adjacentiList
{
    public class Elem 
    {
        public int id;
        public string name;
        public bool isNode;
        public int depth;
    }

    public class Closure
    {
        public int parent;
        public int child;
        public int depth;
    }

    public class AdjacentiListTree {
        protected int _seqId = 0;
        public List<Elem> elements;
        public List<Closure> closures;

        public AdjacentiListTree()
        {
            this.closures = new List<Closure>();
            this.elements = new List<Elem>();
        }

        public void insert(Elem newElem, Elem parent)
        {
            newElem.depth = ++parent.depth;
            elements.Add(newElem);
            closures.Add(new Closure(){
                parent = parent.id,
                child = newElem.id,
                depth = newElem.depth
            });
        }

        public void delete(Elem elem)
        {
            navigateAfter(elem, child => {
                elements.Remove(child);
                var toRemove = closures.Where(cl => cl.parent == child.id).ToList();
                closures.RemoveAll(cl => toRemove.Contains(cl));
            });
        }

        public Elem getElem(int id)
        {
            return elements.Where(el => el.id == id).First();
        }

        public List<Elem> getChildren(Elem elem)
        {
            return closures.Where(closure => elem.id == closure.parent).Select(cl => {
                return elements.Find(e => e.id == cl.child);
            }).ToList();
        }

        protected void navigateAfter(Elem el, Action<Elem> f)
        {
            closures.Where(closure => closure.parent == el.id).Select(cl => {
                return elements.Find(e => e.id == cl.child);
            }).ToList().ForEach(cl => {
                navigate(cl, f);
            });

            f.Invoke(el);
        }

        protected void navigate(Elem el, Action<Elem> f)
        {
 
        }

        public void ForEach(Action<Elem> f)
        {
            var root = elements.Find(el => el.id == 0);
            if (root != null)
            {
                this.navigate(root, f);
            }
        }
    }

    public class AdjacentiListTreeConsoleRender
    {
        public static void render(AdjacentiListTree tree)
        {
            tree.ForEach((e)=>{
                StringBuilder sb = new StringBuilder();
                int depth = e.depth;
                while (depth > 0) {
                    sb.Append(" ");
                    depth--;
                }
                Console.WriteLine($"{sb.ToString()}{(e.isNode ? "-" : " ")}: {e.id}");
            });
        }
    }

    public class AdjacentiListTreeFactory
    {
        protected static void parse(JObject node, AdjacentiListTree tree, int depth = 0)
        {
            //id : 1
            //children : [1, ...] 
            Elem elem = new Elem();
            elem.depth = depth;
            foreach (var jt in node) {
                //Console.WriteLine(jt.Key + ": " + jt.Value);
                if (jt.Key == "id")
                    elem.id = Int32.Parse(jt.Value.ToString());
                if (jt.Key == "children") {
                    if (jt.Value.Type == JTokenType.Array) {
                        var children = (JArray)jt.Value;

                        if (children.Count > 0)
                        {
                            elem.isNode = true;
                            int d = ++depth;
                            //giro per parsare tutti i children
                            foreach (var child in children) {
                                if (child.Type == JTokenType.Object) {

                                    JObject childNode = (JObject)child;
                                    //closure
                                    int id = Int32.Parse(childNode["id"].ToString());
                                    tree.closures.Add(new Closure(){
                                        parent = elem.id,
                                        child = id,
                                        depth = depth
                                    });
                                    
                                    parse(childNode, tree, d);
                                }
                            }
                        }
                    }
                }
            }

            tree.elements.Add(elem);
        }
        public static AdjacentiListTree fromFile(string fileName)
        {
            var str = System.IO.File.ReadAllText(fileName);
            JObject json = JObject.Parse(str);

            var ret = new AdjacentiListTree();
            parse(json, ret);
            
            return ret;
        }
    }
        
}