using UnityEngine;

public class DataSprite : ScriptableObject
{
    //data image cay lớn
    public SpriteObject[] sprSeed;
    public SpriteObject[] sprTree;
    public Sprite[] sprDialogDrag;
    public Sprite[] sprHarvest;
    public nameAnimal[] nameAnimals;
    public Task[] tasks;

    [System.Serializable]
    public struct SpriteObject
    {
        public Sprite[] spr;
    }

    [System.Serializable]
    public struct nameAnimal
    {
        public string nameVi, nameEng;
    }

    //nhiem vu nha chinh
    [System.Serializable]
    public struct Task
    {
        public string nameVi, nameEng;
        public string descriptionVi, descriptionEng;
        public int number;
    }
}