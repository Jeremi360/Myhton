using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace Mython.CodeAnalysis
{
  public class Evaluator
  {
    private readonly ExpressionSyntax _root;

    public Evaluator(ExpressionSyntax root)
    {
      _root = root;
    }

    public int Evaluate()
    {
      return EvaluateExpression(_root);
    }

    private int EvaluateExpression(ExpressionSyntax node)
    {
      if (node is NumberExpressionSyntax n)
      {
        return (int)n.NumberToken.Value;
      }

      if (node is BinaryExpressionSyntax b)
      {
        var left = EvaluateExpression(b.Left);
        var right = EvaluateExpression(b.Right);

        switch (b.OperatorToken.Kind)
        {
          case SyntaxKind.PlusToken:
            return left + right;

          case SyntaxKind.MinusToken:
            return left - right;

          case SyntaxKind.MultToken:
            return left * right;

          case SyntaxKind.DivToken:
            return left / right;

          default:
            throw new Exception($"Unexpected binary operator {b.OperatorToken.Kind}");
        }
      }

      if (node is BracketedExpressionSyntax p)
      {
        return EvaluateExpression(p.Expression);
      }

      throw new Exception("Unexpected node {node.Kind}");
    }
  }
}