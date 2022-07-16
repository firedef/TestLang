using TestLang.Core.errors;

namespace TestLang.Core.lexical; 

public abstract class TokenizerBase {
    public readonly string str;
    public readonly List<Token> tokens = new();
    protected readonly string fileName;

    protected int line;
    protected int posOnLine;
    protected int position;
    
    protected abstract List<string> ignoreTokens { get; }
    protected abstract List<string> availableTokens { get; }
    protected abstract List<(string, int)> segmentEnterTokens { get; }
    protected abstract List<(string, int)> segmentExitTokens { get; }

    protected TokenizerBase(string str, string fileName) {
        this.str = str;
        this.fileName = fileName;
    }

    protected char CharWithOffset(int offset) {
        int pos = offset + position;
        if (pos < 0 || pos >= str.Length) return '\0';
        return str[pos];
    }

    protected bool EqualsCurrent(string s) {
        int l0 = str.Length;
        int l1 = s.Length;
        if (l0 - position < l1) return false;

        for (int i = 0; i < l1; i++) 
            if (str[position + i] != s[i])
                return false;
        return true;
    }

    public virtual TokenizerError Run() {
        int len = str.Length;

        TokenizerError err = Success();
        while (err.isSuccess && position < len)
            err = Next();

        return err;
    }

    protected virtual TokenizerError Next() {
        // if (str[position] == '\n') {
        //     line++;
        //     posOnLine = 0;
        // }
        
        for (int i = 0; i < ignoreTokens.Count; i++) {
            if (!EqualsCurrent(ignoreTokens[i])) continue;
            position += ignoreTokens[i].Length;
            return Success();
        }
        
        foreach ((string, int) tk in segmentEnterTokens)
            if (EqualsCurrent(tk.Item1)) {
                position += tk.Item1.Length;
                return TokenizeSegment(tk);
            }

        for (int i = 0; i < availableTokens.Count; i++) {
            if (!EqualsCurrent(availableTokens[i])) continue;
            AddToken(i, availableTokens[i].Length);
            return Success();
        }

        return SkipInvalidTokens();
    }

    protected virtual TokenizerError TokenizeSegment((string, int) tk) => throw new NotImplementedException();

    protected virtual TokenizerError SkipInvalidTokens() {
        if (tokens.Count != 0 && tokens[^1].id == -1) {
            tokens[^1] = new(-1, str, tokens[^1].start, tokens[^1].length + 1);
            position++;
            return Success();
        }
        
        AddToken(-1, 1);
        return Success();
    }

    protected TokenizerError Success() => new(true, "", fileName, line, position);
    protected TokenizerError Error(string msg) => new(false, msg, fileName, line, position);

    protected void AddToken(int id, int length) {
        tokens.Add(new(id, str, position, length));
        position += length;
    }
}