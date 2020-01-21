using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragItemMachine : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public int idItemMachine;
    public GameObject ob;

    //item sp
    public DataMachine.nguyenLieu[] itemNL;

    //show item when drag item san pham
    public GameObject showVp;
    public Text nameVp;
    public Text timeVp;
    public GameObject[] imgVpShow;
    public Text slSpKho;
    int count, tg;

    int slns;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (GameManager.Instance.dataStorage.dataStorages[idItemMachine].levelUse <= PlayerPrefs.GetInt("level"))
        {
            ObjectDrag.Instance.ObjDrag = Instantiate(ob, CheckDistance.Instance.getScreenToWorldPoint(), Quaternion.identity);
            ObjectDrag.Instance.ObjDrag.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.dataStorage.dataStorages[idItemMachine].icon;
            ObjectDrag.Instance.check = true;
            ObjectDrag.Instance.checkCam = true;
            GameManager.Instance.cameraOnOff(true);
            showVp.SetActive(false);
            transform.localScale = Vector2.zero;
        }

        if (GameManager.Instance.guide)
        {
            //Guide.Instance.tayKeoMachine.SetActive(false);
            Guide.Instance.tayKeoMachine.transform.localPosition = new Vector2(100, 100);
        }

        count = 0;
        if (GameManager.Instance.dataStorage.dataStorages[idItemMachine].levelUse <= PlayerPrefs.GetInt("level"))
        {
            MachineController.Instance.idProduct = idItemMachine;
            MachineController.Instance.listNL = itemNL;
            slSpKho.text = PlayerPrefs.GetInt("ns" + (idItemMachine)).ToString();
            showVp.SetActive(true);
            tg = GameManager.Instance.dataStorage.dataStorages[idItemMachine].time;
            timeVp.text = (tg / 60) + ".m " + (tg % 60) + ".s";
            tg = 0;
            foreach (DataMachine.nguyenLieu item in itemNL)
            {
                imgVpShow[tg].SetActive(true);
                imgVpShow[tg].GetComponent<Image>().sprite = GameManager.Instance.dataStorage.dataStorages[item.id].icon;
                imgVpShow[tg].GetComponent<Image>().SetNativeSize();
                slns = PlayerPrefs.GetInt("ns" + item.id);
                if (slns >= item.number)
                {
                    count++;
                    imgVpShow[tg].transform.GetChild(1).gameObject.SetActive(true);
                }
                else
                {
                    imgVpShow[tg].transform.GetChild(1).gameObject.SetActive(false);
                }
                imgVpShow[tg].transform.GetChild(0).GetComponent<Text>().text = slns + "/" + item.number;
                tg++;
            }
            Language.Instance.setNameText(GameManager.Instance.dataStorage.dataStorages[idItemMachine].nameVi, GameManager.Instance.dataStorage.dataStorages[idItemMachine].nameEng, nameVp);
            if (count == itemNL.Length)
            {
                MachineController.Instance.checkNau = true;
            }
            else
            {
                MachineController.Instance.checkNau = false;
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        for (int i = 0; i < itemNL.Length; i++)
        {
            imgVpShow[i].SetActive(false);
        }
        showVp.SetActive(false);
        transform.localScale = Vector2.one;
        AnimClick.Instance.runTest(gameObject);        
    }
}