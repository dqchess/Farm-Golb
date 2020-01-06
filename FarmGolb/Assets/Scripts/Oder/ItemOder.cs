using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Linq;

public class ItemOder : MonoBehaviour, IPointerDownHandler
{
    public static ItemOder Instance;
    public int id;
    public int timeLive;

    //id Vp Sell
    public List<int> idnsBan;
    int idVp;

    public GameObject showNameWait;
    public Text minutes, second;
    public GameObject showOder;

    //bien check du nguyen lieu chua
    public bool checkSell;
    public GameObject btnSell;
    public GameObject btnDelete;
    public Text exp, coin;

    int coin1, exp1;

    public string strIdVp, nameCustomerVi, nameCustomerEng;

    public Text nameCustomer;
    public Text nameOder;

    //so luong vat phẩm bán
    int maxNumberSell;

    void Start()
    {
        Instance = this;
        initOder();
        if (id == 0)
        {
            Language.Instance.setNameText("Đơn hàng", "Oder", nameOder);
        }
    }

    void initOder()
    {
        if (PlayerPrefs.GetString(gameObject.name) == "")
        {
            maxNumberSell = PlayerPrefs.GetInt("level");
            if (maxNumberSell > 6)
            {
                maxNumberSell = 6;
            }
            //gioi thieu game thi random ve 1 het...

            maxNumberSell = UnityEngine.Random.Range(1, maxNumberSell);
            //random number sell item oder
            PlayerPrefs.SetInt("numberSell" + id, maxNumberSell);
            //random bao nhieu san pham ban
            maxNumberSell = UnityEngine.Random.Range(1, maxNumberSell);
            //random theo san pham
            for (int i = 0; i < maxNumberSell; i++)
            {
                idVp = Oder.Instance.randomIdVp();
                while (randomIdVp(idVp))
                {
                    idVp = Oder.Instance.randomIdVp();
                }
                idnsBan.Add(idVp);
                saveIdVp(idVp);
                showItem(idnsBan);
            }
        }
        else
        {
            idnsBan = Array.ConvertAll(PlayerPrefs.GetString(gameObject.name).Split(','), int.Parse).ToList();
            showItem(idnsBan);
        }
        Oder.Instance.showOder();
    }

    //random so ngau nhien khac nhau
    bool randomIdVp(int idvp)
    {
        if ((88 < idVp && idVp < 93) || (48 < idVp && idVp < 54))
        {
            return true;
        }
        else
        {
            for (int i = 0; i < idnsBan.Count; i++)
            {
                if (idvp == idnsBan[i])
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void saveIdVp(int idvp)
    {
        if (strIdVp == "")
        {
            strIdVp += idvp;
        }
        else
        {
            strIdVp += "," + idvp;
        }
        PlayerPrefs.SetString(gameObject.name, strIdVp);
    }

    //so luong vp ban cua tung loai
    int slBan;
    public void showItem(List<int> idns)
    {
        exp1 = 0;
        coin1 = 0;
        slBan = PlayerPrefs.GetInt("numberSell" + id);
        for (int i = 0; i < idns.Count; i++)
        {
            exp1 += GameManager.Instance.dataStorage.dataStorages[idns[i]].exp * slBan;
            coin1 += GameManager.Instance.dataStorage.dataStorages[idns[i]].coinSell * slBan;
        }
        exp.text = exp1.ToString();
        coin.text = coin1.ToString();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        AnimClick.Instance.animRatationOder(gameObject);
        AnimClick.Instance.clickUI(gameObject);
        loadItemOder();
        if (GameManager.Instance.guide)
        {
            Guide.Instance.tayChiOder.transform.localPosition = new Vector2(637f, -174f);
            Guide.Instance.tayChi.transform.position = new Vector2(100f, 100f);
            btnSell.GetComponent<Button>().enabled = true;
        }
    }

    public void loadItemOder()
    {
        Oder.Instance.itemOder[PlayerPrefs.GetInt("idOder")].GetComponent<ItemOder>().statusClick(false);
        PlayerPrefs.SetInt("idOder", id);
        statusClick(true);
        Language.Instance.setNameText(nameCustomerVi, nameCustomerEng, nameCustomer);
        if (timeLive <= 0)
        {
            showOder.SetActive(true);
            showNameWait.SetActive(false);
            showOder.transform.GetChild(1).GetComponent<Text>().text = exp1.ToString();
            showOder.transform.GetChild(2).GetComponent<Text>().text = coin1.ToString();

            foreach (GameObject ob in Oder.Instance.spShow)
            {
                ob.SetActive(false);
            }

            for (int i = 0; i < idnsBan.Count; i++)
            {
                Oder.Instance.spShow[i].SetActive(true);
                Oder.Instance.spShow[i].transform.GetChild(0).GetComponent<Text>().text = PlayerPrefs.GetInt("ns" + idnsBan[i]) + "/" + PlayerPrefs.GetInt("numberSell" + id);
                Oder.Instance.spShow[i].transform.GetChild(1).GetComponent<Image>().sprite = GameManager.Instance.dataStorage.dataStorages[idnsBan[i]].icon;
                Oder.Instance.spShow[i].transform.GetChild(1).GetComponent<Image>().SetNativeSize();
                if (PlayerPrefs.GetInt("ns" + idnsBan[i]) < PlayerPrefs.GetInt("numberSell" + id))
                {
                    Oder.Instance.spShow[i].transform.GetChild(2).gameObject.SetActive(false);
                }
                else
                {
                    Oder.Instance.spShow[i].transform.GetChild(2).gameObject.SetActive(true);
                }
            }

            if (Oder.Instance.checkBan)
            {
                UpdateOder.Instance.check = checkSell;
                UpdateOder.Instance.coin = coin1;
                UpdateOder.Instance.exp = exp1;
            }

            if (checkSell)
            {
                btnSell.GetComponent<Animator>().enabled = true;
            }
            else
            {
                btnSell.GetComponent<Animator>().enabled = false;
            }

            UpdateOder.Instance.itemOder = gameObject;
        }
        else
        {
            btnSell.GetComponent<Animator>().enabled = false;
            UpdateOder.Instance.check = false;
            UpdateOder.Instance.itemOder = null;
            showOder.SetActive(false);
            showNameWait.SetActive(true);
            Language.Instance.setNameText("Đơn hàng tiếp theo", "Waiting...", showNameWait.GetComponent<Text>());
        }
    }

    void statusClick(bool bl)
    {
        transform.GetChild(0).gameObject.SetActive(bl);
    }

    IEnumerator runTime()
    {
        yield return TimeManager.Instance.wait;
        if (timeLive > 0)
        {
            --timeLive;
            StartCoroutine(runTime());
            minutes.text = (timeLive / 60).ToString();
            second.text = (timeLive % 60).ToString();
        }
        else
        {
            initOder();
            Oder.Instance.showOder();
            transform.GetChild(2).gameObject.SetActive(true);
            showOder.SetActive(true);
            minutes.gameObject.SetActive(false);
            showNameWait.SetActive(false);
        }
    }

    public void deleteItemOder()
    {
        if (timeLive <= 0)
        {
            strIdVp = "";
            PlayerPrefs.SetString(gameObject.name, "");
            idnsBan.Clear();
            initOder();
            Oder.Instance.showOder();
            loadItemOder();
        }
    }

    public void buyItemOder()
    {
        checkSell = false;
        strIdVp = "";
        PlayerPrefs.SetString(gameObject.name, "");
        transform.GetChild(2).gameObject.SetActive(false);
        minutes.gameObject.SetActive(true);
        showOder.SetActive(false);
        showNameWait.SetActive(true);
        timeLive = 240;
        StartCoroutine(runTime());
        for (int i = 0; i < idnsBan.Count; i++)
        {
            if (idnsBan[i] < 26)
            {
                StorageController.Instance.updateStorage(0, idnsBan[i], -PlayerPrefs.GetInt("numberSell" + id));
            }
            else if (true)
            {
                StorageController.Instance.updateStorage(1, idnsBan[i], -PlayerPrefs.GetInt("numberSell" + id));
            }
        }
        idnsBan.Clear();
        Oder.Instance.showOder();
    }
}
