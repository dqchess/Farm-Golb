using UnityEngine;

public class DataShop : ScriptableObject
{
    public ItemShop[] dataItemShop;

    [System.Serializable]
    public struct ItemShop
    {
        public string nameVi;
        public int idType;
        public string nameEng;
        public int levelUse, coin, slkeo, slmax, plusLevel, plusMaxsl;
        public Sprite icon, iconLock;
        public GameObject itemShop;
    }
}