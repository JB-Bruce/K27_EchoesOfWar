public interface IFinishedInteractable : IInteractable
{
    public bool doesStopMovements { get; }
    public bool doesLockView { get; }

    public bool canInteractWithOtherInteractablesWhileInteracted { get; }
    bool isInteracted { get; set; }
    void Uninteract() {}

}