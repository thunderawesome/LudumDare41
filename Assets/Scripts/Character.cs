using InControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(VoxelAnimation))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class Character : MonoBehaviour
{
    #region Private Variables
    private VoxelAnimation m_voxelAnimation;
    private CharacterAction m_actions;

    private Rigidbody m_rigidbody;
    private Animator m_animator;

    private Vector3 m_velocity = Vector3.zero;

    private bool m_isMoving = false;
    #endregion

    #region Public Variables
    public float movementSpeed = 5.0f;
    #endregion

    #region Unity Methods
    // Use this for initialization
    private void Start()
    {
        InitializeComponents();
        InitializeInput();
    }

    // Update is called once per frame
    private void Update()
    {
        Input();
    }
    #endregion

    #region Private Methods
    private void InitializeInput()
    {
        m_actions = new CharacterAction();
        m_actions.Left.AddDefaultBinding(InputControlType.DPadLeft);
        m_actions.Right.AddDefaultBinding(InputControlType.DPadRight);
        m_actions.Special.AddDefaultBinding(InputControlType.Action1);

        //analog stick
        m_actions.Left.AddDefaultBinding(InputControlType.LeftStickLeft);
        m_actions.Right.AddDefaultBinding(InputControlType.LeftStickRight);
        m_actions.Down.AddDefaultBinding(InputControlType.LeftStickDown);
        m_actions.Up.AddDefaultBinding(InputControlType.LeftStickUp);

        //keyboard
        m_actions.Left.AddDefaultBinding(Key.LeftArrow);
        m_actions.Right.AddDefaultBinding(Key.RightArrow);
        m_actions.Special.AddDefaultBinding(Key.Z);
    }

    private void InitializeComponents()
    {
        m_voxelAnimation = GetComponent<VoxelAnimation>();
        m_rigidbody = GetComponent<Rigidbody>();
        m_animator = GetComponent<Animator>();

        m_rigidbody.constraints = RigidbodyConstraints.FreezeRotationX;
        m_rigidbody.constraints = RigidbodyConstraints.FreezeRotationZ;
    }

    private void Input()
    {
        Movement();
        Interact();
    }

    private void Interact()
    {
        if (m_actions.Special.WasPressed)
        {
            // Do something
        }
    }

    private void Movement()
    {
        if (m_actions.Move.IsPressed == true)
        {
            m_isMoving = true;
            CheckIfMoving(m_actions.Move.Value);
        }
        else if (m_actions.Move.WasReleased == true)
        {
            m_isMoving = false;
        }

        m_animator.SetBool("IsMoving", m_isMoving);
        m_animator.SetFloat("Speed", Mathf.Abs(m_velocity.x));
    }

    private void CheckIfMoving(Vector2 inputValue)
    {
        m_velocity = inputValue * movementSpeed;
        m_rigidbody.velocity = m_velocity;
    }
    #endregion
}
