using UnityEngine;

namespace Source.Scripts.GameCore.MapLogic
{
    public class MapInfo : MonoBehaviour
    {
        [SerializeField] private ArmyInfo _player;
        [SerializeField] private ArmyInfo _enemy;

        public ArmyInfo Player => _player;
        public ArmyInfo Enemy => _enemy;
    }
}