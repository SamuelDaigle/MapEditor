using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Map_Editor.GameData
{
    public class GameObject
    {
        [XmlIgnore]
        public BonusType bonusType;
        [XmlIgnore]
        public UtilType utilType;
        [XmlIgnore]
        public TrapType trapType;

        public string Bonus
        {
            get
            {
                return bonusType.ToString();
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    bonusType = default(BonusType);
                }
                else
                {
                    bonusType = (BonusType)Enum.Parse(typeof(BonusType), value);
                }
            }
        }

        public string Utilities
        {
            get
            {
                return utilType.ToString();
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    utilType = default(UtilType);
                }
                else
                {
                    utilType = (UtilType)Enum.Parse(typeof(UtilType), value);
                }
            }
        }

        public string Trap
        {
            get
            {
                return trapType.ToString();
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    trapType = default(TrapType);
                }
                else
                {
                    trapType = (TrapType)Enum.Parse(typeof(TrapType), value);
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
            [Description("../../Resources/Tile/Empty.png")]
            Turret,
            [Description("../../Resources/Object/Turret.png")]
            Spike,
            [Description("../../Resources/Object/Spikes.png")]
            Fire,
            [Description("../../Resources/Object/Fire.png")]
            Freeze,
            [Description("../../Resources/Object/Freeze.png")]
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

        public static string ToDescriptionString(TrapType val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }

        public static string ToDescriptionString(BonusType val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }

        public static string ToDescriptionString(UtilType val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }

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
        public void OnObjectChanged(EventArgs e)
        {
            if (ObjectChanged != null)
            {
                ObjectChanged(this, e);
            }
        }
    }
}
