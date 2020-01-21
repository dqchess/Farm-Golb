using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectDrag : SingletonOne<ObjectDrag>
{
    public bool check;
    public bool checkCam;
    [HideInInspector]
    public GameObject ObjDrag;
    Camera cam;

    private void Start()
    {
        cam = Camera.main;
        checkCam = true;
    }

    void Update()
    {
        if (check)
        {
            ObjDrag.transform.position = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (check)
            {
                check = false;
                ObjDrag.transform.localScale = Vector2.zero;
                Destroy(ObjDrag, 0.5f);

                //show dialog diamond
                if (GameManager.Instance.listUserDiamond.Count > 0)
                {
                    DialogController.Instance.dialogBuyDiamond.SetActive(true);
                }

                if (checkCam)
                    GameManager.Instance.cameraOnOff(false);

                if (GameManager.Instance.guide)
                {
                    if (Guide.Instance.step <= 1)
                    {
                        if (Guide.Instance.check)
                        {
                            Guide.Instance.stepGuide(2);
                            Guide.Instance.check = false;
                        }
                        else
                        {
                            Guide.Instance.stepGuide(0);
                        }
                    }
                    else
                if (Guide.Instance.step == 5)
                    {
                        if (Guide.Instance.check)
                        {
                            Guide.Instance.stepGuide(6);
                            Guide.Instance.check = false;
                        }
                        else
                        {
                            Guide.Instance.stepGuide(4);
                        }
                    }
                    else if (Guide.Instance.step == 9)
                    {
                        if (Guide.Instance.check)
                        {
                            Guide.Instance.stepGuide(10);
                            Guide.Instance.check = false;
                        }
                        else
                        {
                            Guide.Instance.stepGuide(9);
                        }
                    }
                    else if (Guide.Instance.step == 10)
                    {
                        if (Guide.Instance.check)
                        {
                            if (GameManager.Instance.guide)
                            {
                                Guide.Instance.stepGuide(11);
                            }
                            Guide.Instance.check = false;
                        }
                        else
                        {
                            Guide.Instance.stepGuide(9);
                        }
                    }

                }
            }
        }
    }
}