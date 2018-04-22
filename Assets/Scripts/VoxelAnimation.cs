using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelAnimation : MonoBehaviour
{
    #region Public Variables
    public Frame[] frames;

    public Dictionary<string, Frame[]> anim;

    public bool isPlaying = false;
    public bool loop = false;
    public bool pingPong = false;

    public bool playOnStart = true;
    #endregion

    #region Private Variables
    private CoroutineRunner m_coroutineRunner;

    [SerializeField]
    private float m_speed = .1f;

    [SerializeField]
    private int m_currentFrame = 0;
    #endregion

    #region Unity Methods
    // Use this for initialization
    private void Start()
    {
        m_coroutineRunner = new CoroutineRunner(this);
        frames.ThrowErrorIfNull();

        if (playOnStart == true)
        {
            Play();
        }
    }
    #endregion

    #region Public Methods
    public void Play()
    {
        Debug.Log("Play animation");
        m_coroutineRunner.Start(PlayAnimation(), true);
    }

    public void Stop()
    {
        m_coroutineRunner.Stop();
    }
    #endregion

    #region Private Methods
    private IEnumerator PlayAnimation()
    {
        while (loop)
        {
            yield return new WaitForSeconds(m_speed);
            frames[m_currentFrame].gameObject.SetActive(false);
            m_currentFrame++;
            if(m_currentFrame >= frames.Length)
            {
                m_currentFrame = 0;
            }
            frames[m_currentFrame].gameObject.SetActive(true);
        }
    }
    #endregion

}
