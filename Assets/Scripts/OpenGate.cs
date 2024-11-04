using System.Collections;
using UnityEngine;

public class OpenGate : MonoBehaviour
{
    private const float SPEED_OF_OPENING = 0.05f;
    [SerializeField] private Transform _gate;
    [SerializeField] private Vector3 _moveTo;
    private Vector3 _startPos;
    private bool _shouldOpening;

    private void Start()
    {
        _startPos = _gate.position;
        _shouldOpening = false;
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
            _gate.Translate(Vector3.right * SPEED_OF_OPENING * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator Close()
    {
        while (_gate.position.x > _startPos.x)
        {
            _gate.Translate(Vector3.left * SPEED_OF_OPENING * Time.deltaTime);
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