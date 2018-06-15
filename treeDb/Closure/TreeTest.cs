using System;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace TreeSample.Closure
{
    public class TreeTest
    {
        public TreeTest()
        {
            using (var ctx = new appContext())
            {
                ctx.Closure.RemoveRange(ctx.Closure);
                //ctx.Closure.FromSql("DELETE FROM Closure");
                ctx.Tree.RemoveRange(ctx.Tree);
                //ctx.Tree.FromSql("DELETE FROM Tree");
                ctx.SaveChanges();
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
                    ctx.Tree.Add(t);


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
                    ctx.Closure.Add(cl);

                    var cl1 = new Closure(){
                        Child = 1,
                        Parent = 0,
                        Depth = 1,
                        ParentNavigation = t
                    };
                    ctx.Closure.Add(cl1);

                    ctx.Tree.Add(c);


/*
                    var f = new Tree(){
                        Id = 10,
                        Name = "a child leaf",
                        IsLeaf = 1
                    };

                    cl = new Closure(){
                        Child = 10,
                        Parent = 1,
                        Depth = 1
                    };
                    f.Closure.Add(cl);
                    ctx.Tree.Add(f);

                    f = new Tree(){
                        Id = 11,
                        Name = "another child leaf",
                        IsLeaf = 1
                    };

                    cl = new Closure(){
                        Child = 11,
                        Parent = 1,
                        Depth = 1
                    };
                    f.Closure.Add(cl);
                    ctx.Tree.Add(f);

                    f = new Tree(){
                        Id = 12,
                        Name = "yet another child leaf",
                        IsLeaf = 1
                    };

                    cl = new Closure(){
                        Child = 12,
                        Parent = 1,
                        Depth = 1
                    };
                    f.Closure.Add(cl);
                    ctx.Tree.Add(f);*/

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