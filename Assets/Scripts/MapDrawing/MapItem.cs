using UnityEngine;

public class MapItem : ItemScript
{
    [SerializeField] Rigidbody _rb;

    public void SetPosition(Transform t)
    {
        _rb.isKinematic = true;

        transform.parent = t;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        if (_outline)
            _outline.enabled = false;
    }
}
