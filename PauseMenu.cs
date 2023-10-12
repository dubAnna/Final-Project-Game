/*Скрипт для реалізації функції паузи в грі і допомагає обрати рівень або головне меню під час паузи.
Update() - викликається кожен кадр і відповідає за перевірку натискання клавіші "еsc" для активації або деактивації режиму паузи.
public void PauseOnpause() - викликається для ввімкнення або вимкнення режиму паузи:
-> Перевіряє, чи гра вже перебуває в режимі паузи (isPaused).
   --> якщо гра не в паузі, то вмикає режим паузи:
      ---> встановлює isPaused(true)
      ---> активує pauseScreen - відображає вікно паузи
      ---> зупиняє час гри - Time.timeScale = 0f
-> Якщо гра вже в паузі, то вимикає режим паузи:
   --> встановлює isPaused(false)
   --> деактивує pauseScreen.
   --> Повертає час гри в нормальний режим - Time.timeScale = 1f.

public void LevelSelect() - викликається, коли гравець вибирає рівень для переходу під час паузи:
 -> зберігає назву поточного рівня у PlayerPrefs щоб потім коректно завантажити його
 -> завантажує рівень вказаний в полі levelSelect
 -> повертає час гри в нормальний режим - Time.timeScale = 1f

public void MainMenu() - викликається, коли гравець вибирає головне меню гри під час паузи:
 -> завантажує головне меню, вказане в полі mainMenu.
 -> повертає час гри в нормальний режим - Time.timeScale = 1f.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public static Pause instance;
    public string levelSelect,mainMenu;
    public GameObject pauseScreen;
    public bool isPaused;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseOnpause();
        }
    }

    public void PauseOnpause()
    {
        if (isPaused)
        {
            isPaused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1f;
        }else
        {
            isPaused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0f;
        }

    }

    public void LevelSelect(){

        PlayerPrefs.SetString("CurrentLevel", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(levelSelect);
        Time.timeScale = 1f;
    }

    public void MainMenu(){

        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;
    }
}
