namespace Source.Scripts.UI.Windows.LockScreen
{
    public class LockScreenWindow : WindowBase
    {
        protected void Start() => 
            transform.SetAsLastSibling();
    }
}