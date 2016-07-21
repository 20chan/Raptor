using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raptor.Compile.Parse
{
    public struct Token
    {
        public Token(string code, TokenType type, int line)
        {
            Code = code;
            Line = line;
            TokType = type;
        }
        public string Code;
        public TokenType TokType;
        public int Line;
    }

    public enum TokenType
    {
        NONE,
        EOF, //\0
        ERROR, //알 수 없는 토큰
        STRING, //"
        INTEGER, //정수
        REAL, //실수
        IDENTIFIER, //그 외
        QUESTION, //?
        PLUS, //+
        MINUS, //-
        ASTERISK, //*
        DIVIDE, ///
        PERCENT, //%
        PLUSEQUAL, //+=
        MINUSEQUAL, //-=
        ASTERISKEQUAL, //*=
        DIVIDEEQUAL,///=
        PERCENTEQUAL, //%=
        PLUSPLUS, //++
        MINUSMINUS, //--
        HAT, //^
        HATEQUAL, //^=
        DOT, //.
        DOTTO, //..
        ASSIGN, //=
        EQUAL, //==
        GREATER, //>
        GREATEREQUAL, //>=
        LESS, //<
        LESSEQUAL, //<=
        NOTEQUAL, //!=
        COLON, //:
        SEMICOLON, //;
        COMMA, //,
        LBRACKET, //{
        RBRACKET, //}
        LPAREN, //(
        RPAREN, //)
        LSBRACKET, //[
        RSBRACKET, //]
        BAR, //|
        _AND,
        _OR,
        _NOT,
        _FUNC,
        _AS,
        _IF,
        _ELSE,
        _REPEAT,
        _FOR,
        _WHILE,
        _RETURN,
        _TRUE,
        _FALSE,
        _NULL,
        _CONTINUE,
        _BREAK,
        _YIELD,
        _THIS,
        _FROM,
        _WHERE,
        _ORDERBY,
        _SELECT,
        _COMMENTLINE,
        _COMMENTBLOCK
    }
}
