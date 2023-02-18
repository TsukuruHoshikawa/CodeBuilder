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

        public void AddIndent(int _indentLevel = 1)
        {
            indent += _indentLevel;

            space.Clear();
            for (var i = 0; i < indent; ++i)
                space.Append('\t');
        }

        public void NewLine()
        {
            sb.Append('\n');
            sb.Append(space.ToString());
        }

        public void AddCode(string _code)
        {
            sb.Append(_code);
        }

        public void AppendLine(string _code)
        {
            NewLine();
            AddCode(_code);
        }

        public void BeginBlock()
        {
            AppendLine("{");
            AddIndent();
        }

        public void EndBlock()
        {
            AddIndent(-1);
            AppendLine("}");
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
            cb.AppendLine(code);
            cb.BeginBlock();
        }
        public BlockScope(CodeBuilder _codeBuilder)
        {
            cb = _codeBuilder;
            cb.BeginBlock();
        }
        public void Dispose()
        {
            cb.EndBlock();
        }
    }
}
