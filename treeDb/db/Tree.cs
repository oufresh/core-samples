using System;
using System.Collections.Generic;

namespace treeDb.models
{
    public partial class Tree
    {
        public long Id { get; set; }
        public long? Parent { get; set; }
        public string Name { get; set; }
        public long? IsLeaf { get; set; }

        public Closure Closure { get; set; }
    }
}
