namespace Dyskretna
{
    public class EvaluateExp
    {
        private Tree ExpTree;
        private string ExpString;
        private readonly char[] Operators = new char[] {
            'C', // Implikacja
            'K', // Koniunkcja
            'A', // Alternatywa
            'N'  // Negacja
        };

        public static bool Calculate(string exp)
        {
            var Exp = new EvaluateExp
            {
                ExpString = exp
            };
            Exp.ExpTree = Exp.Parse(0, out int endIndex);
            return Exp.Evaluate(0, Exp.ExpTree) ? true : false;
        }

        private bool Evaluate(int i, Tree t)
        {
            switch (t.type)
            {
                case ESymbol.Variable:
                    return (i & (1 << (t.node - 'a'))) != 0;
                default:
                    {
                        switch (t.node)
                        {
                            case 'N':
                                return !Evaluate(i, t.left);
                            case 'K':
                                return Evaluate(i, t.left) ? Evaluate(i, t.right) : false;
                            case 'A':
                                return !Evaluate(i, t.left) ? Evaluate(i, t.right) : true;
                            case 'C':
                                {
                                    return Evaluate(i, t.left) ? Evaluate(i, t.right) : true;
                                }
                        }
                        return true;
                    }
            }
        }

        private Tree Parse(int startIndex, out int endIndex)
        {
            var t = new Tree
            {
                node = ExpString[startIndex],
                type = GetType(ExpString[startIndex])
            };

            endIndex = startIndex;
            if (t.type == ESymbol.Operator)
            {
                switch (t.node)
                {
                    case 'N':
                        t.left = Parse(startIndex + 1, out endIndex);
                        break;
                    default:
                        t.left = Parse(startIndex + 1, out endIndex);
                        t.right = Parse(endIndex + 1, out endIndex);
                        break;
                }
                t.type = ESymbol.Operator;
            }
            return t;
        }

        private ESymbol GetType(char ch)
        {
            foreach (var op in Operators)
            {
                if (op == ch)
                {
                    return ESymbol.Operator;
                }
            }
            return ESymbol.Variable;
        }
    }
}
