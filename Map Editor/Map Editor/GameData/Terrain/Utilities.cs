using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Map_Editor.GameData
{
    public class Utilities : IObject
    {
        public UtilType type;

        public enum UtilType
        {
            Spawn,
            Goal,
            Trigger,
            Jump
        }

        public Utilities(UtilType _type)
        {
            type = _type;
        }
    }
}
