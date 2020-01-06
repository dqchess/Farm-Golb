using UnityEngine;

public abstract class SingletonOne<T> : MonoBehaviour where T : SingletonOne<T>
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = (T)this;
        }
        else
        {

        }
    }

}