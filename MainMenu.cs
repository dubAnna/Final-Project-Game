/*Скрипт головного меню гри. Цей код дозволяє гравцю розпочати нову гру, продовжити або вийти з гри.
Start() - викликається при запуску гри та встановлює видимість кнопки "Продовжити гру" залежно від наявності збереженого прогресу:
-перевіряє startScene + "_unlocked"
-відображає кнопку "Continue" , якщо збережений прогрес існує, або приховує її.

StartGame() - виконується коли гравець натискає кнопку "New game":

- завантажує головний рівень гри вказаний в startScene в unity
- скидає всі збережені дані гри - PlayerPrefs.DeleteAll().

ContinueGame() - викликається, коли гравець натискає кнопку "Continue":
- завантажує рівень, який був збережений як продовження гри вказаний в continueScene в юніті.

QuitGame() - викликається, коли гравець натискає кнопку "Quit game":
- завершує гру - Application.Quit().
*/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class MainMenu : MonoBehaviour
{

    public string startScene, contiueScene;

    public GameObject continueButton;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey(startScene + "_unlocked"))
        {
            continueButton.SetActive(true);
        }else
        {
            continueButton.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(startScene);

        PlayerPrefs.DeleteAll();
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(contiueScene);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting Game");
    }
}
