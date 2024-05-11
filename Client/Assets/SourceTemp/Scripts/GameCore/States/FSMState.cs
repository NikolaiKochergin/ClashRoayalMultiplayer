namespace Source.Scripts.GameCore.States
{
    public abstract class FSMState
    {
        public virtual void Enter() { }
        public virtual void Update() { }
        public virtual void Exit() { }
    }
}