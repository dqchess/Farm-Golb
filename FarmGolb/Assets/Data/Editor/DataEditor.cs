using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DataEditor  {

    [MenuItem("Data/Data/DataStorage")]        
    [MenuItem("Data/Data/DataShop")]    
    [MenuItem("Data/Data/DataMachine")]
    [MenuItem("Data/Data/DataSprite")]    

    public static void DetailSeeds()
    {
        DataStorage dataStorage = ScriptableObject.CreateInstance<DataStorage>();     
        DataShop dataShop = ScriptableObject.CreateInstance<DataShop>();
        DataMachine dataMachine = ScriptableObject.CreateInstance<DataMachine>();       
        DataSprite dataSprite = ScriptableObject.CreateInstance<DataSprite>();        
        
        AssetDatabase.CreateAsset(dataStorage, "Assets/Data/Data/DataStorage.asset");        
        AssetDatabase.CreateAsset(dataShop, "Assets/Data/Data/DataShop.asset");        
        AssetDatabase.CreateAsset(dataMachine, "Assets/Data/Data/DataMachine.asset");        
        AssetDatabase.CreateAsset(dataSprite, "Assets/Data/Data/DataSprite.asset");                 
        
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = dataStorage;              
        Selection.activeObject = dataShop;        
        Selection.activeObject = dataMachine;
        Selection.activeObject = dataSprite;        
    }
}
