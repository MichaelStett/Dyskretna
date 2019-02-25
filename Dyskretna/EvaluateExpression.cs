namespace Dyskretna
{
    public class EvaluateExpression
    {
        public enum ESymbol
        {
            Operator,
            Zmienna
        }

        public class Tree
        {
            public char node;
            public ESymbol type;
            public Tree left, right;
        }

        public int substituteCount = 0;
        private Tree ExpTree;
        private readonly string Expression;
        private readonly char[] Operatory = new char[] {
            'C', // Implikacja
            'K', // Koniunkcja
            'A', // Alternatywa
            'N'  // Negacja
        };

        private EvaluateExpression(string row) => Expression = row;

        public static bool Calculate(string row)
        {
            var ex = new EvaluateExpression(row);
            ex.ExpTree = ex.ParseExpression(0, out int endIndex);
            return ex.VerifyAllCombinations();
        }

        private bool VerifyAllCombinations()
        {
            long limit = 1 << substituteCount;
            for (int i = 0; i < limit; i++)
            {
                if (Evaluate(i, ExpTree))
                {
                    continue;
                }
                return false;
            }
            return true;
        }

        private bool Evaluate(int i, Tree t)
        {
            switch (t.type)
            {
                case ESymbol.Zmienna:
                    return (i & (1 << (t.node - 'a'))) != 0;
                default:
                    switch (t.node)
                    {
                        case 'N':
                            return !Evaluate(i, t.left);
                        case 'A':
                            return !Evaluate(i, t.left) ? Evaluate(i, t.right) : true;
                        case 'K':
                            return Evaluate(i, t.left) ? Evaluate(i, t.right) : false;                        
                        case 'C':
                            return Evaluate(i, t.left) ? Evaluate(i, t.right) : true;
                    }
                    return true;
            }
        }

        private Tree ParseExpression(int startIndex, out int endIndex)
        {
            Tree t = new Tree
            {
                node = Expression[startIndex],
                type = ESymbol.Zmienna
            };

            endIndex = startIndex;
            var type  = GetSymbolType(Expression[startIndex]);
            if (type == ESymbol.Operator)
            {
                switch (Expression[startIndex])
                {
                    case 'N':
                        t.left = ParseExpression(startIndex + 1, out endIndex);
                        break;
                    default:
                        t.left = ParseExpression(startIndex + 1, out endIndex);
                        t.right = ParseExpression(endIndex + 1, out endIndex);
                        break;
                }
                t.type = ESymbol.Operator;
            }
            return t;
        }

        private ESymbol GetSymbolType(char ch)
        {
            foreach (var op in this.Operatory)
            {
                if (op == ch)
                {
                    return ESymbol.Operator;
                }
            }
            return ESymbol.Zmienna;
        }
    }
}
