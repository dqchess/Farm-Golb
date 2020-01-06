using System.Collections;
using UnityEngine;

public class Test3 : SingletonOne<Test3>
{
    public GameObject cam;
    Vector2 vt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnMouseDown()
    {
        vt = transform.position;
        click();
        LeanTween.move(cam, vt, 1f);
        //GameObject.Find("New Sprite").transform.position = transform.position;
        //LeanTween.move(GameObject.Find("New Sprite"), vt, 1f);
        //show dialog canvas        
    }

    public void stop()
    {
        LeanTween.cancel(cam);   
    }

    IEnumerator run()
    {
        LeanTween.scaleY(gameObject, 0.8f, 0.25f);
        yield return new WaitForSeconds(0.25f);
        LeanTween.scaleY(gameObject, 1f, 0.125f);
        yield return new WaitForSeconds(0.125f);
        LeanTween.scaleY(gameObject, 0.9f, 0.125f);
        yield return new WaitForSeconds(0.125f);
        LeanTween.scaleY(gameObject, 1f, 0.125f);               
    }

    public void click()
    {                
        StartCoroutine(run());
    }
}
