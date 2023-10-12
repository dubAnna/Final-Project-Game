/*Скрипт реалізує взаємодію з гравцем, коли той наближається до міни

Основні методи:

OnTriggerEnter2D(Collider2D other) -  метод викликається коли гравець доторкнувся до міни. 
--> якщо other має тег "Player", то міна вибухне, знищуючи себе (Destroy(gameObject)).
---> після цього викликається метод DealDamage() з компонента PlayerHealthController.instance, щоб завдати урон гравцю

Explode() - метод знищує міну, створює об'єкт вибуху та відтворює звук вибуху.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankMine : MonoBehaviour
{
    public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){

            Destroy(gameObject);
            Instantiate(explosion, transform.position, transform.rotation);

            PlayerHealthController.instance.DealDamage();

            AudioManager.instance.PlaySFX(3);
        }
    }
    public void Explode()
    {
        Destroy(gameObject);
        AudioManager.instance.PlaySFX(3);
        Instantiate(explosion, transform.position, transform.rotation);
        
    }
}
