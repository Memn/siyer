using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class MazeCharacterController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 2;
    [SerializeField] private float _turnSpeed = 200;
    [SerializeField] private float _jumpForce = 4;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody _rigidBody;

    private float _currentV;
    private float _currentH;

    private const float Interpolation = 10;
    private const float WalkScale = 0.33f;
    private const float BackwardsWalkScale = 0.16f;
    private const float BackwardRunScale = 0.66f;

    private bool _wasGrounded;

    private float _jumpTimeStamp;
    private const float MinJumpInterval = 0.25f;

    private bool _isGrounded;
    private readonly List<Collider> _collisions = new List<Collider>();

    private void OnCollisionEnter(Collision collision)
    {
        var contactPoints = collision.contacts;
        foreach (var contactPoint in contactPoints)
        {
            if (!(Vector3.Dot(contactPoint.normal, Vector3.up) > 0.5f)) continue;
            if (!_collisions.Contains(collision.collider))
            {
                _collisions.Add(collision.collider);
            }
            _isGrounded = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        var contactPoints = collision.contacts;
        var validSurfaceNormal = contactPoints.Any(contactPoint => Vector3.Dot(contactPoint.normal, Vector3.up) > 0.5f);

        if (validSurfaceNormal)
        {
            _isGrounded = true;
            if (!_collisions.Contains(collision.collider))
            {
                _collisions.Add(collision.collider);
            }
        }
        else
        {
            if (_collisions.Contains(collision.collider))
            {
                _collisions.Remove(collision.collider);
            }

            if (_collisions.Count == 0)
            {
                _isGrounded = false;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (_collisions.Contains(collision.collider))
        {
            _collisions.Remove(collision.collider);
        }

        if (_collisions.Count == 0)
        {
            _isGrounded = false;
        }
    }

    void Update()
    {
        _animator.SetBool("Grounded", _isGrounded);


        TankUpdate();


        _wasGrounded = _isGrounded;
    }

    private void TankUpdate()
    {
        float v = CrossPlatformInputManager.GetAxis("Vertical");
        float h = CrossPlatformInputManager.GetAxis("Horizontal");

        bool walk = Input.GetKey(KeyCode.LeftShift);

        if (v < 0)
        {
            if (walk)
            {
                v *= BackwardsWalkScale;
            }
            else
            {
                v *= BackwardRunScale;
            }
        }
        else if (walk)
        {
            v *= WalkScale;
        }

        Debug.Log("v: " + v.ToString("F2"));
        Debug.Log("h: " + h.ToString("F2"));

        _currentV = Mathf.Lerp(_currentV, v, Time.deltaTime * Interpolation);
        _currentH = Mathf.Lerp(_currentH, h, Time.deltaTime * Interpolation);

        transform.position += transform.forward * _currentV * _moveSpeed * Time.deltaTime;
        transform.Rotate(0, _currentH * _turnSpeed * Time.deltaTime, 0);

        _animator.SetFloat("MoveSpeed", _currentV);

        JumpingAndLanding();
    }

    private void JumpingAndLanding()
    {
        var jumpCooldownOver = (Time.time - _jumpTimeStamp) >= MinJumpInterval;

        if (jumpCooldownOver && _isGrounded && Input.GetKey(KeyCode.Space))
        {
            _jumpTimeStamp = Time.time;
            _rigidBody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }

        if (!_wasGrounded && _isGrounded)
        {
            _animator.SetTrigger("Land");
        }

        if (!_isGrounded && _wasGrounded)
        {
            _animator.SetTrigger("Jump");
        }
    }
}