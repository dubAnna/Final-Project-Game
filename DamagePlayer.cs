/*Скрипт дозволяє реалізувати можливість завдавати гравцю урон при зіткненні з певними об'єктами
Start та Update - ці методи не використовуються в цьому скрипті.

OnTriggerEnter2D(Collider2D other) викликається, коли інший об'єкт входить у зону колізії з об'єктом, до якого доданий цей скрипт: 
-> перевіряється чи об'єкт який зіткнувся з головним об'єктом має тег "Player"
--> якщо так, це означає зіткнення з гравцем.
PlayerHealthController.instance.DealDamage(); - викликається метод DealDamage() у класі PlayerHealthController. 
відповідає за зменшення життя гравця, і в результаті, при зіткненні з цим об'єктом, гравець отримує урон.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
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
        if (other.tag == "Player")
        {
            //Debug.Log("Hit");
            //FindObjectOfType<PlayerHealthController>().DealDamage();
            PlayerHealthController.instance.DealDamage();
        }

    }
}
