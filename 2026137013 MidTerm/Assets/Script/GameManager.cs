using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TMP_InputField inputField;
    public Button gameStartButton;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameStartButton.onClick.AddListener(OnGameStartButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGameStartButtonClicked()
    {
        string playerName = inputField.text;
        if(string.IsNullOrEmpty(playerName))
        {
            Debug.Log("플레이어 이름 입력");
            return;
        }

        PlayerPrefs.SetString("PlayerName",playerName);
        PlayerPrefs.Save();

        Debug.Log("이름 저장" + playerName);

        SceneManager.LoadScene("LV1");



    }
}
