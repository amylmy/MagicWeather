using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEditor;
using LitJson;
using System.IO;
using System;
using System.Net;
using UnityEngine.SceneManagement;

public class MapsAPI : MonoBehaviour {
    //origin
    public GameObject originalTable;
    public GameObject[] tables;
    public GameObject texterror;
    public float lan;
    public float lon;
    public GameObject maincam;
	public GameObject RUN;

    //weather map related
    public GameObject canvas1;
    public string checkState;
    public WWW www;
    public WWW www2;
    public WWW www3;
    public WWW www4;
    public WWW www5;
    public WWW www6;
    public WWW www7;
    public WWW www8;
    public WWW www9;
    public WWW www10;

    //public float lan2;
    //public float lon2;

    float speed = 5.0f;
    string param = ChooseMenu.Filter;
    Transform target;

    // Use this for initialization
    IEnumerator Start () {
        //if (!Input.location.isEnabledByUser)
        //    yield break;


        //Input.location.Start();

        //// Ожидание инициализации
        //int maxWait = 20;
        //while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        //{
        //    yield return new WaitForSeconds(1);
        //    maxWait--;
        //}

        //// Если не прошла инициализации в течении 20 секунд, прервать 
        //if (maxWait < 1)
        //{
        //    print("Time out");
        //    yield break;
        //}

        //// Сообщение об ошибке
        //if (Input.location.status == LocationServiceStatus.Failed)
        //{
        //    //print("Unable to determine device location");
        //    //yield break;
        //    lan = 55.0171f;
        //    lon = 82.9837f;
        //}
        //else
        //{
        //    // Доступ получен вывести данные
        //    lan = Input.location.lastData.latitude;
        //    lon = Input.location.lastData.longitude;
        //}
        ////Отключить отслеживание GPS
        //Input.location.Stop();

        lan = 55.0171f;
        lon = 82.9837f;

        string weatherurl = "https://api.openweathermap.org/data/2.5/weather?lat="+ lan.ToString() + "&lon=" + lon.ToString() + "&appid=759070676508c3e8e554dc86ac2d6dcd";
        string weatherurl2 = "https://api.openweathermap.org/data/2.5/weather?lat=" + lan.ToString() + "&lon=" + (lon+20f).ToString() + "&appid=759070676508c3e8e554dc86ac2d6dcd";
        Debug.Log(weatherurl2);
        
        // string url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location="+lan.ToString()+","+lon.ToString()+"&radius=500&"+param+"&key=AIzaSyBdHiBxdXr5M-DKeAP494aTcEUa9imgrPw";
        //string url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=59.931167,30.360708&radius=500&" +param+"&key=AIzaSyBdHiBxdXr5M-DKeAP494aTcEUa9imgrPw";
        //WWW www = new WWW(url);

        WWW www = new WWW(weatherurl);
        WWW www2 = new WWW(weatherurl2);
        yield return www;

        String[] gameObjectName1 = {"Text11", "Text12", "Text13", "Text14", "Icon1"};
        ProcessWeatherJson(www.text, gameObjectName1);

        String[] gameObjectName2 = { "Text21", "Text22", "Text23", "Text24", "Icon2" };
        ProcessWeatherJson(www2.text, gameObjectName2);

        //String[] gameObjectName3 = { "Text31", "Text32", "Text33", "Text34", "Icon3" };
        //ProcessWeatherJson(www.text, gameObjectName3);

        //String[] gameObjectName4 = { "Text41", "Text42", "Text43", "Text44", "Icon4" };
        //ProcessWeatherJson(www.text, gameObjectName4);

        //String[] gameObjectName5 = { "Text51", "Text52", "Text53", "Text54", "Icon5" };
        //ProcessWeatherJson(www.text, gameObjectName5);

        //String[] gameObjectName6 = { "Text61", "Text62", "Text63", "Text64", "Icon6" };
        //ProcessWeatherJson(www.text, gameObjectName6);


        //if (www.error == null)
        //{
        //texterror.GetComponent<Text>().text = www.text;
        //ProcessLocationJson(www.text);

        //ProcessWeatherJson(www.text);
        //}
        //else
        //{
        //    texterror.GetComponent<Text>().text = "ERROR: " + www.error;
        //}
    }

    private void ProcessWeatherJson(string jsonString, String[] gameObjectName)
    {
        JsonData jsonvale = JsonMapper.ToObject(jsonString);
        tables = new GameObject[jsonvale["main"].Count];

        Material cloudyMat = Resources.Load("Materials/cloudy", typeof(Material)) as Material;
        Material rainyMat = Resources.Load("Materials/rainy", typeof(Material)) as Material;
        Material sunnyMat = Resources.Load("Materials/sunny", typeof(Material)) as Material;
        Material thunderMat = Resources.Load("Materials/thunder", typeof(Material)) as Material;
        
        for (int i = 0; i < jsonvale["main"].Count; i++)
        {
            Transform[] ts = canvas1.transform.GetComponentsInChildren<Transform>(true);
            Debug.Log(ts.ToString());
            foreach (Transform t in ts)
            {
                if (t.gameObject.name == gameObjectName[0])
                {
                    string locationName = jsonvale["name"].ToString();
                    t.gameObject.GetComponent<Text>().text = locationName;
                }
                if (t.gameObject.name == gameObjectName[1])
                {
                    float tempnum = float.Parse(jsonvale["main"]["temp"].ToString()) -273.15f;
                    t.gameObject.GetComponent<Text>().text = tempnum.ToString() + "°C";
                    Debug.Log(tempnum.ToString());
                }
                if (t.gameObject.name == gameObjectName[2])
                {
                    float windspeed = float.Parse(jsonvale["wind"]["speed"].ToString());
                    t.gameObject.GetComponent<Text>().text = "Wind Speed: " + windspeed.ToString() + " m/s";
                }
                if (t.gameObject.name == gameObjectName[3])
                {
                    float humiditynum = float.Parse(jsonvale["main"]["humidity"].ToString());
                    t.gameObject.GetComponent<Text>().text = "Humidity: " + humiditynum + "%";
                }

                //if (t.gameObject.name == gameObjectName[3])
                //{
                //    t.gameObject.GetComponent<Text>().text = "Novosibirsk";
                //}
                if (t.gameObject.name == gameObjectName[4])
                {
                    checkState = jsonvale["weather"][0]["main"].ToString();

                    Debug.Log(jsonvale["weather"][0]["main"].ToString());

                    switch (jsonvale["weather"][0]["main"].ToString())
                    {
                        case "Clear":
                            t.gameObject.GetComponent<RawImage>().material = sunnyMat;
                            Debug.Log("switch: " + t.gameObject.GetComponent<RawImage>().material.ToString());

                            break;
                        case "Clouds":
                            t.gameObject.GetComponent<RawImage>().material = cloudyMat;
                            break;
                        case "Rain":
                            t.gameObject.GetComponent<RawImage>().material = rainyMat;
                            break;
                        case "Fog":
                            t.gameObject.GetComponent<RawImage>().material = thunderMat;
                            break;
                    }
                }
            }

        }

    }

    private void ProcessLocationJson(string jsonString)
    {
        int rad = 6372795;
        float llat1;
        float llong1;
        float llat2;
        float llong2;

        JsonData jsonvale = JsonMapper.ToObject(jsonString);
        tables = new GameObject[jsonvale["results"].Count];

        for (int i = 0; i < jsonvale["results"].Count; i++)
        {
            //gis-lab.info/qa/great-circles.html

            //Debug.Log(jsonvale["results"][i]["geometry"]["location"]["lat"].ToString());
            //Debug.Log(jsonvale["results"][i]["geometry"]["location"]["lng"].ToString());
			llat1 = lan;
			llong1 = lon;
            llat2 = float.Parse(jsonvale["results"][i]["geometry"]["location"]["lat"].ToString());
            llong2 = float.Parse(jsonvale["results"][i]["geometry"]["location"]["lng"].ToString());

            float lat1 = llat1 * (float)Math.PI / 180;
            float lat2 = llat2 * (float)Math.PI / 180;
            float long1 = llong1 * (float)Math.PI / 180;
            float long2 = llong2 * (float)Math.PI / 180;

            float cl1 = (float)Math.Cos(lat1);
            float cl2 = (float)Math.Cos(lat2);
            float sl1 = (float)Math.Sin(lat1);
            float sl2 = (float)Math.Sin(lat2);
            float delta = long2 - long1;
            float cdelta = (float)Math.Cos(delta);
            float sdelta = (float)Math.Sin(delta);

            float x = (cl1 * sl2) - (sl1 * cl2 * cdelta);
            float y = sdelta * cl2;
            float z = (float)Math.Atan(-y / x)*(180/(float)Math.PI);
            if (x < 0) {
                z = z + 180;
            }

            float z2 = (z + 180) % 360 - 180;
            z2 = -z2 / (180 / (float)Math.PI);
            float anglerad2 = z2 - ((2 * (float)Math.PI) * (float)Math.Floor((z2 / (2 * (float)Math.PI))));
            float angledeg = (anglerad2 * 180) / (float)Math.PI;

            y = (float)Math.Sqrt(Math.Pow(cl2 * sdelta, 2) + Math.Pow(cl1 * sl2 - sl1 * cl2 * cdelta, 2));
            x = sl1 * sl2 + cl1 * cl2 * cdelta;
            float ad = (float)Math.Atan2(y, x);
            float dist = ad * rad;

            float tablex = maincam.transform.position.x + dist / 60 * (float)Math.Cos(angledeg);
            float tablez = maincam.transform.position.z + dist / 60 * (float)Math.Sin(angledeg);
			           
            tables[i] = Instantiate(originalTable, new Vector3(tablex, 1.96f, tablez), Quaternion.identity);
            tables[i].transform.LookAt(maincam.transform);
            Transform[] ts = tables[i].transform.GetComponentsInChildren<Transform>(true);
            Material FoodMat = Resources.Load("Materials/Fastfood", typeof(Material)) as Material;
			Material opened = Resources.Load("Materials/opened", typeof(Material)) as Material;
			Material closed = Resources.Load("Materials/closed", typeof(Material)) as Material;//getting material
			Material hz = Resources.Load("Materials/hz", typeof(Material)) as Material;
            foreach (Transform t in ts) {
                if (t.gameObject.name == "Caption")
                {
                    t.gameObject.GetComponent<Text>().text = jsonvale["results"][i]["name"].ToString();
                }
                if (t.gameObject.name == "Type")
                {
					t.gameObject.GetComponent<Text>().text = dist.ToString()+" метра";
                }
                if (t.gameObject.name == "Icon")
                {
                    t.gameObject.GetComponent<RawImage>().material = FoodMat; //applying material
                }
				if (t.gameObject.name == "Status")
				{
					try 
					{
						if (jsonvale ["results"] [i] ["opening_hours"]["open_now"].ToString () == "True") {
							GameObject.Find ("Status").GetComponent<Renderer>().material = opened;
							GameObject.Find ("StatusText").GetComponent<Text>().text = "Открыто";
						} 
						else
						{
							GameObject.Find ("Status").GetComponent<Renderer> ().material = closed;
							GameObject.Find ("StatusText").GetComponent<Text>().text = "Закрыто";
						}
					}
					catch
					{
						GameObject.Find ("Status").GetComponent<Renderer> ().material = hz;
						GameObject.Find ("StatusText").GetComponent<Text>().text = "Нет данных";
					}
				}
            };
        }



    }


    //camera
    //stackoverflow.com/questions/11019296/is-it-possible-to-set-video-from-device-camera-as-background-in-unity3d
    //answers.unity3d.com/questions/9729/how-can-i-display-a-flat-background-2d-image-not-a.html

    // Update is called once per frame
    void Update () {
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
        }
        else
        {
            lan = Input.location.lastData.latitude;
            lon = Input.location.lastData.longitude;
        }

        if (Input.GetMouseButton(0))
        {
            transform.LookAt(maincam.transform);
            transform.RotateAround(maincam.transform.position, Vector3.up, Input.GetAxis("Mouse X") * speed);
        }

    }

	public void RunVasya()
	{
		for(int i=0; i < tables.Length; i++){
		Transform[] ts = tables[i].transform.GetComponentsInChildren<Transform>(true);
		foreach (Transform t in ts) {
			t.gameObject.SetActive (false);
		}
		}
		RUN.SetActive (true);
	}
}
