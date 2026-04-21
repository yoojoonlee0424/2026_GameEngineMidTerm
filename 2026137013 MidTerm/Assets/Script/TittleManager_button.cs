using UnityEngine;
using UnityEngine.SceneManagement;

public class TittleManager_button : MonoBehaviour
{

    public GameObject help;
    public void ButtonLog()
    {
        Debug.Log("1");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("LV1");
    }

    public void CloseGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }



    public void OpenHelp()
    {
        help.SetActive(true);
    }

    public void CloseHelp()
    {
        help.SetActive(false);
    }



    public void Start() 
    {
        help.SetActive(false);
    }
}