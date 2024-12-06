public interface IinteractableCanEnd : IInteractable
{
    public bool DoesNeedToStopAllMovement { get; }
    void Uninteract();
}