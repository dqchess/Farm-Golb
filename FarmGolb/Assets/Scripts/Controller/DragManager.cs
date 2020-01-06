using UnityEngine;
using UnityEngine.UI;

public class DragManager : SingletonOne<DragManager>
{
    public GameObject[] itemDrag;
    public Image sprDialog;
    int dem, dem1;
    int level;
    Image icon;
    public GameObject btnChange;

    private void Start()
    {        
        AnimClick.Instance.showDialog(gameObject);
    }

    public void showItemCrop()
    {
        PlayerPrefs.SetInt("checkDrag", 0);
        level = PlayerPrefs.GetInt("level");
        if (level < 3)
        {
            activeItemDrag(0);
        }
        else if (level < 4)
        {
            activeItemDrag(1);
        }
        else if (level < 7)
        {
            activeItemDrag(2);
        }
        else if (level < 9)
        {
            activeItemDrag(3);
        }
        else if (level < 11)
        {
            activeItemDrag(4);
        }
        else if (level >= 11)
        {
            activeItemDrag(4);
            btnChange.SetActive(true);
            btnChange.GetComponent<Button>().onClick.AddListener(() => changeItemCrop());
        }
        dem = 1;
    }

    void activeItemDrag(int index)
    {
        sprDialog.sprite = GameManager.Instance.dataSprite.sprDialogDrag[index];
        for (int i = 0; i <= index; i++)
        {
            loadImageCrop(i, i);
            itemDrag[i].SetActive(true);
            itemDrag[i].GetComponent<DragItem>().checkDrag = true;
            AnimClick.Instance.runTest2(itemDrag[i]);
        }
    }

    public void changeItemCrop()
    {
        if (dem == 0)
        {
            itemDrag[3].SetActive(true);
            itemDrag[4].SetActive(true);
            loadItemCrop(0, 5);
        }
        else if (dem == 1)
        {
            loadItemCrop(5, 10);
        }
        else if (dem == 2)
        {
            loadItemCrop(10, 15);
        }
        else if (dem == 3)
        {
            itemDrag[3].SetActive(false);
            itemDrag[4].SetActive(false);
            loadItemCrop(15, 18);
        }

        if (dem1 > 0)
        {
            dem++;
        }
        else
        {
            dem = 0;
        }
    }

    public void loadItemCrop(int a, int j)
    {
        for (int i = a; i < j; i++)
        {
            AnimClick.Instance.runTest(itemDrag[i - a]);
            if (GameManager.Instance.dataStorage.dataStorages[i].levelUse <= level)
            {
                dem1++;
                itemDrag[i - a].transform.GetChild(0).gameObject.SetActive(true);
                itemDrag[i - a].transform.GetChild(1).gameObject.SetActive(true);
                loadImageCrop(i - a, i);
                itemDrag[i - a].GetComponent<DragItem>().checkDrag = true;
            }
            else
            {
                itemDrag[i - a].transform.GetChild(2).GetComponent<Image>().sprite = GameManager.Instance.dataStorage.dataStorages[i].iconLock;
                itemDrag[i - a].transform.GetChild(0).gameObject.SetActive(false);
                itemDrag[i - a].transform.GetChild(1).gameObject.SetActive(false);
                itemDrag[i - a].GetComponent<DragItem>().checkDrag = false;
                dem1 = 0;
            }
        }

        if ((j <= 15 && GameManager.Instance.dataStorage.dataStorages[j].levelUse > level) || j > 15)
        {
            dem1 = 0;
        }
    }

    void loadImageCrop(int index, int id)
    {
        itemDrag[index].transform.GetChild(1).GetChild(0).GetComponent<Text>().text = PlayerPrefs.GetInt("ns" + id).ToString();
        icon = itemDrag[index].transform.GetChild(2).GetComponent<Image>();
        icon.sprite = GameManager.Instance.dataStorage.dataStorages[id].icon;
        icon.SetNativeSize();
        itemDrag[index].GetComponent<DragItem>().id = id;
        itemDrag[index].GetComponent<DragItem>().nameTag = "crop";
    }

    //load thu hoach ruong lua
    public void showSickle()
    {
        off();
        sprDialog.sprite = GameManager.Instance.dataSprite.sprDialogDrag[0];
        itemDrag[0].SetActive(true);
        itemDrag[0].transform.GetChild(1).gameObject.SetActive(false);
        icon = itemDrag[0].transform.GetChild(2).GetComponent<Image>();
        icon.sprite = GameManager.Instance.dataSprite.sprHarvest[0];
        icon.SetNativeSize();
        itemDrag[0].GetComponent<DragItem>().checkDrag = true;
        itemDrag[0].GetComponent<DragItem>().id = 100;
        itemDrag[0].GetComponent<DragItem>().nameTag = "cropHarvest";
    }

    //load food and harvest cage
    string nameTag;
    public void showEatAnimal(int idFood)
    {
        off();
        sprDialog.sprite = GameManager.Instance.dataSprite.sprDialogDrag[1];
        switch (idFood)
        {
            case 33: nameTag = "foodChicken"; break;
            case 34: nameTag = "foodCow"; break;
            case 35: nameTag = "foodPig"; break;
            case 36: nameTag = "foodSheep"; break;
            case 37: nameTag = "foodGoat"; break;
            case 38: nameTag = "foodHorse"; break;
        }
        itemDrag[0].SetActive(true);
        itemDrag[0].transform.GetChild(1).GetChild(0).GetComponent<Text>().text = PlayerPrefs.GetInt("ns" + idFood).ToString();
        itemDrag[0].transform.GetChild(1).gameObject.SetActive(true);
        icon = itemDrag[0].transform.GetChild(2).GetComponent<Image>();
        icon.sprite = GameManager.Instance.dataStorage.dataStorages[idFood].icon;
        icon.SetNativeSize();
        itemDrag[0].GetComponent<DragItem>().id = idFood;
        itemDrag[0].GetComponent<DragItem>().nameTag = nameTag;
        itemDrag[0].GetComponent<DragItem>().checkDrag = true;

        itemDrag[1].SetActive(true);
        itemDrag[1].transform.GetChild(1).gameObject.SetActive(false);
        icon = itemDrag[1].transform.GetChild(2).GetComponent<Image>();
        icon.sprite = GameManager.Instance.dataSprite.sprHarvest[idFood - 32];
        icon.SetNativeSize();
        itemDrag[1].GetComponent<DragItem>().id = idFood - 32 + 100;
        itemDrag[1].GetComponent<DragItem>().nameTag = nameTag + idFood;
        itemDrag[1].GetComponent<DragItem>().checkDrag = true;
    }

    //load saw, boom, Axe
    public void destroy(int idDestroy)
    {
        off();
        itemDrag[0].transform.GetChild(1).gameObject.SetActive(true);
        string tagDestroy;
        if (idDestroy == 7)
        {
            tagDestroy = "saw";
        }
        else
        {
            tagDestroy = "boom";
        }
        sprDialog.sprite = GameManager.Instance.dataSprite.sprDialogDrag[0];
        itemDrag[0].SetActive(true);
        if (idDestroy == 9)
        {
            idDestroy = 8;
        }
        itemDrag[0].transform.GetChild(1).GetChild(0).GetComponent<Text>().text = PlayerPrefs.GetInt("ns" + (idDestroy + 84)).ToString();
        itemDrag[0].GetComponent<DragItem>().id = idDestroy + 84;
        icon = itemDrag[0].transform.GetChild(2).GetComponent<Image>();
        icon.sprite = GameManager.Instance.dataStorage.dataStorages[idDestroy + 84].icon;
        icon.SetNativeSize();
        itemDrag[0].GetComponent<DragItem>().nameTag = tagDestroy;
        itemDrag[0].GetComponent<DragItem>().checkDrag = true;
    }

    void off()
    {
        PlayerPrefs.SetInt("checkDrag", 0);
        btnChange.SetActive(false);
        for (int i = 0; i < 5; i++)
        {
            itemDrag[i].SetActive(false);
            AnimClick.Instance.runTest2(itemDrag[i]);
        }
    }
}