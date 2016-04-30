namespace CodeEval.FizzBuzz
{
    using System.Collections.Generic;

    public static class Challenge
    {
        public static IEnumerable<string> FizzBuzz(int fizz, int buzz, int max)
        {
            for (var i = 1; i <= max; ++i)
            {
                if (i % fizz == 0 && i % buzz == 0)
                {
                    yield return "FB";
                }
                else if (i % fizz == 0)
                {
                    yield return "F";
                }
                else if (i % buzz == 0)
                {
                    yield return "B";
                }
                else
                {
                    yield return i.ToString();
                }
            }
        }
    }
}