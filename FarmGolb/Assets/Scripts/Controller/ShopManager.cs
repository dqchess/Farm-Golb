using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;
    public SpriteRenderer spriteDrag;
    public GameObject[] btnShop;
    public Text[] nameBtnShop;
    public GameObject itemShop;
    public ScrollRect scroll;
    public Transform content;

    public GameObject unlock;
    public Image icon;
    public Text number;
    public Text numberMax;
    public Text coin;
    public Text nameItemShop;
    public Text textLock;
    public GameObject btnNumber, btnLevel;

    void Start()
    {
        Instance = this;
        loadNameButton();
        loadItemShop();
    }

    void loadNameButton()
    {
        Language.Instance.setNameText("Xây dựng", "Facility", nameBtnShop[0]);
        Language.Instance.setNameText("Con vật", "Animal", nameBtnShop[1]);
        Language.Instance.setNameText("Cây ăn quả", "Fruit trees", nameBtnShop[2]);
        Language.Instance.setNameText("Trang trí", "Decoration", nameBtnShop[3]);
    }

    public void loadItemShop()
    {
        int i = 0;
        foreach (DataShop.ItemShop item in GameManager.Instance.dataShop.dataItemShop)
        {
            if (!PlayerPrefs.HasKey("levelsd" + i))
            {
                if (i > 0)
                {
                    PlayerPrefs.SetInt("slkeo" + i, item.slkeo);
                    PlayerPrefs.SetInt("slmax" + i, item.slmax);
                    PlayerPrefs.SetInt("levelsd" + i, item.levelUse);
                }
            }

            itemShop.GetComponent<ItemShop>().index = i;
            itemShop.GetComponent<ItemShop>().idLoai = item.idType;

            Language.Instance.setNameText(item.nameVi, item.nameEng, nameItemShop);

            number.text = PlayerPrefs.GetInt("slkeo" + i).ToString();
            numberMax.text = PlayerPrefs.GetInt("slmax" + i).ToString();
            coin.text = item.coin.ToString();

            if (PlayerPrefs.GetInt("level") < PlayerPrefs.GetInt("levelsd" + i))
            {
                itemShop.transform.GetChild(0).gameObject.SetActive(false);
                btnLevel.gameObject.SetActive(true);
                coin.transform.parent.gameObject.SetActive(false);
                Language.Instance.setNameText("Cấp " + PlayerPrefs.GetInt("levelsd" + i).ToString(), "Level " + PlayerPrefs.GetInt("levelsd" + i).ToString(), textLock);
                icon.sprite = item.iconLock;
                if (PlayerPrefs.GetInt("slkeo" + i) == 0 || i > 32)
                {
                    btnNumber.gameObject.SetActive(false);
                }
                else
                {
                    btnNumber.gameObject.SetActive(true);
                    numberMax.text = PlayerPrefs.GetInt("slkeo" + i).ToString();
                }
            }
            else
            {
                itemShop.transform.GetChild(0).gameObject.SetActive(true);
                if (i < 33)
                {
                    btnNumber.gameObject.SetActive(true);
                }
                else
                {
                    btnNumber.gameObject.SetActive(false);
                }
                unlock.SetActive(true);
                icon.sprite = item.icon;
                btnLevel.gameObject.SetActive(false);
                coin.transform.parent.gameObject.SetActive(true);
                onImgNew();
                switch (item.idType)
                {
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
            }
            icon.SetNativeSize();
            Instantiate(itemShop, content);
            i++;
        }
        btnShop[0].SetActive(true);
        for (int j = 12; j < 43; j++)
        {
            content.GetChild(j).gameObject.SetActive(false);
        }
    }

    public int index1;
    public void openButton1()
    {
        SelectButtonShop(0);
        loadItemShop(index1, false);
        index1 = 0;
        loadItemShop(index1, true);
        openNew(0, false);
    }

    public void openButton2()
    {
        SelectButtonShop(1);
        loadItemShop(index1, false);
        index1 = 1;
        loadItemShop(index1, true);
        openNew(1, false);
    }

    public void openButton3()
    {
        SelectButtonShop(2);
        loadItemShop(index1, false);
        index1 = 2;
        loadItemShop(index1, true);
        openNew(2, false);
    }

    public void openButton4()
    {
        SelectButtonShop(3);
        loadItemShop(index1, false);
        index1 = 3;
        loadItemShop(index1, true);
        openNew(3, false);
    }

    void onoffbtn1(int i, int j, bool bl)
    {
        for (; i <= j; i++)
        {
            content.GetChild(i).gameObject.SetActive(bl);
        }
    }

    void loadItemShop(int index, bool bl)
    {
        switch (index)
        {
            case 0:
                onoffbtn1(0, 11, bl);
                break;

            case 1:
                onoffbtn1(12, 24, bl);
                break;

            case 2:
                onoffbtn1(25, 32, bl);
                break;

            case 3:
                onoffbtn1(33, 42, bl);
                break;
        }
    }

    void SelectButtonShop(int index)
    {
        //ShopManager.Instance.content.transform.localPosition = new Vector2(-480f, 96f);
        for (int i = 0; i < btnShop.Length; i++)
        {
            if (i == index)
            {
                btnShop[i].SetActive(true);
            }
            else
            {
                btnShop[i].SetActive(false);
            }
        }
    }

    public void onImgNew()
    {
        if (!ButtonManager.Instance.imgNew.activeSelf)
        {
            ButtonManager.Instance.imgNew.SetActive(true);
        }
    }

    public GameObject[] imgNew1;
    public void openNew(int i, bool check)
    {
        imgNew1[i].SetActive(check);
    }
}
