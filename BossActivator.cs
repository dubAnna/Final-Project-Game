/*Скрипт відповідає за активацію битви з босом як гравець зайде  область заданого об'єкта що активує theBossBattle(боса та його властивості)
ОСновний метод:
OnTriggerEnter2D(Collider2D other) викликається, коли гравець входить в область колайдера цього об'єкта:
- перевіряється чи об'єкт який зіткнувся має тег "Player"
 -->якщо так, то активується графічний об'єкт theBossBattle
* цей об'єкт "BossActivator" вимикається, щоб уникнути подальших зіткнень з гравцем
- викликається метод PlayBossMusic() з AudioManager для відтворення музики битви з босом
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivator : MonoBehaviour
{
    public GameObject theBossBattle;
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
        if(other.tag == "Player")
        {
            theBossBattle.SetActive(true);
            gameObject.SetActive(false);
            AudioManager.instance.PlayBossMusic();
        }
    }
}
