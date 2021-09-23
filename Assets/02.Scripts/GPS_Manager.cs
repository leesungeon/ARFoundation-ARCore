using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class GPS_Manager : MonoBehaviour
{
    public Text[] data_text;

    public float maxWaitTime = 7.0f;
    public float resendTime = 1.0f;

    public float latitude = 0;
    public float longitude = 0;
    public float altitude = 0;

    bool receiveGPS = false;
    float delay = 0;

    void Start()
    {
        StartCoroutine(GPS_On());
    }

    public IEnumerator GPS_On()
    {
        LocationInfo info = Input.location.lastData;

        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);

            while (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                yield return null;
            }
        }

        if (!Input.location.isEnabledByUser)
        {
            data_text[3].text = "GPS OFF";
        }
        
        Input.location.Start();


        while(Input.location.status == LocationServiceStatus.Initializing && delay < maxWaitTime)
        {
            yield return new WaitForSeconds(1.0f);
            delay++;
        }

        if(Input.location.status == LocationServiceStatus.Failed || Input.location.status == LocationServiceStatus.Stopped)
        {
            for(int i = 0; i < data_text.Length; i++)
            {
                data_text[i].text = "위치 정보 수신 실패";
            }
        }

        if(delay >= maxWaitTime)
        {
            for (int i = 0; i < data_text.Length; i++)
            {
                data_text[i].text = "응답 대기 시간 초과";
            }
        }

        latitude = info.latitude;
        longitude = info.longitude;
        altitude = info.altitude;

        data_text[0].text = "위도 : " + info.latitude.ToString();
        data_text[1].text = "경도 : " + info.longitude.ToString();
        data_text[2].text = "고도 : " + info.altitude.ToString();
        data_text[3].text = "GPS Information SEND COMPLETE!";

        receiveGPS = true;

        while (receiveGPS)
        {
            yield return new WaitForSeconds(resendTime);

            info = Input.location.lastData;

            latitude = info.latitude;
            longitude = info.longitude;
            altitude = info.altitude;

            data_text[0].text = "위도 : " + info.latitude.ToString();
            data_text[1].text = "경도 : " + info.longitude.ToString();
            data_text[2].text = "고도 : " + info.altitude.ToString();
            data_text[3].text = "GPS Information SEND COMPLETE!";
        }
    }
}
