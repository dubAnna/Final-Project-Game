/*Скрипт для керування камерою в грі на карті рівня

LateUpdate() викликається кожен кадр після оновлення позиції всіх інших об'єктів у грі 
та бчислює нову позицію камери (xPos та yPos) на основі позиції target, тобто гравця, та меж положення камери.

виикористання Mathf.Clamp() для обмеження позиції камери в межах minPos та maxPos
встановлення нової позиції камери з урахуванням обмежень transform.position = new Vector3(xPos, yPos, transform.position.z).
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSCameraController : MonoBehaviour
{

    public Vector2 minPos, maxPos;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float xPos = Mathf.Clamp(target.position.x, minPos.x, maxPos.x);
        float yPos = Mathf.Clamp(target.position.y, minPos.y, maxPos.y);

        transform.position = new Vector3(xPos, yPos, transform.position.z);
    }
}
