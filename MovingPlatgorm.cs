/*Cкрипт для реалізації платформи, яка пересувається між заданими точками з заданою швидкістю

Основні методи:
Update() - викликається кожен кадр і відповідає за рух платформи:
- Vector3.MoveTowards для плавного руху платформи в напрямку поточної точки (points[currentPoint])
- перевіряє відстань між платформою і поточною точкою - Vector3.Distance.
--> відстань менше 0.5 одиниць 
    ---> оновлює індекс поточної точки currentPoint, перемикаючись на наступну точку
--> currentPoint стає більше або дорівнює кількості точок у масиві points.Length
    ---> відновлює лічильник індексу, починаючи з першої точки
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatgorm : MonoBehaviour
{
    public Transform[] points;
    public float moveSpeed;
    public int currentPoint;

    public Transform platform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        platform.position = Vector3.MoveTowards(platform.position, points[currentPoint].position, moveSpeed *Time.deltaTime);

        if(Vector3.Distance(platform.position, points[currentPoint].position) < 0.5f )
        {
            currentPoint++;
            if(currentPoint >= points.Length)
            {
                currentPoint = 0;
            }
        }
    }
}
