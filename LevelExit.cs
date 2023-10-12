/*Скрипт дозволяє завершувати рівень коли гравець досягає виходу.Коли гравець потрапляє у тригер виходу, гра викликає метод EndLevel() з LevelManager

Основні методи: 
OnTriggerEnter2D(Collider2D other) метод викликається, коли гравець зіштовхується з виходом з рівня. 
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
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
        if(other.tag == "Player")
        {

            LevelManager.instance.EndLevel();
        }
    }
}
