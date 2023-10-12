/*Скрипт керує здоров'ям головного гравця.

Основні методи:
Start() -ініціалізує значення здоров'я гравця та компонент SpriteRenderer
Update() - якщо invincibleCounter більше 0, зменшує його і встановлює кольор рендерера згідно з ним.
DealDamage() - викликається для урону:
перевіряє, чи invincibleCounter менше або дорівнює 0
зменшує здоров'я гравця на 1.
->Якщо здоров'я гравця стає рівним або меншим за 0, гравець "помирає":
    -->Вимикає гравця
       Генерує ефект смерті в позиції гравця.
       Викликає метод RespawnPlayer() з класу LevelManager щоби заспавнити гравця знову
       
HealPlayer() - викликається для підвищення здоров'я гравця:
збільшує поточне здоров'я гравця на 1
перевіряє, чи поточне здоров'я не перевищує максимальне здоров'я.

OnCollisionEnter2D(Collision2D other) та OnCollisionExit2D(Collision2D other)  ці методи допомагають гравцям сприймати платформи як 
"приєднані" до них під час зіткнення та "від'єднані" від них при зміні розташування гравця на платформі і не прив'язують гравця до однієї платформи.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public int currentHealth, maxHealth;

    public float invincibleLength;
    public float invincibleCounter;

    private SpriteRenderer theSR;

    public GameObject deathEffect;

    private void Awake()
    {
        instance = this; 
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime;
        
            if(invincibleCounter <=0)
            {
                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, 1f);
            }

        }
    }

    public void DealDamage()
    {
        if (invincibleCounter <= 0)
        {

            currentHealth--;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                //gameObject.SetActive(false);

                Instantiate(deathEffect, transform.position, transform.rotation);

                LevelManager.instance.RespawnPlayer();
            }

            else 
            {
                invincibleCounter  = invincibleLength;
                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, .5f);
                PlayerController.instance.KnockBack();
                AudioManager.instance.PlaySFX(9);
            }

            UIController.instance.UpdateHealthDisplay(); 
        }
    }

    public void HealPlayer()
    {
        currentHealth++;
        if(currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
    UIController.instance.UpdateHealthDisplay();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Platform")
        {
            transform.parent = other.transform;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.tag == "Platform")
        {
            transform.parent = null;
        }
    }
}
