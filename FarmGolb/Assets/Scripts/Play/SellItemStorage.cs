using UnityEngine;
using UnityEngine.UI;

public class SellItemStorage : MonoBehaviour
{
    public int idVp;
    public Text nameVp;
    public Text description;
    public Text number;
    public Text coin;
    public Image icon;
    DataStorage.DataStorages item;
    int numberSell;

    public void loadItem(int idVp)
    {
        this.idVp = idVp;
        item = GameManager.Instance.dataStorage.dataStorages[idVp];
        icon.sprite = item.icon;
        icon.SetNativeSize();
        Language.Instance.setNameText(item.nameVi, item.nameEng, nameVp);
        Language.Instance.setNameText(item.describeVi, item.describeEng, description);
        numberSell = PlayerPrefs.GetInt("ns" + idVp);
        if (numberSell == 0)
        {
            number.text = "X0";
        }
        else if (numberSell == 1)
        {
            number.text = "X1";
        }
        else
        {
            number.text = "X" + numberSell / 2;
            numberSell = numberSell / 2;
        }
        coin.text = (numberSell * item.coinSell).ToString();
    }

    public void plus()
    {
        if (numberSell + 1 <= PlayerPrefs.GetInt("ns" + idVp))
        {
            number.text = "X" + ++numberSell;
            coin.text = (numberSell * item.coinSell).ToString();
        }
    }

    public void minus()
    {
        if (numberSell > 1)
        {
            number.text = "X" + --numberSell;
            coin.text = (numberSell * item.coinSell).ToString();
        }
    }

    public void sell()
    {
        if (numberSell > 0)
        {
            GameManager.Instance.cameraOnOff(false);
            transform.parent.transform.localScale = Vector3.zero;
            gameObject.SetActive(false);
            StorageController.Instance.updateStorage(StorageController.Instance.id, idVp, -numberSell);
            FlyHarvest.Instance.iconFlyBay(null, transform.position, int.Parse(coin.text), 1);
        }
        else
        {
            Language.Instance.notifyEngOrVi("Not enough resources", "Không đủ vật phẩm");
        }
    }

    public void close()
    {
        gameObject.SetActive(false);
    }
}
