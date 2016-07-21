using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raptor.Compile.Parse.AST
{
    public class Tree
    {
        public virtual string GetTokType() => "";
    }

    public class ExprTree : Tree
    {

    }

    public class LExprTree : ExprTree
    {

    }

    public class LiteralNode : ExprTree
    {

    }
}
