using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    public Text percent;
    public Text nameLoading;
    int i;
    void Start()
    {
        i = 0;
        StartCoroutine(nghi());
        if (Application.systemLanguage == SystemLanguage.English)
        {
            nameLoading.text = "Loading";
        }
        else
        {
            nameLoading.text = "Đang tải";
        }
    }

    IEnumerator nghi()
    {
        yield return new WaitForSeconds(0.04f);
        i += 1;
        percent.text = string.Format("{0:00}%", i);
        if (i == 100)
        {
            SceneManager.LoadScene("GamePlay");
        }
        StartCoroutine(nghi());
    }
}
