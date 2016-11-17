﻿using Malina.DOM;
using System;
using Antlr4.Runtime;

namespace Malina.Compiler
{
    public static class CompilerErrorFactory
    {
        public static CompilerError InputError(string inputName, Exception x)
        {
            return InputError(new LexicalInfo(inputName), x);
        }

        public static CompilerError InputError(LexicalInfo lexicalInfo, Exception error)
        {
            return Instantiate("MCE0001", lexicalInfo, error, lexicalInfo.FileName, error.Message);
        }

        internal static CompilerError FatalError(Exception ex)
        {
            return new CompilerError("MCE0000", ex, ex.Message);
        }

        internal static CompilerError FatalError(Exception ex, string message)
        {
            return new CompilerError("MCE0000", ex, message);
        }

        private static CompilerError Instantiate(string code, LexicalInfo location, Exception error, params object[] args)
        {
            return new CompilerError(code, location, error, args);
        }

        private static CompilerError Instantiate(string code, LexicalInfo location, params object[] args)
        {
            return new CompilerError(code, location, Array.ConvertAll<object, string>(args, DisplayStringFor));
        }

        internal static string DisplayStringFor(object o)
        {
            if (o == null) return "";

            return  o.ToString();
        }

        internal static Exception FileNotFound(string fileName)
        {
            return Instantiate("MCE0002", new LexicalInfo(fileName), fileName);
        }

        internal static CompilerError NsPrefixNotDefined<T>(T node, string fileName) where T : Node
        {
            return Instantiate("MCE0003", new LexicalInfo(fileName, node.start.Line, node.start.Column, node.start.Index), (node as INsNode).NsPrefix);
        }

        internal static CompilerError AliasIsNotDefined(Alias alias, string fileName)
        {
            return Instantiate("MCE0004", new LexicalInfo(fileName, alias.start.Line, alias.start.Column, alias.start.Index), alias.Name);
        }

        internal static CompilerError AliasDefHasCircularReference(NamespaceResolver.NsInfo aliasDefNsInfo)
        {
            var aliasDef = aliasDefNsInfo.ModuleMember as AliasDefinition;
            return Instantiate("MCE0005", new LexicalInfo(aliasDef.Module.FileName,
                aliasDef.start.Line, aliasDef.start.Column, aliasDef.start.Index), aliasDef.Name);
        }

        internal static CompilerError LexerError(Exception e, string message, string fileName, int line, int column)
        {
            return Instantiate("MCE0006", new LexicalInfo(fileName, line, column, 0), message);
        }

        internal static CompilerError ParserError(Exception e, string message, string fileName, int line, int column)
        {
            return Instantiate("MCE0007", new LexicalInfo(fileName, line, column, 0), message);
        }
    }
}