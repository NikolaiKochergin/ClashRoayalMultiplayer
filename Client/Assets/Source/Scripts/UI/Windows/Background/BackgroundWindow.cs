namespace Source.Scripts.UI.Windows.Background
{
    public class BackgroundWindow : WindowBase
    {
        protected override void Initialize() => 
            transform.SetAsFirstSibling();
    }
}