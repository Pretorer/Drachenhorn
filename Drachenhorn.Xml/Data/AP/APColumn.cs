﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml.Serialization;

namespace Drachenhorn.Xml.Data.AP
{
    /// <summary>
    /// The Column of an AP-Table
    /// </summary>
    /// <seealso cref="Drachenhorn.Xml.ChildChangedBase" />
    [Serializable]
    public class APColumn : ChildChangedBase
    {
        #region Properties

        [XmlIgnore]
        private string _name;
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [XmlAttribute("Name")]
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value)
                    return;
                _name = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private ushort _factor;
        /// <summary>
        /// Gets or sets the factor.
        /// </summary>
        /// <value>
        /// The factor.
        /// </value>
        [XmlAttribute("Factor")]
        public ushort Factor
        {
            get { return _factor; }
            set
            {
                if (_factor == value)
                    return;
                _factor = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private ushort _negative;
        /// <summary>
        /// Gets or sets the negative.
        /// </summary>
        /// <value>
        /// The negative.
        /// </value>
        [XmlAttribute("Negative")]
        public ushort Negative
        {
            get { return _negative; }
            set
            {
                if (_negative == value)
                    return;
                _negative = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private ObservableCollection<APValue> _costs = new ObservableCollection<APValue>();
        /// <summary>
        /// Gets or sets the costs.
        /// </summary>
        /// <value>
        /// The costs.
        /// </value>
        [XmlElement("Cost")]
        public ObservableCollection<APValue> Costs
        {
            get { return _costs; }
            set
            {
                if (_costs == value)
                    return;
                _costs = value;
                OnPropertyChanged();
            }
        }

        #endregion Properites

        #region c'tor

        /// <summary>
        /// Initializes a new instance of the <see cref="APColumn"/> class.
        /// </summary>
        public APColumn() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="APColumn"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="factor">The factor.</param>
        /// <param name="negative">The negative.</param>
        public APColumn(string name, ushort factor, ushort negative)
        {
            Name = name;
            Factor = factor;
            Negative = negative;
        }

        #endregion c'tor

        #region Calculation

        /// <summary>
        /// Calculates the costs.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <returns></returns>
        public uint CalculateCosts(int from, int to)
        {
            uint result = 0;
            int start = from;

            if (from < 0)
            {
                result += Negative * (uint)(from * -1);
                start = 0;
            }


            for (int i = start; i <= to; ++i)
            {
                if (i < Costs.Count)
                    result += Costs[i].Value;
                else
                    result += Costs[Costs.Count - 1].Value;
            }

            return result;
        }

        #endregion Calculation


        #region Misc

        /// <summary>
        /// Add new Value to the Column
        /// </summary>
        /// <param name="value">Value to be added.</param>
        public void Add(ushort value)
        {
            this.Costs.Add(new APValue(value));
        }

        #endregion Misc
    }
}
