public interface IBreakdownReceiver
{
    void Break();

    void Repair();
    
    bool IsBroken { get; set; }
}