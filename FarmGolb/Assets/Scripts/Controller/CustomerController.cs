using UnityEngine;
using System.Collections;

public class CustomerController : MonoBehaviour
{
    public int idBuy;
    int quantity;

    void Start()
    {
        //load data
        //quantity = PlayerPrefs.GetInt("quantity");
        //id buy
    }

    public void loadItemBuy(int id)
    {
        idBuy = id;
        //show item on dialog

    }

    public void noBuy()
    {
        customerGoHome();
    }

    public void yesBuy()
    {
        customerGoHome();
        //anim 
        //check 
        if (PlayerPrefs.GetInt("ns" + idBuy) > 0)
        {

        }
    }

    public void close()
    {
        gameObject.SetActive(false);
    }

    void customerGoHome()
    {
        //customer hide

    }
}
