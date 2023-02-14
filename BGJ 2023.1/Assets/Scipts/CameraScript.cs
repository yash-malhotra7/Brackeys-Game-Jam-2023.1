using UnityEngine;
using Cinemachine;

public class CameraScript : MonoBehaviour
{
    CinemachineVirtualCamera vcam;

    // Start is called before the first frame update
    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!FindObjectOfType<GameManager>().isGhostInScene)
        {
            PlayerController Player = FindObjectOfType<PlayerController>();
            vcam.Follow = Player.transform;

        }
        else
        {
            Ghost Ghost = FindObjectOfType<Ghost>();
            vcam.Follow = Ghost.transform;
        }

    }
}
