/*Скрипт для перемикача + взаємодія гравця з ним.

Основні методи:
Start() - ініціалізує змінну theSR, яка відповідає за компонент SpriteRenderer цього об'єкта на початку гри.
OnTriggerEnter2D(Collider2D other) - викликається коли гравець зіштовхується з перемикачем:
В залежності від значення deactivateOnSwitch активує або деактивує змінн objectToSwitch, тоді інший об'єкт в грі.
Змінює спрайт перемикача на downSprite.
Встановлює hasSwitched в true, щоб запобігти наступної активації перемикача.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject objectToSwitch;
    private SpriteRenderer theSR;
    public Sprite downSprite;

    public bool deactivateOnSwitch;

    private bool hasSwitched; 
    // Start is called before the first frame update
    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !hasSwitched)
        {

            if(deactivateOnSwitch)
            {
                objectToSwitch.SetActive(false);
            }else
            {
                objectToSwitch.SetActive(true);
            }
            theSR.sprite = downSprite;
            hasSwitched = true;
        }
    }
}
