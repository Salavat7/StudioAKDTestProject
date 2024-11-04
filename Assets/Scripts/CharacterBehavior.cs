using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterBehavior : MonoBehaviour
{
    private const float MAX_DISTANCE_TO_PICK = 3;
    private const float MAX_ANGLE_OF_CAMERA_ROTATION = 50;
    [SerializeField] private Camera _camera;
    [SerializeField] private Vector3 _offsetOfCamera;
    [SerializeField] private float _playerSpeed;
    private float _moveForward;
    private float _moveRight;
    private Vector3 _angleOfCameraRotation;
    private Rigidbody _playerRb;
    private GameObject _pickedObj;

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

        if (angleOfCameraRotation.x < -MAX_ANGLE_OF_CAMERA_ROTATION)
            angleOfCameraRotation.x = -MAX_ANGLE_OF_CAMERA_ROTATION;

        if (angleOfCameraRotation.x > MAX_ANGLE_OF_CAMERA_ROTATION)
            angleOfCameraRotation.x = MAX_ANGLE_OF_CAMERA_ROTATION;

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

    public void Pick(bool pick)
    {
        if (pick)
        {
            Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            Debug.DrawRay(ray.origin, ray.direction, Color.red);
            RaycastHit hit;

            bool isHit = Physics.Raycast(ray, out hit, MAX_DISTANCE_TO_PICK, LayerMask.GetMask("Pickable"));

            if (isHit && _pickedObj == null)
                _pickedObj = hit.transform.gameObject;

            if (_pickedObj != null)
                if (Vector3.Distance(_pickedObj.transform.position, gameObject.transform.position) < MAX_DISTANCE_TO_PICK)
                {
                    Vector3 pickedPosOnPicker = _camera.transform.position + _camera.transform.forward * 2 + Vector3.up / 3 - _pickedObj.transform.position;
                    _pickedObj.GetComponent<PickableItem>().Picked(pickedPosOnPicker);
                }
                else
                {
                    _pickedObj.GetComponent<PickableItem>().Dropped();
                    _pickedObj = null;
                }
        }
        else
        {
            if (_pickedObj != null)
            {
                _pickedObj.GetComponent<PickableItem>().Dropped();
                _pickedObj = null;
            }
        }
    }
}