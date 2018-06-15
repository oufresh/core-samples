using System;
using System.IO;
using System.Text;

namespace TreeSample.adjacentListModel
{
    public class AdjacentiListTreeConsoleRender
    {
        public static void render(AdjacentListTree tree)
        {
            int depth = 0;
            tree.ForEach((e)=>{
                if (e.IsLeaf == 0)
                    depth++;
                StringBuilder sb = new StringBuilder();
                
                int d = depth;
                while (d > 0) {
                    sb.Append(" ");
                    d--;
                }
                Console.WriteLine($"{sb.ToString()}{(e.IsLeaf == 0? "-" : " ")}: {e.Id}");
            });
        }
    }
    public class AdjacentListModelTest
    {
        public static void test()
        {
            Console.WriteLine("Reading tree from tree.json");
            AdjacentListTree tree = AdjacentiListTreeFactory.fromFile("tree.json");
            Console.WriteLine("Done");
            AdjacentiListTreeConsoleRender.render(tree);
        }
    }
}