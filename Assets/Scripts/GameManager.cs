using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public int GameOverScene = 3;
    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = new GameObject("CameraEffects");
        obj.AddComponent<CameraEffects>();
        obj.transform.parent = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Not sure how we want to do this...
    public void SpawnAmmo()
    {

    }

    public void OnGameOver()
    {
        SceneManager.LoadScene(GameOverScene);
    }
}
