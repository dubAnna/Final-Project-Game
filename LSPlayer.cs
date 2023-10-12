/*Скрипт для керування гравцем на карті рівня в грі

Основні методи:
Update() - викликається кожен кадр і відповідає за рух гравця на карті:
- Vector3.MoveTowards() щоб переміщати гравця до поточної точки currentPoint
- перевіряє чи гравець досягнув поточної точки, і визначає залежно від введених даних гравцем можливість переходу до інших точок на карті 
- перевіряє чи поточна точка є рівнем гри і якщо так -> відображає інформацію про рівень за допомогою LSUIController, ініціює завантаження рівня, якщо гравець натиснув кнопку "Jump".

SetNextPoint(MapPoint nextPoint) встановлює наступну точку для гравця і приховує інформацію про рівень- LSUIController і відтворює зукоий ефект.
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSPlayer : MonoBehaviour
{
    public MapPoint currentPoint;

    public float moveSpeed = 10f;
    private bool levelLoading;

    public LSManager theManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, currentPoint.transform.position, moveSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position, currentPoint.transform.position) < .1f && !levelLoading)
        {

            if(Input.GetAxisRaw("Horizontal") > .5f)
            {
                if(currentPoint.right != null)
                {
                    SetNextPoint(currentPoint.right);
                }
            }

            if(Input.GetAxisRaw("Horizontal") < -.5f)
            {
                if(currentPoint.left != null)
                {
                    SetNextPoint(currentPoint.left);
                }
            }

            if(Input.GetAxisRaw("Vertical") > .5f)
            {
                if(currentPoint.up != null)
                {
                    SetNextPoint(currentPoint.up);
                }
            }

            if(Input.GetAxisRaw("Vertical") < -.5f)
            {
                if(currentPoint.down != null)
                {
                    SetNextPoint(currentPoint.down);
                }
            }

            if(currentPoint.isLevel && currentPoint.levelToLoad != "" && !currentPoint.isLocked)
            {
                LSUIController.instance.ShowInfo(currentPoint);
                if(Input.GetButtonDown("Jump"))
                {
                    levelLoading = true;
                    theManager.LoadLevel();
                }
            }
        }

    }

    public void SetNextPoint(MapPoint nextPoint)
    {
        currentPoint = nextPoint;
        LSUIController.instance.HideInfo();
        AudioManager.instance.PlaySFX(5);
    }
}
