namespace Dyskretna
{
    public class Tree
    {
        public char Node { get; set; }
        public ESymbol Type { get; set; }
        public Tree Left { get; set; }
        public Tree Right { get; set; }

        public Tree(char Node, ESymbol Type)
        {
            this.Node = Node;
            this.Type = Type;
        }
    }
}
