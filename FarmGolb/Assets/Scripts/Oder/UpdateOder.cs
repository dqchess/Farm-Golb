using UnityEngine;
using System.Collections;

public class UpdateOder : MonoBehaviour
{
    public static UpdateOder Instance;
    public GameObject itemOder;
    //buy oder
    public bool check, checkCar;
    public int coin, exp;
    public GameObject xehang, xetien, xekhong;
    public Transform posGo;

    void Start()
    {
        Instance = this;
        checkCar = false;
    }

    public void delete()
    {
        if (!GameManager.Instance.guide)
        {
            if (itemOder != null)
            {
                itemOder.GetComponent<ItemOder>().deleteItemOder();
                //cap nhat lai dau tich
                Oder.Instance.showOder();
            }
        }
    }

    public void buyItemOder()
    {
        if (check && !checkCar)
        {
            Oder.Instance.checkBan = false;

            transform.parent.transform.localScale = Vector3.zero;

            GameManager.Instance.cameraOnOff(false);

            //ban xong xe chay
            check = false;
            checkCar = true;

            xekhong.SetActive(false);

            //mo nhac xe o to di
            StartCoroutine(carRun());

            //cho chay time cua oder day
            ItemOder itemOrder = itemOder.GetComponent<ItemOder>();

            itemOrder.buyItemOder();

            itemOrder.loadItemOder();

            if (GameManager.Instance.guide)
            {
                Guide.Instance.openAll();
                PlayerPrefs.SetInt("step", 20);
            }
        }
        else
        {
            if (checkCar)
            {
                Language.Instance.notifyEngOrVi("Car go sell!", "Xe đang trở hàng!");
            }
            else
            {
                Language.Instance.notifyEngOrVi("No enough sell!", "Chưa đủ nông sản để bán!");
            }
        }
    }

    IEnumerator carRun()
    {
        Language.Instance.onSound(7);
        xehang.SetActive(true);
        yield return new WaitForSeconds(2f);
        LeanTween.move(xehang, posGo.position, 13f);
        yield return new WaitForSeconds(9f);
        xehang.transform.localPosition = new Vector2(-8.1f, 3.6f);
        xehang.SetActive(false);
        xetien.SetActive(true);
    }
}
