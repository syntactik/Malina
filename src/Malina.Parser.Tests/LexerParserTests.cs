﻿using NUnit.Framework;
using static Malina.Parser.Tests.TestUtils;

namespace Malina.Parser.Tests
{
    /// <summary>
    /// Each test can perform 5 type of test:
    /// 1) Lexer - compares generated stream of Tokens with the recorded one
    /// 2) LexerError - compares lexer errors with recorded lexer errors
    /// 3) Parser - compares ParseTree with recorded parse tree
    /// 4) ParserError - compare parser errors with recorded parser errors
    /// 5) Dom - compare generated Dom structure with the recorded Dom structure
    /// </summary>
    [TestFixture]
    public class LexerParserTests
    {


        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void AliasDefinitionWithAttributes()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void AliasDefinitionWithAttrAndElem()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void AliasDefinitionWithDefaultBlockParameters()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void AliasDefinitionWithDefaultInlineBlockParameters()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void AliasDefinitionWithDefaultValueParameters()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void AliasDefinitionWithInlineAttributeDefaultParameters()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void AliasDefinitionWithInlineAttributeParameters()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void AliasDefinitionWithInlineParameters()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void AliasDefinitionWithSimpleParameters()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void AliasWithArguments()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, ParserErrorRecorded, DomRecorded]
        public void AliasWithIncorrectBlock()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void AliasWithInlineArgumentList()
        {
            PerformTest();
        }

        [Test,LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void AliasWithInlineArgumentList2()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void AliasWithInlineArgumentList3()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void AliasWithInlineArgumentList4()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void AliasWithInlineArgumentList5()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void AliasWithInlineArguments()
        {
            PerformTest();
        }

        [Test, LexerRecord, LexerErrorRecorded, ParseTreeRecorded, DomRecorded]
        public void DoubleQuoteMultilineString()
        {
            PerformTest();
        }

        [Test, LexerRecorded, LexerErrorRecorded, ParseTreeRecorded, DomRecorded]
        public void DoubleQuoteMultilineStringEof()
        {
            PerformTest();
        }

        [Test, LexerRecorded, LexerErrorRecorded, ParseTreeRecorded, DomRecorded]
        public void DoubleQuoteMultilineStringEof2()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void ElementList1()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void ElementWithAlias()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void ElementWithAliasAndAliasDefinition()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void ElementWithAliasedAttribute()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void ElementWithAttributes()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void ElementWithAttributesAndOtherElements()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void ElementWithNestedAlias()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void ElementWithNestedAliasAndNestedAliasDefinition()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void ElementWithOpenStringValue()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void ElementWithValue()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void ElementWithNamespace()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void EmptyElement()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void EmptyElementWithNamespace()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void FreeOpenString()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void HybridBlock()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void InlineAliasDefinition()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void InlineDocumentDefinition()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void InlineElementBody1()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void InlineElementBody2()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void InlineElementBody3()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void InlineElementBody4()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void InlineJsonArray()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void JsonArray()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void JsonArrayWithValues()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void JsonArrayWithValuesInParameters()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void JsonArrayItemInObject()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void JsonEmptyArrayAndObject()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void JsonLiteralsInSqs()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void LineComments()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void ModuleNamespaceOverload()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void ModuleWithTwoNamespaces()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void MultiLineComments()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void NamespaceScope1()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void NamespaceScope2()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void NamespaceScope3()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void OneNamespaceNoBrackets()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void OpenStringEndOnDedentAndEof()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]        
        public void OpenStringMultiline()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void OpenStringMultiline2()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void OpenStringMultiline3()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void OpenStringSimple()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void SingleQuoteInterpolation()
        {
            PerformTest();
        }

        [Test, LexerRecorded, LexerErrorRecorded, ParseTreeRecorded, DomRecorded]
        public void SingleQuoteMultiline()
        {
            PerformTest();
        }

        [Test, LexerRecorded, LexerErrorRecorded, ParseTreeRecorded, DomRecorded]
        public void SingleQuotedStringInline()
        {
            PerformTest();
        }


        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void OpenStringEmpty()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void TwoNamespaces()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void ValueAliasDefinition()
        {
            PerformTest();
        }


        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void ElementAtEOF()
        {
            PerformTest();
        }

        [Test, LexerRecorded, ParseTreeRecorded, DomRecorded]
        public void Wsa1()
        {
            PerformTest();
        }
    }
}
