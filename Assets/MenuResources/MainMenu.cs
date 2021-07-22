using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{

    public int GameScene = 1;
    public int MenuScene = 0;

    public bool isGameOver;
    public TMP_Text bountyText;

    // Start is called before the first frame update
    void Start()
    {
        if(isGameOver)
        {
            bountyText.text = "Your final bounty: $" + GameStats.Bounty;
        }
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
