public interface IElectricity
{
    bool hasElectricity { get; set; }
    
    void SwitchElectricity(bool HasElectricity);

    void EnableElectricity();
    void DisableElectricity();
}