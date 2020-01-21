using UnityEngine;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour, IPointerDownHandler
{
    public int id;
    public GameObject ob;
    GameObject init;
    Vector2 target;
    public string nameTag;
    public bool checkDrag;

    //public void OnBeginDrag(PointerEventData eventData)
    //{
    //    if (checkDrag)
    //    {
    //        GameManager.Instance.cameraOnOff(true);
    //        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        init = Instantiate(ob, target, Quaternion.identity);
    //        init.tag = nameTag;            
    //        if (id < 100)
    //        { 
    //            init.transform.GetChild(0).gameObject.SetActive(true);
    //            init.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.dataStorage.dataStorages[id].icon;
    //            init.GetComponent<ItemDrag>().idItem = id;
    //            init.GetComponent<ItemDrag>().updateText();
    //        }
    //        else
    //        {
    //            init.transform.GetChild(0).gameObject.SetActive(false);
    //            init.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.dataSprite.sprHarvest[id - 100];
    //        }
    //        transform.parent.transform.localScale = Vector3.zero;
    //        LevelManager.Instance.huy = init;
    //    }
    //}

    //public void OnDrag(PointerEventData eventData)
    //{
    //    if (init != null)
    //    {
    //        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        init.transform.position = target;
    //    }
    //}

    //public void OnEndDrag(PointerEventData eventData)
    //{
    //    GameManager.Instance.cameraOnOff(false);
    //    if (GameManager.Instance.listUserDiamond.Count > 0)
    //    {
    //        DialogController.Instance.dialogBuyDiamond.SetActive(true);
    //    }
    //    if (init != null)
    //    {
    //        //Destroy(init);
    //    }

    //    if (Guide.Instance.step <= 1)
    //    {
    //        if (Guide.Instance.check)
    //        {
    //            Guide.Instance.stepGuide(2);
    //            Guide.Instance.check = false;
    //        }
    //        else
    //        {
    //            Guide.Instance.stepGuide(0);
    //        }
    //    }
    //    else
    //    if (Guide.Instance.step == 5)
    //    {
    //        if (Guide.Instance.check)
    //        {
    //            Guide.Instance.stepGuide(6);
    //            Guide.Instance.check = false;
    //        }
    //        else
    //        {
    //            Guide.Instance.stepGuide(4);
    //        }
    //    }
    //    else if (Guide.Instance.step == 9)
    //    {
    //        if (Guide.Instance.check)
    //        {
    //            Guide.Instance.stepGuide(10);
    //            Guide.Instance.check = false;
    //        }
    //        else
    //        {
    //            Guide.Instance.stepGuide(9);
    //        }
    //    }
    //    else if (Guide.Instance.step == 10)
    //    {
    //        if (Guide.Instance.check)
    //        {
    //            if (GameManager.Instance.guide)
    //            {
    //                Guide.Instance.stepGuide(11);
    //            }
    //            Guide.Instance.check = false;
    //        }
    //        else
    //        {
    //            Guide.Instance.stepGuide(9);
    //        }
    //    }
    //}

    public void OnPointerDown(PointerEventData eventData)
    {
        if (checkDrag)
        {
            GameManager.Instance.cameraOnOff(true);
            init = Instantiate(ob, CheckDistance.Instance.getScreenToWorldPoint(), Quaternion.identity);
            init.tag = nameTag;
            if (id < 100)
            {
                init.transform.GetChild(0).gameObject.SetActive(true);
                init.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.dataStorage.dataStorages[id].icon;
                init.GetComponent<ItemDrag>().idItem = id;
                init.GetComponent<ItemDrag>().updateText();
            }
            else
            {
                init.transform.GetChild(0).gameObject.SetActive(false);
                init.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.dataSprite.sprHarvest[id - 100];
            }
            ObjectDrag.Instance.ObjDrag = init;
            ObjectDrag.Instance.check = true;
            ObjectDrag.Instance.checkCam = true;
            Destroy(transform.parent.parent.gameObject);
        }
    }
}