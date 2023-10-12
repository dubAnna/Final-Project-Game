/*

Цей код створює ворожого персонажа який рухається між двома точками і чекає протягом деякого часу перед повторним рухом(іказано у файлі FlyingEnemyController)
Важливі змінні:
public float moveSpeed - швидкість руху
public Transform leftPoint, rightPoint - точки між якими ворог буде рухається
private bool movingRight - вказує чи ворог рухається вправо
private Rigidbody2D theRB - керування фізикою ворога
public SpriteRenderer theSR - компонент для зміни напрямку ворога
public float moveTime - час руху ворога між точками
public float waitTime - час очікування перед наступним рухом
private float moveCount, waitCount - лічильники часу для руху та очікування
private Animator anim - компонент анімації ворога


*/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;
    public Transform leftPoint, rightPoint;

    private bool movingRight;
    private Rigidbody2D theRB;
    public SpriteRenderer theSR;

    public float moveTime, waitTime;
    private float moveCount, waitCount;

    private Animator anim;



    // Start is called before the first frame update
    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        leftPoint.parent = null;
        rightPoint.parent = null;

        movingRight = true;

        moveCount = moveTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveCount > 0)
        {
            moveCount -= Time.deltaTime;
            if (movingRight)
            {
                theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);
                theSR.flipX = true;

                if (transform.position.x > rightPoint.position.x)
                {
                    movingRight = false;
                }
            }
            else
            {
                theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);
                theSR.flipX = false;

                if (transform.position.x < leftPoint.position.x)
                {
                    movingRight = true;
                }
            }
            if(moveCount <= 0)
            {
                waitCount = Random.Range(waitTime * .75f, waitTime * 1.25f);

            }
            anim.SetBool("IsMoving", true);

        }else if(waitCount > 0)
        {
            waitCount -= Time.deltaTime;
            theRB.velocity = new Vector2(0f, theRB.velocity.y);

            if (waitCount <= 0)
            {
                moveCount = Random.Range(moveTime * .75f, waitTime * .75f);
            }
            anim.SetBool("IsMoving", false);
        }
        
    }
}
