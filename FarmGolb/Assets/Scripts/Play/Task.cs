using UnityEngine;
using UnityEngine.UI;

public class Task : SingletonOne<Task>
{
    public GameObject itemTask;
    public Transform content;
    //int idTask;

    private void Start()
    {
        int i = 0;
        foreach (DataSprite.Task item in GameManager.Instance.dataSprite.tasks)
        {
            if (!PlayerPrefs.HasKey("taskMax" + i))
            {
                PlayerPrefs.SetInt("taskMax" + i, item.number);
            }
            Language.Instance.setNameText(item.nameVi, item.nameVi, itemTask.transform.GetChild(0).GetComponent<Text>());
            Language.Instance.setNameText(item.descriptionVi, item.descriptionEng, itemTask.transform.GetChild(1).GetComponent<Text>());
            itemTask.transform.GetChild(2).GetComponent<Text>().text = PlayerPrefs.GetInt("task" + i, 0) + "/" + PlayerPrefs.GetInt("taskMax" + i, 0);
            Instantiate(itemTask, content);
            i++;
        }
    }

    public void loadTask()
    {
        //check item task complete
        for (int i = 0; i < content.childCount; i++)
        {
            if (PlayerPrefs.GetInt("task" + i) >= PlayerPrefs.GetInt("taskMax" + i))
            {
                //show button nhan qua

                //ok
            }
        }        
    }

    public void completeTask()
    {
        //animation

        //create
    }

    public void close()
    {
        transform.localScale = Vector2.zero;
        GameManager.Instance.cameraOnOff(false);
    }
}