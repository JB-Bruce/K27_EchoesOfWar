using System.Collections.Generic;
using UnityEngine;

public class DoorSwitchEnigma : MonoBehaviour
{
    [SerializeField] bool[] solution;
    [SerializeField] List<DoorSwitch> switchs;

    [SerializeField] Door linkedDoor;

    private void Start()
    {
        foreach (var item in switchs)
        {
            item.Init(Random.Range(0, 2) == 0 ? true : false, OnSwitchChanged);
        }

        linkedDoor.SetLocked(true);
    }

    private void OnSwitchChanged()
    {
        for (int i = 0; i < switchs.Count; i++)
        {
            if (switchs[i].isOn != solution[i])
            {
                linkedDoor.SetLocked(true);
                return;
            }
        }

        linkedDoor.SetLocked(false);
    }
}
