using Discord;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Discord_Controller : MonoBehaviour
{
    public long applicationID;
    [Space]
    public string state = "Score: ";
    public string details = "High Score: ";
    [Space]
    public string largeImage = "lfbu";
    public string largeText = "Flappy Bird Unity";

    public FlappyBird flappyBirdScript;
    public GameObject flappyBird;
    private int flappyBirdScore;
    private int flappyBirdHighScore;
    private long time;

    private static bool instanceExists;
    public Discord.Discord discord;

    void Awake()
    {
        // Transition the GameObject between scenes, destroy any duplicates
        if (!instanceExists)
        {
            instanceExists = true;
            DontDestroyOnLoad(gameObject);
        }
        else if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Log in with the Application ID
        discord = new Discord.Discord(applicationID, (System.UInt64)Discord.CreateFlags.NoRequireDiscord);
        
        time = System.DateTimeOffset.Now.ToUnixTimeMilliseconds();

        UpdateStatus();
    }

    void Update()
    {
        flappyBirdScript = GameObject.FindGameObjectWithTag("FlappyBird").GetComponent<FlappyBird>();
        flappyBirdScore = flappyBirdScript.score;
        flappyBirdHighScore = PlayerPrefs.GetInt("highScore");

        try
        {
            discord.RunCallbacks();
        }
        catch
        {
            Destroy(gameObject);
        }
    }

    void LateUpdate()
    {
        UpdateStatus();
    }

    void UpdateStatus()
    {
        try
        {
            var activityManager = discord.GetActivityManager();
            var activity = new Discord.Activity
            {
                Details = state + flappyBirdScore.ToString(),
                State = details + flappyBirdHighScore.ToString(),
                Assets =
                {
                    LargeImage = largeImage,
                    LargeText = largeText
                },
                Timestamps =
                {
                    Start = time
                }
            };

            activityManager.UpdateActivity(activity, (res) =>
            {
                if (res != Discord.Result.Ok) Debug.LogWarning("Cannot connect to Discord");
            });
        }
        catch
        {
            Destroy(gameObject);
        }
    }
}
