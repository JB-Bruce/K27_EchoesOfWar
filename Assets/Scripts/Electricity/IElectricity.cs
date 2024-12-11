public interface IElectricity
{
    bool hasElectricity { get; set; }
    
    void Electricity(bool HasElectricity);

    void EnableElectricity();
    void DisableElectricity();
}