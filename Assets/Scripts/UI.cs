using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private void Update()
    {
        if(Input.GetMouseButton(0))
            _text.gameObject.SetActive(false);
    }
}
