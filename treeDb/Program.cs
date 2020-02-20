using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using TreeSample.adjacentiList;

namespace TreeSample
{
    class Program
    {
        public static IConfiguration Configuration { get; set; }

        static void Main(string[] args)
        {
            var exit = false;
            
            var builder = new ConfigurationBuilder();

            Console.WriteLine("Tree test program");
            //Console.WriteLine($"suboption1 = {Configuration["subsection:suboption1"]}");
            //Console.WriteLine($"DefaultConnection = {Configuration["ConnectionStrings:DefaultConnection"]}");

            while (!exit)
            {
                Console.WriteLine("Choose a test");
                Console.WriteLine("1 - BasicTree");
                Console.WriteLine("2 - SimpleTree");
                Console.WriteLine("3 - AdjacentiListTree");
                Console.WriteLine("4 - AdjacentiListModel db tree");
                Console.WriteLine("5 - Closure db tree");
                Console.WriteLine("q - Quit");
                Console.WriteLine("");

                ConsoleKeyInfo k = Console.ReadKey(true);

                switch (k.KeyChar)
                {
                    case 'q':
                        exit = true;
                        break;
                    case '1':
                        Console.WriteLine("Starting test BasicTree");
                        BasicTree.BasicTree bst = new BasicTree.BasicTree();
                        bst.Test();
                        break;
                    case '2':
                        Console.WriteLine("Starting test SimpleTree");
                        SimpleTree.SimpleTree simpleTree = SimpleTree.SimpleTree.fromFile("tree.json");
                        simpleTree.PrintFormattedString();
                        break;
                    case '3':
                        Console.WriteLine("Starting test AdjacentiListTree");
                        var test = new AjacentListTest("tree.json");
                        test.testRender();
                        break;
                    case '4':
                        Console.WriteLine("Starting test AdjacentiListModel db tree");
                        TreeSample.adjacentListModel.AdjacentListModelTest.test();
                        break;
                    case '5':
                        Console.WriteLine("Starting test Closure db tree");
                        var tt = new TreeSample.Closure.TreeTest();
                        tt.Test();
                        break;
                    default:
                    break;
                }
                
                Console.WriteLine("");
            }
        }
    }
}
