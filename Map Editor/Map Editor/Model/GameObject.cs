using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Map_Editor.GameData
{
    /// <summary>
    /// Represents on top of a tile.
    /// </summary>
    public class GameObject
    {
        private BonusType _bonusType;
        private TrapType _trapType;
        private UtilType _utilType;

        [XmlIgnore]
        public BonusType bonusType
        {
            get { return _bonusType; }
            set
            {
                _bonusType = value;
                _utilType = UtilType.None;
                _trapType = TrapType.None;
                OnObjectChanged(EventArgs.Empty);
            }
        }
        [XmlIgnore]
        public UtilType utilType
        {
            get { return _utilType; }
            set
            {
                _utilType = value;
                _bonusType = BonusType.None;
                _trapType = TrapType.None;
                OnObjectChanged(EventArgs.Empty);
            }
        }
        [XmlIgnore]
        public TrapType trapType
        {
            get { return _trapType; }
            set
            {
                _trapType = value;
                _utilType = UtilType.None;
                _bonusType = BonusType.None;
                OnObjectChanged(EventArgs.Empty);
            }
        }

        [Browsable(false)]
        public string Bonus
        {
            get
            {
                return _bonusType.ToString();
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _bonusType = default(BonusType);
                }
                else
                {
                    _bonusType = (BonusType)Enum.Parse(typeof(BonusType), value);
                }
            }
        }

        [Browsable(false)]
        public string Utilities
        {
            get
            {
                return _utilType.ToString();
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _utilType = default(UtilType);
                }
                else
                {
                    _utilType = (UtilType)Enum.Parse(typeof(UtilType), value);
                }
            }
        }
        [Browsable(false)]
        public string Trap
        {
            get
            {
                return _trapType.ToString();
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _trapType = default(TrapType);
                }
                else
                {
                    _trapType = (TrapType)Enum.Parse(typeof(TrapType), value);
                }
            }
        }

        public event EventHandler ObjectChanged;

        public enum BonusType
        {
            None,
            [Description("../../Resources/Tile/Empty.png")]
            Shield,
            [Description("../../Resources/Tile/Bad.png")]
            EMP,
            [Description("../../Resources/Tile/Breaking.png")]
            Implosion,
            [Description("../../Resources/Tile/Door.png")]
            Ricochet,
            [Description("../../Resources/Tile/Floor.png")]
            Missile,
            [Description("../../Resources/Tile/Empty.png")]
            Double,
            [Description("../../Resources/Tile/Bad.png")]
            Speed,
            [Description("../../Resources/Tile/Breaking.png")]
            Power,
            [Description("../../Resources/Tile/Door.png")]
            Decoy,
            [Description("../../Resources/Tile/Floor.png")]
            Dash
        }

        public enum TrapType
        {
            None,
            [Description("../../Resources/Object/Turret.png")]
            Turret,
            [Description("../../Resources/Object/Spikes.png")]
            Spike,
            [Description("../../Resources/Object/Fire.png")]
            Fire,
            [Description("../../Resources/Object/Freeze.png")]
            Freeze,
            [Description("../../Resources/Object/LoseOP.png")]
            Drop
        }

        public enum UtilType
        {
            None,
            [Description("../../Resources/Object/Spawn.png")]
            Spawn,
            [Description("../../Resources/Object/Goal.png")]
            Goal,
            [Description("../../Resources/Object/Ball.png")]
            Ball,
            [Description("../../Resources/Tile/Empty.png")]
            Jump
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// In this case, returns the path of the selected Type.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            string result = "";
            string description = "";

            description = ToDescriptionString(trapType);
            if (description != "")
            {
                result = description;
            }

            description = ToDescriptionString(bonusType);
            if (description != "")
            {
                result = description;
            }

            description = ToDescriptionString(utilType);
            if (description != "")
            {
                result = description;
            }

            return result;
        }

        /// <summary>
        /// Gets the path of the trap type.
        /// </summary>
        /// <param name="val">The value.</param>
        /// <returns></returns>
        public static string ToDescriptionString(TrapType val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }

        /// <summary>
        /// Gets the path of the bonus type.
        /// </summary>
        /// <param name="val">The value.</param>
        /// <returns></returns>
        public static string ToDescriptionString(BonusType val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }

        /// <summary>
        /// Gets the path of the utilities type.
        /// </summary>
        /// <param name="val">The value.</param>
        /// <returns></returns>
        public static string ToDescriptionString(UtilType val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }

        /// <summary>
        /// Sets the object with the path.
        /// </summary>
        /// <param name="_path">The _path.</param>
        public void SetObject(string _path)
        {
            bool found = false;

            foreach (TrapType type in Enum.GetValues(typeof(TrapType)))
            {
                if (ToDescriptionString(type) == _path)
                {
                    trapType = type;
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                foreach (BonusType type in Enum.GetValues(typeof(BonusType)))
                {
                    if (ToDescriptionString(type) == _path)
                    {
                        bonusType = type;
                        found = true;
                        break;
                    }
                }
            }

            if (!found)
            {
                foreach (UtilType type in Enum.GetValues(typeof(UtilType)))
                {
                    if (ToDescriptionString(type) == _path)
                    {
                        utilType = type;
                        break;
                    }
                }
            }

            OnObjectChanged(EventArgs.Empty);
        }

        // Notify the tile.
        /// <summary>
        /// Raises the <see cref="E:ObjectChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public void OnObjectChanged(EventArgs e)
        {
            if (ObjectChanged != null)
            {
                ObjectChanged(this, e);
            }
        }
    }
}
