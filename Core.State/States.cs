namespace Core.StateMachine;

public static class States
{
    public enum ChatState
    {
        Uninitialized,
        Connecting,
        Connected,
        Disconnected
    }

    public enum SystemState
    {
        Idle,
        Busy,
        Error
    }

    public enum BatteryState
    {
        Full,
        Low,
        Critical
    }
}