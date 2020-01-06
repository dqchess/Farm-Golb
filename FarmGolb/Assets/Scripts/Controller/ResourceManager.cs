using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : SingletonOne<ResourceManager>
{
    //0: coin
    //1: diamond
    //public int id;
    public Text txtCoin;
    public Text txtDiamond;

    void Start()
    {
        txtCoin.text = PlayerPrefs.GetInt("resource" + 0).ToString();
        txtDiamond.text = PlayerPrefs.GetInt("resource" + 1).ToString();
    }

    public void minus(int id, int number)
    {
        updateResource(id, -number);
    }

    public void plus(int id, int number)
    {
        updateResource(id, number);
    }

    public bool checkResource(int id, int number)
    {
        if (PlayerPrefs.GetInt("resource" + id) - number >= 0)
        {
            minus(id, number);
            return true;
        }
        else
        {
            if (id == 0)
            {
                Language.Instance.notifyEngOrVi("You no enough coin", "Không đủ tiền");
            }
            else
            {
                Language.Instance.notifyEngOrVi("You no enough diamond", "Không đủ kim cương");
            }
            return false;
        }
    }

    public void updateResource(int id, int number)
    {
        PlayerPrefs.SetInt("resource" + id, PlayerPrefs.GetInt("resource" + id) + number);
        if (id == 0)
        {
            txtCoin.text = PlayerPrefs.GetInt("resource" + id).ToString();
        }
        else
        {
            txtDiamond.text = PlayerPrefs.GetInt("resource" + id).ToString();
        }
    }
}