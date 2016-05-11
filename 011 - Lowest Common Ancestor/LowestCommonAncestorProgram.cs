namespace CodeEval.LowestCommonAncestor
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    static class LowestCommonAncestorProgram
    {
        static void Main(string[] args)
        {
            var root = BuildStaticTree();

            foreach (var result in File.ReadLines(args[0])
                .Where(line => line != null)
                .Select(line => line.Split(' ')
                    .Select(int.Parse)
                    .OrderBy(x => x)
                    .ToArray())
                .Select(values => FindLowestCommonAncestor(root, values).Value))
            {
                Console.WriteLine(result);
            }
        }

        static INode FindLowestCommonAncestor(INode root, IList<int> values)
        {
            while (true)
            {
                if (root.Value < values[0])
                {
                    root = root.Right;
                    continue;
                }

                if (root.Value > values[1])
                {
                    root = root.Left;
                    continue;
                }

                return root;
            }
        }

        static INode BuildStaticTree()
        {
            INode root = new Node(30);

            foreach (var value in new[] { 8, 52, 3, 20, 10, 29 })
            {
                var cur = root;
                while (true)
                {
                    if (cur.Value < value)
                    {
                        if (cur.Right != null)
                        {
                            cur = cur.Right;
                            continue;
                        }

                        cur.Right = new Node(value);
                    }
                    else
                    {
                        if (cur.Left != null)
                        {
                            cur = cur.Left;
                            continue;
                        }

                        cur.Left = new Node(value);
                    }

                    break;
                }
            }

            return root;
        }

        interface INode
        {
            int Value { get; }
            INode Left { get; set; }
            INode Right { get; set; }
        }

        struct Node : INode
        {
            public Node(int value)
                : this()
            {
                Value = value;
            }

            public int Value { get; }
            public INode Left { get; set; }
            public INode Right { get; set; }
        }
    }
}
