using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraScript : MonoBehaviour
{
    public GameObject Object1;
    public GameObject Object2;

    CinemachineVirtualCamera vcam;


    // Start is called before the first frame update
    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        bool changedBody = false;

        if (Input.GetKeyDown(KeyCode.V))
        {
            if (changedBody)
            {
                vcam.Follow = Object1.transform;
                changedBody = false;
            }
            else
            {
                vcam.Follow = Object2.transform;
                changedBody = true;
            }
        }
    }
}
