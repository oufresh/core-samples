using System;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using treeDb.adjacentiList;

namespace treeDb
{
    class Program
    {
        public static IConfiguration Configuration { get; set; }

        static void Main(string[] args)
        {
            var exit = false;
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();

            Console.WriteLine("Tree test program");
            //Console.WriteLine($"suboption1 = {Configuration["subsection:suboption1"]}");
            //Console.WriteLine($"DefaultConnection = {Configuration["ConnectionStrings:DefaultConnection"]}");

            while (!exit)
            {
                Console.WriteLine("Choose a test");
                Console.WriteLine("1 - BasicTree");
                Console.WriteLine("2 - SimpleTree");
                Console.WriteLine("3 - AdjacentiListTree");
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
                        BasicTree bst = new BasicTree();
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
                    default:
                    break;
                }
                
                Console.WriteLine("");
            }

            /*using (var db = new TreeDbContext())
            {
                db.tree.RemoveRange(db.tree);
                db.closure.RemoveRange(db.closure);

                db.Add(new Tree {
                    Id = 1,
                    Parent = 0,
                    Name = "root",
                    IsLeaf = 0
                });

                db.SaveChanges();
            }*/
        }
    }
}
