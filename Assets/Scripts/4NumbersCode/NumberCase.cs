using UnityEngine;
using TMPro;

public class NumberCase : MonoBehaviour
{
    [SerializeField] public int _currentNumber;
    [SerializeField] public int _correctNumber;
    [SerializeField] public TextMeshProUGUI _text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentNumber = Random.Range(0, 9);
    }
    void Update()
    {
        _text.text = _currentNumber.ToString();
    }
}
