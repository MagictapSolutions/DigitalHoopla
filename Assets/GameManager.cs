using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    public int ScoreFinal,turn,AddNextScore;
    public bool canPlay,didScore;
    public GameObject YouLoose, YouWin , HandDetector,HandDetector2;
    
        private const string url = "https://www.gokapture.com/ingramevent/insert_score.php";
public Text scoreTXT,canThrowTXT,ResultScore;

    public Leaderboard LB;
    public Animator HooplaAnim;

    public int employeeId;
    public string gameName;
    public int score;
    public string username;
    public string password;


    // Start is called before the first frame update
    void Start()
    {
        canPlay = true;

        ScoreFinal = 0;

        turn = 0;
    }




    // POSTING Score to USER

    private IEnumerator PostScoreCoroutine()
    {
        int emp =int.Parse(PlayerPrefs.GetString("PName"));
        employeeId = emp;
        gameName = "digital_hoopla";
        score = ScoreFinal;
        username = "ingram_usr";
        password = "64a81e7baac71";



        // Create the JSON data as a string
        string jsonData = "{\"employee_id\":" + employeeId + ",\"game_name\":\"" + gameName + "\",\"score\":" + score + ",\"username\":\"" + username + "\",\"password\":\"" + password + "\"}";

        // Convert the JSON string to bytes
        byte[] postData = System.Text.Encoding.UTF8.GetBytes(jsonData);

        // Create a UnityWebRequest object with the URL and method
        UnityWebRequest request = new UnityWebRequest(url, "POST");

        // Set the request headers
        request.SetRequestHeader("Content-Type", "application/json");
        request.uploadHandler = new UploadHandlerRaw(postData);
        request.downloadHandler = new DownloadHandlerBuffer();

        // Send the request
        yield return request.SendWebRequest();

        // Check for errors
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error sending request: " + request.error);
        }
        else
        {
            // Process the response
            string response = request.downloadHandler.text;
            Debug.Log("Score Sent ? : " + response);
        }
    }




    // Update is called once per frame
    void Update()
    {
        scoreTXT.text = "Score : " + ScoreFinal +"K";

        ResultScore.text =ScoreFinal + "K";
    }

    public void canThrowIt(bool c)
    {
       
       
            canThrowTXT.gameObject.SetActive(c);
        
    }

    public void PlayAnimation(string throwName)
    {
        canPlay = false;

        turn++;

        if (throwName == "Third")
        {
            int randomValue = Random.Range(1, 4);

            if (randomValue == 1 || randomValue == 3)
            {
                didScore = true;

                AddNextScore = randomValue;
            }

            HooplaAnim.SetInteger("AnimPlaying", randomValue);
        }


        else if (throwName == "Second")
        {
            int randomValue2 = Random.Range(10, 14);

            AddNextScore = randomValue2;


            HooplaAnim.SetInteger("AnimPlaying", randomValue2);
        }


        else if (throwName == "First")
        {
            int randomValue3 = Random.Range(6, 9);

            if (randomValue3 == 6 || randomValue3 == 8)
            {
                didScore = true;
                AddNextScore = randomValue3;

            }


            StartCoroutine("ScoreDelay");

            print("Score is : " + AddNextScore);

            HooplaAnim.SetInteger("AnimPlaying", randomValue3);
        }

        Invoke("ResetValue", 3f);
    }



    public void ShowResult()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }


    IEnumerator ScoreDelay()
    {
        yield return new WaitForSeconds(1.4f);

        AddScoring();
    }


    public void AddScoring()
    {
        if (AddNextScore == 6)
        {
            ScoreFinal = ScoreFinal + 30;
        }

        else if (AddNextScore == 8)
        {
            ScoreFinal = ScoreFinal + 50;
        }

        else if(AddNextScore == 10)
        {
            didScore = true;
        }

        else if(AddNextScore == 10)
        {
            ScoreFinal = ScoreFinal + 10;
        }


        else if(AddNextScore == 1)
        {
            ScoreFinal = ScoreFinal + 50;
        }

        else if (AddNextScore == 3)
        {
            ScoreFinal = ScoreFinal + 30;
        }
    }


    public void ResetValue()
    {
        HandDetector.GetComponent<BoxCollider>().enabled = true;

        HandDetector2.GetComponent<BoxCollider>().enabled = true;


        canThrowIt(true);

        HooplaAnim.SetInteger("AnimPlaying", 99);


        if (didScore)
        {
           
            didScore = false;
        }

        if (turn>=3)
        {

            //LB.ShowTopScorers(ScoreFinal);

            // Show Leaderboard here

           // LB.AddScore(PlayerPrefs.GetString("PName"), ScoreFinal);

            if (ScoreFinal>0)
            {
                YouWin.SetActive(true);
            }

            else
            {
                YouLoose.SetActive(true);
            }

            StartCoroutine(PostScoreCoroutine());
            Invoke("ShowResult", 7);
        }

        else if (turn<3)
        {

            canPlay = false;

        }
    }


    // NEXT STEP SHOW SCORE IN UI , TEST GAME AND SHOW GAMEOVER SCREEN ACCORDINGLY


}
