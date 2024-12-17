using System.Collections.Generic;
using UnityEngine;

public class BreakdownManager : MonoBehaviour
{
    [SerializeField] private List<MonoBehaviour> _breakdownCastersMonoBehaviours = new();
    [SerializeField] private List<MonoBehaviour> _breakdownReceiversMonoBehaviours = new();
    
    private readonly List<IBreakdownReceiver> _breakdownReceivers = new();

    private void Start()
    {
        foreach (var breakdownCaster in _breakdownCastersMonoBehaviours)
        {
            breakdownCaster.GetComponent<IBreakdownCaster>().OnBreakDown.AddListener(BreakSomething);
        }

        foreach (var breakdownReceiver in _breakdownReceiversMonoBehaviours)
        {
            _breakdownReceivers.Add(breakdownReceiver.GetComponent<IBreakdownReceiver>());
        }
    }

    private void BreakSomething()
    {
        List<IBreakdownReceiver> breakdownReceivers = new(_breakdownReceivers);

        for (int i = 0; i < breakdownReceivers.Count; i++)
        {
            int index = Random.Range(0, breakdownReceivers.Count);
            IBreakdownReceiver breakdownReceiver = breakdownReceivers[index];
                        
            if (breakdownReceiver.IsBroken)
            {
                breakdownReceivers.RemoveAt(index);
            }
            else
            {
                breakdownReceiver.IsBroken = true;
                breakdownReceiver.Break();
                return;
            }
        }
    }
}