using UnityEngine;

public class MyTimer: MonoBehaviour
{
    public float Timer;
    public bool Finished;
    public static MyTimer instance { get; private set; }

    private void Start()
    {
        /*if (GameObject.FindGameObjectsWithTag("Timer").Length == 1)
            DontDestroyOnLoad(gameObject);
        else
            Destroy(gameObject);*/
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Tick(float val)
    {
        if (!Finished)
            Timer += val;
    }

    public  void Reset()
    {
        Timer = 0;
    }

    private void Update()
    {
        Tick(Time.unscaledDeltaTime);
        if (Timer >= 30) Finished = true;
        else Finished = false;
    }
}