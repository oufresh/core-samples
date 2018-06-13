using System;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace treeDb.db
{
    public class TreeTest
    {
        public TreeTest()
        {
            using (var ctx = new appContext())
            {
                ctx.Closure.FromSql("DELETE FROM Closure");
                ctx.Tree.FromSql("DELETE FROM Tree");
            }
        }

        public void Test()
        {
            try
            {
                
                using (var ctx = new appContext())
                {
                    var t = new Tree(){
                       Id = 0,
                       Name = "root",
                       IsLeaf = 0
                    };

                    var cl = new Closure(){
                        Child = 0,
                        Parent = 0,
                        Depth = 0,
                        ParentNavigation = t
                    };

                    t.Closure.Add(cl);


                    var c = new Tree(){
                        Id = 1,
                        Name = "a child node",
                        IsLeaf = 0
                    };

                    cl = new Closure(){
                        Child = 1,
                        Parent = 1,
                        Depth = 0,
                        ParentNavigation = c
                    };
                    c.Closure.Add(cl);

                    cl = new Closure(){
                        Child = 1,
                        Parent = 0,
                        Depth = 1,
                        ParentNavigation = t
                    };
                    c.Closure.Add(cl);

                    ctx.Tree.Add(c);

                    var f = new Tree(){
                        Id = 1,
                        Name = "a child leaf",
                        IsLeaf = 1
                    };




                    ctx.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}