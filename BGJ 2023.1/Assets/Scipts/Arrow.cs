using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float arrowDamage = 10;
    public float arrowLifetime = 6;

    public LayerMask myLayerMask;

    private BoxCollider2D myBoxCollider;

    private void Awake()
    {
        myBoxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        myBoxCollider.isTrigger = Physics2D.IsTouchingLayers(myBoxCollider, myLayerMask);

        if(arrowLifetime <= 0)
        {
            Destroy(gameObject);
        }

        arrowLifetime -= Time.deltaTime;
    }
}
