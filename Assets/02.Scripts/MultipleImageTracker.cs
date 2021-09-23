using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using UnityEngine.Video;
using System;

[Serializable]
public class MarkerInfo
{
    public string imgName;
    public GameObject targetObj;
}

public class MultipleImageTracker : MonoBehaviour
{
    public MarkerInfo[] markerInfos;

    ARTrackedImageManager imgManager;
    
    void Start()
    {
        imgManager = GetComponent<ARTrackedImageManager>();
        imgManager.trackedImagesChanged += OnTrackedImage;
    }

    private void OnDestroy()
    {
        imgManager.trackedImagesChanged -= OnTrackedImage;
    }

    public void OnTrackedImage(ARTrackedImagesChangedEventArgs args)
    {
        for(int i = 0; i < args.updated.Count; i++)
        {
            ARTrackedImage trackedImg = args.updated[i];

            for(int j = 0; j<markerInfos.Length; j++)
            {
                if(trackedImg.referenceImage.name == markerInfos[j].imgName)
                {
                    if(trackedImg.trackingState == TrackingState.Tracking)
                    {
                        markerInfos[j].targetObj.SetActive(true);
                        markerInfos[j].targetObj.transform.position = trackedImg.transform.position;
                        markerInfos[j].targetObj.transform.rotation = trackedImg.transform.rotation;
                    }
                    else
                    {
                        markerInfos[j].targetObj.SetActive(false);
                    }
                }
            }
        }
    }

    public void OnVideoPlayBtn(int i)
    {
        VideoPlayer video = markerInfos[i].targetObj.transform.GetChild(0).GetComponent<VideoPlayer>();
        video.Play();
    }

    public void OnVideoPauseBtn(int i)
    {
        VideoPlayer video = markerInfos[i].targetObj.transform.GetChild(0).GetComponent<VideoPlayer>();
        video.Pause();
    }

    public void OnTicketBtn(string url)
    {
        Application.OpenURL(url);
    }
}
