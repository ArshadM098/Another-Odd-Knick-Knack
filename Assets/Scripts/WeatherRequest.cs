using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class WeatherRequest : MonoBehaviour
{
    public string lat;
    public string lon;
    public string API_key = "a93b0c11404d077c9140f5f9bdf16892";
    private string url;
    public GameObject WeatherTextObject;
    public GameObject TemperatureObject;
    public bool celsius = false;
    private string units;
    void Start()
    {
        if (celsius == false) {
            units = "imperial";
        }
        else if(celsius == true) {
            units = "metric";
        }
        url = "https://api.openweathermap.org/data/2.5/weather?lat=" + lat + "&lon=" + lon + "&appid=" + API_key+"&units="+ units;
        InvokeRepeating("GetDataFromWeb", 2f, 1f);
    }

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
            WeatherClass weatherInfo = JsonUtility.FromJson<WeatherClass>(rawJson);
            
            switch (webRequest.result)
            {
                case UnityWebRequest.Result.Success:
                    TemperatureObject.GetComponent<TextMeshPro>().text = weatherInfo.main.temp.ToString("0.#") + " C";
                    WeatherTextObject.GetComponent<TextMeshPro>().text = weatherInfo.weather[0].description.ToUpper();
                    break;
                default:
                    Debug.LogError(": Error: " + webRequest.error);
                    break;
            }
        }
    
    }
}

[Serializable]
public class WeatherClass
{
    public Coord coord;
    public Weather[] weather;
    public Main main;
    public string @base;
    public string visibility;
    public Wind wind;
    public Rain rain;
    public Clouds clouds;
    public string dt;
    public Sys sys;
    public string timezone;
    public string id;
    public string name;
    public string code;
}

[Serializable]
public class Coord
{
    public double lon;
    public double lat;
}

[Serializable]
public class Weather
{
    public int id;
    public string main;
    public string description;
    public string icon;
}

[Serializable]
public class Main
{
    public double temp;
    public double feels_like;
    public double temp_min;
    public double temp_max;
    public int pressure;
    public int humidity;
    public int sea_level;
    public int grnd_level;
}

[Serializable]
public class Wind
{
    public double speed;
    public double deg;
    public double gust;
}

[Serializable]
public class Rain
{
    public double lh;
}

[Serializable]
public class Clouds
{
    public int all;
}

[Serializable]
public class Sys
{
    public int type;
    public string id;
    public string country;
    public string sunrise;
    public string sunset;
}
