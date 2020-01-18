using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;

public class ItemBox : MonoBehaviour, IPointerDownHandler
{
    public int id;

    [HideInInspector]
    public int idSp;
    [HideInInspector]
    public int timeLive;
    [HideInInspector]
    public Text txtMinutes, txtSeconds, txtName;
    [HideInInspector]
    public Image iconSp, iconBox;

    bool check, bl;

    public Sprite sprBoxClose, sprBoxOpen;

    public Boat Boat;

    void Start()
    {
        PlayerPrefs.DeleteKey("BoxOpen");
        txtName = transform.GetChild(1).GetChild(1).GetComponent<Text>();
        iconBox = transform.GetChild(0).GetComponent<Image>();
        iconSp = transform.GetChild(2).GetComponent<Image>();
        txtMinutes = transform.GetChild(1).GetChild(1).GetComponent<Text>();
        txtSeconds = txtMinutes.transform.GetChild(0).GetComponent<Text>();

        //save data
        bl = true;
        checkBoxFour();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (check)
        {
            // thu hoach tien
            iconSp.gameObject.SetActive(false);
            check = false;
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
            transform.GetChild(1).gameObject.SetActive(true);
            check = false;            
            timeLive = 240;
            idSp = PlayerPrefs.GetInt("ItemSpBoat", 0);
            PlayerPrefs.SetInt("ItemSpBoat" + id, idSp);
            Language.Instance.setNameText(GameManager.Instance.dataStorage.dataStorages[idSp].nameVi, GameManager.Instance.dataStorage.dataStorages[idSp].nameEng, txtName);
            transform.GetChild(1).gameObject.SetActive(true);
            iconBox.sprite = sprBoxClose;
            iconBox.SetNativeSize();
            iconSp.gameObject.SetActive(true);
            iconSp.sprite = GameManager.Instance.dataStorage.dataStorages[idSp].icon;
            iconSp.SetNativeSize();
            StartCoroutine(timeBox());
            txtMinutes.gameObject.SetActive(true);
            StartCoroutine(Boat.go());
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
}