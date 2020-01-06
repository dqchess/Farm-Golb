using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RewardDay : MonoBehaviour
{
    public Text[] nameDay;
    public Text nameRewardDay;
    public GameObject[] tick;
    string[] day;

    public Button[] btn;
    int index;
    public Text[] confirm;
    public Button bt;

    void Start()
    {
        if (!GameManager.Instance.guide)
        {
            if (!PlayerPrefs.GetString("nameday").Equals(System.DateTime.Now.DayOfWeek.ToString()))
            {
                DialogController.Instance.closeDialogUI();

                day = new string[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };

                Language.Instance.setNameText("Quà tặng hàng ngày", "Daily gifts", nameRewardDay);

                for (int i = 0; i < 6; i++)
                {
                    Language.Instance.setNameText("Thứ " + (i + 2), "Day " + (i + 2), nameDay[i]);

                    if (day[i].Equals(System.DateTime.Now.DayOfWeek.ToString()))
                    {
                        btn[i].enabled = true;
                        bt = btn[i];
                        confirm[i].transform.parent.gameObject.SetActive(true);
                        Language.Instance.setNameText("Xác nhận", "Confirm", confirm[i]);
                    }
                }
                StartCoroutine(offCam());
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void thu2()
    {
        bt.enabled = false;
        tick[0].SetActive(true);
        PlayerPrefs.SetString("nameday", day[0]);
        ResourceManager.Instance.plus(0, 50);
        confirm[0].transform.parent.gameObject.SetActive(false);
    }
    public void thu3()
    {
        bt.enabled = false;
        tick[1].SetActive(true);
        PlayerPrefs.SetString("nameday", day[1]);
        ResourceManager.Instance.plus(0, 100);
        confirm[1].transform.parent.gameObject.SetActive(false);
    }

    public void thu4()
    {
        bt.enabled = false;
        tick[2].SetActive(true);
        PlayerPrefs.SetString("nameday", day[2]);
        ResourceManager.Instance.plus(1, 2);
        confirm[2].transform.parent.gameObject.SetActive(false);
    }
    public void thu5()
    {
        bt.enabled = false;
        tick[3].SetActive(true);
        PlayerPrefs.SetString("nameday", day[3]);
        ResourceManager.Instance.plus(1, 4);
        confirm[3].transform.parent.gameObject.SetActive(false);
    }
    public void thu6()
    {
        bt.enabled = false;
        tick[4].SetActive(true);
        PlayerPrefs.SetString("nameday", day[4]);
        ResourceManager.Instance.plus(0, 200);
        confirm[4].transform.parent.gameObject.SetActive(false);
    }
    public void thu7()
    {
        bt.enabled = false;
        tick[5].SetActive(true);
        PlayerPrefs.SetString("nameday", day[5]);
        ResourceManager.Instance.plus(1, 6);
        confirm[5].transform.parent.gameObject.SetActive(false);
    }

    public void close()
    {
        gameObject.SetActive(false);
        GameManager.Instance.cameraOnOff(false);
    }

    IEnumerator offCam()
    {
        yield return new WaitForSeconds(0.1f);
        GameManager.Instance.cameraOnOff(true);
    }
}
