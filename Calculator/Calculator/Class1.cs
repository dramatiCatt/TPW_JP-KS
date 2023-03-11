namespace Calculator
{
    public class Calc
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hejka, tu nasz kalkulator");
        }

        public int sub(int a, int b)
        {
            return a - b;
        }

        public int mul(int a, int b)
        {
            return a * b;
        }

        public int div(int a, int b)
        {
            return a / b;
        }
    }
}