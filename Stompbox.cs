/*Скрипт для реалізації знищення ворога завдяки встановленому у unity колайдеу із заданим тригером. 
Основні методи:
OnTriggerEnter2D(Collider2D other) - викликається, коли інший об'єкт з тригер-колайдером (наприклад ворог) зіштовхується з цим Stompbox:
Перевіряє, чи це зіштовхнення відбулося з об'єктом "Enemy".
Встановлює батьківський об'єкт ворога в неактивний стан.
Генерує ефект смерті deathEffect у позиції ворога
Генерує випадковий числовий вибір dropSelect в межах від 0 до 100 для випадення з ворога ресурсів.
Порівнює dropSelect з шнсом випадіння chanceToDrop і, якщо dropSelect менше або дорівнює ймовірності випадіння, 
генерує предмет collectible у позиції ворога.
Відтворює звуковий ефект смерті (SFX) за допомогою AudioManager.instance.PlaySFX(3).
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stompbox : MonoBehaviour
{

    public GameObject deathEffect;

    public GameObject collectible;
    [Range(0,100)]public float chanceToDrop;
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
        if (other.tag == "Enemy")
        {
            Debug.Log("Hit Enemy");

            other.transform.parent.gameObject.SetActive(false);

            Instantiate(deathEffect, other.transform.position, other.transform.rotation);

            float dropSelect = Random.Range(0, 100f);

            if (dropSelect <=chanceToDrop)
            {
                Instantiate(collectible, other.transform.position, other.transform.rotation);
            }

            AudioManager.instance.PlaySFX(3);
        }
    }
}
