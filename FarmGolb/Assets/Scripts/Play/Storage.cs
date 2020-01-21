using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class Storage : MonoBehaviour
{
    public int id;
    public GameObject dialogStorage;

    private void OnMouseDown()
    {
        CheckDistance.Instance.posOld();
    }

    private void OnMouseUp()
    {
        if (CheckDistance.Instance.distance() && !EventSystem.current.IsPointerOverGameObject(0))
        {
            AnimClick.Instance.clickObject(gameObject);
            Language.Instance.onSound(1);
            StartCoroutine(showStorage());
        }
    }

    IEnumerator showStorage()
    {
        yield return new WaitForSeconds(0.25f);
        dialogStorage.GetComponent<Animator>().Play("AimBlack", -1, 0);
        AnimClick.Instance.showUI(dialogStorage);
        dialogStorage.GetComponent<StorageController>().loadStorage(id);
        GameManager.Instance.cameraOnOff(true);
    }
}