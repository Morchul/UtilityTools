public interface IAction
{
    public bool IsActive();
    public bool IsInactive();
    public ActionState State { get; set;}
}

public enum ActionState
{
    WAITING = 0,
    ACTIVE = 1,
    INTERRUPTED = 2,
    FINISHED = 3,
    SLEEPING = 4,
    INACTIVE = 5,
}
