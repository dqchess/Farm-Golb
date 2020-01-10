using BitBenderGames;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    int[,] array;
    //data game
    public DataShop dataShop;
    public DataStorage dataStorage;
    public DataMachine dataMachine;
    public DataSprite dataSprite;
    public Tilemap highlightMap;
    public GameObject nenTrangODat;

    public List<GameObject> listUserDiamond;
    GameObject initOb;
    //save data
    public Transform ObTree;
    public Transform ObODat;
    public Transform ObCage;
    public Transform ObMachine;
    int idLoai;
    public bool guide;
    public GameObject Bee;
    public GameObject khoi;

    public bool tangTien;

    void Awake()
    {
        //Application.targetFrameRate = 60;
        //PlayerPrefs.SetInt("resource" + 0, 10000);
        //PlayerPrefs.SetInt("resource" + 1, 10000);        
        try
        {
            MyAdvertisement.ShowFullNormal();
        }
        catch
        {

        }

        PlayerPrefs.SetInt("move", 0);
        PlayerPrefs.SetInt("checkDrag", 1);

        if (!PlayerPrefs.HasKey("scaleX"))
        {
            PlayerPrefs.SetInt("scaleX", 1);
        }

        if (!PlayerPrefs.HasKey("level"))
        {
            PlayerPrefs.SetInt("level", 1);
            PlayerPrefs.SetInt("expmax", 10);

            if (tangTien)
            {
                PlayerPrefs.SetInt("resource" + 0, 1500000);
                PlayerPrefs.SetInt("resource" + 1, 1000000);
                PlayerPrefs.SetInt("ns" + 0, 10);
                PlayerPrefs.SetInt("ns" + 91, 10);
                PlayerPrefs.SetInt("ns" + 92, 10);
            }
            else
            {
                PlayerPrefs.SetInt("resource" + 0, 150);
                PlayerPrefs.SetInt("resource" + 1, 10);
                //cho so luong lua
                PlayerPrefs.SetInt("ns" + 0, 6);
                PlayerPrefs.SetInt("ns" + 91, 3);
                PlayerPrefs.SetInt("ns" + 92, 3);
            }
        }

        Instance = this;

        initArray();
        //guide
        if (PlayerPrefs.GetInt("step") <= 1)
        {
            PlayerPrefs.SetInt("levelsd" + 0, 1);
            PlayerPrefs.SetInt("coinshop" + 0, 10);

            if (PlayerPrefs.GetInt("step") == 0)
            {
                PlayerPrefs.SetInt("slkeo" + 0, 6);
                PlayerPrefs.SetInt("slmax" + 0, 9);

                guideField(-8.5f, 2.8f, 0);
                guideField(-9.35f, 2.4f, 1);
                guideField(-7.6f, 2.4f, 2);
                guideField(-8.5f, 2f, 3);
                guideField(-6.8f, 2, 4);
                guideField(-7.65f, 1.6f, 5);
            }
        }

        if (PlayerPrefs.GetInt("step") < 20)
        {
            guide = true;
        }
        else
        {
            guide = false;
        }

        for (int i = 0; i < 43; i++)
        {
            idLoai = dataShop.dataItemShop[i].idType;
            if (idLoai != 3)
            {
                for (int j = 0; j < PlayerPrefs.GetInt("slkeo" + i); j++)
                {
                    //init doi tuong
                    initOb = Instantiate(dataShop.dataItemShop[i].itemShop, Vector2.zero, Quaternion.identity);
                    initOb.gameObject.name += j;
                    Vector2 pos = new Vector2(PlayerPrefs.GetFloat("posx" + initOb.gameObject.name), PlayerPrefs.GetFloat("posy" + initOb.gameObject.name));
                    initOb.transform.position = highlightMap.GetCellCenterLocal(highlightMap.WorldToCell(pos));
                    initOb.GetComponent<MoveObject>().idLoai = dataShop.dataItemShop[i].idType;
                    int col = initOb.GetComponent<MoveObject>().col;
                    int row = initOb.GetComponent<MoveObject>().row;

                    if (PlayerPrefs.GetInt("scaleX" + initOb.gameObject.name) == 1)
                    {
                        setArray(col, row, pos, true);
                    }
                    else
                    {
                        setArray(row, col, pos, true);
                    }

                    setOrder(initOb, dataShop.dataItemShop[i].idType, initOb.transform.GetChild(0).transform.position.y);

                    setParent(idLoai, initOb);
                }
            }
        }
    }

    void guideField(float posx, float posy, int id)
    {
        PlayerPrefs.SetFloat("posx" + "Field(Clone)" + id, posx);
        PlayerPrefs.SetFloat("posy" + "Field(Clone)" + id, posy);
        PlayerPrefs.SetInt("checkSeed" + "Field(Clone)" + id, 0);
    }

    public void setParent(int idLoai, GameObject initOb)
    {
        switch (idLoai)
        {
            case 0:
                initOb.transform.SetParent(ObODat);
                break;
            case 1:
                initOb.transform.SetParent(ObMachine);
                break;
            case 2:
                initOb.transform.SetParent(ObCage);
                break;
            case 4:
                initOb.transform.SetParent(ObTree);
                break;
        }
    }

    public void cameraOnOff(bool bl)
    {
        if (bl)
        {
            MobileTouchCamera.checkCamFollow = bl;
        }
        else
        {
            StartCoroutine(openCam());
        }
    }

    IEnumerator openCam()
    {
        yield return new WaitForSeconds(0.1f);
        MobileTouchCamera.checkCamFollow = false;
    }

    GameObject obOder;
    public void setOrder(GameObject ob, int idLoai, float y)
    {
        int order = Mathf.RoundToInt((40 - y) * 100f);

        if (idLoai == 0)
        {
            //o dat
            ob.GetComponent<SpriteRenderer>().sortingOrder = order;
            ob.transform.GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = order + 2;
            ob.transform.GetChild(2).GetComponent<SpriteRenderer>().sortingOrder = order + 1;
            ob.transform.GetChild(3).GetComponent<Renderer>().sortingOrder = order + 3;
        }
        else if (idLoai == 1)
        {
            //nha may
            for (int i = 1; i < 10; i++)
            {
                obOder = ob.transform.GetChild(i).gameObject;
                if (i < 9)
                {
                    if (i != 2)
                    {
                        obOder.GetComponent<SpriteRenderer>().sortingOrder = order + i;
                    }
                    else
                    {
                        if (obOder.GetComponent<SpriteRenderer>())
                        {
                            obOder.GetComponent<SpriteRenderer>().sortingOrder = order + i;
                        }
                        else
                        {
                            obOder.GetComponent<Renderer>().sortingOrder = order + i;
                        }
                    }
                }
                else
                {
                    obOder.GetComponent<Renderer>().sortingOrder = order + i;
                }
            }
        }
        else if (idLoai == 2)
        {
            //chuong
            for (int i = 1; i < ob.transform.childCount; i++)
            {
                if (i == 1 || i == 2)
                {
                    ob.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder = order + i;
                }
                else
                {
                    ob.transform.GetChild(i).GetComponent<MeshRenderer>().sortingOrder = order + i;
                    ob.transform.GetChild(i).GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = order + i;
                    ob.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Renderer>().sortingOrder = order + i + 1;
                }
            }
        }
        else if (idLoai == 6)
        {
            for (int i = 1; i < 5; i++)
            {
                ob.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder = order + i;
            }
            ob.transform.GetChild(5).GetComponent<Renderer>().sortingOrder = order + 5;
        }
        else
        {
            //cay lau nam and decorator
            if (idLoai == 4)
            {
                ob.GetComponent<SpriteRenderer>().sortingOrder = order;
                ob.transform.GetChild(2).GetComponent<Renderer>().sortingOrder = order + 1;
            }
            ob.transform.GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = order;
        }
    }

    //init array data
    private void initArray()
    {
        array = new int[89, 89];
        for (int i = 0; i < 89; i++)
        {
            for (int j = 0; j < 89; j++)
            {
                array[i, j] = 0;

                //if (i > 40 && i < 44)
                //{
                //    if (j > 15 && j < 39)
                //    {
                //        array[i, j] = 1;
                //    }
                //}

                //if (i > 13 && i < 42)
                //{
                //    if (j > 37 && j < 41)
                //    {
                //        array[i, j] = 1;
                //    }
                //}

                //if (i > 41 && i < 46)
                //{
                //    if (j > 38 && j < 56)
                //    {
                //        array[i, j] = 1;
                //    }
                //}

                //if (i > 45 && i < 67)
                //{
                //    if (j > 38 && j < 42)
                //    {
                //        array[i, j] = 1;
                //    }
                //}

                ////silo
                //if (i > 37 && i < 40)
                //{
                //    if (j > 51 && j < 54)
                //    {
                //        array[i, j] = 1;
                //    }
                //}

                ////barn
                //if (i > 45 && i < 50)
                //{
                //    if (j > 51 && j < 55)
                //    {
                //        array[i, j] = 1;
                //    }
                //}

                ////sead
                //if (i > 36 && i < 56)
                //{
                //    if (j > 60 && j < 67)
                //    {
                //        array[i, j] = 1;
                //    }
                //}
            }
        }
    }

    //trung la true setArray 1
    //ko trung la false setArray 0
    public void setArray(int col, int row, Vector2 target, bool check)
    {
        target = getCellTileMap(target);

        x = (int)target.x;
        y = (int)target.y;

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                if (check)
                {
                    array[y + i, x + j] = 1;
                }
                else
                {
                    array[y + i, x + j] = 0;
                }
            }
        }
    }

    int x, y;
    public bool checkTrung(int col, int row, Vector2 target)
    {
        target = getCellTileMap(target);
        x = (int)target.x;
        y = (int)target.y;

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                if (array[y + i, x + j] == 1)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public Vector2 getCellTileMap(Vector2 pos)
    {
        x = highlightMap.WorldToCell(pos).x;
        y = highlightMap.WorldToCell(pos).y;
        pos.x = x + 44;
        pos.y = y + 44;
        return pos;
    }
}