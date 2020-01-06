using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using BitBenderGames;

public class Test : MonoBehaviour
{
    public Tile tile;
    public Tilemap highlightMap;
    Vector3Int currentCell;
    MoveObject moveOb;

    float speed = 1.0f; //how fast it shakes
    float amount = 1.0f; //how much it shakes
    private void Update()
    {
        //Debug.Log(highlightMap.WorldToCell(transform.position));
        //Debug.Log(highlightMap.WorldToLocal(transform.position));
        //Debug.Log(highlightMap.LocalToCell(transform.position));
        //Debug.Log(GameManager.Instance.getCellTileMap(transform.position));

    }
    float x, y, a;
    Vector2 vt;
    private void Start()
    {
        x = transform.position.x;
        y = transform.position.y;
        //moveOb = GetComponent<MoveObject>();
        //for (int i = -29; i < 30; i++)
        //{
        //    for (int j = -29; j < 30; j++)
        //    {
        //        highlightMap.SetTile(new Vector3Int(i, j, 0), tile);
        //    }
        //}
        LeanTween.moveLocal(gameObject, new Vector2(0.14f, 1.04f), 5f);
    }

    Vector2 target;
    private void OnMouseDrag()
    {
        MobileTouchCamera.checkCamFollow = true;
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentCell = highlightMap.WorldToCell(target);
        transform.position = highlightMap.GetCellCenterWorld(currentCell);
    }

    private void OnMouseUp()
    {
        MobileTouchCamera.checkCamFollow = false;
    }

    class Task
    {
        string nameTask;
        string description;

        int number = 0;

        void checkTask(int idTask)
        {
            if (PlayerPrefs.GetInt("task" + idTask, 0) >= number)
            {
                //show hien nen nha chinh nhap nhay de nhan qua
                //lam sao de task chuan nhi
            }
        }
    }

    Task tast = new Task();
}
