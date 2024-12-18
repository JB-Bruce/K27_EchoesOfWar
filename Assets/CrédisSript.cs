using UnityEngine;

public class CrédisSript : MonoBehaviour
{
    [SerializeField] GameObject _oneCrédis ;
    [SerializeField] GameObject _OffCrédis;

   public void Crédis()
   {

        _OffCrédis.SetActive(false);
        _oneCrédis.SetActive(true);
   }
}
