using UnityEngine;

public class CheckDistance : SingletonOne<CheckDistance>
{
    Vector2 pos;
    Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    public void posOld()
    {
        pos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    public bool distance()
    {
        return Vector2.Distance(pos, cam.ScreenToWorldPoint(Input.mousePosition)) < 0.2f && PlayerPrefs.GetInt("move") == 0;
    }

    public void moveCam(Vector2 pos)
    {
        if (!GameManager.Instance.guide)
        {
            //8            
            LeanTween.move(cam.gameObject, pos, Vector2.Distance(pos, cam.transform.position) * 5 * Time.deltaTime);
        }
    }

    public Vector2 getScreenToWorldPoint()
    {
        return cam.ScreenToWorldPoint(Input.mousePosition);
    }

    public void CancelLeanTween()
    {
        LeanTween.cancel(cam.gameObject);
    }
}
