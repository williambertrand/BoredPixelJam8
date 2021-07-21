using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public int GameScene = 1;
    public int MenuScene = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayGame()
    {
        SceneManager.LoadScene(GameScene);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(MenuScene);
    }
}
