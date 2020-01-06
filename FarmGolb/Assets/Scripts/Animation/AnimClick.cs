using UnityEngine;
using System.Collections;

public class AnimClick : SingletonOne<AnimClick>
{
    //click object on map
    public void clickObject(GameObject ob)
    {
        StartCoroutine(runObject(ob));
    }

    IEnumerator runObject(GameObject ob)
    {
        LeanTween.scaleY(ob, 0.8f, 0.25f);
        yield return new WaitForSeconds(0.25f);
        LeanTween.scaleY(ob, 1f, 0.125f);
        yield return new WaitForSeconds(0.125f);
        LeanTween.scaleY(ob, 0.9f, 0.125f);
        yield return new WaitForSeconds(0.125f);
        LeanTween.scaleY(ob, 1f, 0.125f);
    }

    //click UI
    public void clickUI(GameObject ob)
    {
        ob.transform.localScale = new Vector2(0.6f, 0.6f);
        StartCoroutine(runClickUI(ob));
    }

    IEnumerator runClickUI(GameObject ob)
    {
        LeanTween.scale(ob, new Vector2(1.2f, 1.2f), 0.1f);
        yield return new WaitForSeconds(0.1f);
        LeanTween.scale(ob, new Vector2(1f, 1f), 0.07f);
        yield return new WaitForSeconds(0.07f);
        LeanTween.scale(ob, new Vector2(1.2f, 1.2f), 0.07f);
        yield return new WaitForSeconds(0.07f);
        LeanTween.scale(ob, new Vector2(1f, 1f), 0.07f);
    }

    public void animRatationOder(GameObject ob)
    {
        StartCoroutine(rotationOder(ob));
    }

    IEnumerator rotationOder(GameObject ob)
    {
        LeanTween.rotateZ(ob, 8, 0.25f);
        yield return new WaitForSeconds(0.25f);
        LeanTween.rotateZ(ob, -8, 0.25f);
        yield return new WaitForSeconds(0.25f);
        LeanTween.rotateZ(ob, 0, 0.25f);        
    }

    //show dialog
    public void showUI(GameObject ob)
    {        
        ob.SetActive(true);
        ob.transform.localScale = Vector2.zero;
        LeanTween.scale(ob, new Vector2(1.2f, 1.2f), 0.3f);
    }

    public void showDialog(GameObject ob)
    {
        ob.SetActive(true);
        ob.transform.localScale = Vector2.zero;
        LeanTween.scale(ob, Vector2.one, 0.3f);
    }

    //animation cua item drag
    public void runTest(GameObject ob)
    {
        StartCoroutine(test(ob));
    }

    //animation cua item drag 0.3s
    public void runTest2(GameObject ob)
    {
        StartCoroutine(test2(ob));
    }

    IEnumerator test(GameObject ob)
    {
        float y = ob.transform.localPosition.y;
        LeanTween.scale(ob, new Vector2(1.2f, 1.2f), 0.06f);        
        LeanTween.moveLocalY(ob, y + 30f, 0.06f);
        yield return new WaitForSeconds(0.06f);
        LeanTween.scale(ob, new Vector2(1f, 1f), 0.06f);
        LeanTween.moveLocalY(ob, y, 0.06f);
    }

    IEnumerator test2(GameObject ob)
    {
        yield return new WaitForSeconds(0.3f);
        if (ob != null)
        {
            runTest(ob);
        }              
    }   
}