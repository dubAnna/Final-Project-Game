/*Скрипт відповідає за рух та поведінку куль боса і за їх взаємодію з гравцем
Основні методи:

Start() - відтворюється звуковий ефект (SFX) кулі боса за допомогою AudioManager.
Update() - у цьому методі куля рухається вліво в напрямку, визначеному значенням speed.
*положення кулі оновлюється шляхом зменшення її transform.position.x з урахуванням швидкості та напрямку, це дозволяє змінювати рух у протилежну сторону

OnTriggerEnter2D(Collider2D other) викликається при зіткненні кулі з іншим об'єктом.
*у випадку, якщо куля зіткнеться з об'єктом, який має тег "Player" то викликається метод DealDamage() компонента PlayerHealthController.instance, щоб завдати урон гравцю.
+ відтворюється звуковий ефект за допомогою AudioManager.
+  кінці методу куля знищується за допомогою Destroy(gameObject).
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlaySFX(2);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(-speed * transform.localScale.x * Time.deltaTime, 0f, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag =="Player")
        {
            PlayerHealthController.instance.DealDamage();
        }
        AudioManager.instance.PlaySFX(1);
        Destroy(gameObject);
    }
}
