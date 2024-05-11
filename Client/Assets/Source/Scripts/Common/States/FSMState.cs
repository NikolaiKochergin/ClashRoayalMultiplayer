namespace Source.Scripts.Common.States
{
    public abstract class FSMState
    {
        public virtual void Enter() { }
        public virtual void Update() { }
        public virtual void Exit() { }
    }
}