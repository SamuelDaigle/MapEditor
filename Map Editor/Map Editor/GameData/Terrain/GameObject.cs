using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Map_Editor.GameData
{
    public class GameObject
    {
        private BonusType bonusType;
        private UtilType utilType;
        private TrapType trapType;

        public event EventHandler ObjectChanged;

        public enum BonusType
        {
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
            [Description("../../Resources/Tile/Empty.png")]
            Turret,
            [Description("../../Resources/Tile/Bad.png")]
            Spike,
            [Description("../../Resources/Tile/Breaking.png")]
            Fire,
            [Description("../../Resources/Tile/Door.png")]
            Freeze,
            [Description("../../Resources/Tile/Floor.png")]
            Drop
        }

        public enum UtilType
        {
            [Description("../../Resources/Tile/Empty.png")]
            Spawn,
            [Description("../../Resources/Tile/Bad.png")]
            Goal,
            [Description("../../Resources/Tile/Breaking.png")]
            Trigger,
            [Description("../../Resources/Tile/Door.png")]
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
        private void OnObjectChanged(EventArgs e)
        {
            if (ObjectChanged != null)
            {
                ObjectChanged(this, e);
            }
        }
    }
}
