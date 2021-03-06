﻿#region license
// Copyright © 2016, 2017 Maxim Trushin (dba Syntactik, trushin@gmail.com, maxim.trushin@syntactik.com)
//
// This file is part of Malina.
// Malina is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// Malina is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.

// You should have received a copy of the GNU Lesser General Public License
// along with Malina.  If not, see <http://www.gnu.org/licenses/>.
#endregion
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Malina.Compiler;
using Malina.Compiler.Pipelines;
using Malina.Compiler.IO;

namespace Malina.Parser.PerformanceTests
{
    [TestFixture]
    public class PerformanceTests
    {
        public static string AssemblyDirectory => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        //[Test]
        public void BigFile()
        {
            Console.WriteLine("Starting BigFile");
            var code = LoadTestCodeRaw();
            var lexer = new MalinaLexer(new AntlrInputStream(code));
            //lexer.RemoveErrorListeners();
            //lexer.ErrorListeners.Clear();
            var t1 = Environment.TickCount;
            var i = 0;
            IToken token;
            for (token = lexer.NextToken(); token.Type != -1; token = lexer.NextToken())
            {
                i++;
            }
            var t2 = Environment.TickCount;

            Console.WriteLine("GetAllTokens Time: {0}", t2 - t1);
            Console.WriteLine("Token Number: {0}", i);
            Assert.Less(t2 - t1, 7000);

            lexer.Reset();
            var parser = MalinaParser.Create(new CommonTokenStream(lexer));
            parser.Interpreter.PredictionMode = PredictionMode.Sll;
            var malinaListener = new MalinaParserListener();
            //var parserErrorListener = new ErrorListener<IToken>();
            //parser.AddErrorListener(parserErrorListener);
            //parser.AddParseListener(malinaListener);
            parser.BuildParseTree = false;
            t1 = Environment.TickCount;
            //var module = parser.module();
            t2 = Environment.TickCount;

            Console.WriteLine("Parse Time: {0}", t2 - t1);
            Assert.Less(t2 - t1, 15000);
            


            lexer.Reset();
            parser.Reset();
            parser.AddParseListener(malinaListener);
            t1 = Environment.TickCount;
            //module = parser.module();
            t2 = Environment.TickCount;
            Console.WriteLine("DOM Time: {0}", t2 - t1);
            Assert.Less(t2 - t1, 25000);
            

            t1 = Environment.TickCount;
            //var visitor = new DOMPrinterVisitor();
            //visitor.Visit(malinaListener.CompileUnit);
            t2 = Environment.TickCount;
            Console.WriteLine("Visitor Time: {0}", t2 - t1);


            //GetAllTokens Time: 5625
            //Token Number: 7764705
            //Parse Time: 0
            //DOM Time: 23516
            //Visitor Time: 6828

            //GetAllTokens Time: 5734
            //Token Number: 7764705
            //Parse Time: 14391
            //DOM Time: 12375
            //Visitor Time: 6046
        }

        //[Test]
        public void BigFile2()
        {
            Console.WriteLine("Starting BigFile");
            var code = LoadTestCodeRaw();
            var lexer = new MalinaLexer(new AntlrInputStream(code));
            //lexer.RemoveErrorListeners();
            //lexer.ErrorListeners.Clear();
            var t1 = Environment.TickCount;
            var i = 0;
            IToken token = null;
            for (token = lexer.NextToken(); token.Type != -1; token = lexer.NextToken())
            {
                i++;
            }
            var t2 = Environment.TickCount;

            Console.WriteLine("GetAllTokens Time: {0}", t2 - t1);
            Console.WriteLine("Token Number: {0}", i);
            Assert.Less(t2 - t1, 7000);

            lexer.Reset();
            var parser = MalinaParser.Create(new CommonTokenStream(lexer));
            parser.Interpreter.PredictionMode = PredictionMode.Sll;
            var malinaListener = new MalinaParserListener();
            //var parserErrorListener = new ErrorListener<IToken>();
            //parser.AddErrorListener(parserErrorListener);
            //parser.AddParseListener(malinaListener);
            parser.BuildParseTree = false;
            t1 = Environment.TickCount;
            //var module = parser.module();
            t2 = Environment.TickCount;

            Console.WriteLine("Parse Time: {0}", t2 - t1);
            Assert.Less(t2 - t1, 15000);
            //Assert.IsFalse(parserErrorListener.HasErrors);


            lexer.Reset();
            parser.Reset();
            parser.AddParseListener(malinaListener);
            t1 = Environment.TickCount;
            parser.module();
            lexer.Reset();
            parser.Reset();
            
            t2 = Environment.TickCount;
            Console.WriteLine("DOM Time: {0}", t2 - t1);
            Assert.Less(t2 - t1, 20000);
            //Assert.IsFalse(parserErrorListener.HasErrors);

            //t1 = Environment.TickCount;
            //var visitor = new DOMPrinterVisitor();
            //visitor.Visit(malinaListener.CompileUnit);
            //t2 = Environment.TickCount;
            //Console.WriteLine("Visitor Time: {0}", t2 - t1);
        }

        //[Test]
        public void BigFileCompilation()
        {
            //28 sec
            var compilerParameters = CreateCompilerParameters("BigFileCompilation.mlx");
            var compiler = new MalinaCompiler(compilerParameters);

            var context = compiler.Run();

            if (context.Errors.Count > 0)
            {
                PrintCompilerErrors(context.Errors);
            }
            Assert.AreEqual(0, context.Errors.Count);
        }

        private CompilerParameters CreateCompilerParameters(string fileName)
        {
            var compilerParameters = new CompilerParameters();
            compilerParameters.Pipeline = new CompileToFiles();

            var dir = AssemblyDirectory + '\\' + "Scenarios" + '\\';

            compilerParameters.OutputDirectory = dir + "Result" + '\\';

            compilerParameters.Input.Add(new FileInput(dir + fileName));

            return compilerParameters;
        }

        public static void PrintCompilerErrors(IEnumerable<CompilerError> errors)
        {
            Console.WriteLine("Compiler Errors:");

            foreach (var error in errors)
            {
                Console.WriteLine();
                Console.Write(error.Code + " " + error.LexicalInfo + ": ");
                Console.WriteLine(error.Message);
                if (error.InnerException != null)
                    Console.WriteLine(error.InnerException.StackTrace);
            }
        }

        public static string LoadTestCodeRaw()
        {
            var testCaseName = GetTestCaseName();

            var fileName = new StringBuilder(AssemblyDirectory + @"\Scenarios\").Append(testCaseName).Append(".mlx").ToString();

            return File.ReadAllText(fileName);
        }

        private static string GetTestCaseName()
        {
            var trace = new StackTrace();
            return trace.GetFrames().Select(f => f.GetMethod()).First(m => m.CustomAttributes.Any(a => a.AttributeType.FullName == "NUnit.Framework.TestAttribute")).Name;
        }

    }
}
