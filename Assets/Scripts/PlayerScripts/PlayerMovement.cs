using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private CharacterController _characterController;
    private Vector3 _moveDirection;

    public float Speed = 5f;
    public float JumpForce = 10f;
    public float Gravity = 20f;
    public float VerticalVelocity;
   
    void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        _moveDirection = new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0f, Input.GetAxis(Axis.VERTICAL));
        _moveDirection = transform.TransformDirection(_moveDirection);
        _moveDirection = _moveDirection * Speed * Time.deltaTime;

        ApplyGravity();

        _characterController.Move(_moveDirection);
    }

    void ApplyGravity()
    {
        VerticalVelocity -= Gravity * Time.deltaTime;
        PlayerJump();
        _moveDirection.y = VerticalVelocity * Time.deltaTime;
    }

    void PlayerJump()
    {
        if (_characterController.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            VerticalVelocity = JumpForce;
        }
    }
}
