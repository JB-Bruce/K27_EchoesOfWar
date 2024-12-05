public interface IInteractable
{
    void Interact();

    bool DoesNeedToStopPlayerMovement { get; }
}