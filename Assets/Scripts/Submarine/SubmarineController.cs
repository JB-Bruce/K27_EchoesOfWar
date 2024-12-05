using UnityEngine;

public class SubmarineController : MonoBehaviour
{
    [SerializeField] private SubmarineBody _submarineBody;
    [SerializeField] private ThrusterLever _thrusterLever;
    [SerializeField] private Rudder _rudder;

    public void SetMovement(float direction)
    {
        _thrusterLever.SetMovement(direction);
        _submarineBody.SetThrust(_thrusterLever.GetThrust());
    }

    public void Rotate(float direction)
    {
        _rudder.SetRotation(direction);
        _submarineBody.Rotate(_rudder.Angle * Time.deltaTime);
    }
}
