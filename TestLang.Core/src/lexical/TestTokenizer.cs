namespace TestLang.Core.lexical; 

public class TestTokenizer : TokenizerBase {
    private static readonly List<string> _ignoreTokens = new() {
        "\r",
    };
    
    private static readonly List<string> _availableTokens = new() {
        "\n",
        "\t",
        "    ",

        "+",
        "-",
        "*",
        "/",
        "!",
        "~",
        "#",
        "$",
        "%",
        "^",
        "&",
        "|",
        "=",
        "_",

        "(",
        ")",
        "[",
        "]",
        "{",
        "}",
        "<",
        ">",

        ":",
        ";",
        ",",
        ".",

        "\"",
        "'",
        "\\",

        "//",
        "///",

        "<<",
        "<<<",
        ">>",
        ">>>",
        "==",
        "!=",
        "=>",
        ">=",
        "<=",
        "&&",
        "||",
        "++",
        "--",
        "**",

        "+=",
        "-=",
        "*=",
        "/=",
        "%=",
        "**=",
        "<<=",
        "<<<=",
        ">>=",
        ">>>=",
        "&=",
        "|=",
        "^=",
        "??=",

        "??",
        "::",
        "?.",
    };

    protected override List<string> ignoreTokens => _ignoreTokens;
    protected override List<string> availableTokens => _availableTokens;
    protected override List<(string, int)> segmentEnterTokens => new();
    protected override List<(string, int)> segmentExitTokens => new();

    static TestTokenizer() {
        _availableTokens.Sort((a, b) => b.Length.CompareTo(a.Length));
    }
    
    public TestTokenizer(string str, string fileName) : base(str, fileName) { }
}