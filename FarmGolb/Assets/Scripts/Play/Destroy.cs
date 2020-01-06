using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class Destroy : MonoBehaviour
{
    //danh id vat pha huy 7. Cau rung to, 8. Da To, 9. Da Nho
    public int idPhaHuy;
    int order;

    bool checkHuy;
    Animator anim;
    GameObject phahuy;

    void Start()
    {
        checkHuy = true;

        if (PlayerPrefs.GetInt(gameObject.name) == 1)
        {
            Destroy(gameObject);
        }
        else
        {
            if (idPhaHuy == 8 || idPhaHuy == 9)
            {
                order = Mathf.RoundToInt((40 - transform.GetChild(0).position.y) * 100f);
                transform.GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = order;
                if (idPhaHuy == 8)
                {
                    GameManager.Instance.setArray(2, 2, transform.position, true);
                }
                else
                {
                    GameManager.Instance.setArray(1, 2, transform.position, true);
                    transform.GetChild(2).GetComponent<Renderer>().sortingOrder = order + 1;
                }
            }
            else
            {
                order = Mathf.RoundToInt((40 - transform.position.y) * 100f);
                if (idPhaHuy == 7)
                {
                    anim = transform.GetChild(0).GetComponent<Animator>();
                    GetComponent<SpriteRenderer>().sortingOrder = order;
                    transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = order + 1;
                }
                else
                {
                    GetComponent<SpriteRenderer>().sortingOrder = order;
                }
                GameManager.Instance.setArray(1, 1, transform.position, true);
            }
        }
    }

    private void OnMouseDown()
    {
        AnimClick.Instance.clickObject(gameObject);
        CheckDistance.Instance.posOld();
    }

    private void OnMouseUp()
    {
        if (!EventSystem.current.IsPointerOverGameObject(0) && CheckDistance.Instance.distance())
        {
            if (!GameManager.Instance.guide)
            {
                Language.Instance.onSound(1);
                cayTo();
            }
            else
            {
                Language.Instance.onSound(1);
                if (Guide.Instance.tree != null)
                {
                    if (Guide.Instance.tree.gameObject.name.Equals(gameObject.name))
                    {
                        cayTo();
                        Guide.Instance.tayChi.transform.position = new Vector2(100f, 100f);
                    }
                }
            }
        }
    }

    Vector2 target;
    void cayTo()
    {
        target = transform.position;
        if (idPhaHuy == 7)
        {
            DialogController.Instance.showDialog(new Vector2(target.x - 0.3f, target.y + 0.3f), 3, idPhaHuy);
        }
        else
        {
            DialogController.Instance.showDialog(new Vector2(target.x - 0.1f, target.y + 0.1f), 3, idPhaHuy);
        }
        CheckDistance.Instance.moveCam(transform.position);
        PlayerPrefs.SetString("namephahuy", gameObject.name);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (PlayerPrefs.GetString("namephahuy") == gameObject.name && (col.gameObject.CompareTag("saw") || col.gameObject.CompareTag("boom")))
        {
            int idDestroy = idPhaHuy;

            if (idPhaHuy == 9)
            {
                idDestroy = 8;
            }

            if (PlayerPrefs.GetInt("ns" + (idDestroy + 84)) - 1 >= 0 && checkHuy)
            {
                StorageController.Instance.updateStorage(1, idDestroy + 84, -1);
                buyDiamond();
                col.gameObject.GetComponent<ItemDrag>().updateText();
            }
            else if (checkHuy)
            {
                GameManager.Instance.listUserDiamond.Add(gameObject);
                DialogController.Instance.dialogBuyDiamond.SetActive(true);
                DialogController.Instance.dialogBuyDiamond.GetComponent<BuyDiamond>().setImage(idDestroy + 84);
            }

            if (GameManager.Instance.guide)
            {
                GetComponent<BoxCollider2D>().enabled = false;
                Guide.Instance.stepGuide(19);
            }
        }
    }

    public void buyDiamond()
    {
        if (idPhaHuy == 7)
        {
            Language.Instance.onSound(3);
            StartCoroutine(sawTree());
        }
        else
        {
            StartCoroutine(boom());
        }
        if (idPhaHuy == 8)
        {
            GameManager.Instance.setArray(2, 2, transform.position, false);
        }
        else
        {
            GameManager.Instance.setArray(1, 1, transform.position, false);
        }
        PlayerPrefs.SetInt(gameObject.name, 1);
        checkHuy = false;
    }

    IEnumerator sawTree()
    {
        StorageController.Instance.updateStorage(1, 89, 2);
        //skeleton.timeScale = 1;
        //skeleton.AnimationName = "chat";
        phahuy = Instantiate(FlyHarvest.Instance.saw, transform.position, Quaternion.identity);
        phahuy.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = order + 2;
        yield return new WaitForSeconds(1.5f);
        Destroy(phahuy);
        //skeleton.AnimationName = "do";
        anim.SetTrigger("CutTree");
        yield return new WaitForSeconds(1f);
        FlyHarvest.Instance.iconFlyBay(null, transform.position, 2, 0);
        FlyHarvest.Instance.iconFlyBay(GameManager.Instance.dataStorage.dataStorages[89].icon, transform.position, 2, 2);
        Destroy(gameObject);
    }

    IEnumerator boom()
    {
        StorageController.Instance.updateStorage(1, 90, 2);
        phahuy = Instantiate(FlyHarvest.Instance.boom, transform.position, Quaternion.identity);
        phahuy.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = order + 2;
        yield return new WaitForSeconds(1f);
        Language.Instance.onSound(5);
        GameObject phahuy1 = Instantiate(FlyHarvest.Instance.animationBoom, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        FlyHarvest.Instance.iconFlyBay(null, transform.position, 2, 0);
        FlyHarvest.Instance.iconFlyBay(GameManager.Instance.dataStorage.dataStorages[90].icon, transform.position, 2, 2);
        Destroy(phahuy);
        Destroy(phahuy1);
        Destroy(gameObject);
    }
}