using UnityEngine;
using UnityEngine.UI;
using BitBenderGames;
using System.Collections;

public class Guide : MonoBehaviour
{
    public static Guide Instance;
    public int guide;

    //khoa tat ca cac thuoc tinh
    public Button shop;
    public Button[] button;
    public ScrollRect scrollShop;
    public GameObject silo;
    public GameObject barn;
    public GameObject oder;
    public GameObject market;
    public int step;

    //ban tay
    public GameObject tayChi;
    public GameObject tayChiShop;
    public GameObject tayKeoThuH;
    public GameObject tayKeoShop;
    public GameObject tayChiKimCuong;

    //tay keo Drag thu hoach odat
    //public Transform DragSickle;

    //tay keo Machine
    //public Transform DragMachine;
    public GameObject tayKeoMachine;
    //tay keo oder
    public Transform UIOder;
    public Transform tayChiOder;
    public bool check;
    Vector2 pos;
    public GameObject tree;
    //MobileTouchCamera camMobile;

    //private void Update()
    //{
    //    if(GameManager.Instance.guide)
    //    {
    //        camMobile.ComputeCamBoundaries();
    //    }
    //}

    void Start()
    {
        Instance = this;
        //camMobile = Camera.main.GetComponent<MobileTouchCamera>();
        step = PlayerPrefs.GetInt("step");
        //step = 13;
        if (GameManager.Instance.guide)
        {
            tree.GetComponent<BoxCollider2D>().enabled = false;
            //tao tay chi tay keo
            tayChi = Instantiate(tayChi, new Vector2(100f, 100f), Quaternion.identity);
            //tayKeoThuH = Instantiate(tayKeoThuH, DragSickle);
            tayChiShop = Instantiate(tayChiShop, shop.transform);
            tayChiShop.SetActive(false);
            tayKeoShop = Instantiate(tayKeoShop, scrollShop.transform);
            //tayChiKimCuong = Instantiate(tayChiKimCuong, DragMachine);
            //tayChiKimCuong.SetActive(false);
            //tayKeoMachine = Instantiate(tayKeoThuH, DragMachine);
            //tayKeoMachine.transform.localPosition = new Vector2(61f, 87f);
            tayChiOder = Instantiate(tayChiOder, UIOder);
            Camera.main.GetComponent<TouchInputController>().enabled = false;
            Camera.main.GetComponent<MobileTouchCamera>().enabled = false;

            if (step == 5)
            {
                step = 4;
            }

            if (step > 12)
            {
                GameManager.Instance.ObCage.GetChild(0).GetComponent<PolygonCollider2D>().enabled = false;
            }

            if (step > 8)
            {
                offODat(false);
            }

            if (step == 14)
            {
                //pos = GameManager.Instance.ObMachine.transform.GetChild(0).GetChild(0).transform.position;
                //moveCam(pos);
                //tayChi.transform.position = pos;
                step = 13;
            }

            if (step == 16)
            {
                check = true;
            }
            //khoa cac item lai
            offUI(false);
            stepGuide(step);            
        }
        else
        {
            DialogController.Instance.dialogDaily.SetActive(true);
        }
    }

    public void stepGuide(int step)
    {
        this.step = step;        
        if (GameManager.Instance.guide)
        {
            //step thu hoach keo o dat trong cay
            if (step == 0)
            {
                //hien ban tay chi o dat
                tayChi.transform.position = new Vector2(-7.65f, 1.6f);
            }
            else if (step == 1)
            {
                tayChi.transform.position = new Vector2(100f, 100f);
            }
            else if (step == 2)
            {
                offODat(false);
                shop.enabled = true;
                tayChiShop.SetActive(true);
            }
            else if (step == 3)
            {
                shop.enabled = false;
                tayChiShop.SetActive(false);
                ButtonManager.Instance.openShop();
            }
            else if (step == 4)
            {
                //khoa 6 o dat con lai
                for (int i = 0; i < 6; i++)
                {
                    if (GameManager.Instance.ObODat.transform.GetChild(i).GetComponent<Field>().sprCayTrong.sprite != null)
                    {
                        GameManager.Instance.ObODat.transform.GetChild(i).GetComponent<PolygonCollider2D>().enabled = false;
                    }
                    else
                    {
                        GameManager.Instance.ObODat.transform.GetChild(i).GetComponent<PolygonCollider2D>().enabled = true;
                    }
                }
                //keo o dat xong hien ban tay chi dat
                moveCam(GameManager.Instance.ObODat.transform.GetChild(6).transform.position);
                tayChi.transform.position = GameManager.Instance.ObODat.transform.GetChild(6).transform.position;
            }
            else if (step == 5)
            {
                tayChi.transform.position = new Vector2(100f, 100f);
            }
            else if (step == 6)
            {
                offODat(false);
                shop.enabled = true;
                tayChiShop.SetActive(true);
            }
            else if (step == 7)
            {
                //keo chuong ga
                shop.enabled = false;
                tayChiShop.SetActive(false);
                //shop bat nen 
                ButtonManager.Instance.openShop();
                ShopManager.Instance.openButton2();
            }
            else if (step == 8)
            {
                //hien keo ga va di chuyen cam den chuong ga                
                moveCam(GameManager.Instance.ObCage.GetChild(0).transform.position);
                tayKeoShop.transform.localPosition = new Vector2(265f, 0f);
                ButtonManager.Instance.openShop();
                ShopManager.Instance.openButton2();
            }
            else if (step == 9)
            {
                //tay chi chuong ga
                shop.enabled = false;
                tayChi.transform.position = GameManager.Instance.ObCage.transform.GetChild(0).GetChild(0).transform.position;
            }
            else if (step == 10)
            {
                //tay keo thu hoach                
                tayChi.transform.position = GameManager.Instance.ObCage.transform.GetChild(0).GetChild(0).transform.position;
                tayKeoThuH.transform.localPosition = new Vector2(-60f, -52f);
            }
            else if (step == 11)
            {
                //nen level                  
                tayKeoThuH.transform.localPosition = Vector2.zero;
                if (PlayerPrefs.GetInt("level") == 2)
                {
                    LevelManager.Instance.dialogLevelUp.SetActive(true);
                    LevelManager.Instance.loadItemLevel();
                }
                else
                {
                    LevelManager.Instance.tangExp(PlayerPrefs.GetInt("expmax") - PlayerPrefs.GetInt("exp"));
                }
            }
            else if (step == 12)
            {
                //khoa chuong ga
                GameManager.Instance.ObCage.GetChild(0).GetComponent<PolygonCollider2D>().enabled = false;
                //vao day tay chi shop hien ra keo 
                shop.enabled = false;
                tayKeoShop.transform.localPosition = new Vector2(265f, 0f);
                ButtonManager.Instance.openShop();
                ShopManager.Instance.openButton1();
            }
            else if (step == 13)
            {
                //di chuyen den nha may
                GameManager.Instance.ObCage.GetChild(0).GetComponent<PolygonCollider2D>().enabled = false;
                pos = GameManager.Instance.ObMachine.transform.GetChild(0).GetChild(0).transform.position;
                moveCam(pos);
                shop.enabled = false;
                tayChi.transform.position = pos;
            }
            else if (step == 14)
            {
                //chi xong cho tay chi dung kim cuong nhanh
                //di chuyen den nha may ca Ui
                //UIMachine hien ra
                //tat tay keo machine di
                //hien tay chi kiem cuong
                //neu con time cua nha may hien ban tay chi nha may(hi huu)
                //ngc lai dung kim cuong                        
                tayKeoMachine.transform.position = new Vector2(100, 100);
                //tayChiKimCuong.SetActive(true);
                if (!check)
                {
                    tayChiKimCuong = Instantiate(Guide.Instance.tayChiKimCuong, DialogController.Instance.dialogMachine.transform.GetChild(0).transform);
                    tayChiKimCuong.transform.localPosition = new Vector2(5, -3);
                }
                check = true;
            }
            else if (step == 15)
            {
                tayChi.transform.position = new Vector2(100f, 100f);
                tayKeoShop.transform.localPosition = Vector2.zero;
                ButtonManager.Instance.openShop();
                ShopManager.Instance.openButton3();
            }
            else if (step == 16)
            {
                GameManager.Instance.ObMachine.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
                moveCam(GameManager.Instance.ObTree.GetChild(0).transform.position);
                shop.enabled = false;
                //if (check)
                //{
                pos = GameManager.Instance.ObTree.transform.GetChild(0).GetChild(0).transform.position;
                tayChi.transform.position = new Vector2(pos.x, pos.y + 0.5f);
                //}
            }
            else if (step == 17)
            {
                tayChi.transform.position = GameManager.Instance.ObTree.transform.GetChild(0).GetChild(0).transform.position;
            }
            else if (step == 18)
            {
                tayChi.transform.position = new Vector2(100f, 100f);
                StartCoroutine(moveTreeDestroy());
            }
            else if (step == 19)
            {
                StartCoroutine(moveOder());
            }
            PlayerPrefs.SetInt("step", step);
        }
    }

    IEnumerator moveOder()
    {
        yield return new WaitForSeconds(1.5f);
        oder.GetComponent<BoxCollider2D>().enabled = true;
        moveCam(oder.transform.position);
        tayChi.transform.position = oder.transform.position;
    }

    IEnumerator moveTreeDestroy()
    {
        tree.GetComponent<BoxCollider2D>().enabled = true;
        yield return TimeManager.Instance.wait;
        pos = tree.transform.position;
        moveCam(pos);
        tayChi.transform.position = new Vector2(pos.x, pos.y + 0.5f);
    }

    public void moveCam(Vector2 pos)
    {
        LeanTween.move(Camera.main.gameObject, pos, 0.5f);
    }

    public void openAll()
    {
        GameManager.Instance.guide = false;
        Camera.main.GetComponent<TouchInputController>().enabled = true;
        Camera.main.GetComponent<MobileTouchCamera>().enabled = true;
        offUI(true);
        GameManager.Instance.ObMachine.GetChild(0).GetComponent<BoxCollider2D>().enabled = true;
        GameManager.Instance.ObCage.GetChild(0).GetComponent<PolygonCollider2D>().enabled = true;
        offODat(true);
        tayChiOder.gameObject.SetActive(false);
        tayChiShop.SetActive(false);
        //tayKeoMachine.SetActive(false);
        tayKeoShop.SetActive(false);
        tayKeoThuH.SetActive(false);
        StartCoroutine(showReward());
    }

    IEnumerator showReward()
    {
        yield return new WaitForSeconds(5f);
        DialogController.Instance.dialogDaily.SetActive(true);
    }

    void offODat(bool bl)
    {
        for (int i = 0; i < GameManager.Instance.ObODat.childCount; i++)
        {
            GameManager.Instance.ObODat.transform.GetChild(i).GetComponent<PolygonCollider2D>().enabled = bl;
        }
    }

    void offUI(bool bl)
    {
        shop.enabled = bl;
        for (int i = 0; i < button.Length; i++)
        {
            button[i].enabled = bl;
        }
        scrollShop.enabled = bl;
        barn.GetComponent<BoxCollider2D>().enabled = bl;
        silo.GetComponent<BoxCollider2D>().enabled = bl;
        oder.GetComponent<BoxCollider2D>().enabled = bl;
        market.GetComponent<BoxCollider2D>().enabled = bl;
    }
}