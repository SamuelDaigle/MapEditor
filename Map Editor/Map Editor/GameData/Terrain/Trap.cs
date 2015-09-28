using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Map_Editor.GameData
{
    public class Trap : IObject
    {
        public TrapType type;

        public enum TrapType
        {
            Turret,
            Spike,
            Fire,
            Freeze,
            Drop
        }

        public Trap(TrapType _type)
        {
            type = _type;
        }
    }
}
