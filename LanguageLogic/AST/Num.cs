﻿using LanguageLogic.Tokens;

namespace LanguageLogic.AST
{
    public class Num : IExpression //Node representing number
    {
        public double Value { get; } //Value of that number
        public Token Token { get; }

        public Num(Token token)
        {
            Value = double.Parse(token.Value);
            Token = token;
        }

        public object Visit(INodeVisitor visitor)
        {
            return visitor.Visit_Num(this);
        }

    }
}
