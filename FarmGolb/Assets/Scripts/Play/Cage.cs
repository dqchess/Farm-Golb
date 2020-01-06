using UnityEngine;
using System.Collections;

public class Cage : MonoBehaviour
{
    public int idFood;
    public int sogaan;
    Vector2 pos;
    int slga;
    MoveObject moveObject;

    public void soGaAn(int dem)
    {
        sogaan += dem;
    }

    void Start()
    {
        if (!PlayerPrefs.HasKey("ns" + idFood))
        {
            StorageController.Instance.updateStorage(1, idFood, 3);
        }
        moveObject = GetComponent<MoveObject>();
        slga = PlayerPrefs.GetInt("numberAnimal" + gameObject.name);
        for (int i = 1; i <= slga; i++)
        {
            transform.GetChild(i + 2).gameObject.SetActive(true);
        }
    }

    private void OnMouseDown()
    {
        CheckDistance.Instance.posOld();
    }

    bool delay;
    private void OnMouseUp()
    {
        if (CheckDistance.Instance.distance() && moveObject.checkUI())
        {
            slga = PlayerPrefs.GetInt("numberAnimal" + gameObject.name);
            if (slga == 0)
            {
                ButtonManager.Instance.openOrCloseShop();
                ShopManager.Instance.openButton2();
            }
            else
            {
                if (sogaan < slga)
                {
                    showEat();
                }
                if (GameManager.Instance.guide)
                {
                    showEat();
                }
            }
            if (!delay)
            {
                StartCoroutine(music());
            }
        }
    }

    IEnumerator music()
    {
        delay = true;
        if (idFood == 33)
        {
            Language.Instance.onSound(9);
        }
        else if (idFood == 34)
        {
            Language.Instance.onSound(10);
        }
        else if (idFood == 35)
        {
            Language.Instance.onSound(11);
        }
        else if (idFood == 36)
        {
            Language.Instance.onSound(12);
        }
        else if (idFood == 37)
        {
            Language.Instance.onSound(13);
        }
        else
        {
            Language.Instance.onSound(14);
        }
        yield return new WaitForSeconds(3f);
        delay = false;
        yield break;
    }

    public void openAnimal()
    {
        int numberAnimal = PlayerPrefs.GetInt("numberAnimal" + gameObject.name);
        slga = 3;
        if (numberAnimal < slga)
        {
            transform.GetChild(numberAnimal + 3).gameObject.SetActive(true);
            PlayerPrefs.SetInt("numberAnimal" + gameObject.name, numberAnimal + 1);
        }
        else
        {
            Language.Instance.notifyEngOrVi("Cage is full", "Chuồng bạn đã đầy");
        }
    }

    public void showEat()
    {
        if (Guide.Instance.step == 9 || Guide.Instance.step == 10)
        {
            DialogController.Instance.showDialog(transform.GetChild(0).transform.position, 2, idFood);
            Guide.Instance.tayChi.transform.position = new Vector2(100f, 100f);
        }
        else
        {
            if (!GameManager.Instance.guide)
            {
                DialogController.Instance.showDialog(transform.GetChild(0).transform.position, 2, idFood);
            }
        }
    }
}