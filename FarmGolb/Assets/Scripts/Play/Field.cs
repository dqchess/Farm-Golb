using UnityEngine;
using System.Collections;

public class Field : MonoBehaviour
{
    public int time, timeLive;
    public int idSeed;
    public SpriteRenderer sprCayTrong;

    public GameObject nentrang;
    public bool showtime;
    Vector2 pos;
    MoveObject moveObject;

    void Start()
    {
        sprCayTrong = transform.GetChild(1).GetComponent<SpriteRenderer>();

        moveObject = GetComponent<MoveObject>();
        if (PlayerPrefs.HasKey("checkSeed" + gameObject.name))
        {
            idSeed = PlayerPrefs.GetInt("checkSeed" + gameObject.name);
            time = GameManager.Instance.dataStorage.dataStorages[idSeed].time;
            timeLive = TimeManager.Instance.timeOutApp(gameObject.name);
            transform.GetChild(1).gameObject.SetActive(true);

            if (timeLive < time)
            {
                timeLive = time - timeLive;
                if (timeLive <= time / 2)
                {
                    sprCayTrong.sprite = GameManager.Instance.dataSprite.sprSeed[1].spr[idSeed];
                }
                else
                {
                    sprCayTrong.sprite = GameManager.Instance.dataSprite.sprSeed[0].spr[idSeed];
                }
                StartCoroutine(timeSeedGrow());
            }
            else
            {
                timeLive = 0;
                transform.GetChild(3).gameObject.SetActive(true);
                sprCayTrong.sprite = GameManager.Instance.dataSprite.sprSeed[2].spr[idSeed];
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
            if (GameManager.Instance.nenTrangODat)
            {
                GameManager.Instance.nenTrangODat.SetActive(false);
            }

            if (!GameManager.Instance.guide)
            {
                nentrang.SetActive(true);
                GameManager.Instance.nenTrangODat = nentrang;
            }

            if (sprCayTrong.sprite == null && timeLive <= 0)
            {
                DialogController.Instance.showDialog(transform.position, 0, 0);
            }
            else if (timeLive > 0)
            {
                DialogController.Instance.showTime(transform.position);
                TimeObject.Instance.showTimeDialog(idSeed, time, timeLive, gameObject);
            }
            else if (timeLive <= 0)
            {
                DialogController.Instance.showDialog(transform.position, 1, 0);
            }

            if (Guide.Instance.step == 0)
            {
                Guide.Instance.stepGuide(1);
            }
            else if (Guide.Instance.step == 4)
            {
                Guide.Instance.stepGuide(5);
            }
        }
    }

    IEnumerator timeSeedGrow()
    {
        yield return TimeManager.Instance.wait;
        if (--timeLive > 0)
        {
            if (timeLive == time / 2)
            {
                sprCayTrong.sprite = GameManager.Instance.dataSprite.sprSeed[1].spr[idSeed];
            }
            StartCoroutine(timeSeedGrow());
        }
        else
        {
            transform.GetChild(1).gameObject.SetActive(true);
            sprCayTrong.sprite = GameManager.Instance.dataSprite.sprSeed[2].spr[idSeed];
            transform.GetChild(3).gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (sprCayTrong.sprite == null && target.gameObject.CompareTag("crop"))
        {
            idSeed = target.gameObject.GetComponent<ItemDrag>().idItem;
            if (PlayerPrefs.GetInt("ns" + idSeed) - 1 >= 0)
            {
                StorageController.Instance.updateStorage(0, idSeed, -1);
                target.gameObject.GetComponent<ItemDrag>().updateText();
                buyDiamond();
                Oder.Instance.showOder();
            }
            else
            {
                sprCayTrong.sprite = GameManager.Instance.dataStorage.dataStorages[idSeed].icon;
                DialogController.Instance.dialogBuyDiamond.GetComponent<BuyDiamond>().setImage(idSeed);
                GameManager.Instance.listUserDiamond.Add(gameObject);
            }

            if (GameManager.Instance.guide)
            {
                Guide.Instance.check = true;
            }
        }

        if (target.gameObject.CompareTag("cropHarvest") && sprCayTrong.sprite != null && timeLive <= 0)
        {
            if (StorageController.Instance.checkIsFullStorage(0, idSeed, GameManager.Instance.dataStorage.dataStorages[idSeed].slThuH))
            {
                Language.Instance.onSound(2);
                Instantiate(FlyHarvest.Instance.effectCrop, transform.transform.position, Quaternion.identity);
                FlyHarvest.Instance.iconFlyBay(null, transform.position, GameManager.Instance.dataStorage.dataStorages[idSeed].exp, 0);
                FlyHarvest.Instance.iconFlyBay(GameManager.Instance.dataStorage.dataStorages[idSeed].icon, transform.position, GameManager.Instance.dataStorage.dataStorages[idSeed].slThuH, 2);
                timeLive = 0;
                sprCayTrong.sprite = null;
                transform.GetChild(1).gameObject.SetActive(false);
                transform.GetChild(3).gameObject.SetActive(false);
                PlayerPrefs.DeleteKey("checkSeed" + gameObject.name);
                Oder.Instance.showOder();
            }

            if (GameManager.Instance.guide)
            {
                Guide.Instance.check = true;
            }
        }
    }

    public void buyDiamond()
    {
        transform.GetChild(1).gameObject.SetActive(true);
        PlayerPrefs.SetInt("checkSeed" + gameObject.name, idSeed);
        TimeManager.Instance.saveTime(gameObject.name);
        sprCayTrong.sprite = GameManager.Instance.dataSprite.sprSeed[0].spr[idSeed];
        time = GameManager.Instance.dataStorage.dataStorages[idSeed].time;
        timeLive = time;
        StartCoroutine(timeSeedGrow());
        animseed();
    }

    void animseed()
    {
        GameObject ob = Instantiate(FlyHarvest.Instance.animationSeed, transform.position, Quaternion.identity);
        ob.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = GameManager.Instance.dataStorage.dataStorages[idSeed].icon;
    }
}