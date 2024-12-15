using System.Collections.Generic;
using UnityEngine;

public class DoorSwitchEnigma : MonoBehaviour, IBreakdownReceiver
{
    [SerializeField] bool[] solution;
    [SerializeField] List<DoorSwitch> frontSwitchs;
    [SerializeField] List<DoorSwitch> backSwitchs;

    [SerializeField] Door linkedDoor;

    public bool _isBroken;
    bool IBreakdownReceiver.IsBroken { get => _isBroken; set => _isBroken = value; }

    private void Start()
    {
        for (int i = 0; i < frontSwitchs.Count; i++)
        {
            frontSwitchs[i].Init(OnFrontSwitchChanged);
            backSwitchs[i].Init(OnBackSwitchChanged);
        }

        ResetSwitchs();
    }

    private void ResetSwitchs()
    {
        for (int i = 0; i < frontSwitchs.Count; i++)
        {
            bool b = Random.Range(0, 2) == 0 ? true : false;
            frontSwitchs[i].SetActivation(b);
            backSwitchs[i].SetActivation(b);
        }

        linkedDoor.SetLocked(true);
    }

    private void OnFrontSwitchChanged()
    {
        for (int i = 0; i < frontSwitchs.Count; i++)
        {
            if(backSwitchs[i].isOn != frontSwitchs[i].isOn)
                backSwitchs[i].SetActivation(frontSwitchs[i].isOn);
        }

        for (int i = 0; i < frontSwitchs.Count; i++)
        {
            if (frontSwitchs[i].isOn != solution[i])
            {
                linkedDoor.SetLocked(true);
                return;
            }
        }

        linkedDoor.SetLocked(false);
    }

    private void OnBackSwitchChanged()
    {
        for (int i = 0; i < backSwitchs.Count; i++)
        {
            if (backSwitchs[i].isOn != frontSwitchs[i].isOn)
                frontSwitchs[i].SetActivation(backSwitchs[i].isOn);
        }

        for (int i = 0; i < backSwitchs.Count; i++)
        {
            if (backSwitchs[i].isOn != solution[i])
            {
                linkedDoor.SetLocked(true);
                return;
            }
        }

        linkedDoor.SetLocked(false);
    }

    public void Break()
    {
        ResetSwitchs();
    }
}
