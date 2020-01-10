using UnityEngine;
using UnityEngine.EventSystems;

public class MarketOpen : MonoBehaviour
{
    public GameObject market;

    private void OnMouseDown()
    {
        CheckDistance.Instance.posOld();
    }

    private void OnMouseUp()
    {
        if (PlayerPrefs.GetInt("level") < 5)
        {
            Language.Instance.notifyEngOrVi("Expansions open at level 5", "Mở khóa cấp 5!");
        }
        else if (CheckDistance.Instance.distance() && !EventSystem.current.IsPointerOverGameObject())
        {
            GameManager.Instance.cameraOnOff(true);
            Language.Instance.onSound(1);
            market.SetActive(true);
            AnimClick.Instance.showUI(market);
        }
    }
}
