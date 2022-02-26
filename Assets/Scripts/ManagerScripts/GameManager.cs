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
    private GameObject m_ball;

    private bool m_game_over = false;

    private void Start()
    {
        m_left_wall = GameObject.Find("LeftWall");
        m_right_wall = GameObject.Find("RightWall");
        m_ball = GameObject.Find("Ball");

        ResetGame();
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

    public void ResetGame()
    {
        m_left_wall.gameObject.transform.position = new Vector3(60f, 0f, 12.1615f);
        m_right_wall.gameObject.transform.position = new Vector3(2f, 0f, 12.1615f);
        m_game_over = false;
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
        if (m_game_over)
            return;

        if (m_ball.gameObject.transform.position.y < 0)
        {
            m_game_over = true;
            Debug.Log("Player Won");
            return;
        }

        if (m_left_wall.gameObject.transform.position.x - m_right_wall.gameObject.transform.position.x < 1)
        {
            m_game_over = true;
            Debug.Log("Player Lost!");
            return;
        }

        m_left_wall.gameObject.transform.position -= new Vector3(ShrinkSpeed * Time.deltaTime, 0f, 0f);
        m_right_wall.gameObject.transform.position += new Vector3(ShrinkSpeed * Time.deltaTime, 0f, 0f);
    }
}