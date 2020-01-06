using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Language : SingletonOne<Language>
{
    public Transform canvas;
    public bool checkLanguage;
    public Text notify;
    string str = string.Empty;
    //audio
    public AudioClip[] clip;
    public AudioSource audioMusic;
    public AudioSource audioSound;
    public AudioSource randomBird;

    IEnumerator bird()
    {
        yield return new WaitForSeconds(Random.Range(13, 40));
        randomBird.PlayOneShot(clip[0], 1f);
        StartCoroutine(bird());
    }

    private void OnEnable()
    {
        checkLanguage = false;
        if (Application.systemLanguage == SystemLanguage.English)
        {
            checkLanguage = true;
        }
        StartCoroutine(bird());
    }

    public void notifyEngOrVi(string notifyEng, string notifyVi)
    {
        Destroy(ob);
        if (checkLanguage)
        {
            notify.text = notifyEng;
        }
        else
        {
            notify.text = notifyVi;
        }
        ob = Instantiate(notify, canvas).gameObject;
        Destroy(ob, 2.4f);
    }

    public void setNameText(string nameVi, string nameEng, Text nameOb)
    {
        if (checkLanguage)
        {
            nameOb.text = nameEng;
        }
        else
        {
            nameOb.text = nameVi;
        }
    }
    GameObject ob;

    public void onSound(int i)
    {
        audioSound.PlayOneShot(clip[i], 1f);
    }
}