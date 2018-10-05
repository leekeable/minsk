using System;

namespace mc
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("> ");
            var line = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(line))
                return;

        }
    }

    class Lexer
    {
        private readonly string _text;

        public Lexer(string text)
        {
            _text = text;
        }
    }
}
