using System.Collections;
using UnityEngine;

public class OpenGate : MonoBehaviour
{
    [SerializeField] private Transform _gate;
    [SerializeField] private Vector3 _moveTo;

    private Vector3 _startPos;
    private float _speedOfOpening = 0.05f;
    private bool _shouldOpening = false;


    private void Start()
    {
        _startPos = _gate.position;
    }

    private void Update()
    {
        if(_shouldOpening)
            StartCoroutine(Open());
        else 
            StartCoroutine(Close());
    }

    private IEnumerator Open()
    {
        while (_gate.position.x < _startPos.x + 8)
        {
            _gate.Translate(Vector3.right * _speedOfOpening * Time.deltaTime);

            yield return null;
        }
    }

    private IEnumerator Close()
    {
        while (_gate.position.x > _startPos.x)
        {
            _gate.Translate(Vector3.left * _speedOfOpening * Time.deltaTime);

            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _shouldOpening = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _shouldOpening = false;
    }
}
