/*Скрипт для управління рівнями на карті рівня в грі дозволяє гравцю переміщатися між рівнями на карті та взаємодіяти з рівнями для їх завантаження

Start() - призначений для пошуку усіх точок на карті рівня та встановлення поточної позиції гравця на карті:
- знаходить усі точки на карті рівня за допомогою FindObjectsOfType<MapPoint>().
- перевіряє, чи є збережена поточна позиція рівня в пам'яті (PlayerPrefs.HasKey("CurrentLevel"))  якщо так ---> встановлює гравця на цю позицію на карті.

LoadLevel() - ініціює завантаження нового рівня

LoadLevelCo() - ініціює завантаження нового рівня з анімацією затемнення та звуковим супроводом:
- звуковий ефект переходу на новий рівень - AudioManager.instance.PlaySFX(4).
- викликає метод FadeToBlack() з LSUIController для затемнення екрану
- певний очікування час для завантаження нового рівня - SceneManager.LoadScene(thePlayer.currentPoint.levelToLoad).
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LSManager : MonoBehaviour
{
    public LSPlayer thePlayer;

    private MapPoint[] allPoints;
    // Start is called before the first frame update
    void Start()
    {
        allPoints = FindObjectsOfType<MapPoint>();
        if(PlayerPrefs.HasKey("CurrentLevel"))
        {
            foreach (MapPoint point in allPoints)
            {
                if(point.levelToLoad == PlayerPrefs.GetString("CurrentLevel"))
                {
                    thePlayer.transform.position = point.transform.position;
                    thePlayer.currentPoint = point;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel()
    {
        StartCoroutine(LoadLevelCo());
    }

    public IEnumerator LoadLevelCo()
    {
        AudioManager.instance.PlaySFX(4);
        LSUIController.instance.FadeToBlack();
        yield return new WaitForSeconds((1f / LSUIController.instance.fadeSpeed) * .25f);
        SceneManager.LoadScene(thePlayer.currentPoint.levelToLoad);
    }
}
