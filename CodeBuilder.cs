using System;
using System.Text;

namespace CodeBuilder
{
    public class CodeBuilder
    {
        StringBuilder sb = new StringBuilder();
        int indent = 0;
        StringBuilder space = new StringBuilder("");
        public void RemoveLastChar()
        {
            sb.Remove(sb.Length - 1, 1);
        }

        public void AddIndent(int indentLevel = 1)
        {
            indent += indentLevel;

            space.Clear();
            for (var i = 0; i < indent; ++i)
                space.Append('\t');
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
            AddCode(space.ToString());
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
            space.Clear();
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
