using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace treeDb.adjacentiList
{
    public class Elem 
    {
        public int id;
        public string name;
        public bool isNode;
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
            //this._seqId++;
        }

        public void delete(Elem elem)
        {

        }

        protected void navigate(Elem el, Action<Elem> f)
        {
            f.Invoke(el);
            closures.ForEach((closure) => {
                if (closure.parent == el.id) {
                    var childElem = elements.Find(e => e.id == closure.child);
                    navigate(childElem, f);
                }
            });
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

    public class AdjacentiListTreeFactory
    {
        protected static void parse(JObject node, AdjacentiListTree tree, int depth = 0)
        {
            //id : 1
            //children : [1, ...] 
            Elem elem = new Elem();
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
                            int d = depth++;
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