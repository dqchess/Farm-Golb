using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ItemStorage : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public int idVp;
    Vector2 pos;

    public void OnPointerDown(PointerEventData eventData)
    {
        pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        AnimClick.Instance.clickUI(gameObject);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (Vector2.Distance(pos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < 0.1f)
        {
            StartCoroutine(showItemSell());
        }
    }

    IEnumerator showItemSell()
    {
        yield return new WaitForSeconds(0.2f);
        StorageController.Instance.showSell(idVp);
    }
}
