using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool cursorActive = true;

    [SerializeField]
    public float ShrinkSpeed = 0.2f;

    private GameObject m_left_wall;
    private GameObject m_right_wall;

    private void Start()
    {
        m_left_wall = GameObject.Find("LeftWall");
        m_right_wall = GameObject.Find("RightWall");
    }

    void EnableCursor(bool enable)
    {
        if (enable)
        {
            cursorActive = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            cursorActive = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void OnEnable()
    {
        AppEvents.MouseCursorEnabled += EnableCursor;
    }

    private void OnDisable()
    {
        AppEvents.MouseCursorEnabled -= EnableCursor;
    }

    private void FixedUpdate()
    {
        m_left_wall.gameObject.transform.position -= new Vector3(ShrinkSpeed * Time.deltaTime, 0f, 0f);
        m_right_wall.gameObject.transform.position += new Vector3(ShrinkSpeed * Time.deltaTime, 0f, 0f);
    }
}