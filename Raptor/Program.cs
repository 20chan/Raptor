using System;
using System.Collections.Generic;
using Raptor.Compile.Parse;

namespace Raptor
{
    static class Program
    {
        static void Main()
        {
            Tokenizer token = new Tokenizer();
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                string s = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.Gray;
                List<Token> result = token.Tokenize(s);
                foreach (Token t in result)
                {
                    Console.WriteLine("{0} : {1}", t.TokType, t.Code);
                }
            }
        }
    }
}
