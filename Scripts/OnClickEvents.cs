using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class OnClickEvents : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI soundText;
    // Start is called before the first frame update
    void Start()
    {
        
        if (GameManager.mute)
            {
            soundText.text = "/";
            }
        else
            {
            soundText.text = "";
            }
    }

    public void QuitGame()
        {
        Application.Quit();
        Debug.Log("Quit");
        }

    public void ToogleMute()
        {
        if (GameManager.mute)
            {
            GameManager.mute = false;
            soundText.text = "";
            }
        else
            {
            GameManager.mute = true;
            soundText.text = "/";
            }
        }

    public void ResetGame()
        {
        PlayerPrefs.SetInt("currentLevelIndex", 1);
        SceneManager.LoadScene("Helix Game");
        }
    }
