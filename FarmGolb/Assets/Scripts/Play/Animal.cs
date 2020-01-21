using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using Spine.Unity;

public class Animal : MonoBehaviour
{
    public int timeLive, time;
    public int idFood;
    SkeletonAnimation anim;
    bool checkAn;

    private void Start()
    {
        anim = GetComponent<SkeletonAnimation>();
        gameObject.name += transform.parent.gameObject.name;
        if (PlayerPrefs.HasKey("timeSave" + gameObject.name))
        {
            timeLive = TimeManager.Instance.timeOutApp(gameObject.name);

            if (timeLive < time)
            {
                timeLive = time - timeLive;
                anim.AnimationName = "eating";
                transform.parent.GetComponent<Cage>().soGaAn(1);
                StartCoroutine(timeRun());
            }
            else
            {
                transform.GetChild(0).gameObject.SetActive(true);
                //thua hoach xong                   
                timeLive = 0;
                anim.AnimationName = "stand 2";
            }
            checkAn = true;
        }
    }

    private void OnBecameVisible()
    {
        anim.enabled = true;
    }

    private void OnBecameInvisible()
    {
        anim.enabled = false;
    }

    private void OnMouseUp()
    {
        if (!EventSystem.current.IsPointerOverGameObject(0) && PlayerPrefs.GetInt("move") == 0)
        {
            if (timeLive > 0)
            {
                DialogController.Instance.showTime(transform.position);
                TimeObject.Instance.showTimeDialog(idFood, time, timeLive, gameObject);
            }
            else
            {
                transform.parent.GetComponent<Cage>().showEat();
            }
        }
    }

    int slthuH;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag(gameObject.tag))
        {
            //kiem tra thuc an
            if (PlayerPrefs.GetInt("ns" + idFood) - 1 >= 0 && !checkAn)
            {
                buyDiamond();
                StorageController.Instance.updateStorage(1, idFood, -1);
                col.gameObject.GetComponent<ItemDrag>().updateText();
                Oder.Instance.showOder();
            }
            else
            {
                if (!checkAn)
                {
                    if (GameManager.Instance.listUserDiamond.Count < 2)
                    {
                        DialogController.Instance.dialogBuyDiamond.GetComponent<BuyDiamond>().setImage(idFood);
                        GameManager.Instance.listUserDiamond.Add(gameObject);
                    }
                }
            }
            //guide
            if (GameManager.Instance.guide)
            {
                timeLive = 0;
                Guide.Instance.check = true;
            }
        }

        if (col.gameObject.CompareTag(gameObject.tag + idFood) && checkAn)
        {
            if (timeLive <= 0)
            {
                slthuH = GameManager.Instance.dataStorage.dataStorages[idFood - 7].slThuH;
                if (StorageController.Instance.checkIsFullStorage(1, idFood - 7, slthuH))
                {
                    Language.Instance.onSound(2);
                    Instantiate(FlyHarvest.Instance.effectCrop, transform.position, Quaternion.identity);
                    FlyHarvest.Instance.iconFlyBay(null, transform.position, GameManager.Instance.dataStorage.dataStorages[idFood - 7].exp, 0);
                    FlyHarvest.Instance.iconFlyBay(GameManager.Instance.dataStorage.dataStorages[idFood - 7].icon, transform.position, slthuH, 2);
                    anim.AnimationName = "stand 1";
                    checkAn = false;
                    PlayerPrefs.DeleteKey("timeSave" + gameObject.name);
                    Oder.Instance.showOder();
                    transform.GetChild(0).gameObject.SetActive(false);
                }

                if (GameManager.Instance.guide)
                {
                    Guide.Instance.check = true;
                }
            }
        }
    }

    public void buyDiamond()
    {
        TimeManager.Instance.saveTime(gameObject.name);
        anim.AnimationName = "eating";
        timeLive = time;
        if (!GameManager.Instance.guide)
        {
            StartCoroutine(timeRun());
            transform.parent.GetComponent<Cage>().soGaAn(1);
        }
        else
        {
            anim.AnimationName = "stand 2";
            transform.GetChild(0).gameObject.SetActive(true);
        }
        checkAn = true;
        hieuUngEat();
    }

    void hieuUngEat()
    {
        GameObject ob = Instantiate(FlyHarvest.Instance.animationSeed, transform.position, Quaternion.identity);
        ob.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = GameManager.Instance.dataStorage.dataStorages[idFood].icon;
    }

    public IEnumerator timeRun()
    {
        yield return TimeManager.Instance.wait;

        if (timeLive > 0)
        {
            --timeLive;
            StartCoroutine(timeRun());
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.parent.GetComponent<Cage>().soGaAn(-1);
            anim.AnimationName = "stand 2";
        }
    }
}
