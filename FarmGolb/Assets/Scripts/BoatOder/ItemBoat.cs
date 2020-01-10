using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemBoat : MonoBehaviour
{
    public int idItem;
    private int idProduct;

    Image icon;
    Text number;

    // Use this for initialization
    void Start()
    {
        icon = transform.GetChild(0).GetComponent<Image>();
        number = transform.GetChild(1).GetComponent<Text>();
        loadItemBoat();
    }

    void loadItemBoat()
    {
        if (!PlayerPrefs.HasKey("itemBoat" + idItem))
        {
            idProduct = Oder.Instance.randomIdVp();
            PlayerPrefs.SetInt("itemBoat" + idItem, idProduct);
            PlayerPrefs.SetInt("numberProduct" + idItem, Random.Range(1, 10));
        }
        else
        {
            idProduct = PlayerPrefs.GetInt("itemBoat" + idItem);
        }
        icon.sprite = GameManager.Instance.dataStorage.dataStorages[idProduct].icon;
        number.text = PlayerPrefs.GetInt("numberProduct" + idItem).ToString();
    }


}
