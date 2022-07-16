namespace TestLang.Core.lexical; 

public readonly struct Token {
    public readonly int id;
    public readonly string originalString;
    public readonly int start;
    public readonly int length;

    public Token(int id, string originalString, int start, int length) {
        this.id = id;
        this.originalString = originalString;
        this.start = start;
        this.length = length;
    }

    public override string ToString() => originalString[start..(start + length)];
}