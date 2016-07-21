using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raptor.Compile.Parse
{
    public class Tokenizer
    {
        public bool IsSplitCharacter(char c)
        {
            switch (c)
            {
                case ' ':
                case '\t':
                case '\n':
                case '=':
                case '+':
                case '-':
                case '*':
                case '/':
                case '%':
                case '^':
                case '?':
                case '.':
                case '\"':
                case '\'':
                case '(':
                case ')':
                case '[':
                case ']':
                case '{':
                case '}':
                case ':':
                case ';':
                case ',':
                case '|':
                case '!':
                    return true;
            }
            return false;
        }

        public char DecodeEscape(char c, out bool err)
        {
            err = false;
            switch (c)
            {
                case '\\':
                    return '\\';
                case 'n':
                    return '\n';
                case 't':
                    return '\t';
                case 'r':
                    return '\r';
                case '\'':
                    return '\'';
                case '"':
                    return '\"';
                case '0':
                    return '\0';
            }
            err = true;
            return '\\';
        }

        public List<Token> Tokenize(string code)
        {
            List<Token> result = new List<Token>();

            TokenType stage = TokenType.NONE;
            int line = 1;
            int marker = 0;
            StringBuilder s = new StringBuilder();

            for (int it = 0; it < code.Length; it++)
            {
                switch (stage)
                {
                    case TokenType.NONE:
                        {
                            switch (code[it])
                            {
                                case '\n':
                                    line++;
                                    break;
                                case ' ':
                                case '\t':
                                case '\r':
                                    break;

                                case '\"':
                                    stage = TokenType.STRING;
                                    s = new StringBuilder();
                                    break;

                                case '0':
                                case '1':
                                case '2':
                                case '3':
                                case '4':
                                case '5':
                                case '6':
                                case '7':
                                case '8':
                                case '9':
                                    stage = TokenType.INTEGER;
                                    marker = it;
                                    
                                    if (it == code.Length - 1)
                                    {
                                        stage = TokenType.NONE;
                                        result.Add(new Token(code[it].ToString(), TokenType.INTEGER, line));
                                    }
                                    break;
                                case '<':
                                    if (it != code.Length - 1 && code[it + 1] == '=')
                                    {
                                        result.Add(new Token("<=", TokenType.LESSEQUAL, line));
                                        it++;
                                    }
                                    else
                                        result.Add(new Token("<", TokenType.LESS, line));
                                    break;
                                case '>':
                                    if (it != code.Length - 1 && code[it + 1] == '=')
                                    {
                                        result.Add(new Token(">=", TokenType.GREATEREQUAL, line));
                                        it++;
                                    }
                                    else
                                        result.Add(new Token(">", TokenType.GREATER, line));
                                    break;
                                case '!':
                                    if (it != code.Length - 1 && code[it + 1] == '=')
                                    {
                                        result.Add(new Token("!=", TokenType.NOTEQUAL, line));
                                        it++;
                                    }
                                    else
                                        result.Add(new Token("!", TokenType.ERROR, line));
                                    break;
                                case '=':
                                    if (it != code.Length - 1 && code[it + 1] == '=')
                                    {
                                        result.Add(new Token("==", TokenType.EQUAL, line));
                                        it++;
                                    }
                                    else
                                        result.Add(new Token("=", TokenType.ASSIGN, line));
                                    break;
                                case '/':
                                    if (it != code.Length - 1 && code[it + 1] == '/')
                                    {
                                        stage = TokenType._COMMENTLINE;
                                        it++;
                                    }
                                    else if (it != code.Length - 1 && code[it + 1] == '*')
                                    {
                                        stage = TokenType._COMMENTBLOCK;
                                        it++;
                                    }
                                    else
                                    {
                                        if (it != code.Length - 1 && code[it + 1] == '=')
                                        {
                                            result.Add(new Token("/=", TokenType.DIVIDEEQUAL, line));
                                            it++;
                                        }
                                        else
                                            result.Add(new Token("/", TokenType.DIVIDE, line));
                                    }
                                    break;
                                case '+':
                                    if (it != code.Length - 1 && code[it + 1] == '=')
                                    {
                                        result.Add(new Token("+=", TokenType.PLUSEQUAL, line));
                                        it++;
                                    }
                                    else if(it != code.Length - 1 && code[it + 1] == '+')
                                    {
                                        result.Add(new Token("++", TokenType.PLUSPLUS, line));
                                        it++;
                                    }
                                    else
                                        result.Add(new Token("+", TokenType.PLUS, line));
                                    break;
                                case '-':
                                    if (it != code.Length - 1 && code[it + 1] == '=')
                                    {
                                        result.Add(new Token("-=", TokenType.MINUSEQUAL, line));
                                        it++;
                                    }
                                    else if (it != code.Length - 1 && code[it + 1] == '+')
                                    {
                                        result.Add(new Token("--", TokenType.MINUSMINUS, line));
                                        it++;
                                    }
                                    else
                                        result.Add(new Token("-", TokenType.MINUS, line));
                                    break;
                                case '*':
                                    if (it != code.Length - 1 && code[it + 1] == '=')
                                    {
                                        result.Add(new Token("*=", TokenType.ASTERISKEQUAL, line));
                                        it++;
                                    }
                                    else
                                        result.Add(new Token("*", TokenType.ASTERISK, line));
                                    break;
                                case '%':
                                    if (it != code.Length - 1 && code[it + 1] == '=')
                                    {
                                        result.Add(new Token("%=", TokenType.PERCENTEQUAL, line));
                                        it++;
                                    }
                                    else
                                        result.Add(new Token("%", TokenType.PERCENT, line));
                                    break;
                                case '^':
                                    if (it != code.Length - 1 && code[it + 1] == '=')
                                    {
                                        result.Add(new Token("^=", TokenType.HATEQUAL, line));
                                        it++;
                                    }
                                    else
                                        result.Add(new Token("%", TokenType.HAT, line));
                                    break;
                                case '.':
                                    if (it != code.Length - 1 && code[it + 1] == '.')
                                    {
                                        result.Add(new Token("..", TokenType.DOTTO, line));
                                        it++;
                                    }
                                    else
                                        result.Add(new Token(".", TokenType.DOT, line));
                                    break;
                                case '?':
                                    result.Add(new Token("?", TokenType.QUESTION, line));
                                    break;
                                case ':':
                                    result.Add(new Token(":", TokenType.COLON, line));
                                    break;
                                case ';':
                                    result.Add(new Token(";", TokenType.SEMICOLON, line));
                                    break;
                                case ',':
                                    result.Add(new Token(",", TokenType.COMMA, line));
                                    break;
                                case '[':
                                    result.Add(new Token("[", TokenType.LSBRACKET, line));
                                    break;
                                case ']':
                                    result.Add(new Token("]", TokenType.RSBRACKET, line));
                                    break;
                                case '{':
                                    result.Add(new Token("{", TokenType.LBRACKET, line));
                                    break;
                                case '}':
                                    result.Add(new Token("}", TokenType.RBRACKET, line));
                                    break;
                                case '(':
                                    result.Add(new Token("(", TokenType.LPAREN, line));
                                    break;
                                case ')':
                                    result.Add(new Token(")", TokenType.RPAREN, line));
                                    break;
                                case '|':
                                    result.Add(new Token("|", TokenType.BAR, line));
                                    break;
                                default:
                                    stage = TokenType.IDENTIFIER;
                                    marker = it;
                                    break;
                            }
                            break;
                        }
                    case TokenType.STRING:
                        {
                            for (; code[it] != '\"'; it++)
                            {
                                if (it == code.Length - 1 || code[it] == '\n' || code[it] == '\r')
                                {
                                    stage = TokenType.NONE;
                                    result.Add(new Token(s.ToString(), TokenType.ERROR, line));
                                    break;
                                }
                                else if (code[it] == '\\')
                                {
                                    bool err = false;
                                    s.Append(DecodeEscape(code[it + 1], out err));
                                    if (err)
                                    {
                                        stage = TokenType.NONE;
                                        result.Add(new Token(s.ToString(), TokenType.ERROR, line));
                                    }
                                }
                                else
                                    s.Append(code[it]);
                            }
                            stage = TokenType.NONE;
                            result.Add(new Token(s.ToString(), TokenType.STRING, line));
                            break;
                        }
                    case TokenType.INTEGER:
                        {
                            if (code[it] == '.')
                            {
                                if (it != code.Length - 1 && code[it + 1] == '.')
                                {
                                    stage = TokenType.NONE;
                                    result.Add(new Token(subString(code, marker, it--), TokenType.INTEGER, line));
                                }
                                else
                                    stage = TokenType.REAL;
                            }
                            else if (IsSplitCharacter(code[it]))
                            {
                                stage = TokenType.NONE;
                                result.Add(new Token(subString(code, marker, it--), TokenType.INTEGER, line));
                            }
                            else if (!char.IsDigit(code, it))
                            {
                                stage = TokenType.NONE;
                                result.Add(new Token(subString(code, marker, it--), TokenType.ERROR, line));
                            }
                            break;
                        }
                    case TokenType.REAL:
                        {
                            if (IsSplitCharacter(code[it]))
                            {
                                stage = TokenType.NONE;
                                result.Add(new Token(subString(code, marker, it--), TokenType.REAL, line));
                            }
                            else if (!char.IsDigit(code[it]))
                            {
                                stage = TokenType.NONE;
                                result.Add(new Token(subString(code, marker, it--), TokenType.ERROR, line));
                            }
                            else if(it == code.Length - 1)
                            {
                                stage = TokenType.NONE;
                                result.Add(new Token(subString(code, marker, it+1), TokenType.REAL, line));
                            }
                            break;
                        }
                    case TokenType.IDENTIFIER:
                        {
                            if (IsSplitCharacter(code[it]))
                            {
                                stage = TokenType.NONE;
                                string cur = subString(code, marker, it--);
                                switch (cur)
                                {
                                    case "and":
                                        result.Add(new Token(cur, TokenType._AND, line));
                                        break;
                                    case "or":
                                        result.Add(new Token(cur, TokenType._OR, line));
                                        break;
                                    case "not":
                                        result.Add(new Token(cur, TokenType._NOT, line));
                                        break;
                                    case "func":
                                        result.Add(new Token(cur, TokenType._FUNC, line));
                                        break;
                                    case "as":
                                        result.Add(new Token(cur, TokenType._AS, line));
                                        break;
                                    case "if":
                                        result.Add(new Token(cur, TokenType._IF, line));
                                        break;
                                    case "else":
                                        result.Add(new Token(cur, TokenType._ELSE, line));
                                        break;
                                    case "repeat":
                                        result.Add(new Token(cur, TokenType._REPEAT, line));
                                        break;
                                    case "for":
                                        result.Add(new Token(cur, TokenType._FOR, line));
                                        break;
                                    case "while":
                                        result.Add(new Token(cur, TokenType._WHILE, line));
                                        break;
                                    case "return":
                                        result.Add(new Token(cur, TokenType._RETURN, line));
                                        break;
                                    case "true":
                                        result.Add(new Token(cur, TokenType._TRUE, line));
                                        break;
                                    case "false":
                                        result.Add(new Token(cur, TokenType._FALSE, line));
                                        break;
                                    case "null":
                                        result.Add(new Token(cur, TokenType._NULL, line));
                                        break;
                                    case "continue":
                                        result.Add(new Token(cur, TokenType._CONTINUE, line));
                                        break;
                                    case "break":
                                        result.Add(new Token(cur, TokenType._BREAK, line));
                                        break;
                                    case "yield":
                                        result.Add(new Token(cur, TokenType._YIELD, line));
                                        break;
                                    case "this":
                                        result.Add(new Token(cur, TokenType._THIS, line));
                                        break;
                                    case "from":
                                        result.Add(new Token(cur, TokenType._FROM, line));
                                        break;
                                    case "where":
                                        result.Add(new Token(cur, TokenType._WHERE, line));
                                        break;
                                    case "orderby":
                                        result.Add(new Token(cur, TokenType._ORDERBY, line));
                                        break;
                                    case "select":
                                        result.Add(new Token(cur, TokenType._SELECT, line));
                                        break;
                                    default:
                                        result.Add(new Token(cur, TokenType.IDENTIFIER, line));
                                        break;
                                }
                            }
                            break;
                        }
                    case TokenType._COMMENTLINE:
                        {
                            if (code[it] == '\n')
                            {
                                line++;
                                stage = TokenType.NONE;
                            }
                            break;
                        }
                    case TokenType._COMMENTBLOCK:
                        {
                            switch (code[it])
                            {
                                case '\n':
                                    line++;
                                    break;
                                case '*':
                                    if (it != code.Length - 1 && code[it + 1] == '/')
                                    {
                                        it++;
                                        stage = TokenType.NONE;
                                    }
                                    break;
                            }
                            break;
                        }
                }
            }
            result.Add(new Token("", TokenType.EOF, line));
            return result;
        }

        private string subString(string str, int mark, int cur)
        {
            return str.Substring(mark, cur - mark);
        }
    }
}