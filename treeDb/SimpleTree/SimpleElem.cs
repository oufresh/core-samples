using System.Collections.Generic;

namespace treeDb.SimpleTree
{
    public class SimpleElem
    {
        public SimpleElem () {
            this.id = 0;
            this.children = new List<int>();
        }
        public SimpleElem(int id)
        {
            this.id = id;
            this.children = new List<int>();
        }

        public SimpleElem(int id, List<int> children)
        {
            this.id = id;
            this.children = children;
        }

        public override string ToString()
        {
            return $"{{id:{id}, children:[{string.Join(",", this.children.ToArray())}]}}";
        }

        public int id;
        public List<int> children;
    }
}
