using UnityEngine;
using UnityEngine.EventSystems;

public class Boat : MonoBehaviour
{
    public static Boat instance;
    public GameObject dialogBoat;

    #region
    //Tàu đến sau 10h nó sẽ chạy

    //Xuất bến sau 4 h nó ms về
    #endregion

    // Use this for initialization
    void Start()
    {
        instance = this;
        //tinh thoi gian xuat ben hay ve ben...
        go();
    }

    private void OnMouseUp()
    {
        if (PlayerPrefs.GetInt("level") < 17)
        {
            Language.Instance.notifyEngOrVi("Expansions open at level 17", "Mở khóa cấp 17!");
        }
        else if (CheckDistance.Instance.distance() && !EventSystem.current.IsPointerOverGameObject())
        {
            GameManager.Instance.cameraOnOff(true);
            Language.Instance.onSound(1);
            dialogBoat.SetActive(true);
            AnimClick.Instance.showUI(dialogBoat);
        }
    }

    //cap ben
    public void dock()
    {

    }

    //xuat ben
    public void export()
    {

    }

    //go
    public void go()
    {
        LeanTween.moveLocal(gameObject, new Vector2(-27, 7), 13);
        //
    }

    // go home
    public void goHome()
    {
        LeanTween.moveLocal(gameObject, new Vector2(-19, 11), 13);
        //
    }
}
