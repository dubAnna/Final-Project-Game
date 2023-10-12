/*Скрипт для керування інтерфейсом гри в Unity. 
Даний код допомагає керувати інтерфейсом гри, відображати інформацію про життя і зібрані ресурси, керувати анімацією затемнення та розтемнення екрану.

Головні методи:
Start() - викликається на початку гри та викликає UpdateGemCount() та FadeFromBlack() для анімації початку гри.
Update() - оновлює затемнення/розтемнення екрану, якщо це необхідно.
UpdateHealthDisplay() - оновлює відображення життя гравця в інтерфейсі залежно від поточного здоров`я.
UpdateGemCount() - оновлює відображення кількості зібраних алмазів в інтерфейсі.
FadeToBlack() та FadeFromBlack() - затемнення та розтемнення екрану під час початку гри, переходження до наступного рівня а також під час смерті гравця.*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public Image heart1, heart2, heart3;
    public Sprite heartFull, heartEmpty, heartHalf;

    public Text gemText;

    public Image fadeScreen;
    public float fadeSpeed;
    private bool shouldFadeToBlack, shouldFadeFromBlack;

    public GameObject levelCompleteText;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateGemCount();
        FadeFromBlack();
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldFadeToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if(fadeScreen.color.a == 1f)
            {
                shouldFadeToBlack = false;
            }
        }

        if (shouldFadeFromBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if(fadeScreen.color.a == 0f)
            {
                shouldFadeFromBlack = false;
            }
        }
        // Add any necessary update logic here
    }

    public void UpdateHealthDisplay()
    {
        switch (PlayerHealthController.instance.currentHealth)
        {
            case 6:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartFull;
                break;

            case 5:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartHalf;
                break;

            case 4:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartEmpty;
                break;

            case 3:
                heart1.sprite = heartFull;
                heart2.sprite = heartHalf;
                heart3.sprite = heartEmpty;
                break;

            case 2:
                heart1.sprite = heartFull;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                break;

            case 1:
                heart1.sprite = heartHalf;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                break;

            case 0:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                break;
        }
    }

    public void UpdateGemCount()
    {

        gemText.text = LevelManager.instance.gemsCollected.ToString();
    }

    public void FadeToBlack()
    {
        shouldFadeToBlack = true;
        shouldFadeFromBlack = false;
    }
    public void FadeFromBlack()
    {
        shouldFadeFromBlack = true;
        shouldFadeToBlack = false;
    }
}
