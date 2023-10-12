/*Скрипт дозволяє гравцю зберігати свою позицію та встановлювати новий пункт спавну після проходження контрольної точки

OnTriggerEnter2D(Collider2D other) - викликається, коли об'єкт(гравець) зіштовхується із чекпоінтом:
-->Якщо цей об'єкт має тег "Player" ---> контрольна точка активується. 
*контрольна точка може бути використана лише раз, коли гравець досягне її

CheckPointController.instance.DeactivateCheckpoints(); - викликає метод DeactivateCheckpoints() у класі CheckPointController.
*деактивує всі інші контрольні точки інформуючи систему що новий пункт спавну встановлений.

theSR.sprite = cpOn; -змінює спрайт контрольної точки на cpOn вказуючи що новий чекпоінт активовано

CheckPointController.instance.SetSpawnPoint(transform.position); - викликає метод SetSpawnPoint(Vector3 spawnPos) у класі CheckPointController. 
*метод встановлює новий пункт спавну на позиції контрольної точки коли гравець її активує.

public void ResetCheckpoint() - використовується щоб скинути контрольну точку у виключений стан(коли гравець повертається до попереднього чекпоінту)

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public SpriteRenderer theSR;
    public Sprite cpOn, cpOff;
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
        if(other.CompareTag("Player"))
        {
            CheckPointController.instance.DeactivateCheckpoints();

            theSR.sprite = cpOn;

            CheckPointController.instance.SetSpawnPoint(transform.position);
        }

    }
    public void ResetCheckpoint()
    {
        theSR.sprite = cpOff;
    }
}
