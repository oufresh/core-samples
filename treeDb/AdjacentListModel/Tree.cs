using System;
using System.Collections.Generic;

namespace TreeSample.adjacentListModel
{
    public partial class Tree
    {
        public long Id { get; set; }
        public long? Parent { get; set; }
        public string Name { get; set; }
        public long? IsLeaf { get; set; }
    }
}
