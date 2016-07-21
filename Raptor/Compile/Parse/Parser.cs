using System.Collections.Generic;
using Raptor.Compile.Parse.TestAST;

namespace Raptor.Compile.Parse
{
    public class Parser
    {
        public List<Token> ParseCalc(string code)
        {
            List<Token> result = new List<Token>();
            Tokenizer token = new Tokenizer();
            List<Token> tokens = token.Tokenize(code);
            
            Stack<Token> stack = new Stack<Token>();

            for(int it = 0; it < tokens.Count; it++)
            {
                switch(tokens[it].TokType)
                {
                    case TokenType.INTEGER:
                    case TokenType.REAL:
                        result.Add(tokens[it]);
                        break;
                    case TokenType.LPAREN:
                        stack.Push(tokens[it]);
                        break;
                    case TokenType.RPAREN:
                        Token tok;
                        do
                        {
                            tok = stack.Pop();
                            result.Add(tok);
                        }
                        while (tok.TokType != TokenType.LPAREN);
                        break;

                    case TokenType.PLUS:

                        break;
                    case TokenType.MINUS:
                        break;
                }
            }

            return result;
        }
    }
}
