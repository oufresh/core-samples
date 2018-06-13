using System;
using System.Collections.Generic;

namespace TreeSample.Closure
{
    public partial class Closure
    {
        public long Parent { get; set; }
        public long Child { get; set; }
        public long? Depth { get; set; }

        public Tree ParentNavigation { get; set; }
    }
}
