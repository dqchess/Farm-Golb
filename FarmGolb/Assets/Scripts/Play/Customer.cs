using UnityEngine;
using System.Collections;

public class Customer : MonoBehaviour
{
    public GameObject dialogCustomer;
    bool check;
    int idBuy;

    void Start()
    {

    }

    private void OnMouseDown()
    {
        //CheckDistance.Instance.posOld();
    }

    private void OnMouseUp()
    {
        if (check)
        {
            //show dialog buy
            AnimClick.Instance.showUI(dialogCustomer);
            //sent idBuy           
            dialogCustomer.GetComponent<CustomerController>().loadItemBuy(idBuy);
        }
    }

    public void runCustomer()
    {
        idBuy = Oder.Instance.randomIdVp();
        StartCoroutine(waitBuy());
    }

    IEnumerator waitBuy()
    {
        yield return new WaitForSeconds(5);
        transform.localScale = Vector2.one;
        //customer run to market        
    }

    public void randomItemStorage()
    {

    }
}