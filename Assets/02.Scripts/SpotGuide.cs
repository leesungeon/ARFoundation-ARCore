using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARCore;
using UnityEngine.UI;

public class SpotGuide : MonoBehaviour
{
    public ARFaceManager faceManager;
    public GameObject[] guideObj;
    public UI_Manager uiManager;
    public Text indexTxt;

    List<GameObject> fox = new List<GameObject>();
    ARCoreFaceSubsystem subSys;

    NativeArray<ARCoreFaceRegionData> regionData;

    void Start()
    {
        for(int i = 0; i < 3; i++)
        {
            InstantiateObj(i);
        }

        InstantiateObj(3);

        subSys = (ARCoreFaceSubsystem)faceManager.subsystem;

        faceManager.facesChanged += OnThreePoints;
        faceManager.facesChanged += OnAllPoints;

    }

    void Update()
    {
        if (uiManager.num == 0)
        {
            for (int i = 0; i < regionData.Length; i++)
            {
                fox[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < regionData.Length; i++)
            {
                fox[i].SetActive(false);
            }
        }
    }

    void OnThreePoints (ARFacesChangedEventArgs args)
    {
        if(args.updated.Count > 0)
        {
            subSys.GetRegionPoses(args.updated[0].trackableId, Allocator.Persistent, ref regionData);

            for(int i = 0; i < regionData.Length; i++)
            {
                fox[i].transform.position = regionData[i].pose.position;
                fox[i].transform.rotation = regionData[i].pose.rotation;
                fox[i].SetActive(true);
            }
        }
        else if(args.removed.Count > 0)
        {
            for (int i = 0; i < regionData.Length; i++)
            {
                fox[i].SetActive(false);
            }
        }
    }

    void OnAllPoints (ARFacesChangedEventArgs args)
    {
        if (args.updated.Count > 0)
        {
            int index = int.Parse(indexTxt.text);

            Vector3 guidePosition = args.updated[0].vertices[index];

            guidePosition = args.updated[0].transform.TransformPoint(guidePosition);

            fox[3].SetActive(true);
            fox[3].transform.position = guidePosition;
        }
        else if(args.removed.Count > 0)
        {
            fox[3].SetActive(false);
        }
    }

    void InstantiateObj(int i)
    {
        GameObject obj = Instantiate(guideObj[i]);
        fox.Add(obj);
        obj.SetActive(false);
    }
}
