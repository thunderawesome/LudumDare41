using InControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(VoxelAnimation))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider))]
public class Character : MonoBehaviour
{
    #region Private Variables
    private VoxelAnimation m_voxelAnimation;
    private CharacterAction m_actions;

    private Rigidbody m_rigidbody;
    private Animator m_animator;
    private Collider m_collider;

    private Vector3 m_velocity = Vector3.zero;

    private bool m_isMoving = false;
    #endregion

    #region Public Variables
    public float movementSpeed = 5.0f;
    public float rotationSpeed = 1000.0f;
    private Vector3 m_gizmoStartPos;
    private Vector3 m_gizmoEndPos;
    private GameObject m_groundCheckStartObj;
    private GameObject m_groundCheckEndObj;
    private float m_groundCheckOffset = .1f;
    #endregion

    #region Unity Methods
    // Use this for initialization
    private void Start()
    {
        InitializeComponents();
        InitializeInput();
        InitializeGroundCheck();
    }

    // Update is called once per frame
    private void Update()
    {
        Input();
    }

    private void OnDrawGizmos()
    {
        if (m_groundCheckEndObj == null || m_groundCheckStartObj == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(m_groundCheckStartObj.transform.position, m_groundCheckOffset);


        Gizmos.color = Color.red;
        Gizmos.DrawSphere(m_groundCheckEndObj.transform.position, m_groundCheckOffset);
    }
    #endregion

    #region Private Methods
    private void InitializeInput()
    {
        m_actions = new CharacterAction();
        m_actions.Left.AddDefaultBinding(InputControlType.DPadLeft);
        m_actions.Right.AddDefaultBinding(InputControlType.DPadRight);
        m_actions.Down.AddDefaultBinding(InputControlType.DPadDown);
        m_actions.Up.AddDefaultBinding(InputControlType.DPadUp);
        m_actions.Special.AddDefaultBinding(InputControlType.Action1);

        //analog stick
        m_actions.Left.AddDefaultBinding(InputControlType.LeftStickLeft);
        m_actions.Right.AddDefaultBinding(InputControlType.LeftStickRight);
        m_actions.Down.AddDefaultBinding(InputControlType.LeftStickDown);
        m_actions.Up.AddDefaultBinding(InputControlType.LeftStickUp);

        //keyboard
        m_actions.Left.AddDefaultBinding(Key.LeftArrow);
        m_actions.Right.AddDefaultBinding(Key.RightArrow);
        m_actions.Left.AddDefaultBinding(Key.A);
        m_actions.Right.AddDefaultBinding(Key.D);
        m_actions.Down.AddDefaultBinding(Key.DownArrow);
        m_actions.Up.AddDefaultBinding(Key.UpArrow);
        m_actions.Down.AddDefaultBinding(Key.S);
        m_actions.Up.AddDefaultBinding(Key.W);
        m_actions.Special.AddDefaultBinding(Key.Z);
    }

    private void InitializeComponents()
    {
        m_voxelAnimation = GetComponent<VoxelAnimation>();
        m_rigidbody = GetComponent<Rigidbody>();
        m_animator = GetComponent<Animator>();
        m_collider = GetComponent<Collider>();

        m_rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    private void InitializeGroundCheck()
    {
        m_gizmoStartPos = m_collider.bounds.ClosestPoint(Vector3.down * 2) + new Vector3(0, m_groundCheckOffset, 0);
        m_gizmoEndPos = m_gizmoStartPos - new Vector3(0, m_groundCheckOffset + m_groundCheckOffset, 0);
        m_groundCheckStartObj = new GameObject("GroundCheck_START");
        m_groundCheckStartObj.transform.position = m_gizmoStartPos;
        m_groundCheckStartObj.transform.parent = transform;

        m_groundCheckEndObj = new GameObject("GroundCheck_END");
        m_groundCheckEndObj.transform.position = m_gizmoEndPos;
        m_groundCheckEndObj.transform.parent = transform;

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
            m_voxelAnimation.Stop();
        }

        if (m_actions.Move.WasPressed == true)
        {
            m_isMoving = true;
            m_voxelAnimation.Play();
        }

        m_animator.SetBool("IsMoving", m_isMoving);
        m_animator.SetFloat("Speed", Mathf.Abs(m_velocity.x));
    }

    private void CheckIfMoving(Vector2 inputValue)
    {
        //horizontal movement
        var _h = inputValue.x;

        //vertical movement
        var _v = inputValue.y;

        //if the absolute values of horizontal and vertical movement is greater than zero
        if (Mathf.Abs(_h) > 0 || Mathf.Abs(_v) > 0)
        {
            //set the rotation vector direction to face
            var moveDir = new Vector3(_h, 0, _v);
            //find the wanted rotation angle based on the rotation vector
            Quaternion wanted_rotation = Quaternion.LookRotation(moveDir, Vector3.up);
            //set the player's rotation to rotate towards the last inputed rotation vector direction
            transform.rotation = Quaternion.RotateTowards(transform.rotation, wanted_rotation,
                rotationSpeed * Time.deltaTime);
        }

        m_velocity = new Vector3(_h, 0, _v) * movementSpeed;
        m_velocity.y = m_rigidbody.velocity.y;
        m_rigidbody.velocity = m_velocity;
    }
    #endregion
}
