using System;

namespace Source.Scripts.GameCore.UnitLogic
{
    public class Health : IHealth
    {
        public Health(float maxValue)
        {
            MaxValue = maxValue;
            CurrentValue = maxValue;
        }

        public float MaxValue { get; }
        public float CurrentValue { get; private set; }

        public event Action Changed;
        public event Action Died;

        public void ApplyDamage(float value)
        {
            CurrentValue -= value;
            if (CurrentValue <= 0)
            {
                CurrentValue = 0;
                Died?.Invoke();
            }
            
            Changed?.Invoke();
        }
    }
}