﻿using System;

namespace Malina.DOM
{
    [Serializable]
    public class Element : Entity, IValueNode, INsNode
    {
        // Fields
        private NodeCollection<Attribute> _attributes;
        private NodeCollection<Entity> _entities;
        private string _nsPrefix;
        private string _value;
        private object _objectValue;
        private ValueType _valueType;

        // Methods

        public override void Accept(IDomVisitor visitor)
        {
            visitor.OnElement(this);
        }

        public override void AppendChild(Node child)
        {
            if (child is Attribute)
            {
                Attributes.Add((Attribute)child);
            }
            else if (child is Entity)
            {
                Entities.Add((Entity)child);
            }
            else
            {
                base.AppendChild(child);
            }
        }
        
        // Properties

        public virtual string Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }
        public NodeCollection<Attribute> Attributes => _attributes ?? (_attributes = new NodeCollection<Attribute>(this));

        public NodeCollection<Entity> Entities
        {
            get { return _entities ?? (_entities = new NodeCollection<Entity>(this)); }
            set
            {
                if (value == _entities) return;

                value?.InitializeParent(this);
                _entities = value;
            }
        }

        public virtual object ObjectValue
        {
            get
            {
                return _objectValue;
            }

            set
            {
                _objectValue = value;
            }
        }

        public ValueType ValueType
        {
            get
            {
                return _valueType;
            }

            set
            {
                _valueType = value;
            }
        }

        public virtual string NsPrefix
        {
            get
            {
                return _nsPrefix;
            }

            set
            {
                _nsPrefix = value;
            }
        }

        public bool IsValueNode => _valueType != ValueType.None;
    }


}