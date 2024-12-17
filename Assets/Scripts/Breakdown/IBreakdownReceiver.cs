public interface IBreakdownReceiver
{
    void Break();
    
    bool IsBroken { get; set; }
}