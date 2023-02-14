using UnityEngine;

public class CheckBox : MonoBehaviour
{
    public bool isPlayerDetected = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isPlayerDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isPlayerDetected = false;
        }
    }
}
