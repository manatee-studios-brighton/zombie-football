using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float distToGround = 0.6f;

    [SerializeField] private ConfigurableJoint hipJoint;
    [SerializeField] private Rigidbody hip;

    [SerializeField] private Animator targetAnimator;

    private bool _walking;
    private bool _headering;
    private bool _jumping;
    private bool _kicking;
    
    private bool _canHeader = true;
    private bool _canJump = true;
    private bool _canKick = true;
    
    private static readonly int AnimatorWalk = Animator.StringToHash("Walk");
    private static readonly int AnimatorHeader = Animator.StringToHash("Headering");
    private static readonly int AnimatorKick = Animator.StringToHash("Kicking");


    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
    }

    void FixedUpdate()
    {
        CharacterJump();
        CharacterKick();
        CharacterHeader();
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

    private void MoveCharacter()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;

            hipJoint.targetRotation = Quaternion.Euler(0f, targetAngle, 0f);

            hip.AddForce(direction * speed);

            _walking = true;
        }
        else
        {
            _walking = false;
        }

        targetAnimator.SetBool(AnimatorWalk, _walking);
    }

    private void CharacterJump()
    {
        //If currently jumping and the character is grounded, set the jumping variable to false
        if (_jumping && IsGrounded())
        {
            _jumping = false;
        }
        
        //If able to jump and the button is pressed, disable the ability to jump and set currently jumping true
        if (_canJump && Input.GetButton("Jump"))
        {
            _canJump = false;
            _jumping = true;
            hip.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        
        //If not currently jumping and not pressing the button, set the ability to jump to be true
        if (!_canJump && !Input.GetButton("Jump") && !_jumping)
        {
            _canJump = true;
        }
        
    }

    private void CharacterKick()
    {
        //If currently kicking and the animation finishes, set the headering variable to false
        if (_kicking && targetAnimator.GetCurrentAnimatorStateInfo(2).normalizedTime >= 1)
        {
            _kicking = false;
        }
        
        //If able to kick and the button is pressed, disable the ability to header and set currently kicking true
        if (_canKick && Input.GetButton("Kick"))
        {
            _canKick = false;
            _kicking = true;
        }
        
        //If not currently kicking and not pressing the button, set the ability to kick to be true
        if (!_canKick && !Input.GetButton("Kick") && !_kicking)
        {
            _canKick = true;
        }
        
        //animate whether or not kicking
        targetAnimator.SetBool(AnimatorKick,_kicking);
    }

    private void CharacterHeader()
    {
        //If currently headering and the animation finishes, set the headering variable to false
        if (_headering && targetAnimator.GetCurrentAnimatorStateInfo(1).normalizedTime >= 1)
        {
            _headering = false;
        }
        
        //If able to header and the button is pressed, disable the ability to header and set currently headering true
        if (_canHeader && Input.GetButton("Header"))
        {
            _canHeader = false;
            _headering = true;
        }
        
        //If not currently headering and not pressing the button, set the ability to header to be true
        if (!_canHeader && !Input.GetButton("Header") && !_headering)
        {
            _canHeader = true;
        }
        
        //animate whether or not headering
        targetAnimator.SetBool(AnimatorHeader,_headering);
    }
}