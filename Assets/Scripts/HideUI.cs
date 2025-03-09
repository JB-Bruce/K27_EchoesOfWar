using System.Collections.Generic;
using UnityEngine;

public class HideUI : MonoBehaviour
{
    public List<GameObject> uiList;

    bool isEnabled = true;

    private void Start()
    {
        SetUI(isEnabled);
    }
    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Comma)))
        {
            isEnabled = !isEnabled;
            SetUI(isEnabled);
        }
    }

    public void SetUI(bool active)
    {
        foreach (GameObject go in uiList)
        {
            go.SetActive(active);
        }
    }
}
