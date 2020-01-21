using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class ItemSp : MonoBehaviour, IPointerDownHandler/*, IPointerUpHandler*/
{
    public int id;
    [SerializeField] ScrollRect scroll;
    [SerializeField] GameObject itemKeo;

    private void Start()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {

        //if (PlayerPrefs.GetInt("ns" + id) > 0)
        //{            
        Vector2 temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        itemKeo.GetComponent<Image>().sprite = GameManager.Instance.dataStorage.dataStorages[id].icon;
        itemKeo.GetComponent<Image>().SetNativeSize();
        ObjectDrag.Instance.ObjDrag = Instantiate(itemKeo, GameManager.Instance.canvas);
        ObjectDrag.Instance.check = true;
        ObjectDrag.Instance.checkCam = false;
        PlayerPrefs.SetInt("ItemSpBoat", id);
        //GameManager.Instance.cameraOnOff(true);
        //}
    }

    //public void OnPointerUp(PointerEventData eventData)
    //{
    //    StartCoroutine(off());
    //}

    //IEnumerator off()
    //{
    //    yield return new WaitForSeconds(0.2f);
    //    GameManager.Instance.cameraOnOff(true);
    //}
}