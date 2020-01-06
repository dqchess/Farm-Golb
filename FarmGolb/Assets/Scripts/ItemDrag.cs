using UnityEngine;
using UnityEngine.UI;

public class ItemDrag : MonoBehaviour
{
    public int idItem;
    public Text txtNumber;

    public void updateText()
    {
        txtNumber.text = PlayerPrefs.GetInt("ns" + idItem).ToString();
    }
}
