using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timer;

    public float lifeTime = 60f;
    private float gameTime;

    private void Awake()
    {
        gameTime = lifeTime;
    }

    private void Update()
    {
        timer.text = gameTime.ToString("F1") + " sec";
        gameTime -= Time.deltaTime;
        
        if (gameTime < 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;
            SceneManager.LoadScene("victory");         
        }

    }
}
