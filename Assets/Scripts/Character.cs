using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VoxelAnimation))]
public class Character : MonoBehaviour
{
    #region Private Variables
    private VoxelAnimation m_voxelAnimation;
    #endregion

    #region Unity Methods
    // Use this for initialization
    private void Start()
    {
        m_voxelAnimation = GetComponent<VoxelAnimation>();
    }

    // Update is called once per frame
    private void Update()
    {

    }
    #endregion
}
