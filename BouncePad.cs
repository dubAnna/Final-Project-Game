/*Скрипт прдставляє пластину Bounce Pad яка підкидає гравця вгору, коли той до неї доторкається

Основні методи:
Start() ініціалізує посилання на компонент анімації (anim)
OnTriggerEnter2D(Collider2D other) -  метод викликається коли гравець торкається до пластини
-якщо other має тег "Player", то він отримає високий підскок по у.
*використовується для зміни швидкості гравця та відтворення анімації для подачі ефекту підскоку
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    private Animator anim;
    public float bounceForce = 20f;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            PlayerController.instance.theRB.velocity = new Vector2(PlayerController.instance.theRB.velocity.x, bounceForce);
            anim.SetTrigger("Bounce");
        }
    }
}
