using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class Test2 : MonoBehaviour/*, IPointerDownHandler*//*, IDragHandler, IPointerUpHandler*/
{
    public int id;
    public GameObject show;

    //void Start()
    //{
    //    Debug.Log(Vector3.Lerp(Vector3.zero, Vector3.one, 0.2f));
    //}

    void Start()
    {
        // draw a 5-unit white line from the origin for 2.5 seconds
        Debug.DrawLine(Vector3.zero, new Vector3(5, 0, 0), Color.white, 2.5f);
    }

    private float q = 0.0f;

    void FixedUpdate()
    {
        // always draw a 5-unit colored line from the origin
        Color color = new Color(q, q, 1.0f);
        Debug.DrawLine(Vector3.zero, new Vector3(5, 5, 0), color);
        q = q + 0.01f;

        if (q > 1.0f)
        {
            q = 0.0f;
        }
    }

    private void OnEnable()
    {
        //if (id == 1)
        //    click1();
    }

    ////nha may
    //public void click()
    //{
    //    //Language.Instance.notifyEngOrVi("chuc ban may man lan sau", "See you again");
    //    transform.localScale = new Vector2(0.8f, 0.8f);
    //    StartCoroutine(run());
    //}

    ////UI
    //public void click1()
    //{
    //    transform.localScale = new Vector2(0.3f, 0.3f);
    //    StartCoroutine(run());
    //}

    //IEnumerator run()
    //{
    //    LeanTween.scale(gameObject, new Vector2(1.1f, 1.1f), 0.1f);
    //    yield return new WaitForSeconds(0.1f);
    //    LeanTween.scale(gameObject, new Vector2(1f, 1f), 0.05f);
    //    yield return new WaitForSeconds(0.05f);
    //    LeanTween.scale(gameObject, new Vector2(1.1f, 1.1f), 0.05f);
    //    yield return new WaitForSeconds(0.05f);
    //    LeanTween.scale(gameObject, new Vector2(1f, 1f), 0.05f);
    //    if (id == 0)
    //        show.SetActive(true);
    //}

    //private void OnMouseDown()
    //{
    //    click();
    //}

    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    click();        
    //}    

    //public void OnDrag(PointerEventData eventData)
    //{
        
    //}

    //public void OnPointerUp(PointerEventData eventData)
    //{
        
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("9000");
    }
}
