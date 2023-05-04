namespace Tests.StateMachine;

[TestFixture]
public class MyStateMachineTests
{
    private Mock<ILogger> _loggerMock;
    private MyStateMachine _stateMachine;

    [SetUp]
    public void SetUp()
    {
        _loggerMock = new Mock<ILogger>();
        _stateMachine = new MyStateMachine(_loggerMock.Object);
    }

    [Test]
    public void TransitionToChatState_ShouldChangeChatState()
    {
        // Arrange
        var trigger = ChatTrigger.Connect;

        // Act
        _stateMachine.TransitionToChatState(trigger);

        // Assert
        Assert.That(_stateMachine.CurrentChatState, Is.EqualTo(ChatState.Connected));
    }

    [Test]
    public void TransitionToSystemState_ShouldChangeSystemState()
    {
        // Arrange
        var trigger = SystemTrigger.StartTask;

        // Act
        _stateMachine.TransitionToSystemState(trigger);

        // Assert
        Assert.That(_stateMachine.CurrentSystemState, Is.EqualTo(SystemState.Busy));
    }

    [Test]
    public void TransitionToBatteryState_ShouldChangeBatteryState()
    {
        // Arrange
        var trigger = BatteryTrigger.BatteryLow;

        // Act
        _stateMachine.TransitionToBatteryState(trigger);

        // Assert
        Assert.That(_stateMachine.CurrentBatteryState, Is.EqualTo(BatteryState.Low));
    }
}
