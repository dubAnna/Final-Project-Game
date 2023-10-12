/* Скрпт дозволяє літаючому ворогу переміщатись муж заданими point1 та point2 вказаними у unity за вказаний час,
а також атакувати гравця коли він знаходиться у доступному для ворога діапазні. Після атаки гравця, чекає заданий час для подальшгопереслідування та атаки.

Основні методи:
Start - ініціалізує початкові значення точок маршруту
Update - визначає логіку руху та атаки ворога в залежності від розташування гравця:
- перевіряє чи минув час очікування після попередньої атаки
- визначає чи ворог знаходиться на визначеній відстані від гравця для атаки
--> якщо ворог вдається атакувати гравця то визначає позицію гравця як ціль для атаки та рухається до неї.
--> в іншому випадку ворог рухається між точками на маршруті.

*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyController : MonoBehaviour
{
    public Transform[] points;
    public float moveSpeed;
    private int currentPoint;

    public SpriteRenderer theSR;

    public float distanceToAttackPlayer, chaseSpeed;

    private Vector3 attackTarget;

    public float waitAfterAttack;
    private float attackCounter;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < points.Length; i++)
        {
            points[i].parent = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (attackCounter > 0)
        {
            attackCounter -= Time.deltaTime;
        }
        else
        {
            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) > distanceToAttackPlayer)
            {
                attackTarget = Vector3.zero;

                transform.position = Vector3.MoveTowards(transform.position, points[currentPoint].position, moveSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, points[currentPoint].position) < 0.5f)
                {
                    currentPoint++;
                    if (currentPoint >= points.Length)
                    {
                        currentPoint = 0;
                    }
                }

                if (transform.position.x < points[currentPoint].position.x)
                {
                    theSR.flipX = true;
                }
                else if (transform.position.x > points[currentPoint].position.x)
                {
                    theSR.flipX = false;
                }
            }
            else
            {
                // Attacking the player
                if (attackTarget == Vector3.zero)
                {
                    attackTarget = PlayerController.instance.transform.position;
                }
                transform.position = Vector3.MoveTowards(transform.position, attackTarget, chaseSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, attackTarget) <= 0.1f)
                {
                    attackCounter = waitAfterAttack;
                    attackTarget = Vector3.zero;
                }
            }
        }
    }
}
