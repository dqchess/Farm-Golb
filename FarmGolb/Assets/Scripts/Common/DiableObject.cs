using UnityEngine;
using System.Collections;

public class DiableObject : MonoBehaviour
{
    public GameObject ob;

    private void OnBecameVisible()
    {
        ob.SetActive(true);
    }

    private void OnBecameInvisible()
    {
        ob.SetActive(false);
    }
}