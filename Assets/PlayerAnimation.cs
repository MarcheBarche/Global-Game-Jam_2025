using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    public AnimationStatus _status { get; private set; }

    [SerializeField] Animator animator;
    public enum AnimationStatus
    {
        IDLE = 0,
        WALK = 1,
        JUMP = 2,
    }

    public void ChangeStatusAnimation(AnimationStatus status)
    {
        animator.SetFloat("status", (float)(int)status);
        this._status = status;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
