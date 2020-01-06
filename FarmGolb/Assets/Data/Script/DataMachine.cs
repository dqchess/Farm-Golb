using UnityEngine;

public class DataMachine : ScriptableObject
{
    public DataMachine.itemMachine[] dataMachine;

    //[System.Serializable]
    //public struct Machine
    //{
    //    public string name;
    //    //id sản phẩm đầu tiên của nhà máy -> id của tất cả sản phẩm còn lại trong nhà máy
    //    public int id;
    //    public itemMachine[] itemMachines;
    //}

    [System.Serializable]
    public struct itemMachine
    {
        public string name;
        public nguyenLieu[] nguyenlieu;
    }

    [System.Serializable]
    public struct nguyenLieu
    {
        //public string type;
        public int id, number;
    }
}
