using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : Singleton<GameManager>
{
    public bool cursorActive = true;

    [SerializeField]
    public float ShrinkSpeed = 0.2f;

    private GameObject m_left_wall;
    private GameObject m_right_wall;
    private GameObject m_ball;

    private bool m_game_over = false;
    private GameObject m_pause_panel;
    private GameObject m_resume_button;
    private GameObject m_main_menu_button;
    private Text m_title_label;

    private void Start()
    {
        m_left_wall = GameObject.Find("LeftWall");
        m_right_wall = GameObject.Find("RightWall");
        m_ball = GameObject.Find("Ball");
        m_pause_panel = GameObject.Find("PausePanel");
        m_resume_button = GameObject.Find("ResumeGameButton");
        m_main_menu_button = GameObject.Find("MainMenuButton");
        m_title_label = GameObject.Find("TitleLabel").GetComponent<Text>();

        m_resume_button.GetComponent<Button>().onClick.AddListener(OnResumeButtonClicked);
        m_main_menu_button.GetComponent<Button>().onClick.AddListener(OnMainMenuButtonClicked);

        m_pause_panel.SetActive(false);
        Debug.Log(m_pause_panel.activeSelf);
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

    private void OnResumeButtonClicked()
    {
        Time.timeScale = 1;
        EnableCursor(false);
        m_pause_panel.SetActive(false);
    }

    private void OnMainMenuButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ResetGame()
    {
        m_left_wall.gameObject.transform.position = new Vector3(60f, 0f, 12.1615f);
        m_right_wall.gameObject.transform.position = new Vector3(2f, 0f, 12.1615f);
        m_game_over = false;
        Time.timeScale = 1;
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
            PauseGame("You Won!", true);
            return;
        }

        if (m_left_wall.gameObject.transform.position.x - m_right_wall.gameObject.transform.position.x < 2)
        {
            m_game_over = true;
            Debug.Log("Player Lost!");
            PauseGame("You Lost!", true);
            return;
        }

        m_left_wall.gameObject.transform.position -= new Vector3(ShrinkSpeed * Time.deltaTime, 0f, 0f);
        m_right_wall.gameObject.transform.position += new Vector3(ShrinkSpeed * Time.deltaTime, 0f, 0f);
    }

    public void PauseGame(string title, bool gameOver)
    {
        if (m_pause_panel.activeSelf)
        {
            OnResumeButtonClicked();
            return;
        }

        if (gameOver)
        {
            m_resume_button.SetActive(false);
        }
        Time.timeScale = 0;
        m_pause_panel.SetActive(true);
        m_title_label.text = title;
        EnableCursor(true);
    }
}