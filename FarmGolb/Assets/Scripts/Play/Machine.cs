using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class Machine : MonoBehaviour
{
    public int id;
    public List<int> idVatPham, idVpNauXong;
    public int time, timeLive;
    public string strIdVatPham;

    public bool showtime;
    int idVp;

    int slIdVp;
    MoveObject moveObject;
    public int numberProducing;

    private void Start()
    {
        moveObject = GetComponent<MoveObject>();
        numberProducing = 0;
        timeLive = TimeManager.Instance.timeOutApp(gameObject.name);

        if (PlayerPrefs.HasKey("idvpmachine" + 0 + gameObject.name))
        {
            foreach (var item in Array.ConvertAll(PlayerPrefs.GetString("idvpmachine" + 0 + gameObject.name).Split(','), int.Parse))
            {
                idVpNauXong.Add(item);
            }
        }

        if (PlayerPrefs.HasKey("idvpmachine" + 1 + gameObject.name))
        {
            foreach (var item in Array.ConvertAll(PlayerPrefs.GetString("idvpmachine" + 1 + gameObject.name).Split(','), int.Parse))
            {
                idVatPham.Add(item);
            }
        }

        if (idVpNauXong.Count > 0)
        {
            transform.GetChild(9).gameObject.SetActive(true);
            showImageVp(true);
        }

        if (idVatPham.Count > 0)
        {
            slIdVp = idVatPham.Count;
            time = GameManager.Instance.dataStorage.dataStorages[idVatPham[0]].time;
            for (int i = 0; i < slIdVp; i++)
            {
                if (i == 0)
                {
                    if (time <= timeLive)
                    {
                        idVpNauXong.Add(idVatPham[0]);
                        idVatPham.RemoveAt(0);
                        timeLive -= time;
                    }
                    else
                    {
                        timeLive = time - timeLive;
                        break;
                    }
                }
                else
                {
                    if (timeLive >= GameManager.Instance.dataStorage.dataStorages[idVatPham[0]].time)
                    {
                        timeLive -= GameManager.Instance.dataStorage.dataStorages[idVatPham[0]].time;
                        idVpNauXong.Add(idVatPham[0]);
                        idVatPham.RemoveAt(0);
                    }
                    else
                    {
                        timeLive = GameManager.Instance.dataStorage.dataStorages[idVatPham[0]].time - timeLive;
                        break;
                    }
                }
            }

            if (idVatPham.Count > 0)
            {
                time = GameManager.Instance.dataStorage.dataStorages[idVatPham[0]].time;
                numberProducing = idVatPham.Count;
                transform.GetChild(2).gameObject.SetActive(true);
                StartCoroutine(timeItem());
                saveIdVp(idVatPham, 1);
            }
            else
            {
                PlayerPrefs.DeleteKey("idvpmachine" + 1 + gameObject.name);
            }

            if (idVpNauXong.Count > 0)
            {
                transform.GetChild(9).gameObject.SetActive(true);
                saveIdVp(idVpNauXong, 0);
                showImageVp(true);
            }
        }
    }

    private void OnMouseDown()
    {
        CheckDistance.Instance.posOld();
    }

    private void OnMouseUp()
    {
        if (CheckDistance.Instance.distance() && moveObject.checkUI())
        {
            if (idVpNauXong.Count == 0)
            {                
                DialogController.Instance.showMachine(transform.position);
                MachineController.Instance.loadItemMachine(gameObject, id, numberProducing);
                if (idVatPham.Count > 0)
                {
                    MachineController.Instance.idProduct = idVatPham[0];
                }
                showtime = true;
                if (GameManager.Instance.guide)
                {
                    Guide.Instance.tayChi.transform.position = new Vector2(100f, 100f);
                }
            }
            else
            {
                //thu hoach tung item
                idVp = idVpNauXong[idVpNauXong.Count - 1];
                //off hieu ung san pham nau xong
                if (idVpNauXong.Count == 1)
                {
                    transform.GetChild(9).gameObject.SetActive(false);
                }

                if (StorageController.Instance.checkIsFullStorage(1, idVp, GameManager.Instance.dataStorage.dataStorages[idVp].slThuH))
                {
                    Language.Instance.onSound(2);
                    Instantiate(FlyHarvest.Instance.effectCrop, transform.position, Quaternion.identity);
                    FlyHarvest.Instance.iconFlyBay(null, transform.position, GameManager.Instance.dataStorage.dataStorages[idVp].exp, 0);
                    FlyHarvest.Instance.iconFlyBay(GameManager.Instance.dataStorage.dataStorages[idVp].icon, transform.position, GameManager.Instance.dataStorage.dataStorages[idVp].slThuH, 2);
                    transform.GetChild(idVpNauXong.Count + 2).gameObject.SetActive(false);
                    idVpNauXong.RemoveAt(idVpNauXong.Count - 1);
                    saveIdVp(idVpNauXong, 0);
                }

                if (GameManager.Instance.guide)
                {
                    Guide.Instance.stepGuide(15);
                }
            }
        }
    }

    public IEnumerator timeItem()
    {
        yield return TimeManager.Instance.wait;

        if (timeLive > 0)
        {
            --timeLive;
            StartCoroutine(timeItem());
            if (showtime)
                MachineController.Instance.showTimeMachine(timeLive, time);
        }
        else
        {
            produceEnd();
            if (GameManager.Instance.guide && Guide.Instance.step == 14)
            {
                Guide.Instance.tayChi.transform.position = GameManager.Instance.ObMachine.GetChild(0).GetChild(0).transform.position;
            }
        }
    }

    public void produceEnd()
    {
        numberProducing -= 1;
        idVpNauXong.Add(idVatPham[0]);
        idVatPham.RemoveAt(0);
        showImageVp(true);

        saveIdVp(idVatPham, 1);
        saveIdVp(idVpNauXong, 0);

        if (idVpNauXong.Count == 1)
        {
            transform.GetChild(9).gameObject.SetActive(true);
        }

        if (numberProducing > 0)
        {
            timeLive = GameManager.Instance.dataStorage.dataStorages[idVatPham[0]].time;
            time = timeLive;
            StartCoroutine(timeItem());
        }
        else if (numberProducing == 0)
        {
            transform.GetChild(2).gameObject.SetActive(false);
        }

        if (showtime)
            MachineController.Instance.updateIconProducing(numberProducing);
    }

    //save id vp
    //0 la nau xong, 1 la chua nau
    public void saveIdVp(List<int> idvp, int a)
    {
        strIdVatPham = "";
        for (int i = 0; i < idvp.Count; i++)
        {
            if (strIdVatPham == "")
            {
                strIdVatPham += idvp[i];
            }
            else
            {
                strIdVatPham += "," + idvp[i];
            }
        }

        if (idvp.Count > 0)
        {
            PlayerPrefs.SetString("idvpmachine" + a + gameObject.name, strIdVatPham);
        }
        else
        {
            PlayerPrefs.DeleteKey("idvpmachine" + a + gameObject.name);
        }
    }

    public void showImageVp(bool bl)
    {
        for (int i = 0; i < idVpNauXong.Count; i++)
        {
            if (bl)
            {
                transform.GetChild(i + 3).gameObject.SetActive(true);
                transform.GetChild(i + 3).GetComponent<SpriteRenderer>().sprite = GameManager.Instance.dataStorage.dataStorages[idVpNauXong[i]].icon;
            }
            else
            {
                transform.GetChild(i + 3).gameObject.SetActive(false);
            }
        }
    }

    public void producing(int idSp)
    {
        if (numberProducing == 0)
        {
            TimeManager.Instance.saveTime(gameObject.name);
            timeLive = GameManager.Instance.dataStorage.dataStorages[idSp].time;
            time = timeLive;
            //Animation machine...
            transform.GetChild(2).gameObject.SetActive(true);
            StartCoroutine(timeItem());
        }
        numberProducing++;
        idVatPham.Add(idSp);
        saveIdVp(idVatPham, 1);
    }
}