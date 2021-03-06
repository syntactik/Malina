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
using Malina.DOM;
using Newtonsoft.Json;
using Attribute = Malina.DOM.Attribute;
using ValueType = Malina.DOM.ValueType;

namespace Malina.Compiler.Generator
{
    public class JsonGenerator : AliasResolvingVisitor
    {
        public enum BlockState
        {
            Object, //Json block is object
            Array   //Json block is array
        }

        private readonly Func<string, JsonWriter> _writerDelegate;

        private JsonWriter _jsonWriter;
        private bool _blockStart;
        private Stack<BlockState> _blockState;


        /// <summary>
        /// This constructor should be used if output depends on the name of the document.
        /// </summary>
        /// <param name="writerDelegate">Delegate will be called for the each Document. The name of the document will be sent in the string argument.</param>
        /// <param name="context"></param>
        public JsonGenerator(Func<string, JsonWriter> writerDelegate, CompilerContext context):base(context)
        {
            _writerDelegate = writerDelegate;
            _context = context;
        }

        public override void OnDocument(Document node)
        {
            _currentDocument = node;

            using (_jsonWriter = _writerDelegate(node.Name))
            {
                _blockStart = true;
                _blockState = new Stack<BlockState>();
                base.OnDocument(node);

                if (_blockState.Count > 0)
                {
                    if (_blockState.Pop() == BlockState.Array)
                        _jsonWriter.WriteEndArray();
                    else
                        _jsonWriter.WriteEndObject();
                }
                //Empty document. Writing an empty object as a value.
                else
                {
                    _jsonWriter.WriteStartObject();
                    _jsonWriter.WriteEndObject();
                }
            }
            _currentDocument = null;
        }
        
        public override void OnValue(string value, ValueType type)
        {
            if (type == ValueType.Null)
            {
                _jsonWriter.WriteNull();
                return;
            }

            bool boolValue;
            if (type == ValueType.Boolean && bool.TryParse(value, out boolValue))
            {
                _jsonWriter.WriteValue(boolValue);
            }
            else
            {
                decimal numberValue;
                if (type == ValueType.Number && decimal.TryParse(value, out numberValue))
                {
                    if (numberValue % 1 == 0)
                        _jsonWriter.WriteValue((long) numberValue);
                    else
                        _jsonWriter.WriteValue(numberValue);
                }
                    
                else
                    _jsonWriter.WriteValue(value);
            }
        }

        public override void OnAttribute(Attribute node)
        {
            CheckBlockStart(node);

            _jsonWriter.WritePropertyName((node.NsPrefix != null ? node.NsPrefix + "." : "") + node.Name);
            ResolveValue(node);
        }

        public override void OnElement(Element node)
        {
            CheckBlockStart(node);

            if (!string.IsNullOrEmpty(node.Name))
                _jsonWriter.WritePropertyName((node.NsPrefix != null ? node.NsPrefix + "." : "") + node.Name);

            if (ResolveValue(node)) return; //Block has value therefore it has no block.

            //Working with node's block
            _blockStart = true;
            var prevBlockStateCount = _blockState.Count;

            base.OnElement(node);

            _blockStart = false;

            if (_blockState.Count > prevBlockStateCount)
            {
                if (_blockState.Pop() == BlockState.Array)
                    _jsonWriter.WriteEndArray();
                else
                    _jsonWriter.WriteEndObject();
                return;
            }

            //Element hase nor block no value. Writing an empty object as a value.
            if (!string.IsNullOrEmpty(node.Name) || node.ValueType == ValueType.EmptyObject)
            {
                _jsonWriter.WriteStartObject();
                _jsonWriter.WriteEndObject();
                //return;
            }
        }

        private void CheckBlockStart(Node node)
        {
            if (!_blockStart) return;

            //This element is the first element of the block. It decides if the block is array or object
            if (string.IsNullOrEmpty(node.Name))
            {
                _jsonWriter.WriteStartArray(); //start array
                _blockState.Push(BlockState.Array);
            }
            else
            {
                _jsonWriter.WriteStartObject(); //start array
                _blockState.Push(BlockState.Object);
            }
            _blockStart = false;
        }


    }
}
