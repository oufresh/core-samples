using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TreeSample.SimpleTree
{
    public class SimpleTree
    {
        protected Dictionary<int, SimpleElem> _tree;
        SimpleTree()
        {
            this._tree = new Dictionary<int, SimpleElem>();
        }

        protected void parseNode(JObject node)
        {
            //id : 1
            //children : [1, ...] 
            SimpleElem elem = new SimpleElem();
            foreach (var jt in node) {
                //Console.WriteLine(jt.Key + ": " + jt.Value);
                if (jt.Key == "id")
                    elem.id = Int32.Parse(jt.Value.ToString());
                if (jt.Key == "children") {
                    if (jt.Value.Type == JTokenType.Array) {

                        var children = (JArray)jt.Value;

                        //mi giro per prendere tutti gli id dei children
                        foreach (var child in children) {
                            if (child.Type == JTokenType.Object)
                            {
                                JObject childNode = (JObject)child;
                                foreach (var prop in childNode) {
                                    if (prop.Key == "id")
                                        elem.children.Add(Int32.Parse(prop.Value.ToString()));
                                }
                            }
                        }

                        //giro per parsare tutti i children
                        foreach (var child in children) {
                            if (child.Type == JTokenType.Object) {
                                JObject childNode = (JObject)child;
                                parseNode(childNode);
                            }
                        }
                    }

                }
            }

            this._tree.Add(elem.id, elem);
        }

        public override string ToString()
        {
            StringBuilder strb = new StringBuilder();

            foreach (var se in _tree) {
                strb.Append(se.ToString() + Environment.NewLine);
            }

            return strb.ToString();
        }

#region render
        protected void renderNode(SimpleElem node, int depth)
        {
            StringBuilder strb = new StringBuilder();
            while (depth != 0) {
                strb.Append(" ");
                depth--;
            }
            Console.WriteLine($"{strb.ToString()}-{node.id}");
        }

        protected void renderLeaf(SimpleElem leaf, int depth)
        {
            StringBuilder strb = new StringBuilder();
            while (depth != 0) {
                strb.Append(" ");
                depth--;
            }
            Console.WriteLine($"{strb.ToString()} {leaf.id}");
        }

        protected void render(int id = 0, int depth = 0)
        {
            SimpleElem el = this._tree[id];
            if (el != null)
            {
                if (el.children.Count > 0)
                {
                    renderNode(el, depth);
                    int d = ++depth;
                    foreach (int cId in el.children) {
                        render(cId, d);
                    }
                }
                else
                    renderLeaf(el, depth);
            }
        }

        public void PrintFormattedString()
        {
            render();
        }
#endregion
        public static SimpleTree fromFile(string fileName)
        {
            var str = System.IO.File.ReadAllText(fileName);
            JObject json = JObject.Parse(str);

            var ret = new SimpleTree();
            ret.parseNode(json);
            
            return ret;
        }
    }
}
