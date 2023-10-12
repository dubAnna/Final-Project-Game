/*скрипт для обробки ситуації коли гравець зіштовхується з об'єктом який викликає моментальну смерть гравця

private void OnTriggerEnter2D(Collider2D other) викликається, коли інший об'єкт входить у колайдер поточного об'єкта:
-перевіряє чи має об'єкт тег "Player"
--> якщо так -  викликає метод RespawnPlayer() з LevelManager.instance
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
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
            LevelManager.instance.RespawnPlayer();
        }
    }
}
