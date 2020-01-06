using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class StorageController : MonoBehaviour
{
    //0: dataSilo,  1: dataBarn
    public int id;
    public static StorageController Instance;

    public GameObject itemVp;
    public Text nameNangCap;
    public Text nameSell;
    public Text slKho;
    public Transform content;
    //public RectTransform muiten;

    public List<GameObject> obSilo;
    public List<GameObject> obBarn;
    public GameObject dialogSell;
    int tongsoluong;
    public Text name2;

    void Start()
    {
        if (!PlayerPrefs.HasKey("storage" + 0))
        {
            PlayerPrefs.SetInt("storage" + 0, 6);
            PlayerPrefs.SetInt("storageMax" + 0, 50);
            PlayerPrefs.SetInt("storage" + 1, 6);
            PlayerPrefs.SetInt("storageMax" + 1, 50);
        }
        Instance = this;
        load();
        Language.Instance.setNameText("Nâng cấp", "Upgrade", nameNangCap);
        Language.Instance.setNameText("Bán", "Sell", nameSell);
        Language.Instance.setNameText("Bạn có thể có vật phẩm khi sản xuất ở nhà máy Quặng", "You can get items by the Ore factory production", name2);
        foreach (GameObject item in itemUpgrade)
        {
            Language.Instance.setNameText("Hoàn thành !", "Complete !", item.transform.GetChild(5).GetComponent<Text>());
        }
        bl = true;
    }

    public void load()
    {
        int index = 0;
        obSilo.Clear();
        obBarn.Clear();

        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }
        foreach (DataStorage.DataStorages item in GameManager.Instance.dataStorage.dataStorages)
        {
            if (item.levelUse <= PlayerPrefs.GetInt("level"))
            {
                itemVp.GetComponent<ItemStorage>().idVp = index;
                itemVp.transform.GetChild(0).GetComponent<Image>().sprite = item.icon;
                itemVp.transform.GetChild(0).GetComponent<Image>().SetNativeSize();
                itemVp.transform.GetChild(2).GetComponent<Text>().text = PlayerPrefs.GetInt("ns" + index).ToString();
                if (index < 26)
                {
                    obSilo.Add(Instantiate(itemVp, content));
                }
                else
                {
                    obBarn.Add(Instantiate(itemVp, content));
                }
            }
            index++;
        }
    }

    public void loadStorage(int idStorage)
    {
        //content.localPosition = new Vector2(306f, -5f);
        id = idStorage;
        tongsoluong = 0;
        string str;
        if (id == 0)
        {
            offBarn(obBarn);
            updateStorage(obSilo);
            str = tongsoluong + "/" + PlayerPrefs.GetInt("storageMax" + id);
            Language.Instance.setNameText("Nhà lương : " + str, "Silo : " + str, slKho);
        }
        else
        {
            offBarn(obSilo);
            updateStorage(obBarn);
            str = tongsoluong + "/" + PlayerPrefs.GetInt("storageMax" + id);
            Language.Instance.setNameText("Nhà kho : " + str, "Barn : " + str, slKho);
        }
        if (tongsoluong > PlayerPrefs.GetInt("storageMax" + id))
        {
            tongsoluong = PlayerPrefs.GetInt("storageMax" + id);
        }
        //muiten.localPosition = new Vector2(((float)tongsoluong / PlayerPrefs.GetInt("storageMax" + id) * 340 - 170), 210f);
    }

    void offBarn(List<GameObject> ob)
    {
        foreach (GameObject item in ob)
        {
            item.SetActive(false);
        }
    }

    void updateStorage(List<GameObject> ob)
    {
        int idVp;
        for (int i = 0; i < ob.Count; i++)
        {
            ob[i].SetActive(true);
            idVp = ob[i].GetComponent<ItemStorage>().idVp;
            ob[i].transform.GetChild(2).GetComponent<Text>().text = PlayerPrefs.GetInt("ns" + idVp).ToString();
            tongsoluong += PlayerPrefs.GetInt("ns" + idVp);
        }
    }

    //check storage
    public bool checkIsFullStorage(int idStorage, int idSp, int number)
    {
        if (PlayerPrefs.GetInt("storage" + idStorage) + number <= PlayerPrefs.GetInt("storageMax" + idStorage))
        {
            updateStorage(idStorage, idSp, number);
            randomItemUpgrade();
            return true;
        }
        else
        {
            Language.Instance.notifyEngOrVi("Storage is full", "Nhà chứa đã đầy");
        }
        return false;
    }

    //them san pham hoac tru di
    public void updateStorage(int idStorage, int idSp, int number)
    {
        PlayerPrefs.SetInt("storage" + idStorage, PlayerPrefs.GetInt("storage" + idStorage) + number);
        PlayerPrefs.SetInt("ns" + idSp, PlayerPrefs.GetInt("ns" + idSp) + number);
        //muiten.localPosition = new Vector2(((float)PlayerPrefs.GetInt("storage" + idStorage) / PlayerPrefs.GetInt("storageMax" + idStorage) * 340 - 170), 210f);
        Oder.Instance.showOder();
    }

    public void showSell(int idVp)
    {
        dialogSell.SetActive(true);
        dialogSell.GetComponent<SellItemStorage>().loadItem(idVp);
    }

    bool check;
    int numberUpgrade;
    public GameObject[] itemUpgrade;
    int count;
    public void showUpgrade()
    {
        if (!check)
        {
            count = 0;
            transform.GetChild(6).gameObject.SetActive(false);
            transform.GetChild(7).gameObject.SetActive(true);
            check = true;
            numberUpgrade = PlayerPrefs.GetInt("storageMax" + id) / 50;

            if (numberUpgrade > 10)
            {
                numberUpgrade = 10;
            }

            if (id == 0)
            {
                showItemUpgrade(0, 49);
                showItemUpgrade(1, 51);
                showItemUpgrade(2, 52);
            }
            else
            {
                showItemUpgrade(0, 50);
                showItemUpgrade(1, 51);
                showItemUpgrade(2, 53);
            }
        }
        else
        {
            if (count == 3)
            {
                if (id == 0)
                {
                    updateStorage(1, 49, -numberUpgrade);
                    updateStorage(1, 51, -numberUpgrade);
                    updateStorage(1, 52, -numberUpgrade);
                }
                else
                {
                    updateStorage(1, 50, -numberUpgrade);
                    updateStorage(1, 51, -numberUpgrade);
                    updateStorage(1, 53, -numberUpgrade);
                }
                PlayerPrefs.SetInt("storageMax" + id, PlayerPrefs.GetInt("storageMax" + id) + 50);
                Language.Instance.notifyEngOrVi("You have successfully upgraded", "Bạn đã nâng cấp thành công");
            }
            else
            {
                Language.Instance.notifyEngOrVi("No enough resource", "Không đủ vật phẩm để bạn nâng cấp");
            }
            close();
        }
    }

    void showItemUpgrade(int i, int idUpgrade)
    {
        itemUpgrade[i].transform.GetChild(0).GetComponent<Image>().sprite = GameManager.Instance.dataStorage.dataStorages[idUpgrade].icon;
        itemUpgrade[i].transform.GetChild(0).GetComponent<Image>().SetNativeSize();
        itemUpgrade[i].transform.GetChild(3).GetComponent<Text>().text = numberUpgrade.ToString();
        itemUpgrade[i].transform.GetChild(1).GetComponent<Text>().text = PlayerPrefs.GetInt("ns" + idUpgrade).ToString();
        if (PlayerPrefs.GetInt("ns" + idUpgrade) >= numberUpgrade)
        {
            count++;
            itemUpgrade[i].transform.GetChild(4).gameObject.SetActive(false);
            itemUpgrade[i].transform.GetChild(5).gameObject.SetActive(true);
            itemUpgrade[i].transform.GetChild(1).GetComponent<Text>().color = new Color32(82, 38, 0, 255);
        }
        else
        {
            itemUpgrade[i].transform.GetChild(4).gameObject.SetActive(true);
            itemUpgrade[i].transform.GetChild(5).gameObject.SetActive(false);
            itemUpgrade[i].transform.GetChild(1).GetComponent<Text>().color = Color.red;
            itemUpgrade[i].transform.GetChild(4).GetChild(0).GetComponent<Text>().text = (numberUpgrade - PlayerPrefs.GetInt("ns" + idUpgrade)).ToString();
        }
    }
    //silo go mieng, sat mieng, dinh
    //49  51 52
    //barn go thanh, sat mieng, oc vit
    //50  51 53
    //go mieng
    int idItemUpgrade;
    public void diamondUpgrade1()
    {
        if (id == 0)
        {
            idItemUpgrade = 49;
        }
        else
        {
            idItemUpgrade = 50;
        }
        checkDiamond(idItemUpgrade, 0);
    }
    public void diamondUpgrade2()
    {
        checkDiamond(51, 1);
    }
    public void diamondUpgrade3()
    {
        if (id == 0)
        {
            idItemUpgrade = 52;
        }
        else
        {
            idItemUpgrade = 53;
        }
        checkDiamond(idItemUpgrade, 2);
    }

    void checkDiamond(int idItemUpgrade, int index)
    {
        if (ResourceManager.Instance.checkResource(1, numberUpgrade - PlayerPrefs.GetInt("ns" + idItemUpgrade)))
        {
            count++;
            PlayerPrefs.SetInt("ns" + idItemUpgrade, numberUpgrade);
            PlayerPrefs.SetInt("storage" + 1, PlayerPrefs.GetInt("storage" + 1) + numberUpgrade);
            itemUpgrade[index].transform.GetChild(1).GetComponent<Text>().text = numberUpgrade.ToString();
            itemUpgrade[index].transform.GetChild(1).GetComponent<Text>().color = new Color32(82, 38, 0, 255);
            itemUpgrade[index].transform.GetChild(4).gameObject.SetActive(false);
            itemUpgrade[index].transform.GetChild(5).gameObject.SetActive(true);
        }
    }

    public void close()
    {
        transform.GetChild(6).gameObject.SetActive(true);
        transform.GetChild(7).gameObject.SetActive(false);
        check = false;
        transform.localScale = Vector3.zero;
        GameManager.Instance.cameraOnOff(false);
    }

    int checkRandom, idRandom;
    bool bl;
    public void randomItemUpgrade()
    {
        if (++checkRandom > 30 && bl)
        {
            bl = false;
            idRandom = Random.Range(0, 3);
            if (idRandom != 0)
            {
                idRandom = Random.Range(49, 54);
            }
            else
            {
                idRandom = Random.Range(91, 93);
            }
            updateStorage(1, idRandom, 1);
            FlyHarvest.Instance.iconFlyBay(GameManager.Instance.dataStorage.dataStorages[idRandom].icon, Camera.main.transform.position, 1, 4);
            StartCoroutine(statusCheckRandom());
        }
    }

    IEnumerator statusCheckRandom()
    {
        yield return new WaitForSeconds(5);
        checkRandom = 0;
        bl = true;
    }
}