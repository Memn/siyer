﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class MazeDirectCharacterController : MonoBehaviour
{
    private const float NormalSpeed = 2;
    private const float RunSpeed = 4;
    [SerializeField] private float _moveSpeed = NormalSpeed;

    [SerializeField] private float _jumpForce = 3;
    [SerializeField] private Transform _camera;

    private Animator _animator;
    private Rigidbody _rigidBody;


    private float _currentV;
    private float _currentH;

    private const float Interpolation = 10;
    private readonly float _backwardsWalkScale = 0.16f;
    private readonly float _backwardRunScale = 0.66f;

    private bool _wasGrounded;
    private Vector3 _currentDirection = Vector3.zero;

    private float _jumpTimeStamp;
    private const float runInterval = 2.5f;

    private bool _isGrounded;
    private readonly List<Collider> _collisions = new List<Collider>();

    private const float MaxZ = 25.5f;
    private const float MinZ = -1.7f;
    private const float MaxX = 25.5f;
    private const float MinX = -1.7f;
    private const float MaxY = 1;
    private const float MinY = 0.05f;


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

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _animator.SetBool("Grounded", _isGrounded);

        DirectUpdate();

        _wasGrounded = _isGrounded;
    }

    private void DirectUpdate()
    {
        var v = CrossPlatformInputManager.GetAxis("Vertical");
        var h = CrossPlatformInputManager.GetAxis("Horizontal");

        _currentV = Mathf.Lerp(_currentV, v, Time.deltaTime * Interpolation);
        _currentH = Mathf.Lerp(_currentH, h, Time.deltaTime * Interpolation);


        var direction = _camera.forward * _currentV + _camera.right * _currentH;

        var directionLength = direction.magnitude;
        direction.y = 0;
        direction = direction.normalized * directionLength;

        if (direction != Vector3.zero)
        {
            _currentDirection = Vector3.Slerp(_currentDirection, direction, Time.deltaTime * Interpolation);

            transform.rotation = Quaternion.LookRotation(_currentDirection);
            var clamped = transform.position + _currentDirection * _moveSpeed * Time.deltaTime;
            clamped.x = Mathf.Clamp(clamped.x, MinX, MaxX);
            clamped.y = Mathf.Clamp(clamped.y, MinY, MaxY);
            clamped.z = Mathf.Clamp(clamped.z, MinZ, MaxZ);
            transform.position = clamped;

            _animator.SetFloat("MoveSpeed", direction.magnitude);
        }

        Running();
    }

    private void Running()
    {
        var runOver = Time.time - _jumpTimeStamp >= runInterval;
        if (!runOver) return;
        if (!Input.GetKey(KeyCode.Space) && !CrossPlatformInputManager.GetButton("Jump")) return;
        _moveSpeed = RunSpeed;
        Invoke("RunningEnd", runInterval);
    }

    private void RunningEnd()
    {
        _moveSpeed = NormalSpeed;
    }
}