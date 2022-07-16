
using TestLang.Core.lexical;

const string s = @"
class Hello
    bool World(i32 user)
        Console.WriteLine($""Hello {user}"")
        ret user == 42
";

TestTokenizer tokenizer = new(s, "Str");
tokenizer.Run();

Console.ReadKey();