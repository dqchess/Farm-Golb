using UnityEngine;
using System.Collections.Generic;

public class DialogController : SingletonOne<DialogController>
{
    //UI Crop, Machine
    public GameObject prefabsUICrop;
    public GameObject prefabsUIMachine;
    public GameObject prefabsUITime;

    //UI
    public GameObject dialogCrop;
    public GameObject dialogMachine;
    public GameObject dialogTime;

    public GameObject dialoSetting;
    public GameObject dialoStorage;
    public GameObject dialoOder;
    public GameObject dialoMarket;
    public GameObject dialogReward;
    public GameObject dialogDaily;
    public GameObject dialogBuyDiamond;
    public GameObject dialogBoat;

    //Object move harvest
    public Transform cloverExp;
    public Transform coin;
    public Transform diamond;

    //dialog move
    public GameObject iconMove;
    public GameObject dialogMove;
    float scale;

    //dialog Field, Cage, Destroy
    public void showDialog(Vector2 pos, int type, int id)
    {
        if (GameManager.Instance.guide && dialogCrop != null)
        {
            Destroy(dialogCrop);
        }

        scale = (float)Camera.main.orthographicSize / 270;
        dialogCrop = (GameObject)Instantiate(prefabsUICrop, pos, Quaternion.identity);
        dialogCrop.transform.GetChild(0).GetComponent<RectTransform>().localScale = new Vector2(scale, scale);

        if (GameManager.Instance.guide)
        {
            Guide.Instance.tayKeoThuH.SetActive(true);
            Instantiate(Guide.Instance.tayKeoThuH, dialogCrop.transform.GetChild(0).transform);
        }

        switch (type)
        {
            case 0:
                dialogCrop.GetComponent<DragManager>().showItemCrop();
                break;
            case 1:
                dialogCrop.GetComponent<DragManager>().showSickle();
                break;
            case 2:
                dialogCrop.GetComponent<DragManager>().showEatAnimal(id);
                break;
            case 3:
                dialogCrop.GetComponent<DragManager>().destroy(id);
                break;
        }
    }

    public void showMachine(Vector2 pos)
    {
        scale = (float)Camera.main.orthographicSize / 300;
        dialogMachine = (GameObject)Instantiate(prefabsUIMachine, pos, Quaternion.identity);
        dialogMachine.transform.GetChild(0).GetComponent<RectTransform>().localScale = new Vector2(scale, scale);
        if (GameManager.Instance.guide)
        {
            Guide.Instance.tayKeoMachine = Instantiate(Guide.Instance.tayKeoThuH, dialogMachine.transform.GetChild(0).transform);
            Guide.Instance.tayKeoMachine.SetActive(true);
            Guide.Instance.tayKeoMachine.transform.localPosition = new Vector2(67, 89);
        }
    }

    public void showTime(Vector2 pos)
    {
        scale = (float)Camera.main.orthographicSize / 270;
        dialogTime = (GameObject)Instantiate(prefabsUITime, pos, Quaternion.identity);
        dialogTime.transform.GetChild(0).GetComponent<RectTransform>().localScale = new Vector2(scale, scale);
    }

    public void close()
    {
        if (!GameManager.Instance.guide)
        {
            if (GameManager.Instance.nenTrangODat)
            {
                GameManager.Instance.nenTrangODat.SetActive(false);
            }

            if (dialogCrop != null)
            {
                Destroy(dialogCrop);
            }

            if (dialogMachine != null)
            {
                Destroy(dialogMachine);
            }

            if (dialogTime != null)
            {
                Destroy(dialogTime);
            }
            PlayerPrefs.SetInt("checkDrag", 1);
        }
    }

    public void closeDialogUI()
    {
        dialoSetting.SetActive(false);
        dialogReward.transform.localScale = Vector2.zero;
        dialoStorage.transform.localScale = Vector2.zero;
        dialogReward.transform.localScale = Vector2.zero;
        dialoOder.transform.localScale = Vector2.zero;
        dialogBoat.transform.localScale = Vector2.zero;
        dialoMarket.SetActive(false);
        if (ButtonManager.Instance.check)
        {
            ButtonManager.Instance.openOrCloseShop();
        }
    }
}