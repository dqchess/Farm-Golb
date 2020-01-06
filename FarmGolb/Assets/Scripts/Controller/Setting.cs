using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Setting : MonoBehaviour
{
    bool check, check1;
    public GameObject music;
    public GameObject sound;
    public GameObject exitGame;
    public GameObject setting;
    public Text nameSetting;
    public Text nameButtonSetting;
    public Text txtExitGame, nameExit, yesExit, noExit;
    //dialog thank
    public GameObject thank;
    public GameObject show;
    public RectTransform btnMusic;
    public RectTransform btnSound;

    private void Start()
    {
        check = false;
        check1 = false;

        Language.Instance.setNameText("Cài đặt", "Setting", nameSetting);
        Language.Instance.setNameText("Nhạc nền", "Music", setting.transform.GetChild(2).GetComponent<Text>());
        Language.Instance.setNameText("Âm thanh", "Sound", setting.transform.GetChild(3).GetComponent<Text>());
        Language.Instance.setNameText("Bật", "On", music.transform.GetChild(1).GetComponent<Text>());
        Language.Instance.setNameText("Bật", "On", sound.transform.GetChild(1).GetComponent<Text>());
        Language.Instance.setNameText("Thoát", "Exit game", nameButtonSetting);
        Language.Instance.setNameText("Bạn có muốn thoát trò chơi không?", "Do you want exit game really?", txtExitGame);
        Language.Instance.setNameText("Cám ơn bạn", "Thank you", thank.transform.GetChild(0).GetComponent<Text>());
        Language.Instance.setNameText("Có", "Yes", yesExit);
        Language.Instance.setNameText("Hủy", "Cancel", noExit);
        Language.Instance.setNameText("Thoát trò chơi", "Exit game", nameExit);
    }

    private void OnEnable()
    {
        show.gameObject.SetActive(true);
        transform.GetChild(3).gameObject.SetActive(true);
    }

    public void onOffMusic()
    {
        music.transform.GetChild(0).gameObject.SetActive(check);
        Language.Instance.audioMusic.enabled = check;
        if (check)
        {
            Language.Instance.setNameText("Bật", "On", music.transform.GetChild(1).GetComponent<Text>());
            btnMusic.localPosition = new Vector2(55f, -1.7f);
        }
        else
        {
            Language.Instance.setNameText("Tắt", "Off", music.transform.GetChild(1).GetComponent<Text>());
            btnMusic.localPosition = new Vector2(-50f, -1.7f);
        }
        check = !check;
    }

    public void onOffSound()
    {
        sound.transform.GetChild(0).gameObject.SetActive(check1);
        Language.Instance.audioSound.enabled = check1;
        if (check1)
        {
            Language.Instance.setNameText("Bật", "On", sound.transform.GetChild(1).GetComponent<Text>());
            btnSound.localPosition = new Vector2(55f, 0f);
        }
        else
        {
            Language.Instance.setNameText("Tắt", "Off", sound.transform.GetChild(1).GetComponent<Text>());
            btnSound.localPosition = new Vector2(-50f, 0f);
        }
        check1 = !check1;
    }

    public void closeSetting()
    {
        show.gameObject.SetActive(false);
        GameManager.Instance.cameraOnOff(false);
    }

    public void showExitGame()
    {
        //AnimClick.Instance.clickUI(setting.transform.GetChild(4).gameObject);
        exitGame.SetActive(true);
        setting.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(false);
    }

    public void exitGameYes()
    {
        try
        {
            MyAdvertisement.ShowFullNormal();
        }
        catch
        {
        }
        thank.SetActive(true);
        StartCoroutine(exit());
    }

    IEnumerator exit()
    {
        yield return new WaitForSeconds(1.5f);
        Application.Quit();
    }

    public void exitGameNo()
    {
        exitGame.SetActive(false);
        setting.SetActive(true);
        closeSetting();
    }
}
