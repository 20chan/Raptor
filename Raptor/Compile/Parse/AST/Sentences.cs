using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raptor.Compile.Parse.AST
{
    public class SentenceTree : Tree
    {
        
    }

    public class SimpleSentenceTree : SentenceTree
    {

    }

    public class AssignTree : SimpleSentenceTree
    {
        public LExprTree Left;
        public ExprTree Right;
    }

    public class CallTree : SimpleSentenceTree
    {
        public ExprTree Left;
    }

    public class IfTree : SentenceTree
    {
        public List<ExprTree> Cond;
        public BlockTree TrueSentences, FalseSentences;
    }

    public class WhileTree : SentenceTree
    {
        public ExprTree Cond;
        public BlockTree Sentences;
    }

    public class ReturnTree : SentenceTree
    {
        public ExprTree Return;
    }

    public class DeclareVarTree : SentenceTree
    {
        public string Name, Type;
        //TODO: 구헌
    }
}
