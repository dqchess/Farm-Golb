using UnityEngine;

public class DataStorage : ScriptableObject
{
    public DataStorages[] dataStorages;

    //storage, nang cap, 
    [System.Serializable]
    public struct DataStorages
    {
        public string nameVi, nameEng, describeEng, describeVi;
        public int coinBuy, coinSell, time, slThuH, levelUse, exp;
        public Sprite icon, iconLock;
    }    
}