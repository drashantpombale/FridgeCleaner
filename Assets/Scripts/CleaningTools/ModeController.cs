

public class ModeController : MonoSingletonGeneric<ModeController>
{

    public ResetModeEvent resetEvent = new ResetModeEvent();
    public enum GameMode
    {
        Default,
        SprayMode,
        CleanMode,
    }

    public GameMode currentMode { get; private set; }

    private void Start()
    {
        currentMode = GameMode.Default;
    }

    public void SprayMode()
    {
        ResetEvent();
        currentMode = GameMode.SprayMode;
    }

    public void CleanMode() 
    {
        ResetEvent();
        currentMode = GameMode.CleanMode;
    }

    public void ResetEvent() 
    {
        resetEvent.Invoke();
    }

    public void ResetMode() 
    {
        currentMode = GameMode.Default;
    }
}
