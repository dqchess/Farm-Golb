using UnityEngine.UI;
using UnityEngine;

public class Market : MonoBehaviour
{
    public static Market instance;
    public GameObject[] spMua;

    int[] idSp;

    int slSp, slDamua;
    public Text txtName;
    int coin, slmua;

    private void Start()
    {
        instance = this;
        slSp = 3;
        slDamua = 0;
        idSp = new int[10];
        initMarket();
        Language.Instance.setNameText("Chợ", "Market", txtName);
    }

    public void initMarket()
    {
        int level = PlayerPrefs.GetInt("level");

        if (8 <= level && level < 18)
        {
            slSp = 4;
        }
        else if (18 <= level && level < 28)
        {
            slSp = 5;
        }
        else if (28 <= level && level < 38)
        {
            slSp = 6;
        }
        else if (38 <= level && level < 48)
        {
            slSp = 7;
        }
        else if (48 <= level && level < 58)
        {
            slSp = 8;
        }
        else if (58 <= level && level < 68)
        {
            slSp = 9;
        }
        else if (level >= 68)
        {
            slSp = 10;
        }

        for (int i = 0; i < slSp; i++)
        {
            randomItemMarket(i);
        }
    }

    int idNs;
    void randomItemMarket(int i)
    {
        idNs = Oder.Instance.randomIdVp();
        set(true, i);
        idSp[i] = idNs;
        spMua[i].GetComponent<ButtonMarket>().id = i;
        setImage(i, GameManager.Instance.dataStorage.dataStorages[idNs].icon, GameManager.Instance.dataStorage.dataStorages[idNs].coinBuy, GameManager.Instance.dataStorage.dataStorages[idNs].slThuH);
    }

    void setImage(int i, Sprite icon, int tienmua, int slthuH)
    {
        spMua[i].transform.GetChild(1).GetComponent<Image>().sprite = icon;
        spMua[i].transform.GetChild(3).GetComponent<Text>().text = (tienmua * slthuH).ToString();
        spMua[i].transform.GetChild(4).GetComponent<Text>().text = slthuH.ToString();
        spMua[i].transform.GetChild(1).GetComponent<Image>().SetNativeSize();
    }

    public void buySp(int index)
    {
        if (slSp > index)
        {
            idNs = idSp[index];
            coin = GameManager.Instance.dataStorage.dataStorages[idNs].coinBuy;
            slmua = GameManager.Instance.dataStorage.dataStorages[idNs].slThuH;
            setBuy(coin * slmua, slmua, index, 0);
        }
    }

    bool checkStorage;
    void setBuy(int coin, int slmua, int index, int loai)
    {
        if (PlayerPrefs.GetInt("resource" + 0) - coin >= 0)
        {
            if (idNs < 26)
            {
                checkStorage = StorageController.Instance.checkIsFullStorage(0, idNs, slmua);
            }
            else
            {
                checkStorage = StorageController.Instance.checkIsFullStorage(1, idNs, slmua);
            }

            if (checkStorage)
            {
                slDamua++;
                ResourceManager.Instance.minus(0, coin);
                set(false, index);
                Oder.Instance.showOder();
                if (slDamua == slSp)
                {
                    initMarket();
                    slDamua = 0;
                }
            }
        }
        else
        {
            Language.Instance.notifyEngOrVi("You no enough coin", "Không đủ tiền");
        }
    }

    public void close()
    {
        GameManager.Instance.cameraOnOff(false);
        gameObject.SetActive(false);
    }

    public void set(bool bl, int index)
    {
        spMua[index].transform.GetChild(1).gameObject.SetActive(bl);
        spMua[index].transform.GetChild(3).gameObject.SetActive(bl);
        spMua[index].transform.GetChild(4).gameObject.SetActive(bl);
        spMua[index].GetComponent<Button>().enabled = bl;
    }
}