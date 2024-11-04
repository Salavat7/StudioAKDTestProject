using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PickableItem : MonoBehaviour
{
    private const float SPEED_OF_ITEM = 3;
    private bool _picked;
    private Rigidbody _rigidbody;
    private Vector3 _pickedPosOnPicker;
    
    private void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if(_picked)
        {
            _rigidbody.useGravity = false;
            _rigidbody.velocity = SPEED_OF_ITEM * _pickedPosOnPicker / _rigidbody.mass;
        }
        else
        {
            _rigidbody.useGravity = true;
        }
    }

    public void Picked(Vector3 pickedPosOnPicker)
    {
        _pickedPosOnPicker = pickedPosOnPicker;
        _picked = true;
    }

    public void Dropped()
    {
        _picked = false;
    }
}
