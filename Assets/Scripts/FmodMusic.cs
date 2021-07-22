using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FmodMusic : MonoBehaviour
{
    private static FMOD.Studio.EventInstance Music;
    public string eventMusic;
    bool hasStarted = false;
    // Start is called before the first frame update
    private void OnEnable()
    {
        Debug.Log("on enable music");
        if (!hasStarted)
        {
            Debug.Log("PLAYING");
            Music = FMODUnity.RuntimeManager.CreateInstance(eventMusic);
            Music.start();
            Music.release();
            hasStarted = true;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Stop()
    {
        Music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void Progress()
    {
        Music.setParameterByName("transition", 2);
    }

    private void OnDestroy()
    {
        Music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

}
