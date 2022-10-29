using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GameManager : MonoBehaviour
{
    private GameObject tower;
    private float rotationSpeed = 150;
    public static bool gameOver;
    public static bool levelComplete;
    public static bool isGameStarted;

    public bool isConnectedToGooglePlay;

    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject levelCompletePanel;
    [SerializeField] Slider gameProgressSlider;
    [SerializeField] GameObject gamePlayPanel;
    [SerializeField] GameObject startMenuPanel;
    [SerializeField] GameObject newPlayer;
    
    public static int currentLevelIndex;
    public static int numberOfPassedRings;
    public static bool mute = false;
    public static int score = 0;
    public static int highScore;

    [SerializeField] TextMeshProUGUI currentLevelText;
    [SerializeField] TextMeshProUGUI nextLevelText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;

    private Vector3 newBallPos = new Vector3(0, 1, 2);

    public TextMeshProUGUI errorText;

    private void Awake()
        {
        currentLevelIndex = PlayerPrefs.GetInt("currentLevelIndex", 1);
        highScore = PlayerPrefs.GetInt("highScore", 0);

        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        }

    // Start is called before the first frame update
    void Start()
    {
        numberOfPassedRings = 0;
        isGameStarted = gameOver = levelComplete = false;
        Time.timeScale = 1;
        tower = GameObject.Find("Helix Manager");
       // Cursor.lockState = CursorLockMode.Locked;
        currentLevelText.text = currentLevelIndex.ToString();
        nextLevelText.text = (currentLevelIndex + 1).ToString();
        LogInToGooglePlay();

        errorText.text = "";
        }

    // Update is called once per frame
    void Update()
    {
        HelixRotation();
        GameOver();
        LevelComplete();
        Slide();
        StartGame();
        HighScore();
        NewBall();
                
        scoreText.text = "Score: " + score;
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("highScore", score);
        }

    void HelixRotation()
        {
        

        //For PC
        if (Input.GetMouseButton(0))
            {
            float mouseX = Input.GetAxisRaw("Mouse X");
            tower.transform.Rotate(0, -mouseX * rotationSpeed * Time.deltaTime, 0);
            }

        //For Mobile
        if (!isGameStarted) return;
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
            float xDelta = Input.GetTouch(0).deltaPosition.x;
            tower.transform.Rotate(0, -xDelta * rotationSpeed * Time.deltaTime, 0);
            }
        }

    void GameOver()
        {
        if (gameOver)
            {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);

            if (Input.GetButtonDown("Fire1"))
                {
                score = 0;
                SceneManager.LoadScene(0);
                }
            }
        }

    void LevelComplete()
        {
        if (levelComplete)
            {
            levelCompletePanel.SetActive(true);
            
            if (Input.GetButtonDown("Fire1"))
                {
                PlayerPrefs.SetInt("currentLevelIndex", currentLevelIndex+1);
                SceneManager.LoadScene("Helix Game");
                
                }
            
            }
        }

    void Slide()
        {
        int progress = numberOfPassedRings * 100 / FindObjectOfType<HelixManager>().numberOfRings;
        gameProgressSlider.value = progress;
        }

    void StartGame()
        {
        if (Input.GetMouseButtonDown(0) && !isGameStarted || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !isGameStarted)
            {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))  return;
            
            isGameStarted = true;
            gamePlayPanel.SetActive(true);
            startMenuPanel.SetActive(false);
            
            }
        }

    void HighScore()
        {
        if (score > PlayerPrefs.GetInt("highScore", 0))
            {
            PlayerPrefs.SetInt("highScore", score);
            }
        }

   void NewBall()
        {
        if (PlayerController.passedLevels == 4)
            {
            PlayerPrefs.SetInt("passedLevels", 0);
            Instantiate(newPlayer, newBallPos, Quaternion.identity);        
            
            }

        Debug.Log(PlayerController.passedLevels);
        }

    void LogInToGooglePlay()
        {
            PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
        }

    
    void ProcessAuthentication(SignInStatus status)
        {
        if(status == SignInStatus.Success)
            {
            isConnectedToGooglePlay = true; 
            } else isConnectedToGooglePlay = false;
        }
    
   
    public void onButtonPostToLeaderboard()
        {
        Debug.Log("posting");
       
        try
            {
            Leaderboard.PostToLeaderboard(highScore);
            }
        catch 
            {
            Debug.Log("Unable to post");
            }
        }

    public void OnButtonShowLeaderboard()
        {
        Debug.Log("Showing Leaderboard");
        Leaderboard.ShowLeaderboard();
        }
    }
