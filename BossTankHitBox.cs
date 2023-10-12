/*Скрипт дозволяє гравцю завдати урон босу атакуючи його зверху

OnTriggerEnter2D(Collider2D other) - метод викликається, коли гравець доторкнувся до області вразливості боса.
-> якщо other має тег "Player" і позиція гравця по осі Y більше, ніж позиція цієї області вразливості, то це означає, що гравець здійснив атаку зверху
У цьому випадку викликається метод TakeHit() з компонента bossCont і область вразливості вимикається встановлюючи активність у false.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankHitbox : MonoBehaviour
{
    public BossTankController bossCont;
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
        if(other.tag == "Player" && PlayerController.instance.transform.position.y > transform.position.y)
        {
            bossCont.TakeHit();
            gameObject.SetActive(false);
        }
    }
}
