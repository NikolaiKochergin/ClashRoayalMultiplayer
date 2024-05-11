using System;
using System.Collections.Generic;
using Source.Scripts.GameCore.Battle.UnitLogic;

namespace Source.Scripts.GameCore.Battle.MapLogic
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