using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace treeDb
{
    class Elem 
    {
        public Elem(int id)
        {
            this.id = id;
            this.children = new List<int>();
        }

        public Elem(int id, List<int> children)
        {
            this.id = id;
            this.children = children;
        }

        public int id;
        public List<int> children;
    }

    class BasicTree
    {
        protected void renderNode(Elem node, int depth)
        {
            StringBuilder strb = new StringBuilder();
            //Console.WriteLine($"depth {depth}-{node.id}");
            while (depth != 0) {
                strb.Append(" ");
                depth--;
            }
            Console.WriteLine($"{strb.ToString()}-{node.id}");
        }

        protected void renderLeaf(Elem node, int depth)
        {
            StringBuilder strb = new StringBuilder();
            //Console.WriteLine($"depth {depth}-{node.id}");
            while (depth != 0) {
                strb.Append(" ");
                depth--;
            }
            Console.WriteLine($"{strb.ToString()} {node.id}");
        }

        protected void render(Dictionary<int, Elem> myTree, Elem currentElem = null, int depth = 0)
        {
            if (currentElem == null)
            {
                Elem root = myTree[0];
                if (root != null) {
                    renderNode(root, depth);
                    int d = ++depth;
                    foreach (int eldId in root.children) {
                        var el = myTree[eldId];
                        render(myTree, el, d);
                    }
                }
            }
            else
            {
                if (currentElem.children.Count > 0) {
                    renderNode(currentElem, depth);
                    int d = ++depth;
                    foreach (int eldId in currentElem.children) {
                        var el = myTree[eldId];
                        render(myTree, el, d);
                    }
                }
                else {
                    renderLeaf(currentElem, depth);
                }
            }
        }
        public void Test()
        {
            Dictionary<int, Elem> myTree = new Dictionary<int, Elem>();

            var root = new Elem(0);
            root.children.Add(1);
            root.children.Add(2);
            myTree.Add(root.id, root);

            var el1 = new Elem(1);
            el1.children.Add(37);
            myTree.Add(el1.id, el1);

            var el37 = new Elem(37);
            myTree.Add(el37.id, el37);

            var el2 = new Elem(2);
            var el21 = new Elem(21);
            var el22 = new Elem(22);
            var el23 = new Elem(23);

            el2.children.Add(21);
            el2.children.Add(22);
            el2.children.Add(23);

            myTree.Add(el2.id, el2);
            myTree.Add(el21.id, el21);
            myTree.Add(el22.id, el22);
            myTree.Add(el23.id, el23);

            render(myTree);
        }
    }
}
