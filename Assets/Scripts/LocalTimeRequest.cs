using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class LocalTimeRequest : MonoBehaviour
{
    public string url = "http://worldtimeapi.org/api/timezone/Europe/Paris";
    public GameObject timeTextObject;
    void Start()
    {
        InvokeRepeating("GetDataFromWeb", 2f, 1f);
    }

    void GetDataFromWeb()
    {
        StartCoroutine(GetRequest(url)); // Starting CoRoutine
    }

    public class TimeClass
    {
        public string datetime;

        public static TimeClass CreateFromJSON(string jsonString)
        {   
            return JsonUtility.FromJson<TimeClass>(jsonString);
        }
        public string GetStandardTime()
        {
            string timeSegment = this.datetime.Split('T')[1][0..5];
            return timeSegment + "PM";
        }
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            string rawJson = webRequest.downloadHandler.text;
            TimeClass newJson = TimeClass.CreateFromJSON(rawJson);
            string timeString = newJson.GetStandardTime();
            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    timeTextObject.GetComponent<TextMeshPro>().text = timeString;

                    break;
            }
        }
    }
}