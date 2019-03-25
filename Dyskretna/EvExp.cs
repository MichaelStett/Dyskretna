namespace Dyskretna
{
    public partial class EvExp
    {
        /// <summary>
        /// Main expression tree.
        /// </summary>
        private Tree ExpTree;

        /// <summary>
        /// Expression string.
        /// </summary>
        private string ExpString;

        /// <summary>
        /// Operators C, K, A, N, E.
        /// </summary>
        private readonly char[] Operators = new char[] { 'C', 'K', 'A', 'N', 'E' };

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="exp">expression string</param>
        private EvExp(string exp)
        {
            ExpString = exp;
        }

        /// <summary>
        /// Calculates whether the expression is tautology
        /// </summary>
        /// <param name="exp">expression string</param>
        /// <returns><see cref="bool"/></returns>
        public static bool Calculate(string exp)
        {
            var Exp = new EvExp(exp);
            Exp.ExpTree = Exp.Parse(0, out int endIndex);
            return Exp.Evaluate(Exp.ExpTree) ? true : false;
        }

        /// <summary>
        /// Returns Tree element to the <see cref="ExpTree"/>
        /// </summary>
        /// <param name="startIndex">Array index</param>
        /// <param name="endIndex">Array index</param>
        /// <returns><see cref="bool"/></returns>
        private Tree Parse(int startIndex, out int endIndex)
        {
            var t = new Tree(ExpString[startIndex], GetType(ExpString[startIndex]));
            endIndex = startIndex;
            if (t.Type == ESymbol.Operator)
            {
                if (t.Node == 'N')
                {
                    t.Left = Parse(startIndex + 1, out endIndex);
                }
                else
                {
                    t.Left = Parse(startIndex + 1, out endIndex);
                    t.Right = Parse(endIndex + 1, out endIndex);
                }
                t.Type = ESymbol.Operator;
            }
            return t;
        }

        /// <summary>
        /// Evaluate expression over <see cref="ExpTree"/>
        /// </summary>
        /// <param name="t">Tree object</param>
        /// <returns><see cref="bool"/></returns>
        private bool Evaluate(Tree t)
        {
            switch (t.Type)
            {
                case ESymbol.Variable:
                    return (0 & (1 << (t.Node - 'a'))) != 0;
                default:
                    switch (t.Node)
                    {
                        case 'C':
                            if(Evaluate(t.Left))
                            {
                                return Evaluate(t.Right);
                            }
                            return true;
                        case 'K':
                            if (Evaluate(t.Left))
                            {
                                return Evaluate(t.Right);
                            }
                            else
                            {
                                return false;
                            }                            
                        case 'A':
                            if (!Evaluate(t.Left))
                            {
                                return Evaluate(t.Right);
                            }
                            else
                            {
                                return true;
                            }
                        case 'N':
                            return !Evaluate(t.Left);
                        case 'E':
                            return Evaluate(t.Left) == Evaluate(t.Right);
                    }
                    return true;
            }
        }

        /// <summary>
        /// Get type of Tree node
        /// </summary>
        /// <param name="ch">character to compare</param>
        /// <returns><see cref="ESymbol"/></returns>
        private ESymbol GetType(char ch)
        {
            foreach (var op in Operators)
                if (op == ch) return ESymbol.Operator;
            return ESymbol.Variable;
        }
    }
}