using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float _speed;
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _dashDuration;
    [SerializeField] private float _dashCooldown;

    private Rigidbody _rb;
    private Vector3 _dir;
    private bool _doDash;
    private float _elapsedTime;
    private float _elapsedCooldown;

    public void Move(InputAction.CallbackContext context)
    {

        _dir = context.ReadValue<Vector3>();

    }

    public void Dash(InputAction.CallbackContext context)
    {

        if (context.performed && _elapsedCooldown >= _dashCooldown)
        {
            _doDash = true;
            _elapsedTime = 0;
            _elapsedCooldown = 0;
        }

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _dashCooldown += _dashDuration;
        _elapsedCooldown = _dashCooldown;
        _elapsedTime = _dashDuration;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if(_doDash)
        {
            _rb.linearVelocity = _dir * _dashSpeed * Time.deltaTime;
        }
        else
        {
            _rb.linearVelocity = _dir * _speed * Time.deltaTime;
        }

        if(_elapsedTime < _dashDuration)
        {
            _elapsedTime += Time.deltaTime;
        }
        else
        {
            _doDash = false;
        }

        if (_elapsedCooldown < _dashCooldown)
        {
            _elapsedCooldown += Time.deltaTime;
        }


    }
}
