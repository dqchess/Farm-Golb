using UnityEngine;
using UnityEngine.EventSystems;

public class HomeMain : MonoBehaviour
{
    public GameObject task;

    private void OnMouseDown()
    {
        CheckDistance.Instance.posOld();
    }

    private void OnMouseUp()
    {
        if (CheckDistance.Instance.distance() && !EventSystem.current.IsPointerOverGameObject(0))
        {  
            //Task.Instance.loadTask();
            //Language.Instance.onSound(1);
            //task.GetComponent<Animator>().Play("AimBlack", -1, 0);
            //AnimClick.Instance.showUI(task);
            //GameManager.Instance.cameraOnOff(true);
        }
    }
}
