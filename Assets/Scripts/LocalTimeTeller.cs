using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class LocalTimeTeller : MonoBehaviour
{
    public string Location;
    private string url;
    public GameObject timeTextObject;
    public bool HourClock = false;
    // Start is called before the first frame update
    void Start()
    {
        url = "http://worldtimeapi.org/api/timezone/" + Location;
        InvokeRepeating("GetDataFromWeb", 2f, 1f);
    }

    // Update is called once per frame
    void GetDataFromWeb()
    {
        StartCoroutine(GetRequest(url)); // Starting CoRoutine
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            string rawJson = webRequest.downloadHandler.text;
            WorldTimeClass timeInfo = JsonUtility.FromJson<WorldTimeClass>(rawJson);
            switch (webRequest.result)
            {
                case UnityWebRequest.Result.Success:
                    if (HourClock == false)
                    {
                        timeTextObject.GetComponent<TextMeshPro>().text = timeInfo.getTime1();
                    }
                    else {

                        timeTextObject.GetComponent<TextMeshPro>().text = timeInfo.getTime2();
                    }
                    break;
                default:
                    Debug.LogError(": Error: " + webRequest.error);
                    break;
            }
        }
    }

    [Serializable]
    public class WorldTimeClass
    {
        public string abbreviation;
        public string client_ip;
        public string datetime;
        public int day_of_week;
        public int day_of_year;
        public bool dst;
        public string dst_from;
        public int dst_offset;
        public string dst_until;
        public int raw_offset;
        public string timezone;
        public string unixtime;
        public string utc_datetime;
        public string utc_offset;
        public int week_number;

        public string getTime1()
        {
            string timetext = this.datetime.Split('T')[1].Split('+')[0];
            string[] timesplit = timetext.Split(':');
            int h = Convert.ToInt32(timesplit[0]);
            string m = timesplit[1];
            return this.AMPMFormat(h, m);
        }

        public string getTime2()
        {
            string timetext = this.datetime.Split('T')[1].Split('+')[0];
            string[] timesplit = timetext.Split(':');

            return timesplit[0] + timesplit[1];
        }
        public string AMPMFormat(int a, string b) {
            string hr = a.ToString();
            string suffix = "AM";
            if (a < 12) {
                suffix = "AM";
            }
            else {
                suffix = "PM";
            }
            int temp_a = a;
            if (a > 12) {
                temp_a = (a - 12);
            }
            if (temp_a < 10)
            {
                hr = "0" + temp_a.ToString();
            }
            else {
                hr = temp_a.ToString();
            }

            return hr+":"+b + " " + suffix;
        }
    }

}