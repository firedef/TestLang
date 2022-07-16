namespace TestLang.Core.errors; 

public class TokenizerError : Error {
    public readonly string msg;
    public readonly string fileName;
    public readonly int line;
    public readonly int linePos;

    public TokenizerError(bool isSuccess, string msg, string fileName, int line, int linePos) {
        this.isSuccess = isSuccess;
        this.msg = msg;
        this.fileName = fileName;
        this.line = line;
        this.linePos = linePos;
    }

    public override string ToString() => $"{fileName} line {line} pos {linePos}: {msg}";
}