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
using Malina.DOM;
using NUnit.Framework;
using System.Collections.Generic;
using static Malina.Compiler.Tests.TestUtils;

namespace Malina.Compiler.Tests
{
    [TestFixture, Category("Compiler")]
    public class CompilerTestFixture
    {

        [Test]
        public void AliasDefWithDefaultAndBlockParameter()
        {
            var errorsExpected = new List<CompilerError>
            {
                new CompilerError(new LexicalInfo("ModuleWithAliasDef.mlx", 3, 3,-1), "Default parameter must be the only parameter."),
                new CompilerError(new LexicalInfo("ModuleWithAliasDef.mlx", 9, 3,-1), "Default parameter must be the only parameter."),
            };
            PerformCompilerTest(errorsExpected);
        }

        [Test]
        public void AliasDefWithDefaultAndValueParameter()
        {
            var errorsExpected = new List<CompilerError>
            {
                new CompilerError(new LexicalInfo("Module.mlx", 1, 31,-1), "Default parameter must be the only parameter."),
            };
            PerformCompilerTest(errorsExpected);
        }

        [Test]
        public void AliasHasCircularReference()
        {
            var errorsExpected = new List<CompilerError>
            {
                new CompilerError(new LexicalInfo("ModuleWithAlias1.mlx", 2,1,-1), "Alias Definition 'Address1' has circular reference."),
                new CompilerError(new LexicalInfo("ModuleWithAlias1.mlx", 9,1,-1), "Alias Definition 'Address3' has circular reference."),
                new CompilerError(new LexicalInfo("ModuleWithAlias1.mlx", 16,1,-1), "Alias Definition 'Address4' has circular reference."),
                new CompilerError(new LexicalInfo("ModuleWithAlias2.mlx", 2,1,-1), "Alias Definition 'Address2' has circular reference."),
                new CompilerError(new LexicalInfo("ModuleWithAlias2.mlx", 9,1,-1), "Alias Definition 'Address5' has circular reference."),
                new CompilerError(new LexicalInfo("ModuleWithAlias2.mlx", 17,1,-1), "Alias Definition 'Address6' has circular reference.")
            };
            PerformCompilerTest(errorsExpected);
        }

        [Test, RecordedTest]
        public void AliasParameterWithDefaultValue()
        {
            PerformCompilerTest();
        }

        [Test, RecordedTest]
        public void ModulesWithNsDocumentAndNsAlias()
        {
            PerformCompilerTest();
        }
        
        [Test, RecordedTest]
        public void MultipleFilesWithSchemaCompilation()
        {
            PerformCompilerTest();
        }

        [Test, RecordedTest]
        public void NestedAliases()
        {
            PerformCompilerTest();
        }

        [Test, RecordedTest]
        public void NestedAliasesWithParameters()
        {
            PerformCompilerTest();
        }
        
        [Test]
        public void MissingAlias()
        {
            var errorsExpected = new List<CompilerError>
            {
                new CompilerError(new LexicalInfo("ModuleWithDocument.mlx", 7,3,-1), "Alias 'Address.UK.Cambridge' is not defined.")
            };
            PerformCompilerTest(errorsExpected);
        }
        
        [Test, RecordedTest]
        public void MixedContentInXml()
        {
            PerformCompilerTest();
        }


        [Test]
        public void NamespaceIsNotDefined()
        {
            var errorsExpected = new List<CompilerError>
            {
                new CompilerError(new LexicalInfo("ModuleWithDocument.mlx", 3,1,-1), "Namespace prefix 'ipo' is not defined."),
                new CompilerError(new LexicalInfo("ModuleWithDocument.mlx", 4,2,-1), "Namespace prefix 'ipo2' is not defined.")
            };
            PerformCompilerTest(errorsExpected);
        }

        [Test, RecordedTest]
        public void NamespaceScope()
        {
            PerformCompilerTest();
        }


        [Test]
        public void DuplicateDocumentName()
        {
            var errorsExpected = new List<CompilerError>
            {
                new CompilerError(new LexicalInfo("ModuleWithDocument1.mlx", 2,1,-1), "Duplicate document name - 'PurchaseOrder'."),
                new CompilerError(new LexicalInfo("ModuleWithDocument2.mlx", 2,1,-1), "Duplicate document name - 'PurchaseOrder'.")
            };
            PerformCompilerTest(errorsExpected);
        }
        
        [Test, RecordedTest]
        public void EmptyParameters()
        {
            PerformCompilerTest();
        }

        [Test]
        public void DuplicateAliasDefinition()
        {
            var errorsExpected = new List<CompilerError>
            {
                new CompilerError(new LexicalInfo("ModuleWithAliasDef1.mlx", 1,1,-1), "Duplicate alias definition name - 'Address'."),
                new CompilerError(new LexicalInfo("ModuleWithAliasDef2.mlx", 2,1,-1), "Duplicate alias definition name - 'Address'.")
            };
            PerformCompilerTest(errorsExpected);
        }
        
        [Test, RecordedTest]
        public void AliasInsideSqs()
        {
            PerformCompilerTest();
        }


        [Test, RecordedTest]
        public void AliasWithArguments()
        {
            PerformCompilerTest();
        }


        [Test, RecordedTest]
        public void AliasParameterWithDefaultBlock()
        {
            PerformCompilerTest();
        }

        [Test]
        public void ExtraRootElementInDocument()
        {
            var errorsExpected = new List<CompilerError>
            {
                new CompilerError(new LexicalInfo("ModuleWithDocument1.mlx", 2, 1,-1), "Document 'PurchaseOrder' must have only one root element."),
                new CompilerError(new LexicalInfo("ModuleWithDocument1.mlx", 12, 1,-1), "Document 'PurchaseOrder2' must have only one root element."),
                new CompilerError(new LexicalInfo("ModuleWithDocument1.mlx", 22, 1,-1), "Alias Definition 'Address1' has circular reference."),
                new CompilerError(new LexicalInfo("ModuleWithDocument1.mlx", 26, 1,-1), "Alias Definition 'Address2' has circular reference."),
                new CompilerError(new LexicalInfo("ModuleWithDocument1.mlx", 30, 1,-1), "Document 'PurchaseOrder3' must have only one root element."),
                new CompilerError(new LexicalInfo("ModuleWithDocument1.mlx", 34, 1,-1), "Document 'PurchaseOrder4' must have at least one root element."),
            };
            PerformCompilerTest(errorsExpected);
        }
        
        [Test, RecordedTest]
        public void FoldedOpenString()
        {
            PerformCompilerTest();
        }

        [Test, RecordedTest]
        public void FoldedSQS()
        {
            PerformCompilerTest();
        }

        [Test, RecordedTest]
        public void JsonArray()
        {
            PerformCompilerTest();
        }

        [Test, RecordedTest]
        public void JsonArrayInAlias()
        {
            PerformCompilerTest();
        }
        
        [Test]
        public void JsonArrayInXmlDocument()
        {
            var errorsExpected = new List<CompilerError>
            {
                new CompilerError(new LexicalInfo("JsonArray.mlx", 3, 3,-1), "Can not define array item in the xml document."),
                new CompilerError(new LexicalInfo("JsonArray.mlx", 7, 5,-1), "Can not define array item in the xml document."),
                new CompilerError(new LexicalInfo("JsonArray.mlx", 10, 5,-1), "Can not define array item in the xml document."),
            };
            PerformCompilerTest(errorsExpected);
        }

        [Test]
        public void JsonArrayItemInObject()
        {
            var errorsExpected = new List<CompilerError>
            {
                new CompilerError(new LexicalInfo("JsonArray.mlj", 4, 3,-1), "Array item is not expected."),
                new CompilerError(new LexicalInfo("JsonArray.mlj", 6, 3,-1), "Array item is not expected."),
                new CompilerError(new LexicalInfo("JsonArray.mlj", 9, 3,-1), "Array item is not expected."),
                new CompilerError(new LexicalInfo("JsonArray.mlj", 14, 2,-1), "Array item is not expected."),
            };
            PerformCompilerTest(errorsExpected);
        }

        [Test, RecordedTest]
        public void JsonArrayWithValues()
        {
            PerformCompilerTest();
        }

        [Test, RecordedTest]
        public void JsonArrayWithValuesInAlias()
        {
            PerformCompilerTest();
        }

        [Test, RecordedTest]
        public void JsonArrayWithValuesInParameters()
        {
            PerformCompilerTest();
        }

        [Test, RecordedTest]
        public void JsonEmptyArrayAndObject()
        {
            PerformCompilerTest();
        }

        [Test, RecordedTest]
        public void JsonLiteralsInSqs()
        {
            PerformCompilerTest();
        }

        [Test]
        public void JsonPropertyInArray()
        {
            var errorsExpected = new List<CompilerError>
            {
                new CompilerError(new LexicalInfo("JsonArray.mlj", 6, 3,-1), "Array item is expected."),
                new CompilerError(new LexicalInfo("JsonArray.mlj", 7, 3,-1), "Array item is expected."),
                new CompilerError(new LexicalInfo("JsonArray.mlj", 11, 3,-1), "Array item is expected."),
                new CompilerError(new LexicalInfo("JsonArray.mlj", 15, 2,-1), "Array item is expected."),

            };
            PerformCompilerTest(errorsExpected);
        }

        [Test, RecordedTest]
        public void LineComments()
        {
            PerformCompilerTest();
        }


        [Test]
        public void ParameterInDocument()
        {
            var errorsExpected = new List<CompilerError>
            {
                new CompilerError(new LexicalInfo("ModuleWithDocument.mlx", 7, 4,-1), "Parameters can't be declared in documents."),
            };
            PerformCompilerTest(errorsExpected);
        }

        [Test]
        public void SchemaValidation()
        {
            var errorsExpected = new List<CompilerError>
            {
                new CompilerError(new LexicalInfo("ModuleWithDocument.mlx", 4, 1,-1), "XML validation error - 'The element 'purchaseOrder' in namespace 'http://www.example.com/myipo' has incomplete content. List of possible elements expected: 'comment' in namespace 'http://www.example.com/myipo' as well as 'Items'.'."),
                new CompilerError(new LexicalInfo("ModuleWithDocument.mlx", 5, 2,-1), "XML validation error - 'The element 'shipTo' has incomplete content. List of possible elements expected: 'name, street' in namespace 'http://www.example.com/myipo'.'."),
                new CompilerError(new LexicalInfo("ModuleWithDocument.mlx", 6, 2,-1), "XML validation error - 'The element 'billTo' has incomplete content. List of possible elements expected: 'name, street' in namespace 'http://www.example.com/myipo'.'."),
                new CompilerError(new LexicalInfo("ModuleWithDocument.mlx", 12, 1,-1), "XML validation error - 'The element 'purchaseOrder' in namespace 'http://www.example.com/myipo' has incomplete content. List of possible elements expected: 'shipTo'.'."),
            };
            PerformCompilerTest(errorsExpected);
        }


        [Test]
        public void SchemaValidationXsdMissing()
        {
            var errorsExpected = new List<CompilerError>
            {
                new CompilerError(new LexicalInfo("ipo.xsd", 15, 5,-1), "XML validation error - 'Type 'http://www.example.com/myipo:Address' is not declared.'."),
            };
            PerformCompilerTest(errorsExpected);
        }

        [Test, RecordedTest]
        public void SingleQuoteEscape()
        {
            PerformCompilerTest();
        }

        [Test]
        public void AliasWithIncorrectBlock()
        {
            var errorsExpected = new List<CompilerError>
            {
                new CompilerError(new LexicalInfo("ModuleWithDocument.mlx", 9, 5,-1), "Unexpected default block argument."),
            };
            PerformCompilerTest(errorsExpected);
        }

        [Test]
        public void AliasWithIncorrectType()
        {
            var errorsExpected = new List<CompilerError>
            {
                new CompilerError(new LexicalInfo("ModuleWithDocument.mlx", 4, 4,-1), "Can not use value alias in the block."),
                new CompilerError(new LexicalInfo("ModuleWithDocument.mlx", 12, 11,-1), "Can not use block alias as value."),
            };
            PerformCompilerTest(errorsExpected);
        }

        [Test]
        public void AliasWithMissedArgument()
        {
            var errorsExpected = new List<CompilerError>
            {
                new CompilerError(new LexicalInfo("ModuleWithDocument.mlx", 4, 4,-1), "Argument 'street' is missing."),
            };
            PerformCompilerTest(errorsExpected);
        }

        [Test]
        public void AliasWithMissedDefaultBlockParameter()
        {
            var errorsExpected = new List<CompilerError>
            {
                new CompilerError(new LexicalInfo("ModuleWithDocument.mlx", 2, 2,-1), "Default block argument is missing."),
            };
            PerformCompilerTest(errorsExpected);
        }

        [Test]
        public void AliasWithMissedDefaultValueParameter()
        {
            var errorsExpected = new List<CompilerError>
            {
                new CompilerError(new LexicalInfo("Module.mlx", 5, 13,-1), "Default value argument is missing."),
            };
            PerformCompilerTest(errorsExpected);
        }

        [Test]
        public void AliasWithUnexpectedDefaultBlockParameter()
        {
            var errorsExpected = new List<CompilerError>
            {
                new CompilerError(new LexicalInfo("ModuleWithDocument.mlx", 6, 4,-1), "Unexpected default block argument."),
            };
            PerformCompilerTest(errorsExpected);
        }

        [Test]
        public void AliasWithUnexpectedDefaultValueParameter()
        {
            var errorsExpected = new List<CompilerError>
            {
                new CompilerError(new LexicalInfo("Module.mlx", 5, 13,-1), "Unexpected default value argument."),
            };
            PerformCompilerTest(errorsExpected);
        }

        [Test]
        public void AliasWithUnexpectedArgument()
        {
            var errorsExpected = new List<CompilerError>
            {
                new CompilerError(new LexicalInfo("ModuleWithDocument.mlx", 2, 20,-1), "Unexpected argument."),
            };
            PerformCompilerTest(errorsExpected);
        }

        [Test]
        public void ArgumentInTheElementBlock()
        {
            var errorsExpected = new List<CompilerError>
            {
                new CompilerError(new LexicalInfo("ModuleWithDocument.mlx", 3, 3,-1), "Argument can be defined only in an alias' block."),
            };
            PerformCompilerTest(errorsExpected);
        }

        [Test, RecordedTest]
        public void AliasWithAttributes()
        {
            PerformCompilerTest();
        }

        [Test, RecordedTest]
        public void AliasWithDefaultBlockParameter()
        {
            PerformCompilerTest();
        }

        [Test, RecordedTest]
        public void AliasWithDefaultValueParameter()
        {
            PerformCompilerTest();
        }

        [Test, RecordedTest]
        public void ArgumentWithObjectValue()
        {
            PerformCompilerTest();
        }

        [Test]
        public void AliasWithDuplicateArguments()
        {
            var errorsExpected = new List<CompilerError>
            {
                new CompilerError(new LexicalInfo("ModuleWithDocument.mlx", 6, 5,-1), "Duplicate argument name - 'name'."),
                new CompilerError(new LexicalInfo("ModuleWithDocument.mlx", 7, 5,-1), "Duplicate argument name - 'name'."),
            };
            PerformCompilerTest(errorsExpected);
        }

        [Test]
        public void AliasWithIncorrectArgumentType()
        {
            var errorsExpected = new List<CompilerError>
            {
                new CompilerError(new LexicalInfo("ModuleWithDocument.mlx", 5, 5,-1), "Value argument is expected."),
                new CompilerError(new LexicalInfo("ModuleWithDocument.mlx", 7, 5,-1), "Block argument is expected."),
            };
            PerformCompilerTest(errorsExpected);
        }

        [Test, RecordedTest]
        public void DotEscapedInId()
        {
            PerformCompilerTest();
        }


        [Test, RecordedTest]
        public void TwoModulesWithDocumentAndAlias()
        {
            PerformCompilerTest();
        }

        [Test]
        public void UndeclaredNamespace()
        {
            var errorsExpected = new List<CompilerError>
            {
                new CompilerError(new LexicalInfo("ModuleWithDocument.mlx", 2, 1,-1), "Namespace prefix 'ipo' is not defined."),
                new CompilerError(new LexicalInfo("ModuleWithDocument.mlx", 5, 1,-1), "Namespace prefix 'ipo2' is not defined."),
            };
            PerformCompilerTest(errorsExpected);
        }

        [Test]
        public void UnresolvedAliasInsideSqs()
        {
            var errorsExpected = new List<CompilerError>
            {
                new CompilerError(new LexicalInfo("ModuleWithDocument.mlx", 4, 7,-1), "Alias 'bla' is not defined."),
                new CompilerError(new LexicalInfo("ModuleWithDocument.mlx", 5, 15,-1), "Alias 'user.first' is not defined."),
                new CompilerError(new LexicalInfo("ModuleWithDocument.mlx", 5, 30,-1), "Alias 'user.last' is not defined."),
            };
            PerformCompilerTest(errorsExpected);
        }
        
        [Test]
        public void ValueAliasWithMissedArgument()
        {
            var errorsExpected = new List<CompilerError>
            {
                new CompilerError(new LexicalInfo("ModuleWithDocument.mlx", 4, 14,-1), "Argument 'street' is missing."),
                new CompilerError(new LexicalInfo("ModuleWithDocument.mlx", 5, 11,-1), "Argument 'name' is missing."),
            };
            PerformCompilerTest(errorsExpected);
        }
    }
}
