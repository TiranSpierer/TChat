using Core.LoggerExtensions;
using Serilog;
using Stateless;
using static Core.StateMachine.States;
using static Core.StateMachine.Triggers;

namespace Core.StateMachine;

public class MyStateMachine
{
    private readonly StateMachine<ChatState, ChatTrigger>       _chatMachine;
    private readonly StateMachine<SystemState, SystemTrigger>   _systemMachine;
    private readonly StateMachine<BatteryState, BatteryTrigger> _batteryMachine;
    private readonly ILogger _logger;

    public MyStateMachine(ILogger _logger)
    {
        _chatMachine    = new StateMachine<ChatState, ChatTrigger>(ChatState.Uninitialized);
        _systemMachine  = new StateMachine<SystemState, SystemTrigger>(SystemState.Idle);
        _batteryMachine = new StateMachine<BatteryState, BatteryTrigger>(BatteryState.Full);

        ConfigureMachines();
        this._logger = _logger;
    }

    #region Public Properties

    public ChatState CurrentChatState => _chatMachine.State;

    public SystemState CurrentSystemState => _systemMachine.State;

    public BatteryState CurrentBatteryState => _batteryMachine.State;

    #endregion

    #region Public Methods

    public void TransitionToChatState(ChatTrigger trigger)
    {
        _logger.AddContext().Information($"ChatStateMachine Triggering '{trigger}'");
        _chatMachine.Fire(trigger);
    }

    public void TransitionToSystemState(SystemTrigger trigger)
    {
        _systemMachine.Fire(trigger);
    }

    public void TransitionToBatteryState(BatteryTrigger trigger)
    {
        _batteryMachine.Fire(trigger);
    }


    #endregion

    #region Private Methods

    private bool IsBatteryCharging()
    {
        return true;
    }

    private void ConnectToChatServer()
    {
    }

    private void DisconnectFromChatServer()
    {
    }

    private void StartTask()
    {
    }

    private void CompleteTask()
    {
    }

    private void HandleTaskError()
    {
    }

    private void SendLowBatteryNotification()
    {
    }

    private void CancelLowBatteryNotification()
    {
    }

    private void EnterPowerSaveMode()
    {
    }

    private void ExitPowerSaveMode()
    {
    }

    private void SendCriticalBatteryNotification()
    {
    }

    #endregion

    #region Configurations

    private void ConfigureMachines()
    {
        ConfigureChatMachine();
        ConfigureSystemMachine();
        ConfigureBatteryMachine();
    }

    private void ConfigureChatMachine()
    {
        _chatMachine.Configure(ChatState.Uninitialized)
                    .Permit(ChatTrigger.Initialize, ChatState.Connecting);

        _chatMachine.Configure(ChatState.Connecting)
                    .OnEntry(ConnectToChatServer)
                    .OnExit(DisconnectFromChatServer)
                    .Permit(ChatTrigger.Connect, ChatState.Connected)
                    .Permit(ChatTrigger.ConnectionFailed, ChatState.Disconnected);

        _chatMachine.Configure(ChatState.Connected)
                    .Permit(ChatTrigger.Disconnect, ChatState.Disconnected);

        _chatMachine.Configure(ChatState.Disconnected)
                    .Permit(ChatTrigger.Connect, ChatState.Connecting);
    }

    private void ConfigureSystemMachine()
    {
        _systemMachine.Configure(SystemState.Idle)
                      .Permit(SystemTrigger.StartTask, SystemState.Busy);

        _systemMachine.Configure(SystemState.Busy)
                      .OnEntry(StartTask)
                      .OnExit(CompleteTask)
                      .OnEntryFrom(SystemTrigger.TaskError, HandleTaskError)
                      .Permit(SystemTrigger.CompleteTask, SystemState.Idle)
                      .Permit(SystemTrigger.TaskError, SystemState.Error);

        _systemMachine.Configure(SystemState.Error)
                      .Permit(SystemTrigger.StartTask, SystemState.Busy);
    }

    private void ConfigureBatteryMachine()
    {
        _batteryMachine.Configure(BatteryState.Full)
                       .Permit(BatteryTrigger.BatteryLow, BatteryState.Low)
                       .Permit(BatteryTrigger.BatteryCritical, BatteryState.Critical);

        _batteryMachine.Configure(BatteryState.Low)
                       .OnEntry(SendLowBatteryNotification)
                       .OnExit(CancelLowBatteryNotification)
                       .OnEntryFrom(BatteryTrigger.BatteryCritical, EnterPowerSaveMode)
                       .OnExit(ExitPowerSaveMode)
                       .Permit(BatteryTrigger.BatteryCritical, BatteryState.Critical);

        _batteryMachine.Configure(BatteryState.Critical)
                       .OnEntry(SendCriticalBatteryNotification)
                       .OnEntryFrom(BatteryTrigger.BatteryLow, EnterPowerSaveMode)
                       .OnExit(ExitPowerSaveMode)
                       .Ignore(BatteryTrigger.BatteryCritical)
                       .PermitIf(BatteryTrigger.BatteryLow, BatteryState.Low, IsBatteryCharging);

        _batteryMachine.OnUnhandledTrigger((state, trigger) =>
        {
            _logger.Warning($"Unhandled trigger '{trigger}' in state '{state}'");
            TransitionToBatteryState(BatteryTrigger.BatteryCritical);
        });
    }

    #endregion

}
