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
        public List<Elem> elements;
        public List<Closure> closures;

        public AdjacentiListTree()
        {
            this.closures = new List<Closure>();
            this.elements = new List<Elem>();
        }

       
    }

    public class AdjacentiListTreeFactory
    {
        protected static void parse(JObject node, AdjacentiListTree tree)
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
                            elem.isNode = true;

                        //mi giro per prendere tutti gli id dei children
                        /*foreach (var child in children) {
                            if (child.Type == JTokenType.Object)
                            {
                                JObject childNode = (JObject)child;
                                foreach (var prop in childNode) {
                                    if (prop.Key == "id")
                                        elem.children.Add(Int32.Parse(prop.Value.ToString()));
                                }
                            }
                        }*/

                        //giro per parsare tutti i children
                        foreach (var child in children) {
                            if (child.Type == JTokenType.Object) {
                                JObject childNode = (JObject)child;
                                parse(childNode, tree);
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