using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    int exp;
    int expmax;
    int level;
    public Text txtLevel, txtExp, txtExpMax;

    public Transform contentLevel, contentShop;
    public GameObject dialogLevelUp;

    //xoa tat ca object khi keo
    public GameObject huy;
    public Text levelUp1, nameLevelUp;

    private void Start()
    {
        Instance = this;
        exp = PlayerPrefs.GetInt("exp");
        level = PlayerPrefs.GetInt("level");
        expmax = PlayerPrefs.GetInt("expmax");
        txtLevel.text = level.ToString();
        txtExp.text = exp.ToString();
        txtExpMax.text = expmax.ToString();
        transform.GetChild(0).GetComponent<Image>().fillAmount = (float)exp / expmax;
        Language.Instance.setNameText("Lên Cấp", "Level Up", nameLevelUp);
        Language.Instance.setNameText("Mới", "New", itemNew.transform.GetChild(1).GetComponent<Text>());
        dialogLevelUp.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => close());
    }

    public void tangExp(int expPlus)
    {
        PlayerPrefs.SetInt("exp", PlayerPrefs.GetInt("exp") + expPlus);
        if (PlayerPrefs.GetInt("exp") >= PlayerPrefs.GetInt("expmax"))
        {
            DialogController.Instance.closeDialogUI();
            try
            {
                MyAdvertisement.ShowFullNormal();
            }
            catch
            {

            }
            if (huy)
            {
                Destroy(huy);
            }
            Language.Instance.onSound(6);
            GameManager.Instance.cameraOnOff(true);
            dialogLevelUp.SetActive(true);            
            PlayerPrefs.SetInt("exp", PlayerPrefs.GetInt("exp") - PlayerPrefs.GetInt("expmax"));
            PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);

            if (PlayerPrefs.GetInt("level") > 7)
            {
                PlayerPrefs.SetInt("expmax", PlayerPrefs.GetInt("expmax") + 30 * PlayerPrefs.GetInt("level"));
            }
            else
            {
                PlayerPrefs.SetInt("expmax", 15 * PlayerPrefs.GetInt("level"));
            }
            level = PlayerPrefs.GetInt("level");
            loadItemLevel();
            //update lai shop
            for (int i = 0; i < 43; i++)
            {
                contentShop.GetChild(i).GetComponent<ItemShop>().updateLenLevel();
            }
            Oder.Instance.idVatPhamUnlock();
            txtLevel.text = level.ToString();
            levelUp1.text = txtLevel.text;
            loadDataCrop();
            StorageController.Instance.load();
        }
        exp = PlayerPrefs.GetInt("exp");
        expmax = PlayerPrefs.GetInt("expmax");
        transform.GetChild(0).GetComponent<Image>().fillAmount = (float)exp / expmax;
        txtExp.text = exp.ToString();
        txtExpMax.text = expmax.ToString();
    }

    //check shop new
    public GameObject itemNew;
    public void loadItemLevel()
    {
        //if (GameManager.Instance.guide)
        //{
        //    levelUp1.text = "2";
        //}

        for (int i = 0; i < contentLevel.childCount; i++)
        {
            Destroy(contentLevel.GetChild(i).gameObject);
        }

        int slkeo, slmax;
        for (int j = 0; j < 43; j++)
        {
            slkeo = PlayerPrefs.GetInt("slkeo" + j);
            slmax = PlayerPrefs.GetInt("slmax" + j);
            if (PlayerPrefs.GetInt("levelsd" + j) <= level)
            {
                if (slkeo < slmax || j >= 33)
                {
                    itemNew.transform.GetChild(2).GetComponent<Image>().sprite = GameManager.Instance.dataShop.dataItemShop[j].icon;
                    itemNew.transform.GetChild(2).GetComponent<Image>().GetComponent<Image>().SetNativeSize();
                    Instantiate(itemNew, contentLevel);
                }
            }
        }
    }

    void loadDataCrop()
    {
        for (int i = 0; i < 18; i++)
        {
            if (GameManager.Instance.dataStorage.dataStorages[i].levelUse <= level)
            {
                if (!PlayerPrefs.HasKey("ns" + i))
                {
                    StorageController.Instance.updateStorage(0, i, 6);
                }
            }
            else
            {
                break;
            }
        }
    }

    public void close()
    {
        GameManager.Instance.cameraOnOff(false);
        dialogLevelUp.SetActive(false);
        if (GameManager.Instance.guide)
        {
            Guide.Instance.stepGuide(12);
        }
    }
}
