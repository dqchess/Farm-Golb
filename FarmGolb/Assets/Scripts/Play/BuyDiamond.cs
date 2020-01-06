using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyDiamond : MonoBehaviour
{
    public List<GameObject> listUserDiamond;
    public Text txtsl;
    public Text slbt, name1, name2;
    int sl;
    GameObject ob;
    public Image icon;

    private void OnEnable()
    {
        GameManager.Instance.cameraOnOff(true);
        listUserDiamond = GameManager.Instance.listUserDiamond;
        sl = listUserDiamond.Count;
        txtsl.text = sl.ToString();
        if (Language.Instance.checkLanguage)
        {
            slbt.text = "Buy for " + sl;
        }
        else
        {
            slbt.text = "Mua với " + sl;
        }
    }

    private void Start()
    {
        Language.Instance.setNameText("Không đủ tài nguyên!", "Not enough resource !", name1);
        Language.Instance.setNameText("Bạn có muốn mua vật phẩm\n còn thiếu bằng kim cương không?", "Do you want to buy missing\n items with diamonds ? ", name2);
    }

    public void buyDiamond()
    {
        //AnimClick.Instance.clickUI(gameObject);
        if (ResourceManager.Instance.checkResource(1, sl))
        {
            for (int i = 0; i < listUserDiamond.Count; i++)
            {
                ob = listUserDiamond[i];
                if (ob.GetComponent<Field>())
                {
                    ob.GetComponent<Field>().buyDiamond();
                }
                else if (ob.GetComponent<Animal>())
                {
                    ob.GetComponent<Animal>().buyDiamond();
                }
                else if (ob.GetComponent<Destroy>())
                {
                    ob.GetComponent<Destroy>().buyDiamond();
                }
                else if (ob.GetComponent<MachineController>())
                {
                    ob.GetComponent<MachineController>().producingItem();
                }
            }
        }
        else
        {
            huy();
        }
        GameManager.Instance.listUserDiamond.Clear();
        gameObject.SetActive(false);
    }

    public void close()
    {
        huy();
        GameManager.Instance.cameraOnOff(false);
        GameManager.Instance.listUserDiamond.Clear();
        gameObject.SetActive(false);
    }

    void huy()
    {
        for (int i = 0; i < listUserDiamond.Count; i++)
        {
            ob = listUserDiamond[i];
            if (ob.GetComponent<Field>())
            {
                ob.GetComponent<Field>().sprCayTrong.sprite = null;
            }
        }
    }

    public void setImage(int id)
    {
        icon.sprite = GameManager.Instance.dataStorage.dataStorages[id].icon;
        icon.SetNativeSize();
    }
}