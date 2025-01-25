using UnityEngine;

public class BubbleController : MonoBehaviour
{
    [SerializeField] float bubbleForce = .3f; 
    private Rigidbody2D rb;
    Animation anim;

    public PlayerController parentPlayer;

    private float timer = 5f;
    private float currentTimer = 0f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("SPIKE"))
            Destroy(this.gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = this.transform.GetChild(0).gameObject.GetComponent<Animation>();
        Shoot();
        
    }

    public void Shoot()
    {
        rb.AddForce(-transform.up * bubbleForce, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        currentTimer += Time.deltaTime;
        if (currentTimer > timer)
            Destroy(this.gameObject);

        if (rb.linearVelocity.x < 0.1f)
        {
            this.transform.rotation = Quaternion.identity;
            anim.Play();
        }
    }
}
