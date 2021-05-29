using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static string guidId;

    private ApiController apiController;
    bool getNextScene;
    public void NextScene()
    {
        getNextScene = true;

    }

    void Update()
    {
        if (getNextScene)
        {
            getNextScene = false;
            apiController = (new GameObject("ApiController")).AddComponent<ApiController>();
            Debug.Log("NextScene called");
            apiController.StartApiCall<GameStart>("/gamestart", startGame);

        }
    }

    private void startGame(GameStart gameStart)
    {
        if (gameStart == null)
        {
            Debug.Log("gameStart null");
        }
        guidId = gameStart.guid;
        Debug.Log(guidId);
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
