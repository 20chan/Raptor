using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raptor.Compile.Parse.TestAST
{
    /*
     * <expr> ::= <expr> + <expr> | <expr> - <expr> | <expr> * <expr> | <expr> / <expr> | <expr> % <expr> | (<expr>) | <literal>
     * <literal> ::= <real>
     */
    public class Tree
    {
        public virtual string GetCode() { return ""; }
    }

    public class ExprTree : Tree
    {
        public virtual double GetValue() { return 0; }
    }

    public class PlusExprTree : ExprTree
    {
        public ExprTree Left, Right;
        public override double GetValue()
        {
            return Left.GetValue() + Right.GetValue();
        }
        public override string GetCode()
        {
            return Left.GetCode() + "+" + Right.GetCode();
        }
    }

    public class MinusExprTree : ExprTree
    {
        public ExprTree Left, Right;
        public override double GetValue()
        {
            return Left.GetValue() - Right.GetValue();
        }
        public override string GetCode()
        {
            return Left.GetCode() + "-" + Right.GetCode();
        }
    }

    public class MultiExprTree : ExprTree
    {
        public ExprTree Left, Right;
        public override double GetValue()
        {
            return Left.GetValue() * Right.GetValue();
        }
        public override string GetCode()
        {
            return Left.GetCode() + "*" + Right.GetCode();
        }
    }

    public class DivideExprTree : ExprTree
    {
        public ExprTree Left, Right;
        public override double GetValue()
        {
            return Left.GetValue() / Right.GetValue();
        }
        public override string GetCode()
        {
            return Left.GetCode() + "/" + Right.GetCode();
        }
    }

    public class ParenTree : ExprTree
    {
        public ExprTree Inner;
        public override string GetCode()
        {
            return "(" + Inner.GetCode() + ")";
        }
    }

    public class LiteralTree : ExprTree
    {

    }

    public class RealTree : LiteralTree
    {
        public double Value;
        public override string GetCode()
        {
            return Value.ToString();
        }
        public override double GetValue()
        {
            return Value;
        }
    }
}
