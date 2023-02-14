using UnityEngine;

public class StopBox : MonoBehaviour
{
    public bool isPlayerTooClose = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isPlayerTooClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isPlayerTooClose = false;
        }
    }
}
