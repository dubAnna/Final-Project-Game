/*скрипт для обробки збору ресурсів гравцем. Цей код дозволяє збирати алмази та збільшує їх кількість а також додає можливість лікуватись.
OnTriggerEnter2D(Collider2D other) - викликається, коли гравець зіштовхується з ресурсом:
->перевіряє чи зіштовхнувся гравець з підбором і чи підбір ще не був зібраний !isCollected.
  -->Якщо підібрано алмаз
     --->Збільшує кількість зібраних гемів - LevelManager.instance.gemsCollected++.
     --->Встановлює isCollected (true).
     --->Знищує алмаз.
     --->Генерує pickupEffect в позиції в якій бул підібрано алмаз.
     --->Оновлює відображення кількості зібраних гемів на інтерфейсі - UIController.instance.UpdateGemCount().
     --->Відтворює звуковий ефект (SFX) - AudioManager.instance.PlaySFX(6).
     
  -->Якщо підбір - це можливість лікуватися:
     --->Перевіряє чи здоров'я гравця не дорівнює максимальному здоров'ю.
         ----> Якщо так - викликає метод HealPlayer() з класу PlayerHealthController, щоб підвищити здоров'я гравця.
         ---->Встановлює  isCollected (true).
         ---->Знищує об'єкт.
         ---->Генерує ефект підбору (pickupEffect) в позиції підібраного об'єкту.
         ---->Відтворює звуковий ефект (SFX) - AudioManager.instance.PlaySFX(7).
     --->Якщо ні - нічого не статься
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public bool isGem, isHeal;
    private bool isCollected;
    public GameObject pickupEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !isCollected)
        {
            if(isGem)
            {
                LevelManager.instance.gemsCollected++;

                isCollected = true;
                Destroy(gameObject);

                Instantiate (pickupEffect, transform.position, transform.rotation);

                UIController.instance.UpdateGemCount();

                AudioManager.instance.PlaySFX(6);
            }

            if(isHeal)
            {
                if(PlayerHealthController.instance.currentHealth != PlayerHealthController.instance.maxHealth)
                {
                    PlayerHealthController.instance.HealPlayer();
                    isCollected = true;
                    Destroy(gameObject);
                    Instantiate (pickupEffect, transform.position, transform.rotation);

                    AudioManager.instance.PlaySFX(7);
                }
            }
        }
    }
}
