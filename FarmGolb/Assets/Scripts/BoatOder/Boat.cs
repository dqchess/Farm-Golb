using UnityEngine;
using System.Collections;

public class Boat : Seaport
{
    //public static Boat instance;      
    public Vector3 posHome, posGo;

    //private void Start()
    //{
    //    posHome = transform.position;
    //}

    public override void OnMouseUp()
    {
        base.OnMouseUp();
    }

    public IEnumerator go()
    {
        LeanTween.moveLocal(gameObject, posGo, 13);
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(true);
        yield return new WaitForSeconds(13);
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
    }

    // go home
    public IEnumerator goHome()
    {
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(true);
        LeanTween.moveLocal(gameObject, posHome, 13);
        yield return new WaitForSeconds(13);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
    }
}
