using UnityEngine;
public class FlyHarvest : SingletonOne<FlyHarvest>
{
    public GameObject flyExp;
    public GameObject flyOther;
    GameObject ob;
    public GameObject boom;
    public GameObject saw;
    public GameObject animationBoom;
    public GameObject animationSeed;
    public GameObject effectCrop;

    public void iconFlyBay(Sprite spr, Vector2 pos, int sl, int Loai)
    {
        if (Loai == 0)
        {
            ob = Instantiate(flyExp, pos, Quaternion.identity);
        }
        else
        {
            ob = Instantiate(flyOther, pos, Quaternion.identity);
        }
        ob.GetComponent<RecieveCoin>().id = Loai;
        ob.GetComponent<RecieveCoin>().numberCoin = sl;
        if (Loai == 2 || Loai == 3 || Loai == 4)
        {
            ob.GetComponent<RecieveCoin>().spNhaMay = spr;
        }
    }
}