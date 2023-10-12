/*Скрипт менеджера рівня гри дозволяє керувати респауном гравця після смерті та завершенням рівнів, 
а також зберігати статистику рівнів, таку як зібрані алмази та час проходження рівнів.

Start() - встановлює час рівня timeInLevel на початку гри
Update() - оновлює час рівня timeInLevel кожен кадр
RespawnPlayer() - викликається для респауну гравця після смерті
RespawnCo() відновлює стан гравця після смерті та затемнює екран та показує гравця на рівні
EndLevel() - викликається для завершення рівня
EndLevelCo() - виконує завершення рівня:
- відтворює звук перемоги та блокує  слідування камери за гравцемм
- відображає текст про завершення рівня
- затемнює екран, а потім  плавно затемнює та відновлює його
- встановлює прапорець для рівня, який вказує на його розблокування на основній  мапі (PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_unlocked", 1)).
- зберігає кількість зібраних алмазів та час проходження рівня у PlayerPrefs
- завантажує наступний рівень levelToLoad
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;

    public float waitToRespawn;

    public int gemsCollected;

    public string levelToLoad;

    public float timeInLevel;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        timeInLevel = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timeInLevel += Time.deltaTime;
    }
    public void RespawnPlayer()
    {
        StartCoroutine(RespawnCo());
    }
    private IEnumerator RespawnCo(){

        PlayerController.instance.gameObject.SetActive(false);

        AudioManager.instance.PlaySFX(8);



        yield return new WaitForSeconds(waitToRespawn - (1f / UIController.instance.fadeSpeed));
        UIController.instance.FadeToBlack();

         yield return new WaitForSeconds((1f / UIController.instance.fadeSpeed)+ .2f);
         UIController.instance.FadeFromBlack();

        PlayerController.instance.gameObject.SetActive(true);

        PlayerController.instance.transform.position = CheckPointController.instance.spawnPoint;

        PlayerHealthController.instance.currentHealth = PlayerHealthController.instance.maxHealth;
        UIController.instance.UpdateHealthDisplay();
    }

    public void EndLevel()
    {
        StartCoroutine(EndLevelCo());
    }

    public IEnumerator EndLevelCo() 
    {
        AudioManager.instance.PlayLevelVictory();
        PlayerController.instance.stopInput= true;
        NewBehaviourScript.instance.stopFollow = true;
        UIController.instance.levelCompleteText.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        UIController.instance.FadeToBlack();
        yield return new WaitForSeconds((1f/UIController.instance.fadeSpeed) + 3f);
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_unlocked", 1);
        PlayerPrefs.SetString("CurrentLevel", SceneManager.GetActiveScene().name);

        if(PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_gems"))
        {
            if(gemsCollected > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_gems"))
            {
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_gems", gemsCollected);
            }
        } else
        {
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_gems", gemsCollected);
        }

        if(PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_time"))
        {
           if(timeInLevel < PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + "_time"))
           {
                PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_time", timeInLevel);
           }
        }else
        {
        PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_time", timeInLevel);
        }

        SceneManager.LoadScene(levelToLoad);
    }
    
}
