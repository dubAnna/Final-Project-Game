/*Скрипт для управління об'єктами на мапі рівнів гри та містить інформацію про кожний рівень
Start() - викликається при запуску гри та встановлює параметри кожної точки на мапі:

-Перевіряє levelToLoad
-зберігає значення зібраних алмазів і найкращий час для цього рівня - PlayerPrefs.
-відображає значки гемів та часовий значок на мапі
-блокує рівень якщо не виконані умови для його розблокування (якщо не пройдено levelToChecк).
-загальний вигляд персонажа на мапі відповідає управлінню його об'єктом
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPoint : MonoBehaviour
{

    public MapPoint up,right, down, left;
    public bool isLevel, isLocked;
    public string levelToLoad, levelToCheck, levelName;

    public int gemsCollected, totalGems;
    public float bestTime, targetTime;

    public GameObject gemBadge, timeBadge;
    // Start is called before the first frame update
    void Start()
    {
        if(isLevel && levelToLoad != null)
        {

            if(PlayerPrefs.HasKey(levelToLoad + "_gems"))
                {
                    gemsCollected = PlayerPrefs.GetInt(levelToLoad + "_gems");
                }
            
            if(PlayerPrefs.HasKey(levelToLoad + "_time"))
            {
                bestTime = PlayerPrefs.GetFloat(levelToLoad + "_time");
            }
            
            if(gemsCollected >= totalGems)
            {
                gemBadge.SetActive(true);
            }

            if(bestTime <= targetTime && bestTime != 0 )
            {
                timeBadge.SetActive(true);
            }
            isLocked = true;

            if(levelToCheck != null)
            {
                if(PlayerPrefs.HasKey(levelToCheck + "_unlocked"))
                {
                    if(PlayerPrefs.GetInt(levelToCheck + "_unlocked") == 1)
                    {
                        isLocked = false;
                    }
                }
            }
            if(levelToLoad == levelToCheck)
            {
                isLocked = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
