using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class EmergenceSystem : MonoBehaviour
{
    [SerializeField] private SubmarineButton _emergenceButton;
    [SerializeField] private Color _canEmergenceColor = Color.green;
    [SerializeField] private Color _canNotEmergenceColor = Color.red;
    
    [Header("Unlock")]
    [SerializeField] private BoardManager _boardManager;
    private bool _isCodeDiscovered;

    [FormerlySerializedAs("emergenceText")]
    [Header("")]
    [SerializeField] private TextMeshProUGUI _emergenceText;
    [SerializeField] private string goodPositionText = "Good position";
    [SerializeField] private string wrongPositionText = "Wrong position";
    
    private readonly UnityEvent _onEmergenceButtonPressed = new();
    [SerializeField] private Animator _animator;


    private void Start()
    {
        _emergenceButton.OnButtonPressed.AddListener(Emergence);
        _emergenceButton.CanBePressed = () =>_isCodeDiscovered;
        _boardManager.OnCodeDiscovered.AddListener(OnCodeDiscovered);
        
    }

    private void Emergence()
    {
        if (_isCodeDiscovered)
        {
            _onEmergenceButtonPressed.Invoke();
            _emergenceButton.SetCanBePressed(() => { return false; });
            Debug.Log("Emergence");
        }
    }

    public void IsAtGoodPosition(bool isAtGoodPosition)
    {
        //_emergenceText.text = isAtGoodPosition ? goodPositionText : wrongPositionText;
        //_emergenceText.color = isAtGoodPosition ? _canEmergenceColor : _canNotEmergenceColor;
    }

    private void OnCodeDiscovered()
    {
        if (_isCodeDiscovered) return;
        _isCodeDiscovered = true;
        _animator.Play("Open");
    }
    
    public UnityEvent OnEmergenceButtonPressed => _onEmergenceButtonPressed;
}
