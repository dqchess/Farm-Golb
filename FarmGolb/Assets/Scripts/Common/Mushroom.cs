using UnityEngine;

public class Mushroom : MonoBehaviour
{
    void Start()
    {
        int order = Mathf.RoundToInt((40 - transform.GetChild(0).position.y) * 100f);
        transform.GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = order;
        GameManager.Instance.setArray(1, 1, transform.position, true);
    }
}