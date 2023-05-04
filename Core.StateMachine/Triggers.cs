namespace Core.StateMachine;

public class Triggers
{
    public enum ChatTrigger
    {
        Initialize,
        Connect,
        ConnectionFailed,
        Disconnect
    }

    public enum SystemTrigger
    {
        StartTask,
        CompleteTask,
        TaskError
    }

    public enum BatteryTrigger
    {
        BatteryLow,
        BatteryCritical
    }
}