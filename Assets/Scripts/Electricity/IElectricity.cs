public interface IElectricity
{
    bool hasElectricity { get; set; }
    
    void Electricity(bool hasElectricity);

    void EnableElectricity();
    void DisableElectricity();
}