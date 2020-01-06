using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RecieveCoin : MonoBehaviour
{
    private bool isRun;
    private bool PointedCoin;
    private int indexArrayCoin;
    private int numberPointsCoin = 25;
    private float speed = 2f;
    private Vector3[] positionCoin;
    private Transform pointerCoin;
    public int numberCoin;
    public int id;
    [SerializeField] GameObject Coin1;        //doi tuong bay    
    [SerializeField] Text NumberCoinText;   //so coin   
    [SerializeField] Transform pointerRight, pointerLeft;//vi tri luon xuong
    [SerializeField] Sprite exp, coin;
    public string obBayDen;
    public Sprite spNhaMay;

    Image imgIcon;

    void Start()
    {
        imgIcon = transform.GetChild(0).GetComponent<Image>();

        if (id == 0)
        {
            pointerCoin = DialogController.Instance.cloverExp;
        }
        if (id == 1)
        {
            //coin
            imgIcon.sprite = coin;
            pointerCoin = DialogController.Instance.coin;
        }
        if (id == 2)
        {
            //sp nha may, field
            pointerCoin = DialogController.Instance.coin;
            imgIcon.sprite = spNhaMay;
        }
        else if (id == 3)
        {
            //kim cuong
            pointerCoin = DialogController.Instance.diamond;
            imgIcon.sprite = spNhaMay;
        }
        else if (id == 4)
        {
            //fly item random
            pointerCoin = DialogController.Instance.coin;
            imgIcon.sprite = spNhaMay;
            LeanTween.scale(imgIcon.gameObject, new Vector2(1.5f, 1.5f), 1f);
        }

        imgIcon.SetNativeSize();
        positionCoin = new Vector3[numberPointsCoin];
        NumberCoinText.text = "x" + numberCoin;
        DrawQuadraticCurveItem();
        //StartCoroutine(waitRun());
        ItemFly();
        StartCoroutine(upSpeed());
        Coin1.SetActive(true);
    }

    void DrawQuadraticCurveItem()
    {
        for (int i = 1; i < numberPointsCoin + 1; i++)
        {
            float t = i / (float)numberPointsCoin;
            if (id == 0)
            {
                positionCoin[i - 1] = CalculateQuadraticBezierPoint(t, transform.position, pointerLeft.position, pointerCoin.position);
            }
            else
            {
                positionCoin[i - 1] = CalculateQuadraticBezierPoint(t, transform.position, pointerRight.position, pointerCoin.position);
            }
        }
    }

    Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = uu * p0;
        p += 2 * u * t * p1;
        p += tt * p2;
        p.z = 0;
        return p;
    }

    IEnumerator upSpeed()
    {
        yield return new WaitForSeconds(0.3f);
        //speed += 1.5f;
        if (id != 4)
        {
            speed += 2;
        }
        else
        {
            speed = 7;
        }
        StartCoroutine(upSpeed());
    }

    IEnumerator waitRun()
    {
        yield return new WaitForSeconds(1f);
        ItemFly();
    }

    void ItemFly()
    {
        StartCoroutine(bay());
    }

    IEnumerator bay()
    {
        if (id != 4)
        {
            yield return new WaitForSeconds(.1f);
        }
        else
        {
            yield return new WaitForSeconds(1.1f);
        }
        transform.GetChild(0).GetComponent<Animator>().enabled = false;
        isRun = true;
        DrawQuadraticCurveItem();
        if (Vector3.Distance(Coin1.transform.position, positionCoin[indexArrayCoin]) < 0.1f)
        {
            if (indexArrayCoin < positionCoin.Length - 1) indexArrayCoin = indexArrayCoin + 1;
        }
        if (Vector3.Distance(Coin1.transform.position, positionCoin[numberPointsCoin - 1]) < 0.1f)
        {
            if (PointedCoin == false)
            {
                //bay den vi tri va cong kinh nghiem   
                Coin1.SetActive(false);
                Destroy(gameObject);
                PointedCoin = true;
                if (id == 0)
                {
                    LevelManager.Instance.tangExp(numberCoin);
                }
                else if (id == 1)
                {
                    //coin
                    ResourceManager.Instance.plus(0, numberCoin);
                }
                else if (id == 3)
                {
                    //diamond
                    ResourceManager.Instance.plus(1, numberCoin);
                }                
            }
        }
        Coin1.transform.position = Vector3.MoveTowards(Coin1.transform.position, positionCoin[indexArrayCoin], Time.deltaTime * speed);
    }

    void Update()
    {
        if (isRun == true)
        {
            ItemFly();
            if (PointedCoin == true) Destroy(gameObject);
        }
    }
}
