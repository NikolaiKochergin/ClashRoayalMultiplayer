using System;
using System.Collections.Generic;
using Source.Scripts.GameCore.UnitLogic;

namespace Source.Scripts.GameCore.MapLogic
{
    [Serializable]
    public class ArmyInfo
    {
        public List<Tower> _towers;
        public List<UnitBase> _units;

        public IReadOnlyList<Tower> Towers => _towers;
        public IReadOnlyList<UnitBase> Units => _units;
    }
}