using UnityEngine;
using UnityEngine.UI;

public class TimeManager : SingletonOne<TimeManager>
{
    int second;
    int minutes;
    int hour;
    int dayOfYear;
    int timeSystem;

    public WaitForSeconds wait;

    private void OnEnable()
    {
        wait = new WaitForSeconds(1f);
    }

    public int getTimeSystem()
    {
        second = System.DateTime.Now.Second;
        minutes = System.DateTime.Now.Minute;
        hour = System.DateTime.Now.Hour;
        dayOfYear = System.DateTime.Now.DayOfYear;

        return second + minutes * 60 + hour * 3600 + dayOfYear * 86400;
    }

    public void saveTime(string nameObject)
    {
        PlayerPrefs.SetInt("timeSave" + nameObject, getTimeSystem());
    }

    public int timeOutApp(string name)
    {
        return getTimeSystem() - PlayerPrefs.GetInt("timeSave" + name, 0);
    }
}