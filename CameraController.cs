/*Скрипт відповідає за камеру та слідування за головним героєм гри
Основні методи:
Awake() встановлює статичний екземпляр instance на цей об'єкт щоб інші скрипти могли отримувати доступ до контролера.

Start() при запуску гри цей метод ініціалізує початкові значення, включаючи позицію камери та останню відому позицію героя.
Update() слідкує за героєм та переміщає камеру вгору або вниз так, щоб герой завжди був видимим на екрані.
*переміщує фони гри відповідно до руху камери

** при встановленні stopFollow(true), камера перестане слідкувати за гравцем
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public static NewBehaviourScript instance;
    public Transform target;
    public Transform farBackground, middleBackground;
    public float minHeight, maxHeight;

    public bool stopFollow;
    
   // private float lastXPos;
    private Vector2 lastPos;
    
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //lastXPos = transform.position.x;
        lastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if( !stopFollow)
        {
            transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
            float clampedY = Mathf.Clamp(transform.position.y, minHeight, maxHeight);
            
            transform.position = new Vector3(transform.position.x, clampedY, transform.position.z); 
         //  transform.position = new Vector3(target.position.x, Mathf.Clamp(target.position.y, minHeight, maxHeight),transform.position.z);

            //float amountToMoveX = transform.position.x - lastXPos;

            Vector2 amountToMove = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y);

            farBackground.position = farBackground.position + new Vector3(amountToMove.x, amountToMove.y, 0f);
            middleBackground.position += new Vector3(amountToMove.x, amountToMove.y, 0f) *.5f;

            //lastXPos = transform.position.x;
            lastPos = transform.position;
        }
    }
}
