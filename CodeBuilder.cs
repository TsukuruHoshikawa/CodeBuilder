using System;
using System.Text;

namespace Tsukuru
{
    public class CodeBuilder
    {
        StringBuilder sb = new StringBuilder();
        int indent = 0;
        string space = "";
        public void RemoveLastChar()
        {
            sb.Remove(sb.Length - 1, 1);
        }

        public void AddIndent(int indentLevel = 1)
        {
            indent += indentLevel;
            space = new string('\t', indent);
        }

        public void AddCode(string code)
        {
            sb.Append(code);
        }

        public void AddCode(char code)
        {
            sb.Append(code);
        }

        public void NewLine()
        {
            AddCode('\n');
            AddCode(space);
        }

        public void NewLine(string code)
        {
            NewLine();
            AddCode(code);
        }

        public void NewLine(char code)
        {
            NewLine();
            AddCode(code);
        }

        public void BeginBlock()
        {
            NewLine('{');
            AddIndent();
        }

        public void EndBlock()
        {
            AddIndent(-1);
            NewLine('}');
        }

        public override string ToString()
        {
            return sb.ToString();
        }

        public void Clear()
        {
            sb.Clear();
            indent = 0;
            space = "";
        }

        public BlockScope CreateBlockScope()
        {
            return new BlockScope(this);
        }

        public BlockScope CreateBlockScope(string code)
        {
            return new BlockScope(this, code);
        }
    }
    public class BlockScope : IDisposable
    {
        CodeBuilder cb;
        public BlockScope(CodeBuilder codeBuilder, string code)
        {
            cb = codeBuilder;
            cb.NewLine(code);
            cb.BeginBlock();
        }
        public BlockScope(CodeBuilder codeBuilder)
        {
            cb = codeBuilder;
            cb.BeginBlock();
        }
        public void Dispose()
        {
            cb.EndBlock();
        }
    }
}
