using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;

public class ItemBox : MonoBehaviour, IPointerDownHandler
{
    public int id;

    [HideInInspector]
    public int idSp;
    //[HideInInspector]
    public int timeLive;
    [HideInInspector]
    public Text txtMinutes, txtSeconds, txtName;
    [HideInInspector]
    public Image iconSp, iconBox;

    bool check, bl;

    public Sprite sprBoxClose, sprBoxOpen;

    public Boat Boat;

    public bool checkThuH;

    Coroutine cor;

    void Start()
    {
        PlayerPrefs.DeleteKey("BoxOpen");
        txtName = transform.GetChild(1).GetChild(0).GetComponent<Text>();
        iconBox = transform.GetChild(0).GetComponent<Image>();
        iconSp = transform.GetChild(2).GetComponent<Image>();
        txtMinutes = transform.GetChild(1).GetChild(1).GetComponent<Text>();
        txtSeconds = txtMinutes.transform.GetChild(0).GetComponent<Text>();

        //save data
        bl = true;
        checkBoxFour();
        checkThuH = false;

        //save time
        if (PlayerPrefs.HasKey("ItemSpBoat" + id))
        {
            timeLive = TimeManager.Instance.timeOutApp(gameObject.name);
            idSp = PlayerPrefs.GetInt("ItemSpBoat" + id);
            iconSp.sprite = GameManager.Instance.dataStorage.dataStorages[idSp].icon;
            iconSp.SetNativeSize();
            Language.Instance.setNameText(GameManager.Instance.dataStorage.dataStorages[idSp].nameVi, GameManager.Instance.dataStorage.dataStorages[idSp].nameEng, txtName);
            iconSp.SetNativeSize();
            if (timeLive < 240)
            {
                transform.GetChild(1).gameObject.SetActive(true);
                timeLive = 240 - timeLive;
                Boat.transform.position = new Vector2(23, -11);
                cor = StartCoroutine(timeBox());
            }
            else
            {
                checkThuH = true;
                check = true;
                timeLive = 0;
                XongHang();
                //transform.GetChild(1).gameObject.SetActive(true);
            }
            Boat.GetComponent<Boat>().itemBox = GetComponent<ItemBox>();
            iconBox.sprite = sprBoxClose;
            iconBox.SetNativeSize();
            transform.GetChild(2).gameObject.SetActive(true);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (check && checkThuH)
        {
            Vector2 pos = Camera.main.transform.position;
            FlyHarvest.Instance.iconFlyBay(null, pos, GameManager.Instance.dataStorage.dataStorages[idSp].coinSell, 1);
            // thu hoach tien
            iconBox.sprite = sprBoxOpen;
            iconBox.SetNativeSize();
            iconSp.gameObject.SetActive(false);
            check = false;
            checkThuH = false;
            PlayerPrefs.DeleteKey("ItemSpBoat" + id);
        }

        if (id == 4 && ResourceManager.Instance.checkResource(1, 10) && !PlayerPrefs.HasKey("BoxOpen"))
        {
            PlayerPrefs.SetInt("BoxOpen", 1);
            transform.GetChild(0).gameObject.SetActive(true);
            //transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(3).gameObject.SetActive(false);
            bl = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (timeLive <= 0 && bl && !check)
        {
            idSp = PlayerPrefs.GetInt("ItemSpBoat", 0);
            if (PlayerPrefs.GetInt("ns" + idSp) - 1 >= 0)
            {
                transform.GetChild(1).gameObject.SetActive(true);
                check = true;
                timeLive = 240;                
                PlayerPrefs.SetInt("ItemSpBoat" + id, idSp);
                Language.Instance.setNameText(GameManager.Instance.dataStorage.dataStorages[idSp].nameVi, GameManager.Instance.dataStorage.dataStorages[idSp].nameEng, txtName);
                transform.GetChild(1).gameObject.SetActive(true);
                iconBox.sprite = sprBoxClose;
                iconBox.SetNativeSize();
                iconSp.gameObject.SetActive(true);
                iconSp.sprite = GameManager.Instance.dataStorage.dataStorages[idSp].icon;
                iconSp.SetNativeSize();
                cor = StartCoroutine(timeBox());
                txtMinutes.gameObject.SetActive(true);
                StartCoroutine(Boat.go());
                Boat.GetComponent<Boat>().itemBox = GetComponent<ItemBox>();
                //save time
                TimeManager.Instance.saveTime(gameObject.name);
                Language.Instance.setNameText(GameManager.Instance.dataStorage.dataStorages[idSp].nameVi, GameManager.Instance.dataStorage.dataStorages[idSp].nameEng, txtName);
                StorageController.Instance.updateStorage(0, idSp, -1);
            }
            else
            {
                Language.Instance.notifyEngOrVi("No enough resource", "Không đủ sản phẩm để bán");
            }         
        }
    }

    IEnumerator timeBox()
    {
        yield return TimeManager.Instance.wait;
        if (--timeLive > 0)
        {
            txtMinutes.text = (timeLive / 60).ToString();
            txtSeconds.text = (timeLive - timeLive / 60 * 60).ToString();
            StartCoroutine(timeBox());
        }
        else
        {
            //xong hang   
            StartCoroutine(Boat.goHome());
            XongHang();
        }
    }

    void XongHang()
    {
        txtMinutes.gameObject.SetActive(false);
        check = true;
        transform.GetChild(1).gameObject.SetActive(false);

        //thuyen ve ben
    }

    void checkBoxFour()
    {
        if (id == 4)
        {
            if (PlayerPrefs.GetInt("BoxOpen") == 1)
            {
                //mo Box va cho keo
                transform.GetChild(0).gameObject.SetActive(true);
                //transform.GetChild(1).gameObject.SetActive(true);
                bl = true;
            }
            else
            {
                transform.GetChild(3).gameObject.SetActive(true);
                //tat box
                bl = false;
            }
        }
    }

    public void dungNhanh()
    {
        if (ResourceManager.Instance.checkResource(0, 350))
        {
            checkThuH = true;
            StopCoroutine(cor);
            timeLive = 0;
            Boat.go1();
            XongHang();
        }
    }
}