using System;
using System.Collections.Generic;
using System.Linq;
using Minsk.CodeAnalysis;

namespace Minsk
{
    class Program
    {
        static void Main(string[] args)
        {
            bool showtree = false;
            while (true)
            {
                Console.Write("> ");
                var line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    return;

                if (line == "#showtree")                
                {
                    showtree = !showtree;
                    Console.WriteLine(showtree ? "Showing parse tree" : "Not showing parse tree");
                }
                else if (line == "#cls")
                {
                    Console.Clear();
                }

                var syntaxTree = SyntaxTree.Parse(line);

                if (showtree)
                {
                    var color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    PrettyPrint(syntaxTree.Root);
                    Console.ForegroundColor = color;
                }

                if (!syntaxTree.Diagnostics.Any())
                {
                    var e = new Evaluator(syntaxTree.Root);
                    var result = e.Evaluate();
                    Console.WriteLine(result);
                }
                else
                {
                    var color = Console.ForegroundColor;                    
                    Console.ForegroundColor = ConsoleColor.DarkRed;

                    foreach (var diag in syntaxTree.Diagnostics)
                    {
                        Console.WriteLine(diag);
                    }
                    Console.ForegroundColor = color;
                }
            }
        }
        static void PrettyPrint(SyntaxNode node, string indent = "", bool islast = true)
        {
            var marker = islast ? "└──" : "├──";

            Console.Write(indent);
            Console.Write(marker);
            Console.Write(node.Kind);

            if (node is SyntaxToken t && t.Value != null)
            {
                Console.Write(" ");
                Console.Write(t.Value);
            }

            Console.WriteLine();

            indent += islast ? "    " : "│   ";

            var lastChild = node.GetChildren().LastOrDefault();

            foreach(var v in node.GetChildren() )
            {
                PrettyPrint(v, indent, v == lastChild);
            }
        }
    }
}
