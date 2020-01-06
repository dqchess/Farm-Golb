using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemShop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public int index, idLoai;
    //drag item shop    
    bool checkTrung, checkDrag;
    GameObject init;
    SpriteRenderer spr;
    Vector3 target;
    float y;

    //Item
    public GameObject unlock;
    public Image icon;
    public Text number;
    public Text numberMax;
    public Text coin;
    public Text nameItemShop;
    public Text textLock;
    DataShop.ItemShop item;
    public GameObject btnNumber, btnLevel;
    public void updateLenLevel()
    {
        item = GameManager.Instance.dataShop.dataItemShop[index];
        if (idLoai != 5 && PlayerPrefs.GetInt("level") >= (item.plusLevel + PlayerPrefs.GetInt("levelsd" + index)))
        {
            PlayerPrefs.SetInt("slmax" + index, PlayerPrefs.GetInt("slmax" + index) + item.plusMaxsl);
            PlayerPrefs.SetInt("levelsd" + index, PlayerPrefs.GetInt("levelsd" + index) + item.plusLevel);
            numberMax.text = PlayerPrefs.GetInt("slmax" + index).ToString();
        }

        if (PlayerPrefs.GetInt("level") >= PlayerPrefs.GetInt("levelsd" + index))
        {
            if (idLoai != 5)
            {
                btnNumber.gameObject.SetActive(true);
                number.text = PlayerPrefs.GetInt("slkeo" + index).ToString();
                numberMax.text = PlayerPrefs.GetInt("slmax" + index).ToString();
            }
            switch (idLoai)
            {
                case 1:
                case 0:
                    ShopManager.Instance.openNew(0, true);
                    break;
                case 2:
                case 3:
                case 6:
                    ShopManager.Instance.openNew(1, true);
                    break;
                case 4:
                    ShopManager.Instance.openNew(2, true);
                    break;
                case 5:
                    ShopManager.Instance.openNew(3, true);
                    break;
            }
            ShopManager.Instance.openNew(ShopManager.Instance.index1, false);
            ShopManager.Instance.onImgNew();

            unlock.SetActive(true);
            coin.transform.parent.gameObject.SetActive(true);
            btnLevel.gameObject.SetActive(false);
            icon.sprite = item.icon;
            icon.SetNativeSize();
            Language.Instance.setNameText(item.nameVi, item.nameEng, nameItemShop);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        ShopManager.Instance.scroll.OnBeginDrag(eventData);
        item = GameManager.Instance.dataShop.dataItemShop[index];
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        y = target.y;
        checkDrag = PlayerPrefs.GetInt("levelsd" + index) <= PlayerPrefs.GetInt("level") && (int.Parse(number.text) < int.Parse(numberMax.text) || idLoai == 5);

        if (checkDrag)
        {
            spr = ShopManager.Instance.spriteDrag;
            spr.transform.position = target;
            spr.sprite = item.icon;
            transform.GetChild(1).gameObject.SetActive(false);
        }

        if (GameManager.Instance.guide)
        {
            if (index == 0 && Guide.Instance.step > 3)
            {
                checkDrag = false;
                transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        ShopManager.Instance.scroll.OnDrag(eventData);
        if (checkDrag)
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = 0;
            if (init == null)
            {
                spr.transform.position = target;
            }
            if (target.y - y > 1)
            {
                spr.transform.position = new Vector2(100f, 100f);
                transform.GetChild(1).gameObject.SetActive(true);
                init = Instantiate(item.itemShop, target, Quaternion.identity);
                y = 100;
                ButtonManager.Instance.closeShop();
                LevelManager.Instance.huy = init;
            }
            else if (init != null)
            {
                if (idLoai == 3)
                {
                    init.transform.position = target;
                }
                else
                {
                    target = GameManager.Instance.highlightMap.GetCellCenterLocal(GameManager.Instance.highlightMap.WorldToCell(target));
                    target.z = target.y;
                    init.transform.position = target;
                    //check trung
                    if ((target.y < 10 && target.y > -12) && (-18 < target.x && target.x < 17))
                    {
                        checkTrung = GameManager.Instance.checkTrung(init.GetComponent<MoveObject>().col, init.GetComponent<MoveObject>().row, init.transform.position);
                    }
                    else
                    {
                        checkTrung = true;
                    }

                    if (checkTrung)
                    {
                        setColor(Color.red);
                    }
                    else
                    {
                        setColor(Color.white);
                    }
                }
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        ShopManager.Instance.scroll.OnEndDrag(eventData);
        if (y == 100)
        {
            if (checkTrung)
            {
                ButtonManager.Instance.openShop();
                Language.Instance.notifyEngOrVi("Not place", "Không đủ chỗ");
                Destroy(init);
            }
            else if (init != null)
            {
                //check coin
                if (ResourceManager.Instance.checkResource(0, item.coin))
                {
                    int slkeo = PlayerPrefs.GetInt("slkeo" + index);
                    int slmax = PlayerPrefs.GetInt("slmax" + index);
                    int levelsd = PlayerPrefs.GetInt("levelsd" + index);
                    GameManager.Instance.setParent(idLoai, init);
                    if (idLoai == 3)
                    {
                        if (init.GetComponent<AnimalDrag>().checkCage)
                        {
                            if (slkeo + 1 <= slmax)
                            {
                                setShop(slkeo, slmax, levelsd);
                            }
                            init.GetComponent<AnimalDrag>().cage.openAnimal();
                            if (slkeo + 1 == 1 && index == 13)
                            {
                                Guide.Instance.stepGuide(9);
                            }
                        }
                        else
                        {
                            ButtonManager.Instance.openShop();
                            Language.Instance.notifyEngOrVi("Not in the right place", "Không đúng chỗ");
                        }
                        Destroy(init);
                    }
                    else
                    {
                        init.gameObject.name += slkeo;
                        init.GetComponent<MoveObject>().idLoai = idLoai;

                        PlayerPrefs.SetInt("scaleX" + init.gameObject.name, 1);

                        PlayerPrefs.SetFloat("posx" + init.gameObject.name, target.x);
                        PlayerPrefs.SetFloat("posy" + init.gameObject.name, target.y);

                        if (idLoai == 4)
                        {
                            StartCoroutine(init.GetComponent<Tree>().timeTree());
                            TimeManager.Instance.saveTime(init.gameObject.name);
                            if (Guide.Instance.step == 15)
                            {
                                init.GetComponent<Tree>().timeLive = 0;
                            }
                        }
                        //set lai order                 
                        GameManager.Instance.setArray(init.GetComponent<MoveObject>().col, init.GetComponent<MoveObject>().row, init.transform.position, true);
                        GameManager.Instance.setOrder(init, idLoai, init.transform.GetChild(0).position.y);
                    }

                    if (idLoai != 3 && idLoai != 5)
                    {
                        setShop(slkeo, slmax, levelsd);
                    }

                    if (idLoai == 5)
                    {
                        PlayerPrefs.SetInt("slkeo" + index, slkeo + 1);
                        GameManager.Instance.cameraOnOff(false);
                    }

                    if (idLoai == 0)
                    {
                        PlayerPrefs.SetInt("scaleX" + init.gameObject.name, -PlayerPrefs.GetInt("scaleX"));
                        init.transform.localScale = new Vector2(-PlayerPrefs.GetInt("scaleX"), 1f);
                        PlayerPrefs.SetInt("scaleX", -PlayerPrefs.GetInt("scaleX"));
                        //guide
                        if (slkeo + 1 == 7)
                        {
                            Guide.Instance.stepGuide(4);
                        }
                    }
                    else
                    {
                        PlayerPrefs.SetInt("scaleX" + init.gameObject.name, 1);
                    }

                    if (idLoai == 1)
                    {
                        if (!PlayerPrefs.HasKey("slnau" + init.gameObject.name))
                        {
                            PlayerPrefs.SetInt("slnau" + init.gameObject.name, 2);
                        }
                    }

                    if (idLoai == 1 || idLoai == 2)
                    {
                        Instantiate(GameManager.Instance.khoi, init.transform.GetChild(0).transform.position, Quaternion.identity);
                    }

                    //guide keo chuong ga xong
                    if (index == 12 && slkeo + 1 == 1)
                    {
                        Guide.Instance.stepGuide(8);
                    }
                    //guide keo nha may
                    if (index == 1 && slkeo + 1 == 1)
                    {
                        Guide.Instance.stepGuide(13);
                    }
                    //guide tree
                    if (index == 25 && slkeo + 1 == 1)
                    {
                        Guide.Instance.stepGuide(16);
                    }
                }
                else
                {
                    GameManager.Instance.cameraOnOff(false);
                    Destroy(init);
                }
            }
            LevelManager.Instance.huy = null;
        }

        if (checkDrag)
        {
            spr.transform.position = new Vector2(100f, 100f);
            transform.GetChild(1).gameObject.SetActive(true);
        }

        init = null;
    }

    void setShop(int slkeo, int slmax, int levelsd)
    {
        PlayerPrefs.SetInt("slkeo" + index, slkeo + 1);
        if (slkeo + 1 == slmax)
        {
            PlayerPrefs.SetInt("levelsd" + index, levelsd + item.plusLevel);
            PlayerPrefs.SetInt("slmax" + index, slmax + item.plusMaxsl);
            coin.transform.parent.gameObject.SetActive(false);
            btnLevel.gameObject.SetActive(true);
            unlock.SetActive(false);
            Language.Instance.setNameText("Cấp " + (levelsd + item.plusLevel), "Level " + (levelsd + item.plusLevel), textLock);
            icon.sprite = item.iconLock;
            icon.SetNativeSize();
            numberMax.text = PlayerPrefs.GetInt("slkeo" + index).ToString();
            GameManager.Instance.cameraOnOff(false);
            PlayerPrefs.SetInt("checkDrag", 1);
        }
        else
        {
            if (!GameManager.Instance.guide)
            {
                ButtonManager.Instance.openShop();
            }
        }
        number.text = PlayerPrefs.GetInt("slkeo" + index).ToString();
    }

    void setColor(Color color)
    {
        if (idLoai == 2)
        {
            init.transform.GetChild(1).GetComponent<SpriteRenderer>().color = color;
            init.transform.GetChild(2).GetComponent<SpriteRenderer>().color = color;
        }
        else if (idLoai == 4)
        {
            init.transform.GetChild(1).GetComponent<SpriteRenderer>().color = color;
        }
        else if (idLoai == 0)
        {
            init.GetComponent<SpriteRenderer>().color = color;
        }
        else
        {
            init.transform.GetChild(1).GetComponent<SpriteRenderer>().color = color;
        }
    }
}