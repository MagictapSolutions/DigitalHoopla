using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using System.Collections;


public class SceneLoader : MonoBehaviour
{
    private const string url = "https://www.gokapture.com/ingramevent/check_employee.php";

    public string responseString;

    public KeyboardOpener KO;

    public InputField pName;

    public GameObject beginGameObject,ShowError;
    // Start is called before the first frame update
    void Start()
    {
       // StartCoroutine(PostAPIResponse());
    }

    public void ValidateUser()
    {
        StartCoroutine(PostAPIResponse());
    }


    private IEnumerator PostAPIResponse()
    {
        // Create the JSON data as a string
        string jsonData = "{\"employee_id\": "+pName.text+", \"username\": \"ingram_usr\", \"password\": \"64a81e7baac71\"}";

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
           // Debug.Log("API response: " + response);

            responseString = response;


            if (responseString == "{\"exists\":true}")
            {
                ShowError.SetActive(false);

                print("WORKS");

                PlayerPrefs.SetString("PName", pName.text);

                KO.CloseOSK();

                UnityEngine.SceneManagement.SceneManager.LoadScene(1);

            }

            else if (responseString == "{\"exists\":false}")
            {
                print("FAIL");

               

                ShowError.SetActive(true);
            }
        }
    }






    public void NextScreen()
    {
        beginGameObject.SetActive(false);
    }

    public void MoveForward()
    {
        if (string.IsNullOrEmpty(pName.text))
        {

        }

        else
        {


            //beginGameObject.SetActive(false);
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }


    public void LoadSceneHere(int b)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(b);
    }
}
