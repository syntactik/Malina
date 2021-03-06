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
using Antlr4.Runtime.Misc;
using System.Collections.Generic;
using Antlr4.Runtime;
using Malina.DOM;
using Alias = Malina.DOM.Antlr.Alias;
using AliasDefinition = Malina.DOM.Antlr.AliasDefinition;
using Argument = Malina.DOM.Antlr.Argument;
using Document = Malina.DOM.Antlr.Document;
using Element = Malina.DOM.Antlr.Element;
using Module = Malina.DOM.Antlr.Module;
using Namespace = Malina.DOM.Antlr.Namespace;
using Parameter = Malina.DOM.Antlr.Parameter;
using Scope = Malina.DOM.Antlr.Scope;

namespace Malina.Parser
{
    public partial class MalinaParser
    {

        public static MalinaParser Create(ITokenStream stream, IAntlrErrorStrategy strategy = null)
        {
            var parser = new MalinaParser(stream);
            if (strategy == null)
                strategy = new MalinaErrorStrategy();
            parser.ErrorHandler = strategy;
            return parser;
        }

        #region MODULE NodeContext
        public partial class ModuleContext : INodeContext<Module>
        {
            public Module Node { get; set; }
            public void ApplyContext()
            {
                this.SetNodeLocation();
                if (Node.FileName != null && Node.FileName.EndsWith(".mlj")) Node.TargetFormat = Module.TargetFormats.Json;
            }
        }
        #endregion


        #region DOCUMENT NodeContext
        public partial class Document_stmtContext : INodeContext<Document>
        {
            public Document Node { get; set; }
            public void ApplyContext()
            {
                this.SetNodeLocation();
                var id = DOCUMENT_ID();
                Node.IDInterval = new Interval(id.Symbol.StartIndex, id.Symbol.StopIndex);
            }
        }

        public partial class Scope_stmtContext : INodeContext<Scope>
        {
            public Stack<DOM.Node> NodeStack;
            public Scope Node { get; set; }
            public void ApplyContext()
            {
                this.SetNodeLocation();
                
                var id = SCOPE_ID();
                if (id != null)
                {
                    var el = NodeStack.Peek();

                    NodeContextExtensions.SetNodeLocation(el, start, stop);
                    var dot = MalinaParserListener.FindChar(id.Symbol.StartIndex, id.Symbol.StopIndex, id.Symbol.InputStream, '.');
                    if (dot > -1)
                    {
                        ((Element) el).IdInterval = new Interval(dot + 1, id.Symbol.StopIndex);
                        Node.IdInterval = new Interval(id.Symbol.StartIndex, dot - 1);
                    }
                    else
                    {
                        Node.IdInterval = new Interval(id.Symbol.StartIndex, id.Symbol.StopIndex);
                    }                    
                }                
            }

        }
        public partial class Scope_inlineContext : INodeContext<Scope>
        {
            public Stack<DOM.Node> NodeStack;
            public Scope Node { get; set; }
            public void ApplyContext()
            {
                this.SetNodeLocation();
                var id = SCOPE_ID();
                if (id != null)
                {
                    var el = NodeStack.Peek();

                    NodeContextExtensions.SetNodeLocation(el, start, stop);
                    var dot = MalinaParserListener.FindChar(id.Symbol.StartIndex, id.Symbol.StopIndex, id.Symbol.InputStream, '.');
                    if(dot > -1)
                    {
                        ((Element) el).IdInterval = new Interval(dot + 1, id.Symbol.StopIndex);
                        Node.IdInterval = new Interval(id.Symbol.StartIndex, dot - 1);
                    }
                    else
                    {
                        Node.IdInterval = new Interval(id.Symbol.StartIndex, id.Symbol.StopIndex);
                    }
                }
            }
        }
        #endregion

        public partial class Namespace_declaration_stmtContext : INodeContext<Namespace>
        {
            public Namespace Node { get; set; }
            public void ApplyContext()
            {
                this.SetNodeLocation();
                var id = NAMESPACE_ID();
                Node.IDInterval = new Interval(id.Symbol.StartIndex, id.Symbol.StopIndex);
            }
        }
        

        #region ALIAS_DEF NodeContext
        public partial class Alias_def_stmtContext : INodeContext<AliasDefinition>
        {
            public AliasDefinition Node { get; set; }

            public void ApplyContext()
            {
                this.SetNodeLocation();
                var id = ALIAS_DEF_ID();
                Node.IdInterval = new Interval( id.Symbol.StartIndex, id.Symbol.StopIndex);
                if(value() == null && block_inline() == null && ns_block() == null && block() == null) Node.ValueType = ValueType.EmptyObject;
            }
        }
        #endregion

        #region ATTRIBUTE NodeContext
        public partial class Attr_stmtContext : INodeContext<DOM.Antlr.Attribute>
        {
            public DOM.Antlr.Attribute Node { get; set; }
            
            public void ApplyContext()
            {
                this.SetNodeLocation();
                var id = ATTRIBUTE_ID();
                var mt = (MalinaToken) id.Symbol;
                Node.IdInterval = new Interval(mt.StartIndex, mt.StopIndex);
            }
        }


        public partial class Value_attr_inlineContext : INodeContext<DOM.Antlr.Attribute>
        {
            public DOM.Antlr.Attribute Node { get; set; }
            public void ApplyContext()
            {
                this.SetNodeLocation();
                var id = ATTRIBUTE_ID();
                var mt = (MalinaToken) id.Symbol;
                Node.IdInterval = new Interval(mt.StartIndex, mt.StopIndex);

            }
        }

        public partial class Empty_attr_inlineContext : INodeContext<DOM.Antlr.Attribute>
        {
            public DOM.Antlr.Attribute Node { get; set; }
            public void ApplyContext()
            {
                this.SetNodeLocation();
                var id = ATTRIBUTE_ID();
                var mt = (MalinaToken) id.Symbol;
                Node.IdInterval = new Interval(mt.StartIndex, mt.StopIndex);

            }
        }
        #endregion

        #region ELEMENT NodeContext

        #region STATEMENT Context
        public partial class Value_element_stmtContext : INodeContext<Element>
        {
            public Element Node { get; set; }

            public void ApplyContext()
            {
                this.SetNodeLocation();
                var id = ELEMENT_ID();
                var mt = (MalinaToken) id.Symbol;
                Node.IdInterval = new Interval(mt.StartIndex, mt.StopIndex);
            }
        }

        public partial class Empty_element_stmtContext : INodeContext<Element>
        {
            public Element Node { get; set; }

            public void ApplyContext()
            {
                this.SetNodeLocation();
                var id = ELEMENT_ID();
                var mt = (MalinaToken) id.Symbol;
                Node.IdInterval = new Interval(mt.StartIndex, mt.StopIndex);
                if (COLON() != null) Node.ValueType = ValueType.EmptyObject;
            }
        }

        public partial class Block_element_stmtContext : INodeContext<Element>
        {
            public Element Node { get; set; }

            public void ApplyContext()
            {
                this.SetNodeLocation();
                var id = ELEMENT_ID();
                var mt = (MalinaToken) id.Symbol;
                Node.IdInterval = new Interval(mt.StartIndex, mt.StopIndex);
            }
        }

        public partial class Hybrid_block_element_stmtContext : INodeContext<Element>
        {
            public Element Node { get; set; }

            public void ApplyContext()
            {
                this.SetNodeLocation();
                var id = ELEMENT_ID();
                var mt = (MalinaToken)id.Symbol;
                Node.IdInterval = new Interval(mt.StartIndex, mt.StopIndex);
            }
        }

        #endregion

        #region INLINE Context
        public partial class Value_element_inlineContext : INodeContext<Element>
        {
            public Element Node { get; set; }

            public void ApplyContext()
            {
                this.SetNodeLocation();
                var id = ELEMENT_ID();
                var mt = (MalinaToken) id.Symbol;
                Node.IdInterval = new Interval(mt.StartIndex, mt.StopIndex);
            }

        }

        public partial class Empty_element_inlineContext : INodeContext<Element>
        {
            public Element Node { get; set; }

            public void ApplyContext()
            {
                this.SetNodeLocation();
                var id = ELEMENT_ID();
                var mt = (MalinaToken) id.Symbol;
                Node.IdInterval = new Interval(mt.StartIndex, mt.StopIndex);
                if (COLON() != null) Node.ValueType = ValueType.EmptyObject;
            }

        }

        public partial class Block_element_inlineContext : INodeContext<Element>
        {
            public Element Node { get; set; }

            public void ApplyContext()
            {
                this.SetNodeLocation();
                var id = ELEMENT_ID();
                var mt = (MalinaToken) id.Symbol;
                Node.IdInterval = new Interval(mt.StartIndex, mt.StopIndex);
            }

        }
        #endregion

        #endregion

        #region ARRAY_ITEM Context
        #region STATEMENT Context
        public partial class Value_array_item_stmtContext : INodeContext<Element>
        {
            public Element Node { get; set; }

            public void ApplyContext()
            {
                this.SetNodeLocation();
                Node.IdInterval = new Interval(1, 0);
            }
        }

        public partial class Block_array_item_stmtContext : INodeContext<Element>
        {
            public Element Node { get; set; }

            public void ApplyContext()
            {
                this.SetNodeLocation();
                Node.IdInterval = new Interval(1, 0);
            }
        }

        public partial class Hybrid_block_array_item_stmtContext : INodeContext<Element>
        {
            public Element Node { get; set; }

            public void ApplyContext()
            {
                this.SetNodeLocation();
                Node.IdInterval = new Interval(1, 0);
            }
        }

        public partial class Empty_array_item_stmtContext : INodeContext<Element>
        {
            public Element Node { get; set; }

            public void ApplyContext()
            {
                this.SetNodeLocation();
                Node.IdInterval = new Interval(1, 0);
            }
        }

        #endregion
        #region INLINE Context
        public partial class Value_array_item_inlineContext : INodeContext<Element>
        {
            public Element Node { get; set; }

            public void ApplyContext()
            {
                this.SetNodeLocation();
                Node.IdInterval = new Interval(1, 0);
            }

        }
        public partial class Block_array_item_inlineContext : INodeContext<Element>
        {
            public Element Node { get; set; }

            public void ApplyContext()
            {
                this.SetNodeLocation();
                Node.IdInterval = new Interval(1, 0);
            }
        }

        public partial class Empty_array_inlineContext : INodeContext<Element>
        {
            public Element Node { get; set; }

            public void ApplyContext()
            {
                this.SetNodeLocation();
                Node.IdInterval = new Interval(1, 0);
            }
        }
        #endregion
        #endregion


        #region PARAMETER NodeContext
        #region STATEMENT Context
        public partial class Empty_parameter_stmtContext : INodeContext<Parameter>
        {
            public Parameter Node { get; set; }

            public void ApplyContext()
            {
                this.SetNodeLocation();
                var id = PARAMETER_ID();
                Node.IDInterval = new Interval(id.Symbol.StartIndex, id.Symbol.StopIndex);
                if (COLON() != null) Node.ValueType = ValueType.EmptyObject;
            }
        }

        public partial class Value_parameter_stmtContext : INodeContext<Parameter>
        {
            public Parameter Node { get; set; }

            public void ApplyContext()
            {
                this.SetNodeLocation();
                var id = PARAMETER_ID();
                Node.IDInterval = new Interval(id.Symbol.StartIndex, id.Symbol.StopIndex);
            }
        }

        public partial class Hybrid_block_parameter_stmtContext : INodeContext<Parameter>
        {
            public Parameter Node { get; set; }

            public void ApplyContext()
            {
                this.SetNodeLocation();
                var id = PARAMETER_ID();
                Node.IDInterval = new Interval(id.Symbol.StartIndex, id.Symbol.StopIndex);
            }
        }

        public partial class Block_parameter_stmtContext : INodeContext<Parameter>
        {
            public Parameter Node { get; set; }

            public void ApplyContext()
            {
                this.SetNodeLocation();
                var id = PARAMETER_ID();
                Node.IDInterval = new Interval(id.Symbol.StartIndex, id.Symbol.StopIndex);
            }
        }
        #endregion

        #region INLINE Context
        public partial class Empty_parameter_inlineContext : INodeContext<Parameter>
        {
            public Parameter Node { get; set; }

            public void ApplyContext()
            {
                this.SetNodeLocation();
                var id = PARAMETER_ID();
                Node.IDInterval = new Interval(id.Symbol.StartIndex, id.Symbol.StopIndex);
                if (COLON() != null) Node.ValueType = ValueType.EmptyObject;
            }
        }

        public partial class Value_parameter_inlineContext : INodeContext<Parameter>
        {
            public Parameter Node { get; set; }

            public void ApplyContext()
            {
                this.SetNodeLocation();
                var id = PARAMETER_ID();
                Node.IDInterval = new Interval(id.Symbol.StartIndex, id.Symbol.StopIndex);

            }
        }

        public partial class Block_parameter_inlineContext : INodeContext<Parameter>
        {
            public Parameter Node { get; set; }

            public void ApplyContext()
            {
                this.SetNodeLocation();
                var id = PARAMETER_ID();
                Node.IDInterval = new Interval(id.Symbol.StartIndex, id.Symbol.StopIndex);
            }
        }

        #endregion

        #endregion

        #region ALIAS NodeContext
        #region STATEMENT Context
        public partial class Empty_alias_stmtContext : INodeContext<Alias>
        {
            public Alias Node { get; set; }

            public void ApplyContext()
            {
                this.SetNodeLocation();
                var id = ALIAS_ID();
                Node.IdInterval = new Interval(id.Symbol.StartIndex, id.Symbol.StopIndex);
                if (COLON() != null) Node.ValueType = ValueType.EmptyObject;
            }
        }

        public partial class Block_alias_stmtContext : INodeContext<Alias>
        {
            public Alias Node { get; set; }

            public void ApplyContext()
            {
                this.SetNodeLocation();
                var id = ALIAS_ID();
                Node.IdInterval = new Interval(id.Symbol.StartIndex, id.Symbol.StopIndex);
            }
        }
        public partial class Hybrid_block_alias_stmtContext : INodeContext<Alias>
        {
            public Alias Node { get; set; }

            public void ApplyContext()
            {
                this.SetNodeLocation();
                var id = ALIAS_ID();
                Node.IdInterval = new Interval(id.Symbol.StartIndex, id.Symbol.StopIndex);
            }
        }
        

        public partial class Value_alias_stmtContext : INodeContext<Alias>
        {
            public Alias Node { get; set; }

            public void ApplyContext()
            {
                this.SetNodeLocation();
                var id = ALIAS_ID();
                Node.IdInterval = new Interval(id.Symbol.StartIndex, id.Symbol.StopIndex);

            }
        }
        #endregion

        #region INLINE Context
        public partial class Empty_alias_inlineContext : INodeContext<Alias>
        {
            public Alias Node { get; set; }

            public void ApplyContext()
            {
                this.SetNodeLocation();
                var id = ALIAS_ID();
                Node.IdInterval = new Interval(id.Symbol.StartIndex, id.Symbol.StopIndex);
                if (COLON() != null) Node.ValueType = ValueType.EmptyObject;
            }
        }

        public partial class Block_alias_inlineContext : INodeContext<Alias>
        {
            public Alias Node { get; set; }

            public void ApplyContext()
            {
                this.SetNodeLocation();
                var id = ALIAS_ID();
                Node.IdInterval = new Interval(id.Symbol.StartIndex, id.Symbol.StopIndex);
            }
        }

        public partial class Value_alias_inlineContext : INodeContext<Alias>
        {
            public Alias Node { get; set; }

            public void ApplyContext()
            {
                this.SetNodeLocation();
                var id = ALIAS_ID();
                Node.IdInterval = new Interval(id.Symbol.StartIndex, id.Symbol.StopIndex);
            }
        }
        #endregion
        #endregion


        #region ARGUMENT NodeContext
        #region STATEMENT Context
        public partial class Empty_argument_stmtContext : INodeContext<Argument>
        {
            public Argument Node { get; set; }

            public void ApplyContext()
            {
                this.SetNodeLocation();
                var id = ARGUMENT_ID();
                Node.IDInterval = new Interval(id.Symbol.StartIndex, id.Symbol.StopIndex);
                if (COLON() != null) Node.ValueType = ValueType.EmptyObject;
            }
        }

        public partial class Block_argument_stmtContext : INodeContext<Argument>
        {
            public Argument Node { get; set; }

            public void ApplyContext()
            {
                this.SetNodeLocation();
                var id = ARGUMENT_ID();
                Node.IDInterval = new Interval(id.Symbol.StartIndex, id.Symbol.StopIndex);
            }
        }

        public partial class Value_argument_stmtContext : INodeContext<Argument>
        {
            public Argument Node { get; set; }

            public void ApplyContext()
            {
                this.SetNodeLocation();
                var id = ARGUMENT_ID();
                Node.IDInterval = new Interval(id.Symbol.StartIndex, id.Symbol.StopIndex);
            }
        }

        public partial class Hybrid_block_argument_stmtContext : INodeContext<Argument>
        {
            public Argument Node { get; set; }

            public void ApplyContext()
            {
                this.SetNodeLocation();
                var id = ARGUMENT_ID();
                Node.IDInterval = new Interval(id.Symbol.StartIndex, id.Symbol.StopIndex);
            }
        }
        #endregion

        #region INLINE Context
        public partial class Empty_argument_inlineContext : INodeContext<Argument>
        {
            public Argument Node { get; set; }

            public void ApplyContext()
            {
                this.SetNodeLocation();
                var id = ARGUMENT_ID();
                Node.IDInterval = new Interval(id.Symbol.StartIndex, id.Symbol.StopIndex);
                if (COLON() != null) Node.ValueType = ValueType.EmptyObject;
            }
        }

        public partial class Block_argument_inlineContext : INodeContext<Argument>
        {
            public Argument Node { get; set; }

            public void ApplyContext()
            {
                this.SetNodeLocation();
                var id = ARGUMENT_ID();
                Node.IDInterval = new Interval(id.Symbol.StartIndex, id.Symbol.StopIndex);
            }
        }

        public partial class Value_argument_inlineContext : INodeContext<Argument>
        {
            public Argument Node { get; set; }

            public void ApplyContext()
            {
                this.SetNodeLocation();
                var id = ARGUMENT_ID();
                Node.IDInterval = new Interval(id.Symbol.StartIndex, id.Symbol.StopIndex);
            }
        }
        #endregion
        #endregion



    }
}
