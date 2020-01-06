using UnityEngine;

public class ButtonMarket : MonoBehaviour
{
    public int id;

    public void click()
    {
        Market.instance.buySp(id);
    }
}
