using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float _jumpForce = 10.0f;
    [SerializeField] 
    private bool _grounded = false;
    private Rigidbody2D _rigid;
    private bool _resetJumpNeeded = false;
    private bool isFacingRight = true;
    private float move;
    private UIManager _uiManager;
    public Animator animator;
    [SerializeField]
    private AudioClip _jumpSound;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private float _cutJumpHeight = 0.5f;
    public float horizontalDampingWhenStopping;
    public float horizontalDampingWhenTurning;
    public float horizontalDampingBasic;


    // Start is called before the first frame update
    void Start()
    {
        //_ammo = 10;

        _rigid = GetComponent<Rigidbody2D>();
        if(_rigid == null)
        {
            Debug.LogError("The rigid body is NULL");
        }

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if(_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL.");
        }

        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("AudioSource on the player is null");
        }
        else
        {
            _audioSource.clip = _jumpSound;
        }
    }

    private void FixedUpdate()
    {
        float horizontalVelocity = _rigid.velocity.x;
        horizontalVelocity += Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 0.01f)
            horizontalVelocity *= Mathf.Pow(1f - horizontalDampingWhenStopping, Time.deltaTime * 10f);
        else if (Mathf.Sign(Input.GetAxisRaw("Horizontal")) != Mathf.Sign(horizontalVelocity))
            horizontalVelocity *= Mathf.Pow(1f - horizontalDampingWhenTurning, Time.deltaTime * 10f);
        else
            horizontalVelocity *= Mathf.Pow(1f - horizontalDampingBasic, Time.deltaTime * 10f);

        _rigid.velocity = new Vector2(horizontalVelocity, _rigid.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(_rigid.velocity.x)); //needs to be always positive

    }


    void Update()
    {
        if (Time.timeScale > 0f)
        {
            Flip();
            if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Joystick1Button0)) && _grounded)
            {
                Jump(_jumpForce);
            }
            CheckForGrounded();
            CutJump();
            JumpAnimation();
        }
    }

    private void Flip()
    {
        move = Input.GetAxisRaw("Horizontal");
        if (isFacingRight &&  move < 0f || !isFacingRight && move > 0f)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }

    public void Jump(float force)
    {
        _rigid.velocity = new Vector2(_rigid.velocity.x, force);
        _grounded = false;
        _resetJumpNeeded = true;
        _audioSource.Play();
        StartCoroutine(ResetJumpNeededRoutine());
    }

    private void CheckForGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 0.9f, 1 << 6);
        //Debug.DrawRay(transform.position, Vector2.down * 0.9f, Color.green);
        //Debug.LogError(hitInfo.collider);
        if (hitInfo.collider != null)
        {
            if (_resetJumpNeeded == false)
                _grounded = true;
        }
    }

    private void CutJump()
    {
        if ((Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.Joystick1Button0)) && (_rigid.velocity.y > 0))
        {
            _rigid.velocity = new Vector2(_rigid.velocity.x, _rigid.velocity.y * _cutJumpHeight);
         
        }
    }

    private void JumpAnimation()
    {
        if (_rigid.velocity.y != 0 && _grounded == false) //If in the air
        {
            animator.SetBool("IsJumping", true);
        }

        if (_grounded == true)
        {
            animator.SetBool("IsJumping", false);
            animator.SetBool("JumpingUp", false);
            animator.SetBool("FallingDown", false);
        }

        if (_rigid.velocity.y > 0 && _grounded == false)
        {
            animator.SetBool("JumpingUp", true);
            animator.SetBool("FallingDown", false);
        }

        if (_rigid.velocity.y <= 0 && _grounded == false)
        {
            animator.SetBool("FallingDown", true);
            animator.SetBool("JumpingUp", false);
        }
    }

    IEnumerator ResetJumpNeededRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        _resetJumpNeeded = false;
    }

}
