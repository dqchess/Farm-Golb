using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DailyReward : MonoBehaviour
{
    public int id;
    public Sprite imgLock, img;
    //public Text txtTime;
    int timeout, day, timeRunOut, timeLive;
    public Sprite kimcuong;
    public Image imgBt;
    public Text nameVd;
    public Text nameDialog;
    public Text nameButton;

    private void Start()
    {
        if (id == 0)
        {
            Language.Instance.setNameText("Phần thưởng !", "Reward !", nameDialog);
        }
        Language.Instance.setNameText("Phần thưởng", "Reward", nameVd);
        Language.Instance.setNameText("Xem", "Watch", nameButton);
    }

    public void watchVideo()
    {
        //if (timeLive <= 0)
        //{
            PlayerPrefs.SetInt("idVideo", id);
            //cho hien video
            if (MobileRewardAd.instance.rewardBasedVideoAd.IsLoaded())
            {
                MobileRewardAd.instance.qcVideo = gameObject;
                MobileRewardAd.instance.rewardBasedVideoAd.Show();
            }
            else
            {
                Language.Instance.notifyEngOrVi("Please try again later. Wish you luck next time!", "Mời bạn thử lại lần khác. Chúc bạn may mắn lần sau!");
            }
            //xemXong(id);
        //}
    }

    public void xemXong(int id)
    {
        Vector2 pos = Camera.main.transform.position;
        switch (id)
        {
            case 0:
                FlyHarvest.Instance.iconFlyBay(null, pos, 50, 1);
                //setImage();
                break;
            case 1:
                FlyHarvest.Instance.iconFlyBay(kimcuong, pos, 3, 3);
                //setImage();
                break;
            case 2:
                FlyHarvest.Instance.iconFlyBay(GameManager.Instance.dataStorage.dataStorages[91].icon, pos, 2, 2);
                StorageController.Instance.updateStorage(1, 91, 2);
                //setImage();
                break;
            case 3:
                FlyHarvest.Instance.iconFlyBay(GameManager.Instance.dataStorage.dataStorages[92].icon, pos, 2, 2);
                StorageController.Instance.updateStorage(1, 92, 2);
                //setImage();
                break;
                //case 4:
                //    FlyHarvest.Instance.iconFlyBay(GameManager.Instance.dataStorage.dataStorages[51].icon, pos, 2, 2);
                //    StorageController.Instance.updateStorage(1, 51, 2);
                //    setImage();
                //    break;
                //case 5:
                //    FlyHarvest.Instance.iconFlyBay(GameManager.Instance.dataStorage.dataStorages[49].icon, pos, 2, 2);
                //    StorageController.Instance.updateStorage(1, 49, 2);
                //    setImage();
                //    break;
                //case 6:
                //    FlyHarvest.Instance.iconFlyBay(GameManager.Instance.dataStorage.dataStorages[50].icon, pos, 2, 2);
                //    StorageController.Instance.updateStorage(1, 50, 2);
                //    setImage();
                //    break;
                //case 7:
                //    FlyHarvest.Instance.iconFlyBay(GameManager.Instance.dataStorage.dataStorages[52].icon, pos, 2, 2);
                //    StorageController.Instance.updateStorage(1, 52, 2);
                //    setImage();
                //    break;
                //case 8:
                //    FlyHarvest.Instance.iconFlyBay(GameManager.Instance.dataStorage.dataStorages[53].icon, pos, 2, 2);
                //    StorageController.Instance.updateStorage(1, 53, 2);
                //    setImage();
                //    break;
        }
        close();
    }

    //void setImage()
    //{
    //    timeLive = 120;
    //    imgBt.sprite = imgLock;
    //    imgBt.SetNativeSize();
    //    StartCoroutine(runTime());
    //    Language.Instance.setNameText("Chờ", "Wait", nameButton);
    //}

    //public IEnumerator runTime()
    //{
    //    yield return TimeManager.Instance.wait;
    //    if (--timeLive > 0)
    //    {
    //        StartCoroutine(runTime());
    //        txtTime.text = (timeLive / 60) + ":" + (timeLive % 60);
    //    }
    //    else
    //    {
    //        Language.Instance.setNameText("Xem", "Watch", nameButton);
    //        imgBt.sprite = img;
    //        imgBt.SetNativeSize();
    //        switch (id)
    //        {
    //            case 0:
    //                txtTime.text = "+50";
    //                break;
    //            case 1:
    //                txtTime.text = "+3";
    //                break;
    //            case 2:
    //                txtTime.text = "+2";
    //                break;
    //            case 3:
    //                txtTime.text = "+2";
    //                break;
    //                //case 4:
    //                //    txtTime.text = "+2";
    //                //    break;
    //                //case 5:
    //                //    txtTime.text = "+2";
    //                //    break;
    //                //case 6:
    //                //    txtTime.text = "+2";
    //                //    break;
    //                //case 7:
    //                //    txtTime.text = "+2";
    //                //    break;
    //                //case 8:
    //                //    txtTime.text = "+2";
    //                //    break;
    //        }
    //    }
    //}

    public void close()
    {
        //DialogController.Instance.dialogReward.transform.localScale = Vector2.zero;
        DialogController.Instance.dialogReward.SetActive(false);
        GameManager.Instance.cameraOnOff(false);
    }
}
