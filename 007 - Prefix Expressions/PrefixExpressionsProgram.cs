namespace CodeEval.PrefixExpressions
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    static class PrefixExpressionsProgram
    {
        static void Main(string[] args)
        {
            foreach (var result in File.ReadLines(args[0])
                .Where(line => line != null)
                .Select(line => line.Split(' '))
                .Select(tokens => new PrefixCalculator(tokens)))
            {
                Console.WriteLine(result.Result);
            }
        }

        class PrefixCalculator
        {
            int _index;

            public PrefixCalculator(IList<string> tokens)
            {
                Result = (int)Evaluate(tokens);
            }

            public int Result { get; }

            float Evaluate(IList<string> tokens)
            {
                switch (tokens[_index])
                {
                    case "*":
                    {
                        ++_index;
                        var a = Evaluate(tokens);
                        var b = Evaluate(tokens);
                        return a * b;
                    }

                    case "/":
                    {
                        ++_index;
                        var a = Evaluate(tokens);
                        var b = Evaluate(tokens);
                        return a / b;
                    }

                    case "+":
                    {
                        ++_index;
                        var a = Evaluate(tokens);
                        var b = Evaluate(tokens);
                        return a + b;
                    }

                    default:
                    {
                        return int.Parse(tokens[_index++]);
                    }
                }
            }
        }
    }
}
