using Submarine;
using UnityEngine;

public class SubmarineController : MonoBehaviour
{
    [SerializeField] private SubmarineBody _submarineBody;
    [SerializeField] private ThrusterLever _thrusterLever;
    public float rotateForce = 1f;

    private void Update()
    {
        _submarineBody.SetThrust(_thrusterLever.GetThrust());
        
        if (Input.GetKey(KeyCode.Q))
            _submarineBody.Rotate(-1 * rotateForce);
        else if (Input.GetKey(KeyCode.D))
            _submarineBody.Rotate(1 * rotateForce);
    }
}
