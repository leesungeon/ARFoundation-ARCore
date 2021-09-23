using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public ARFaceManager faceManager;
    public Material[] faceMats;
    public Text indexTxt;

    public int num;
    int indexNum = 0;
    int indexCnt = 468;

    void Start()
    {
        num = 0;

        indexTxt.text = indexNum.ToString();
    }

    public void MaskToggleBtn()
    {
        foreach (ARFace face in faceManager.trackables)
        {
            if(face.trackingState == TrackingState.Tracking)
            {
                face.gameObject.SetActive(!face.gameObject.activeSelf);
                indexNum = 0;
            }
        }
    }

    public void SwitchMaterial(int count)
    {
        foreach(ARFace face in faceManager.trackables)
        {
            if(face.trackingState == TrackingState.Tracking)
            {
                MeshRenderer mr = face.GetComponent<MeshRenderer>();

                num += count;

                if (num > faceMats.Length - 1) num = 0;
                if (num < 0) num = faceMats.Length - 1;

                mr.material = faceMats[num];
            }
        }
    }

    public void IndexIncrese()
    {
        int number = Mathf.Min(++indexNum, indexCnt - 1);
        indexTxt.text = number.ToString();
    }

    public void IndexDecease()
    {
        int number = Mathf.Max(--indexNum, 0);
        indexTxt.text = number.ToString();
    }
}

