using UnityEngine;

public class BubbleController : MonoBehaviour
{
    private Rigidbody2D rb;
    Animation anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = this.transform.GetChild(0).gameObject.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 launchDirection = new Vector2(1, 0);
            rb.AddForce(launchDirection * 0.3f, ForceMode2D.Impulse);
        }
        if (rb.linearVelocity.x < 0.1f)
        {
            anim.Play();
        }
    }
}
