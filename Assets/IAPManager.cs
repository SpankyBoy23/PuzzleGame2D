using UnityEngine.UI;
using UnityEngine;

public class IAPManager : MonoBehaviour
{
    [SerializeField] Button btn;

    void Start()
    {
        btn.onClick.AddListener(Buy);
    }

    void Buy()
    {
        GetComponent<iapManager>().OnClick_BuyProduct();
    }
}
