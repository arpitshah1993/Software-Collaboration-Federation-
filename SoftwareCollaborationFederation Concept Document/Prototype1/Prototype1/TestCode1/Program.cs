using System;

namespace TestDemo
{
    public class TestCode1
    {
        //add two int
        public int add(int a, int b)
        {
            return a + b;
        }
        //substitute two int
        public int sub(int a, int b)
        {
            return a - b;
        }
        //multiply two int
        public int multi(int a, int b)
        {
            return a * b;
        }
        static void Main(string[] args)
        {
            try
            {
                TestCode1 ctt = new TestCode1();
                int ans = ctt.add(3, 2);
                Console.Write("\n" + ans + "\n");
                ans = ctt.sub(3, 2);
                Console.Write("\n" + ans + "\n");
                ans = ctt.multi(3, 2);
                Console.Write("\n" + ans + "\n");
            }
            catch (Exception ex)
            {
                Console.Write("\nException caught ex: {0}\n", ex.Message);
            }
        }
    }
}
