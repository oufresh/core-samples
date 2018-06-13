using System;
using System.Collections.Generic;

namespace treeDb.db
{
    public partial class Tree
    {
        public Tree()
        {
            Closure = new HashSet<treeDb.db.Closure>();
        }

        public long Id { get; set; }
        public long? Parent { get; set; }
        public string Name { get; set; }
        public long? IsLeaf { get; set; }

        public ICollection<Closure> Closure { get; set; }
    }
}
