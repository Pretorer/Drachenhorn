﻿using System;
using System.Xml.Serialization;
using Drachenhorn.Xml.Sheet.InventoryInfo;

namespace Drachenhorn.Xml.Sheet.CombatInfo
{
    /// <summary>
    /// Weapons that can be used by a Character
    /// </summary>
    /// <seealso cref="Drachenhorn.Xml.Sheet.InventoryInfo.InventoryItem" />
    [Serializable]
    public class Weapon : InventoryItem
    {
        #region Properties

        [XmlIgnore]
        private int _handicap;
        /// <summary>
        /// Gets or sets the Handicap.
        /// </summary>
        /// <value>
        /// The Handicap.
        /// </value>
        [XmlAttribute("Handicap")]
        public int Handicap
        {
            get { return _handicap; }
            set
            {
                if (_handicap == value)
                    return;
                _handicap = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private DamageType _type;
        /// <summary>
        /// Gets or sets the WeaponType.
        /// </summary>
        /// <value>
        /// The WeaponType.
        /// </value>
        [XmlAttribute("Type")]
        public DamageType Type
        {
            get { return _type; }
            set
            {
                if (_type == value)
                    return;
                _type = value;
                OnPropertyChanged();
            }
        }

        #endregion Properties
    }
}