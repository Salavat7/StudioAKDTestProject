using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterBehavior : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Vector3 _offsetOfCamera;
    [SerializeField] private float _playerSpeed;
    private Vector3 _angleOfCameraRotation;
    private float _moveForward;
    private float _moveRight;
    private Rigidbody _playerRb;

    private void Start()
    {
        _playerRb = gameObject.GetComponent<Rigidbody>();
        _angleOfCameraRotation = _camera.transform.eulerAngles;
        _moveForward = 0;
        _moveRight = 0;
    }

    private void FixedUpdate()
    {
        _camera.transform.position = gameObject.transform.position + _offsetOfCamera;
        _camera.transform.eulerAngles = _angleOfCameraRotation;
        _playerRb.AddForce(_camera.transform.forward * _playerSpeed * _moveForward);
        _playerRb.AddForce(_camera.transform.right * _playerSpeed * _moveRight);
    }

    public void SetRotationOfCamera(Vector3 angleOfCameraRotation)
    {
        angleOfCameraRotation = new Vector3(_angleOfCameraRotation.x + angleOfCameraRotation.x, _angleOfCameraRotation.y + angleOfCameraRotation.y, 0);

        if(angleOfCameraRotation.x < -45)
            angleOfCameraRotation.x = -45;
        
        if(angleOfCameraRotation.x > 50)
            angleOfCameraRotation.x = 50;

        _angleOfCameraRotation = angleOfCameraRotation;
    }

    public void SetForwardMoving(float moveForward)
    {
        _moveForward = moveForward;
    }

    public void SetRightMoving(float moveRright)
    {
        _moveRight = moveRright;
    }
}
