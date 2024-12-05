using UnityEngine;

public class SubmarineController : MonoBehaviour
{
    [SerializeField] private SubmarineBody _submarineBody;
    [SerializeField] private ThrusterLever _thrusterLever;
    [SerializeField] private Rudder _rudder;
    public float rotateForce = 1f;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Z))
            Move(1);
        else if (Input.GetKey(KeyCode.S))
            Move(-1);
        
        if (Input.GetKey(KeyCode.Q))
            Rotate(-1);
        else if (Input.GetKey(KeyCode.D))
            Rotate(1);
    }

    public void Move(int direction)
    {
        _thrusterLever.Move(direction);
        _submarineBody.SetThrust(_thrusterLever.GetThrust());
    }

    public void Rotate(int direction)
    {
        _rudder.Rotate(direction);
        _submarineBody.Rotate(_rudder.Angle * Time.deltaTime);
    }
}
