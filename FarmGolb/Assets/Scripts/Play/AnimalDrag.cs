using UnityEngine;

public class AnimalDrag : MonoBehaviour
{
    SpriteRenderer spr;
    public bool checkCage;
    public Cage cage;

    private void Start()
    {
        spr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        set();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag(gameObject.tag))
        {
            if (!checkCage)
            {
                spr.color = Color.white;
                checkCage = true;
                cage = col.gameObject.GetComponent<Cage>();
            }
        }
    }

    void set()
    {
        spr.color = Color.red;
        checkCage = false;
        cage = null;
    }
}
