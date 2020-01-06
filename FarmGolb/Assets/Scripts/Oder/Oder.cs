using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Oder : MonoBehaviour
{
    public static Oder Instance;
    public GameObject[] itemOder;
    public GameObject dialogOder;

    public List<int> idVpUnLock;
    //Image vat pham cua ItemOder
    public GameObject[] spShow;
    public bool checkBan, check;

    private void OnEnable()
    {
        Instance = this;
        idVatPhamUnlock();
        showOder();
        checkBan = true;
        check = false;
    }

    private void OnMouseDown()
    {
        CheckDistance.Instance.posOld();
    }

    private void OnMouseUp()
    {
        if (CheckDistance.Instance.distance() && !EventSystem.current.IsPointerOverGameObject(0))
        {
            Language.Instance.onSound(1);
            GameManager.Instance.cameraOnOff(true);
            dialogOder.GetComponent<Animator>().Play("AimBlack", -1, 0);
            AnimClick.Instance.showUI(dialogOder);
            check = true;
            showOder();
        }
    }

    int count, numberItemOder;
    public void showOder()
    {
        numberItemOder = PlayerPrefs.GetInt("level");

        if (numberItemOder < 6)
        {
            for (int k = numberItemOder; k < 6; k++)
            {
                itemOder[k].SetActive(false);
            }
        }
        else
        {
            numberItemOder = 6;
        }
        if (GameManager.Instance.guide)
        {
            numberItemOder = 1;
        }
        for (int l = 0; l < 6; l++)
        {
            if (l < numberItemOder)
            {
                transform.GetChild(l + 6).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(l + 6).gameObject.SetActive(false);
            }
        }
        for (int i = 0; i < numberItemOder; i++)
        {
            itemOder[i].SetActive(true);
            count = 0;
            ItemOder item = itemOder[i].GetComponent<ItemOder>();
            for (int j = 0; j < item.idnsBan.Count; j++)
            {
                if (PlayerPrefs.GetInt("ns" + item.idnsBan[j]) - PlayerPrefs.GetInt("numberSell" + i) >= 0)
                {
                    count++;
                }
            }

            if (count == item.idnsBan.Count && item.idnsBan.Count > 0)
            {
                setTickOder(i, true);
            }
            else
            {
                setTickOder(i, false);
            }

            if (check)
            {
                itemOder[PlayerPrefs.GetInt("idOder", 0)].GetComponent<ItemOder>().loadItemOder();
            }
        }
    }

    public void setTickOder(int i, bool bl)
    {
        transform.GetChild(i).gameObject.SetActive(bl);
        itemOder[i].GetComponent<ItemOder>().checkSell = bl;
        itemOder[i].transform.GetChild(2).transform.GetChild(2).gameObject.SetActive(bl);
    }

    public void close()
    {
        if (!GameManager.Instance.guide)
        {
            GameManager.Instance.cameraOnOff(false);
            dialogOder.transform.localScale = Vector2.zero;
        }
    }

    public void idVatPhamUnlock()
    {
        int count = 0;
        foreach (DataStorage.DataStorages item in GameManager.Instance.dataStorage.dataStorages)
        {
            if (item.levelUse <= PlayerPrefs.GetInt("level"))
            {
                idVpUnLock.Add(count);
            }
            ++count;
        }
    }

    public int randomIdVp()
    {
        return idVpUnLock[Random.Range(0, idVpUnLock.Count)];
    }
}