using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Seaport : MonoBehaviour
{
    public GameObject dialogBoat;
    public Transform ScrollView;
    public GameObject itemVp;
    int level;
    

    private void Start()
    {
        level = PlayerPrefs.GetInt("level");
        load();        
    }

    public virtual void OnMouseDown()
    {
        CheckDistance.Instance.posOld();
    }

    public virtual void OnMouseUp()
    {        
        if (PlayerPrefs.GetInt("level") < 7)
        {            
            Language.Instance.notifyEngOrVi("Expansions open at level 7", "Mở khóa cấp 7!");
        }
        else if (CheckDistance.Instance.distance() && !EventSystem.current.IsPointerOverGameObject(0))
        {
            GameManager.Instance.cameraOnOff(true);
            Language.Instance.onSound(1);
            dialogBoat.transform.localScale = Vector3.one;            

            //show ra
            loadItemBoat();            
        }
    }

    public void loadItemBoat()
    {
        if (level != PlayerPrefs.GetInt("level"))
        {            
            load();
        }
    }

    public void load()
    {
        int index = 0;

        for (int i = 0; i < ScrollView.childCount; i++)
        {
            Destroy(ScrollView.GetChild(i).gameObject);
        }

        foreach (DataStorage.DataStorages item in GameManager.Instance.dataStorage.dataStorages)
        {
            if (item.levelUse <= PlayerPrefs.GetInt("level"))
            {
                itemVp.GetComponent<ItemSp>().id = index;
                itemVp.GetComponent<Image>().sprite = item.icon;
                itemVp.GetComponent<Image>().SetNativeSize();
                Instantiate(itemVp, ScrollView);
            }
            index++;
        }
    }

    public void exit()
    {
        GameManager.Instance.cameraOnOff(false);
        Language.Instance.onSound(1);
        dialogBoat.transform.localScale = Vector3.zero;
    }
}
