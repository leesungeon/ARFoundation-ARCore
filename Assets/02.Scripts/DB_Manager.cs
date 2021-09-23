using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using System;
using UnityEngine.UI;

public class DB_Manager : MonoBehaviour
{
    Vector3 currentPos;
    string objName = " ";
    string curKey = " ";
    bool isSearch = false;

    public Text text;

    public GPS_Manager GPS_Manager;
    public string databaseUrl = "https://arproject-b7549-default-rtdb.asia-southeast1.firebasedatabase.app/";

    void Start()
    {
        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new Uri(databaseUrl);

        //SaveData();
        LoadData();
    }

    void SaveData()
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

        ImageGPSData data1 = new ImageGPSData("Seoul", 37.0f, 23.4f, 123f, false);
        ImageGPSData data2 = new ImageGPSData("Busan", 137.0f, 1223.4f, 13.5f, false);
        ImageGPSData data3 = new ImageGPSData("Daegu", 237.0f, 223.4f, 0.3f, false);

        string jsondata1 = JsonUtility.ToJson(data1);
        string jsondata2 = JsonUtility.ToJson(data2);
        string jsondata3 = JsonUtility.ToJson(data3);

        reference.Child("Korea").Child("area1").SetRawJsonValueAsync(jsondata1);
        reference.Child("Korea").Child("area2").SetRawJsonValueAsync(jsondata2);
        reference.Child("Korea").Child("area3").SetRawJsonValueAsync(jsondata3);

        print("데이터 저장 완료!");
    }

    void LoadData()
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("Korea");
        reference.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                foreach (DataSnapshot data in snapshot.Children)
                {
                    IDictionary GPSdata = (IDictionary)data.Value;
                    Debug.Log("이름 : " + GPSdata["name"] + "위도" + GPSdata["latitude_data"] + "경도" + GPSdata["longitude_data"] + "고도" + GPSdata["altitude_data"]);
                }
            }
        }
        );
    }
}

public class ImageGPSData
{
    public string name = "";
    public float latitude_data = 0;
    public float longitude_data = 0;
    public float altitude_data = 0;
    public bool isCaptured = false;

    public ImageGPSData(string Name, float Lat, float Long, float Alt, bool objCaptured)
    {
        name = Name;
        latitude_data = Lat;
        longitude_data = Long;
        altitude_data = Alt;
        isCaptured = objCaptured;
    }
}
