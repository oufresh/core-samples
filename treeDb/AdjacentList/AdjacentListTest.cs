using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace treeDb.adjacentiList
{
    public class AjacentListTest
    {
        protected AdjacentiListTree tree;
        public AjacentListTest(string nameFile)
        {
            tree = AdjacentiListTreeFactory.fromFile(nameFile);
        }

        public void testRender()
        {
            Console.WriteLine("");
            Console.WriteLine("Test rendering tree");
            AdjacentiListTreeConsoleRender.render(tree);
            Console.WriteLine("");

            Console.WriteLine("");
            Console.WriteLine("Search child with id 1");
            Elem e = tree.getElem(1);
            if (e != null) {
                Console.WriteLine("Print children");
                Console.WriteLine("Elem " + e.id);
                List<adjacentiList.Elem> cs = tree.getChildren(e);
                cs.ForEach(child => {
                    Console.WriteLine($"Child Elem {(child.isNode ? "-" : " ")} {child.id}");
                });
            }
            Console.WriteLine("");

            Console.WriteLine("");
            Console.WriteLine("Inser a child of 1");
            
            Elem newElem = new Elem();
            newElem.id = 111;
            newElem.isNode = false;
            newElem.name = "Ciccio";

            tree.insert(newElem, e);
            Console.WriteLine("Print again children of 1");
            Console.WriteLine("Elem " + e.id);
            List<adjacentiList.Elem> children = tree.getChildren(e);
            children.ForEach(child => {
                Console.WriteLine($"Child Elem {(child.isNode ? "-" : " ")} {child.id}");
            });
            Console.WriteLine("");

            Console.WriteLine("Inser a new node 4 with children ... 401, 402, 403");

            Elem node = new Elem(){
                id = 4,
                name = "prova",
                isNode = true
            };

            tree.insert(node, e);

            for (int i = 0; i < 4; i++) {
                Elem el = new Elem(){
                    id = (400+i),
                    name = "prova 4",
                    isNode = false
                };

                tree.insert(el, node);
            }

            Console.WriteLine("Print again children of 1");
            Console.WriteLine("Elem " + e.id);
            children = tree.getChildren(e);
            children.ForEach(child => {
                Console.WriteLine($"Child Elem {(child.isNode ? "-" : " ")} {child.id}");
            });
            Console.WriteLine("");
            Elem t = tree.getElem(4);
            Console.WriteLine("Print children of 4");
            Console.WriteLine("Elem " + t.id);
            children = tree.getChildren(t);
            children.ForEach(child => {
                Console.WriteLine($"Child Elem {(child.isNode ? "-" : " ")} {child.id}");
            });

            Console.WriteLine("");
            AdjacentiListTreeConsoleRender.render(tree);
            Console.WriteLine("");
            Console.WriteLine("Delete 401 child of 4");
            Elem d = tree.getElem(401);
            tree.delete(d);
            Console.WriteLine("");
            AdjacentiListTreeConsoleRender.render(tree);
            Console.WriteLine("");

            Console.WriteLine("Delete node 4 with its children");
            Elem dn = tree.getElem(4);
            tree.delete(dn);
            Console.WriteLine("");
            AdjacentiListTreeConsoleRender.render(tree);
            Console.WriteLine("");
        }
    }
}