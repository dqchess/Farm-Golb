using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MachineController : MonoBehaviour
{
    public static MachineController Instance;
    //button nhan thêm ô sản xuất
    public RectTransform btnBoxProduce;
    public int numberBoxProduce;
    Vector2[] vector;

    int idMachine;
    GameObject machine;
    //sl dang sx
    int numberProducing;

    //image product
    //image san pham nau
    public Image[] imgBoxProduce;
    public GameObject[] itemProduct;
    public Text nameMachine, minutes, minutes1, second;
    public Image slider;

    private void Awake()
    {
        Instance = this;
        vector = new Vector2[] { new Vector2(148f, -91f),
            new Vector2(260f, -91f),
            new Vector2(371f, -91f)
        };
    }

    private void OnDisable()
    {
        if (machine != null)
        {
            machine.GetComponent<Machine>().showtime = false;
        }
    }

    //load item nen dialog
    public void loadItemMachine(GameObject machine, int idMachine, int numberProducing)
    {
        this.idMachine = idMachine;
        this.machine = machine;
        this.numberProducing = numberProducing;
        numberBoxProduce = PlayerPrefs.GetInt("slnau" + machine.name);
        Language.Instance.setNameText(GameManager.Instance.dataShop.dataItemShop[idMachine].nameVi, GameManager.Instance.dataShop.dataItemShop[idMachine].nameEng, nameMachine);
        initIdProduct();
        //load 3 item
        for (int i = 0; i < 3; i++)
        {
            loadItemProduct(i, 0);
        }
        showBoxProduce();
        updateIconProducing(numberProducing);
        check = true;
    }

    bool check;
    public void changeItemMachine()
    {
        if (check)
        {
            itemProduct[2].SetActive(false);
            for (int i = 0; i <= idProductLast - idProductFirst - 3; i++)
            {
                loadItemProduct(i, 3);
                if (i == 2)
                {
                    itemProduct[2].SetActive(true);
                }
            }
        }
        else
        {
            itemProduct[2].SetActive(true);
            for (int i = 0; i < 3; i++)
            {
                loadItemProduct(i, 0);
            }
        }
        check = !check;
    }

    void loadItemProduct(int i, int a)
    {
        itemProduct[i].SetActive(true);
        AnimClick.Instance.runTest(itemProduct[i]);
        if (GameManager.Instance.dataStorage.dataStorages[idProductFirst + i + a].levelUse > PlayerPrefs.GetInt("level"))
        {
            itemProduct[i].transform.GetChild(0).gameObject.SetActive(false);
            itemProduct[i].GetComponent<Image>().sprite = GameManager.Instance.dataStorage.dataStorages[idProductFirst + i + a].iconLock;
        }
        else
        {
            itemProduct[i].transform.GetChild(0).gameObject.SetActive(true);
            itemProduct[i].GetComponent<Image>().sprite = GameManager.Instance.dataStorage.dataStorages[idProductFirst + i + a].icon;
        }
        itemProduct[i].GetComponent<DragItemMachine>().idItemMachine = idProductFirst + i + a;
        itemProduct[i].GetComponent<DragItemMachine>().itemNL = GameManager.Instance.dataMachine.dataMachine[idProductFirst + i + a - 33].nguyenlieu;
        itemProduct[i].GetComponent<Image>().SetNativeSize();
    }

    public void boxProduce()
    {
        if (numberBoxProduce < 6)
        {
            if (ResourceManager.Instance.checkResource(1, 3))
            {
                ++numberBoxProduce;
                if (numberBoxProduce < 5)
                {
                    btnBoxProduce.localPosition = vector[numberBoxProduce - 2];
                    imgBoxProduce[numberBoxProduce].transform.parent.gameObject.SetActive(true);
                }
                PlayerPrefs.SetInt("slnau" + machine.name, numberBoxProduce);
            }
        }
        if (numberBoxProduce == 5)
        {
            btnBoxProduce.gameObject.SetActive(false);
        }
    }
    void showBoxProduce()
    {
        if (numberBoxProduce == 5)
        {
            btnBoxProduce.gameObject.SetActive(false);
        }
        else
        {
            btnBoxProduce.gameObject.SetActive(true);
            btnBoxProduce.localPosition = vector[numberBoxProduce - 2];
        }

        for (int i = 0; i < 5; i++)
        {
            imgBoxProduce[i].gameObject.SetActive(false);

            if (i <= numberBoxProduce && i > 2)
            {
                imgBoxProduce[i].transform.parent.gameObject.SetActive(true);
            }

            if (numberBoxProduce < i)
            {
                imgBoxProduce[i].transform.parent.gameObject.SetActive(false);
            }
        }
    }
    //id : 1 - 12
    int idProductFirst, idProductLast;
    void initIdProduct()
    {
        switch (idMachine)
        {
            case 1:
                idProductFirst = 33;
                idProductLast = 38;
                break;
            case 2:
                idProductFirst = 39;
                idProductLast = 43;
                break;
            case 3:
                idProductFirst = 44;
                idProductLast = 48;
                break;
            case 4:
                idProductFirst = 49;
                idProductLast = 53;
                break;
            case 5:
                idProductFirst = 54;
                idProductLast = 58;
                break;
            case 6:
                idProductFirst = 59;
                idProductLast = 63;
                break;
            case 7:
                idProductFirst = 64;
                idProductLast = 68;
                break;
            case 8:
                idProductFirst = 69;
                idProductLast = 73;
                break;
            case 9:
                idProductFirst = 74;
                idProductLast = 78;
                break;
            case 10:
                idProductFirst = 79;
                idProductLast = 83;
                break;
            case 11:
                idProductFirst = 84;
                idProductLast = 88;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (checkNau)
        {
            producingItem();
        }
        else
        {
            if (numberProducing < 5 && numberProducing < numberBoxProduce && numberProducing + machine.GetComponent<Machine>().idVpNauXong.Count < 6)
            {
                //Language.Instance.notifyEngOrVi("Not enough resource", "Không đủ nguyên liệu");
                GameManager.Instance.listUserDiamond.Add(gameObject);
                DialogController.Instance.dialogBuyDiamond.SetActive(true);
                DialogController.Instance.dialogBuyDiamond.GetComponent<BuyDiamond>().setImage(idProduct);
            }
            else
            {
                Language.Instance.notifyEngOrVi("Machine is full", "Nhà máy đã đầy");
            }            
        }

        if (GameManager.Instance.guide)
        {
            Guide.Instance.stepGuide(14);
        }
    }

    public DataMachine.nguyenLieu[] listNL;
    public int idProduct;
    public bool checkNau;
    public void producingItem()
    {
        if (numberProducing < 5 && numberProducing < numberBoxProduce && numberProducing + machine.GetComponent<Machine>().idVpNauXong.Count < 6)
        {
            machine.GetComponent<Machine>().producing(idProduct);
            if (checkNau)
            {
                foreach (DataMachine.nguyenLieu item in listNL)
                {
                    StorageController.Instance.updateStorage(0, item.id, -item.number);
                }
            }
            ++numberProducing;
            showTimeProduce();
            showIconProduce(numberProducing - 1, idProduct);
            if (numberProducing == 0)
            {
                //hien time nen
                transform.GetChild(8).gameObject.SetActive(true);
            }
        }
        else
        {
            Language.Instance.notifyEngOrVi("Machine is full", "Nhà máy đã đầy");
        }

    }

    //dung kim cuong nhanh
    public void useDiamond()
    {
        if (machine.GetComponent<Machine>().timeLive > 0 && ResourceManager.Instance.checkResource(1, 2))
        {
            machine.GetComponent<Machine>().timeLive = 0;
            if (GameManager.Instance.guide)
            {
                Destroy(Guide.Instance.tayChiKimCuong);
                StartCoroutine(offGuide());
            }
        }
    }

    IEnumerator offGuide()
    {
        yield return new WaitForSeconds(0.7f);
        gameObject.SetActive(false);
    }

    //update image producing khi click vao nha may
    public void updateIconProducing(int slDangsx)
    {
        numberProducing = slDangsx;

        showTimeProduce();

        for (int i = 0; i < 5; i++)
        {
            if (numberProducing > i)
            {
                showIconProduce(i, machine.GetComponent<Machine>().idVatPham[i]);
            }
            else
            {
                imgBoxProduce[i].gameObject.SetActive(false);
            }
        }
    }

    //khi keo nau
    void showIconProduce(int index, int idSp)
    {
        imgBoxProduce[index].gameObject.SetActive(true);
        imgBoxProduce[index].sprite = GameManager.Instance.dataStorage.dataStorages[idSp].icon;
        imgBoxProduce[index].SetNativeSize();
    }

    void showTimeProduce()
    {
        if (numberProducing == 0)
        {
            transform.GetChild(8).gameObject.SetActive(false);
            transform.GetChild(7).transform.localPosition = new Vector2(-25f, -8f);
            imgBoxProduce[0].gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(8).gameObject.SetActive(true);
            transform.GetChild(7).transform.localPosition = new Vector2(-43f, 44f);
            imgBoxProduce[0].gameObject.SetActive(true);
        }
    }

    public void showTimeMachine(int timeLive, int time)
    {
        if (timeLive > 60)
        {
            minutes.text = (timeLive / 60).ToString();
            second.text = (timeLive % 60).ToString();
            minutes1.text = ".m";
        }
        else
        {
            minutes.text = "";
            minutes1.text = "";
            second.text = timeLive.ToString();
        }
        slider.fillAmount = 1 - (float)timeLive / time;
    }
}