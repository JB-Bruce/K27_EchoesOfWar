using UnityEngine;

public class BoutonMenu : MonoBehaviour
{
    public GameObject _MenuActif;
    public GameObject _MenuInactif1;
    public GameObject _MenuInactif2;
    
    public void ChangeMenu()
    {
        
        _MenuInactif1.SetActive(false);
        _MenuInactif2.SetActive(false);
        _MenuActif.SetActive(true);
    } 
}
