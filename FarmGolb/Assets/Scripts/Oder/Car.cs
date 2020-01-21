using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class Car : MonoBehaviour
{
    public static Car instance;
    public int coin, exp;
    public Transform endtien2;
    Vector2 pos;
    public GameObject xekhong;

    private void Start()
    {
        instance = this;
    }

    private void OnEnable()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        //mo thi chay xe
        pos = transform.position;
        LeanTween.move(gameObject, endtien2.position, 6f);
        StartCoroutine(musicCarend());
    }

    IEnumerator musicCarend()
    {
        yield return new WaitForSeconds(6f);
        Language.Instance.onSound(8);
        GetComponent<BoxCollider2D>().enabled = true;
        //khi bat cac dialo
        if (PlayerPrefs.GetInt("step") == 20)
        {
            Guide.Instance.tayChi.transform.position = transform.position;
            Guide.Instance.moveCam(transform.position);
            DialogController.Instance.close();
            DialogController.Instance.closeDialogUI();
            GameManager.Instance.cameraOnOff(false);
        }
    }

    private void OnMouseUp()
    {
        if (!EventSystem.current.IsPointerOverGameObject(0))
        {
            Language.Instance.onSound(2);
            UpdateOder.Instance.checkCar = false;
            FlyHarvest.Instance.iconFlyBay(null, transform.position, UpdateOder.Instance.exp, 0);
            FlyHarvest.Instance.iconFlyBay(null, transform.position, UpdateOder.Instance.coin, 1);
            transform.position = pos;
            xekhong.SetActive(true);
            gameObject.SetActive(false);
            Oder.Instance.checkBan = true;
            if (PlayerPrefs.GetInt("step") == 20)
            {
                GameManager.Instance.cameraOnOff(false);
                Guide.Instance.tayChi.SetActive(false);
                PlayerPrefs.SetInt("step", 21);
                Language.Instance.notifyEngOrVi("Congratulations on completing the game tutorial", "Xin chúc mừng bạn đã hoàn thành hướng dẫn");
            }
        }
    }
}
