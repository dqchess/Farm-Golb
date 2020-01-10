using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;

public class MoveObject : MonoBehaviour
{
    public static MoveObject Instance;
    public int col, row, idLoai;
    Vector3 target, pos;
    public bool checkDrag, checkTrung;
    float timeDown;
    GameObject iconMove;
    GameObject dialogMove;
    int scaleOld;
    Vector3 posOld;

    void Start()
    {
        if (PlayerPrefs.HasKey("scaleX" + gameObject.name))
        {
            transform.localScale = new Vector2(PlayerPrefs.GetInt("scaleX" + gameObject.name), 1f);
        }
        Instance = this;
        checkDrag = false;
    }

    private void OnMouseDown()
    {
        if (checkDrag)
        {
            GameManager.Instance.cameraOnOff(true);
            pos = CheckDistance.Instance.getScreenToWorldPoint();
        }
        else
        {
            if (GameManager.Instance.guide)
            {
                timeDown = 1000f;
            }
            else
            {
                AnimClick.Instance.clickObject(gameObject);
                CheckDistance.Instance.posOld();
                timeDown = Time.time;
            }
        }
    }

    private void OnMouseUp()
    {
        if (checkUI())
        {
            Language.Instance.onSound(1);
            if (CheckDistance.Instance.distance())
            {
                CheckDistance.Instance.moveCam(transform.GetChild(0).position);
            }
            if (Time.time - (timeDown + 0.5f) < 0.6f && iconMove != null)
            {
                Destroy(iconMove);
                StopCoroutine(coroutine);
            }
        }
        if (checkDrag)
        {
            GameManager.Instance.cameraOnOff(false);
        }
    }

    private void OnMouseDrag()
    {
        if (checkUI() && Time.time > timeDown + 0.5f && PlayerPrefs.GetInt("move") == 0 && PlayerPrefs.GetInt("checkDrag") == 1 && !GameManager.Instance.guide)
        {
            if (iconMove == null && dialogMove == null)
            {
                posOld = transform.position;
                iconMove = Instantiate(DialogController.Instance.iconMove, transform.GetChild(0).position, Quaternion.identity);
                coroutine = StartCoroutine(showDialogMove());
            }
        }

        if (checkDrag)
        {
            target = CheckDistance.Instance.getScreenToWorldPoint();
            if (target.x != pos.x)
            {
                target = GameManager.Instance.highlightMap.GetCellCenterWorld(GameManager.Instance.highlightMap.WorldToCell(target));
                target.z = -10;
                transform.position = target;
                checkTrungOb();
            }
        }
    }

    public void checkTrungOb()
    {
        dialogMove.transform.position = transform.GetChild(0).position;
        if ((target.y < 10 && target.y > -12) && (-18 < target.x && target.x < 17))
        {
            if (PlayerPrefs.GetInt("scaleX" + gameObject.name) == 1)
            {
                checkTrung = GameManager.Instance.checkTrung(col, row, transform.position);
            }
            else
            {
                checkTrung = GameManager.Instance.checkTrung(row, col, transform.position);
            }
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

    void setColor(Color color)
    {        
        if (idLoai == 2)
        {
            transform.GetChild(1).GetComponent<SpriteRenderer>().color = color;
            transform.GetChild(2).GetComponent<SpriteRenderer>().color = color;
        } else if (idLoai == 0)
        {            
            GetComponent<SpriteRenderer>().color = color;
        }
        else
        {
            transform.GetChild(1).GetComponent<SpriteRenderer>().color = color;
        }
    }

    public void backOld()
    {
        transform.localScale = new Vector2(scaleOld, 1f);
        PlayerPrefs.SetInt("scaleX" + gameObject.name, scaleOld);
        transform.position = posOld;
        setArrayLuoi(true, posOld);
        setColor(Color.white);
        setOrder();
    }

    public void setOrder()
    {
        if (checkDrag)
        {
            GameManager.Instance.setOrder(this.gameObject, idLoai, transform.GetChild(0).position.y);
            checkDrag = false;
            PlayerPrefs.SetInt("move", 0);
            Destroy(dialogMove);
        }
    }

    //them gia tri vao array trong GameManager    
    public void setArrayLuoi(bool bl, Vector3 pos)
    {
        if (PlayerPrefs.GetInt("scaleX" + gameObject.name) == 1)
        {
            GameManager.Instance.setArray(col, row, pos, bl);
        }
        else
        {
            GameManager.Instance.setArray(row, col, pos, bl);
        }
    }

    //check cham UI
    public bool checkUI()
    {
        return !EventSystem.current.IsPointerOverGameObject();
    }

    //move object and save
    Coroutine coroutine;
    IEnumerator showDialogMove()
    {
        yield return new WaitForSeconds(0.6f);
        setArrayLuoi(false, posOld);
        scaleOld = (int)transform.localScale.x;
        GameManager.Instance.cameraOnOff(true);
        checkDrag = true;
        PlayerPrefs.SetInt("move", 1);
        Destroy(iconMove);
        dialogMove = Instantiate(DialogController.Instance.dialogMove, transform.GetChild(0).position, Quaternion.identity);
        dialogMove.transform.GetChild(0).GetChild(1).GetComponent<Button>().onClick.AddListener(() => yesMove());
        dialogMove.transform.GetChild(0).GetChild(2).GetComponent<Button>().onClick.AddListener(() => scaleMove());
        dialogMove.transform.GetChild(0).GetChild(3).GetComponent<Button>().onClick.AddListener(() => noMove());
    }

    public void yesMove()
    {
        if (checkTrung)
        {
            backOld();
        }
        else
        {
            PlayerPrefs.SetFloat("posx" + gameObject.name, transform.position.x);
            PlayerPrefs.SetFloat("posy" + gameObject.name, transform.position.y);
            posOld = transform.position;
            posOld.z = posOld.y;
            transform.position = posOld;
            setArrayLuoi(true, transform.position);
            setOrder();
        }
    }

    public void scaleMove()
    {
        if ((int)transform.localScale.x == 1)
        {
            PlayerPrefs.SetInt("scaleX" + gameObject.name, -1);
            transform.localScale = new Vector2(-1, 1f);
        }
        else
        {
            PlayerPrefs.SetInt("scaleX" + gameObject.name, 1);
            transform.localScale = Vector2.one;
        }
        checkTrungOb();
    }

    public void noMove()
    {
        PlayerPrefs.SetInt("scaleX" + gameObject.name, scaleOld);
        transform.localScale = new Vector2(scaleOld, 1f);
        backOld();
    }
}