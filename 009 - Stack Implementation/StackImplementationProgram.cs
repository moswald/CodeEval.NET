namespace CodeEval.StackImplementation
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    static class StackImplementationProgram
    {
        static void Main(string[] args)
        {
            foreach (var result in File.ReadLines(args[0])
                .Where(line => line != null)
                .Select(line => line.Split(' ').Select(int.Parse))
                .Select(
                    values =>
                    {
                        var stack = new CustomStack();
                        foreach (var value in values)
                        {
                            stack.Push(value);
                        }

                        return stack;
                    })
                .Select(PopAlternate)
                .Select(values => string.Join(" ", values)))
            {
                Console.WriteLine(result);
            }
        }

        static IEnumerable<int> PopAlternate(CustomStack stack)
        {
            for (;;)
            {
                if (stack.IsEmpty)
                    yield break;

                yield return stack.Pop();

                if (stack.IsEmpty)
                    yield break;

                var ignore = stack.Pop();
            }
        }

        class CustomStack
        {
            readonly IList<int> _stack = new List<int>();

            public bool IsEmpty => !_stack.Any();

            public int Pop()
            {
                var first = _stack.First();
                _stack.RemoveAt(0);
                return first;
            }

            public void Push(int value)
            {
                _stack.Insert(0, value);
            }
        }
    }
}
