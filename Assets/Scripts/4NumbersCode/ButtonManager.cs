using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private List<Vector3> _indicatorPositions;
    [SerializeField] BoardManager _boardManager;
    [SerializeField] Transform _indicatorTransform;

    public void AddToCurrentNumber()
    {
        if (_boardManager._selectedNumberCase._currentNumber < 9)
        {
            _boardManager._selectedNumberCase._currentNumber++;
        } 
        else
        {
            _boardManager._selectedNumberCase._currentNumber = 0;
        }
    }

    public void SubstractToCurrentNumber()
    {
        if (_boardManager._selectedNumberCase._currentNumber > 0)
        {
            _boardManager._selectedNumberCase._currentNumber--;
        }
        else
        {
            _boardManager._selectedNumberCase._currentNumber = 9;
        }
    }

    public void GoesToTheLeftCase()
    {
        if (_boardManager._selectedNumberCaseIndex > 0)
        {
            _boardManager._selectedNumberCaseIndex--;
            _boardManager._selectedNumberCase = _boardManager._numberCases[_boardManager._selectedNumberCaseIndex];
        }
        else
        {
            _boardManager._selectedNumberCaseIndex = _boardManager._numberCases.Count - 1;
            _boardManager._selectedNumberCase = _boardManager._numberCases[_boardManager._selectedNumberCaseIndex];
        }
        _indicatorTransform.position = _indicatorPositions[_boardManager._selectedNumberCaseIndex];
    }

    public void GoesToTheRightCase()
    {
        if (_boardManager._selectedNumberCaseIndex < _boardManager._numberCases.Count - 1)
        {
            _boardManager._selectedNumberCaseIndex++;
            _boardManager._selectedNumberCase = _boardManager._numberCases[_boardManager._selectedNumberCaseIndex];
        }
        else
        {
            _boardManager._selectedNumberCaseIndex = 0;
            _boardManager._selectedNumberCase = _boardManager._numberCases[_boardManager._selectedNumberCaseIndex];
        }
        _indicatorTransform.localPosition = _indicatorPositions[_boardManager._selectedNumberCaseIndex];
    }

    public void CorrectCode()
    {

    }
}
