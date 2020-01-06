using UnityEngine;
using System.Collections;
using Observer;

public class Tree : MonoBehaviour
{
    public int idTree;
    public int time, timeLive;
    Animator animator;

    bool checkThuHoach;
    int slThuH;
    MoveObject moveObject;    

    void Start()
    {        
        moveObject = GetComponent<MoveObject>();
        animator = transform.GetChild(1).GetComponent<Animator>();
        timeLive = time;
        if (PlayerPrefs.HasKey("timeSave" + gameObject.name))
        {
            timeLive = TimeManager.Instance.timeOutApp(gameObject.name);
            if (timeLive < time)
            {
                timeLive = time - timeLive;

                if (timeLive <= time / 2)
                {
                    BeeFlyTree();
                    transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = GameManager.Instance.dataSprite.sprTree[1].spr[idTree];
                }
                StartCoroutine(timeTree());
            }
            else
            {
                timeLive = 0;
                transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = GameManager.Instance.dataSprite.sprTree[2].spr[idTree];
                transform.GetChild(2).gameObject.SetActive(true);
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
            Language.Instance.onSound(1);
            if (timeLive <= 0)
            {
                if (StorageController.Instance.checkIsFullStorage(0, idTree + 18, 1))
                {
                    Language.Instance.onSound(2);
                    TimeManager.Instance.saveTime(gameObject.name);
                    transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = GameManager.Instance.dataSprite.sprTree[0].spr[idTree];
                    transform.GetChild(2).gameObject.SetActive(false);
                    timeLive = time;
                    StartCoroutine(timeTree());
                    Instantiate(FlyHarvest.Instance.effectCrop, transform.transform.position, Quaternion.identity);
                    FlyHarvest.Instance.iconFlyBay(null, transform.position, GameManager.Instance.dataStorage.dataStorages[idTree + 18].exp, 0);
                    FlyHarvest.Instance.iconFlyBay(GameManager.Instance.dataStorage.dataStorages[idTree + 18].icon, transform.position, 1, 2);
                }
                if (GameManager.Instance.guide)
                {
                    Guide.Instance.stepGuide(18);
                }
            }
            else
            {
                DialogController.Instance.showTime(new Vector2(transform.position.x, transform.position.y + 1.7f));
                TimeObject.Instance.showTimeDialog(idTree + 18, time, timeLive, gameObject);
            }
        }
    }

    public IEnumerator timeTree()
    {
        yield return TimeManager.Instance.wait;
        if (--timeLive > 0)
        {
            if (timeLive == time / 2)
            {
                BeeFlyTree();
                transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = GameManager.Instance.dataSprite.sprTree[1].spr[idTree];
            }
            StartCoroutine(timeTree());
        }
        else
        {
            transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = GameManager.Instance.dataSprite.sprTree[2].spr[idTree];
            transform.GetChild(2).gameObject.SetActive(true);
        }

        if (GameManager.Instance.guide && Guide.Instance.guide == 16)
        {
            timeLive = 0;
            Guide.Instance.stepGuide(17);
        }
    }

    private void OnBecameVisible()
    {
        animator.enabled = true;
    }

    private void OnBecameInvisible()
    {
        animator.enabled = false;
    }

    public void BeeFlyTree()
    {
        this.PostEvent(EventID.OnBeeFlyTree, transform.position);
    }
}