﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Xml.Serialization;
using Antlr.Runtime;

namespace Drachenhorn.Xml.Data.AP
{
    /// <summary>
    /// Value for AP-Columns
    /// </summary>
    public class APValue : ChildChangedBase, ISerializable
    {
        #region Properties
        
        private ushort _value;
        /// <summary>
        /// AP-Value
        /// </summary>
        public ushort Value
        {
            get { return _value; }
            set
            {
                if (_value == value)
                    return;
                _value = value;
                OnPropertyChanged();
            }
        }

        #endregion Properties


        #region c'tor

        /// <inheritdoc />
        public APValue() { }

        /// <inheritdoc />
        public APValue(ushort value)
        {
            Value = value;
        }

        #endregion c'tor


        #region Interface

        /// <inheritdoc />
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("SerializationInfo can not be null.");

            info.AddValue("Value", this.ToString(), typeof(string));
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Value.ToString();
        }

        #endregion Interface
    }
}
