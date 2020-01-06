using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeObject : SingletonOne<TimeObject>
{
    public Text txtName, txtMinutes, txtSeconds;
    public Image slider;
    public GameObject obTangToc;
    WaitForSeconds wait;
    int time, timeLive;
    public Sprite sprDiamond;

    private void Start()
    {
        wait = new WaitForSeconds(1);
        AnimClick.Instance.showDialog(gameObject);
    }

    public void showTimeDialog(int idSeed, int time, int timeLive, GameObject ob)
    {
        obTangToc = ob;
        if (idSeed < 26)
        {
            Language.Instance.setNameText(GameManager.Instance.dataStorage.dataStorages[idSeed].nameVi, GameManager.Instance.dataStorage.dataStorages[idSeed].nameEng, txtName);
        }
        else
        {
            Language.Instance.setNameText(GameManager.Instance.dataSprite.nameAnimals[idSeed - 33].nameVi, GameManager.Instance.dataSprite.nameAnimals[idSeed - 33].nameEng, txtName);
        }
        this.timeLive = timeLive;
        this.time = time;
        StartCoroutine(timeRun());
    }

    public void useDiamond()
    {
        if (ResourceManager.Instance.checkResource(1, 1))
        {
            animUserDiamond(obTangToc.transform.position);
            if (obTangToc.GetComponent<Field>())
                obTangToc.GetComponent<Field>().timeLive = 0;
            if (obTangToc.GetComponent<Animal>())
                obTangToc.GetComponent<Animal>().timeLive = 0;
            if (obTangToc.GetComponent<Tree>())
                obTangToc.GetComponent<Tree>().timeLive = 0;
            if (obTangToc.GetComponent<Hive>())
                obTangToc.GetComponent<Hive>().timeLive = 0;
            PlayerPrefs.SetInt("timeSave" + obTangToc.name, PlayerPrefs.GetInt("timeSave" + obTangToc.name, 0) - 1200);
            Destroy(gameObject);
        }
    }

    IEnumerator timeRun()
    {
        yield return wait;
        if (--timeLive > 0)
        {
            txtMinutes.text = (timeLive / 60).ToString();
            txtSeconds.text = (timeLive - timeLive / 60 * 60).ToString();
            slider.fillAmount = 1 - (float)timeLive / time;
            StartCoroutine(timeRun());
        }
    }

    void animUserDiamond(Vector3 target)
    {
        GameObject ob = Instantiate(FlyHarvest.Instance.animationSeed, target, Quaternion.identity);
        Instantiate(FlyHarvest.Instance.effectCrop, target, Quaternion.identity);
        ob.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprDiamond;
    }
}
