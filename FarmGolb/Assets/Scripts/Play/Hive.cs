using UnityEngine;
using System.Collections;
using Observer;

public class Hive : MonoBehaviour
{
    public int time;
    public int timeLive;
    MoveObject moveObject;
    int numberHoney;
    GameObject bee;
    public Transform BeeBack;

    private void Start()
    {
        this.RegisterListener(EventID.OnBeeFlyTree, (param) => takeHoney((Vector3)param));
        gameObject.transform.SetParent(GameManager.Instance.ObCage);
        moveObject = GetComponent<MoveObject>();
        numberHoney = PlayerPrefs.GetInt("hive" + gameObject.name);
        if (PlayerPrefs.HasKey("timeSave" + gameObject.name))
        {
            timeLive = TimeManager.Instance.timeOutApp(gameObject.name);

            if (timeLive < time)
            {
                timeLive = time - timeLive;
            }
            else
            {
                timeLive = 0;
            }
            StartCoroutine(timeHive());
        }

        if (numberHoney > 0)
        {
            for (int i = 0; i < numberHoney; i++)
            {
                transform.GetChild(i + 2).gameObject.SetActive(true);
            }
            transform.GetChild(5).gameObject.SetActive(true);
        }
    }

    public void takeHoney(Vector3 pos)
    {
        if (PlayerPrefs.GetInt("hive" + gameObject.name) < 3 && timeLive <= 0)
        {
            timeLive = 1;
            bee = Instantiate(GameManager.Instance.Bee, BeeBack.position, Quaternion.identity);
            if (transform.position.x < pos.x)
            {
                bee.transform.localScale = new Vector2(-1f, 1f);
            }
            else
            {
                bee.transform.localScale = new Vector2(1f, 1f);
            }
            LeanTween.scale(bee.transform.GetChild(0).gameObject, Vector2.one, 1f);
            StartCoroutine(backHive(pos));
        }
    }

    IEnumerator backHive(Vector2 pos)
    {
        yield return TimeManager.Instance.wait;
        LeanTween.move(bee, new Vector2(pos.x, pos.y + 0.5f), 5f);
        yield return new WaitForSeconds(15f);
        pos = bee.transform.localScale;
        bee.transform.localScale = new Vector2(-pos.x, pos.y);
        LeanTween.move(bee, BeeBack.position, 5f);
        yield return new WaitForSeconds(5f);
        LeanTween.scale(bee.transform.GetChild(0).gameObject, Vector2.zero, 1.5f);
        yield return new WaitForSeconds(0.5f);
        timeLive = time;
        StartCoroutine(timeHive());
        TimeManager.Instance.saveTime(gameObject.name);
    }

    private void OnMouseDown()
    {
        CheckDistance.Instance.posOld();
    }

    private void OnMouseUp()
    {
        if (CheckDistance.Instance.distance() && moveObject.checkUI())
        {
            numberHoney = PlayerPrefs.GetInt("hive" + gameObject.name);
            if (numberHoney > 0)
            {
                if (StorageController.Instance.checkIsFullStorage(1, 32, 1))
                {
                    Language.Instance.onSound(2);
                    transform.GetChild(numberHoney + 1).gameObject.SetActive(false);
                    if (numberHoney == 1)
                    {
                        transform.GetChild(5).gameObject.SetActive(false);
                    }
                    PlayerPrefs.SetInt("hive" + gameObject.name, numberHoney - 1);
                    FlyHarvest.Instance.iconFlyBay(null, transform.position, GameManager.Instance.dataStorage.dataStorages[32].exp, 0);
                    FlyHarvest.Instance.iconFlyBay(GameManager.Instance.dataStorage.dataStorages[32].icon, transform.position, 1, 2);
                }
            }
            else
            {
                if (timeLive > 1)
                {
                    DialogController.Instance.showTime(new Vector2(transform.position.x, transform.position.y + 1.7f));
                    TimeObject.Instance.showTimeDialog(39, time, timeLive, gameObject);
                }
            }
        }
    }

    public IEnumerator timeHive()
    {
        yield return TimeManager.Instance.wait;
        if (--timeLive > 0)
        {
            StartCoroutine(timeHive());
        }
        else
        {
            PlayerPrefs.DeleteKey("timeSave" + gameObject.name);
            transform.GetChild(PlayerPrefs.GetInt("hive" + gameObject.name) + 2).gameObject.SetActive(true);
            PlayerPrefs.SetInt("hive" + gameObject.name, PlayerPrefs.GetInt("hive" + gameObject.name) + 1);
            transform.GetChild(5).gameObject.SetActive(true);
        }
    }
}
