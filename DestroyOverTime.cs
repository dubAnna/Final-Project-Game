/*Скрипт для додавання до об'єкта який автоматично знищується після певного часу.
Якщо доати цей скрипт до об'єкта, то після LifeTime секунд від початку гри цей об'єкт буде автоматично знищений
Update - викликається кожен кадр.
Destroy(gameObject, LifeTime) - поставити об'єкт на чергу на знищення через час, вказаний в LifeTime.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    public float LifeTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, LifeTime);
    }
}
