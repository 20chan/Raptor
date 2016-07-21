using System;
using System.IO;
using System.CodeDom.Compiler;

namespace Raptor.Compile
{
    class Compiler
    {
        public Compiler()
        {

        }

        public void Compile()
        {
            CodeDomProvider compiler = CodeDomProvider.CreateProvider("c#");
        }
    }
}
