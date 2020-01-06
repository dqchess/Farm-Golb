using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager Instance;
    public GameObject showShop;
    public GameObject nenDen;
    public GameObject BtnSetting;
    Animator animator;
    public bool check, check1;
    public GameObject imgNew;

    private void OnEnable()
    {
        Instance = this;
        check1 = false;
        animator = showShop.GetComponent<Animator>();
    }

    public void openOrCloseShop()
    {
        Language.Instance.onSound(1);
        if (GameManager.Instance.guide)
        {
            GameManager.Instance.cameraOnOff(true);
            if (Guide.Instance.step == 2)
            {
                Guide.Instance.stepGuide(3);
            }

            if (Guide.Instance.step == 6)
            {
                Guide.Instance.stepGuide(7);
            }
        }
        else
        {
            if (check)
            {
                PlayerPrefs.SetInt("checkDrag", 1);
                GameManager.Instance.cameraOnOff(false);
                animator.SetBool("shop", false);
            }
            else
            {
                GameManager.Instance.cameraOnOff(true);
                animator.enabled = true;
                animator.SetBool("shop", true);
                DialogController.Instance.close();
                PlayerPrefs.SetInt("checkDrag", 0);
            }
            check = !check;
            nenDen.SetActive(check);
        }
        imgNew.SetActive(false);
    }

    public void openShop()
    {
        animator.enabled = true;
        animator.SetBool("shop", true);
        nenDen.SetActive(check = true);
    }

    public void closeShop()
    {
        animator.SetBool("shop", false);
        nenDen.SetActive(check = false);
    }

    public void openSetting()
    {
        Language.Instance.onSound(1);
        if (tangCap)
        {
            LevelManager.Instance.tangExp(PlayerPrefs.GetInt("expmax") - PlayerPrefs.GetInt("exp"));
            //DialogController.Instance.dialoSetting.SetActive(true);
            //GameManager.Instance.cameraOnOff(true);
            //AnimClick.Instance.clickUI(BtnSetting);
            //AnimClick.Instance.showUI(DialogController.Instance.dialoSetting);
        }
        else
        {
            DialogController.Instance.dialoSetting.SetActive(true);
            GameManager.Instance.cameraOnOff(true);
            AnimClick.Instance.clickUI(BtnSetting);
            AnimClick.Instance.showUI(DialogController.Instance.dialoSetting);
        }
    }

    public void openRewardVideo()
    {
        Language.Instance.onSound(1);
        DialogController.Instance.dialogReward.GetComponent<Animator>().Play("AimBlack", -1, 0);
        AnimClick.Instance.showUI(DialogController.Instance.dialogReward);
        GameManager.Instance.cameraOnOff(true);
    }

    public bool tangCap;
}