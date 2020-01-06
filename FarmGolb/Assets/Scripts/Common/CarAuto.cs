using UnityEngine;
using System.Collections;

public class CarAuto : MonoBehaviour
{
    Vector2 target;
    public Transform posGo;
    private void Start()
    {
        target = transform.position;
        StartCoroutine(randomTimeAuto());
    }

    IEnumerator randomTimeAuto()
    {
        yield return new WaitForSeconds(Random.Range(10, 30));
        LeanTween.move(gameObject, posGo.position, 30);
        yield return new WaitForSeconds(30);
        transform.position = target;
        StartCoroutine(randomTimeAuto());
    }
}
