using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float distToGround = 0.6f;

    [SerializeField] private ConfigurableJoint hipJoint;
    [SerializeField] private Rigidbody hip;

    [SerializeField] private Animator targetAnimator;

    private bool _walk;
    private static readonly int AnimatorWalk = Animator.StringToHash("Walk");
    

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;

            hipJoint.targetRotation = Quaternion.Euler(0f, targetAngle, 0f);

            hip.AddForce(direction * this.speed);

            _walk = true;
        }
        else
        {
            _walk = false;
        }

        targetAnimator.SetBool(AnimatorWalk, this._walk);
    }

    void FixedUpdate()
    {
        if (Input.GetButton("Jump") && IsGrounded())
        {
            hip.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private bool IsGrounded()
    {
        int layerMask = 1 << 6;

        layerMask = ~layerMask;

        if (Physics.Raycast(hip.position, transform.TransformDirection(Vector3.down),distToGround, layerMask))
        {
            return true;
        }

        return false;

    }
}
