using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Map_Editor.GameData
{
    public class Bonus : IObject
    {
        public BonusType type;

        public enum BonusType
        {
            Shield,
            EMP,
            Implosion,
            Ricochet,
            Missile,
            Double,
            Speed,
            Power,
            Decoy,
            Dash
        }

        public Bonus(BonusType _type)
        {
            type = _type;
        }
    }
}
