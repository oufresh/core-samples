using System;
using System.Collections.Generic;

namespace TreeSample.Closure
{
    public partial class Tree
    {
        public Tree()
        {
            Closure = new HashSet<TreeSample.Closure.Closure>();
        }

        public long Id { get; set; }
        public long? Parent { get; set; }
        public string Name { get; set; }
        public long? IsLeaf { get; set; }

        public ICollection<Closure> Closure { get; set; }
    }
}
