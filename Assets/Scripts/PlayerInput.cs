using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerInput : MonoBehaviour
{
    //const float MAX_ANGLE_OF_ROTATION = 40;
    const int PLAYER_SPEED = 10;

    [SerializeField] private Camera _camera;
    [SerializeField] private float _camSens;
    [SerializeField] private Vector3 _offSetOfCamera;

    private Rigidbody _playerRb;
    private Vector3 _lastMousePos;
    private float _lenghtOfRay = 2;
    private int _pickableLayer = 3;
    private GameObject _pickedObj;
    private Rigidbody _pickedItemRb;
    private float _maxPickableDistance = 3f;

    private void Start()
    {
        _playerRb = gameObject.GetComponent<Rigidbody>();
        _lastMousePos = Input.mousePosition;

        _pickedObj = null;
        _pickedItemRb = null;
    }

    private void FixedUpdate()
    {
        //Moovement of player
        float horizontalInput = Input.GetAxis("Horizontal");
        float vetricalInput = Input.GetAxis("Vertical");
        _playerRb.AddForce(_camera.transform.forward * vetricalInput * PLAYER_SPEED);
        _playerRb.AddForce(_camera.transform.right * horizontalInput * PLAYER_SPEED);

        //Moovement of camera
        _camera.transform.position = gameObject.transform.position + _offSetOfCamera;

        //Rotation of camera
        _lastMousePos = Input.mousePosition - _lastMousePos;
        _lastMousePos = new Vector3(-_lastMousePos.y * _camSens, _lastMousePos.x * _camSens, 0);
        _lastMousePos = new Vector3(_camera.transform.eulerAngles.x + _lastMousePos.x, _camera.transform.eulerAngles.y + _lastMousePos.y, 0);

        //Debug.Log(_lastMousePos.x);

        if (_lastMousePos.x > 40 && _lastMousePos.x < 300)
            _lastMousePos = new Vector3(40, _lastMousePos.y, 0);
        else if (_lastMousePos.x < 320 && _lastMousePos.x > 40)
            _lastMousePos = new Vector3(320, _lastMousePos.y, 0);

        _camera.transform.eulerAngles = _lastMousePos;
        _lastMousePos = Input.mousePosition;


        //Pickup items
        if (Input.GetMouseButton(0))
        {
            Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            Debug.DrawRay(ray.origin, ray.direction * _lenghtOfRay, Color.red);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
                if (hit.transform.gameObject.layer == _pickableLayer)
                    _pickedObj = hit.transform.gameObject;

            if (_pickedObj != null)
            {
                _pickedItemRb = _pickedObj.GetComponent<Rigidbody>();


                if (Vector3.Distance(gameObject.transform.position, _pickedObj.transform.position) < _maxPickableDistance)
                {
                    _pickedItemRb.useGravity = false;
                    Vector3 pickedPosOnPlayer = _camera.transform.position + _camera.transform.forward * 2 + Vector3.up / 3 - _pickedObj.transform.position;
                    _pickedItemRb.velocity = pickedPosOnPlayer / _pickedItemRb.mass;
                }
                else
                {
                    _pickedItemRb.useGravity = true;
                }

                //Debug.Log("PICKED");
            }
        }
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (_pickedObj != null)
            {
                _pickedItemRb.useGravity = true;
                _pickedObj = null;
            }
        }
    }
}
