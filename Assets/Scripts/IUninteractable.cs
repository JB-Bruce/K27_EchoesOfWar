public interface IUninteractable : IInteractable
{
    public bool DoesNeedToStopAllMovement { get; }
    void Uninteract();
}