/*Скрипт керує босом, включаючи рух, стрільбу, ушкодження та реакції на дії гравця

Основні методи:
Update()- у методі використовується оператор switch-case, щоб визначити дії для кожного стану (стрільба, ушкодження, рух)
*для кожного стану використовуються окремі умови та логіка.
TakeHit() - викликається, коли боса  вражають або атакують:
- змінює стан боса currentState = bossStates.hurt
- запускає анімацію ушкодження - anim.SetTrigger("Hit")
- відтворює звук ушкодження за допомогою AudioManager.
- зменшує кількість здоров'я боса (health--).
*--> якщо кількість здоров'я боса стає <= 0, то бос isDefeated(поражений)
*--> якщо бос не поражений, то зменшуються інтервали між пострілами та створеннями мін, щоб зробити битву з кожним ударом більш складною.

EndMovement() викликається, коли бос танка закінчив рух:
- змінює стан боса на "стрільба" - (currentState = bossStates.shooting).
- запускає анімацію зупинення руху - (anim.SetTrigger("StopMoving")).
- вмикає область ушкодження hitBox
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankController : MonoBehaviour
{
    public enum bossStates { shooting, hurt, moving, ended };
    public bossStates currentState;

    public Transform theBoss;
    public Animator anim;

    [Header("Movement")]
    public float moveSpeed;
    public Transform leftPoint, rightPoint;
    private bool moveRight;
    public GameObject mine;
    public Transform minePoint;
    public float timeBetweenMines;
    private float mineCounter;

    [Header("Shooting")]
    public GameObject bullet;
    public Transform firePoint;
    public float timeBetweenShots;
    private float shotCounter;

    [Header("Hurt")]
    public float hurtTime;
    private float hurtCounter;
    public GameObject hitBox;

    [Header("Health")]
    public int health = 5;
    public GameObject explosion, winPlatform;
    private bool isDefeated;
    public float shotSpeedUp, mineSpedUp; 

    void Start()
    {
        currentState = bossStates.shooting;
    }

    void Update()
    {
        switch (currentState)
        {
            case bossStates.shooting:
                shotCounter -= Time.deltaTime;
                if (shotCounter <= 0)
                {
                    shotCounter = timeBetweenShots;
                    var newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
                    newBullet.transform.localScale = theBoss.localScale;
                }
                break;

            case bossStates.hurt:
                if (hurtCounter > 0)
                {
                    hurtCounter -= Time.deltaTime;
                    if (hurtCounter <= 0)
                    {
                        currentState = bossStates.moving;
                        mineCounter = 0;
                        if (isDefeated)
                        {
                            theBoss.gameObject.SetActive(false);

                            Instantiate(explosion, theBoss.position, theBoss.rotation);

                            winPlatform.SetActive(true);

                            AudioManager.instance.StopBossMusic();

                            currentState = bossStates.ended;
                        }
                    }
                }
                break;

            case bossStates.moving:
                if (moveRight)
                {
                    theBoss.position += new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
                    if (theBoss.position.x > rightPoint.position.x)
                    {
                        theBoss.localScale = new Vector3(1f, 1f, 1f);
                        moveRight = false;
                        EndMovement();
                    }
                }
                else
                {
                    theBoss.position -= new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
                    if (theBoss.position.x < leftPoint.position.x)
                    {
                        theBoss.localScale = new Vector3(-1f, 1f, 1f);
                        moveRight = true;
                        EndMovement();
                    }
                }
                mineCounter -= Time.deltaTime;
                if (mineCounter <= 0)
                {
                    mineCounter = timeBetweenMines;
                    Instantiate(mine, minePoint.position, minePoint.rotation);
                }
                break;
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeHit();
        }
    }

    public void TakeHit()
    {
        currentState = bossStates.hurt;
        hurtCounter = hurtTime;
        anim.SetTrigger("Hit");
        AudioManager.instance.PlaySFX(0);

        BossTankMine[] mines = FindObjectsOfType<BossTankMine>();
        if (mines.Length > 0)
        {
            foreach (BossTankMine foundMine in mines)
            {
                foundMine.Explode();
            }
        }
        health--;

        if (health <= 0)
        {
            isDefeated = true;

        }else 
        {
            timeBetweenShots /= shotSpeedUp;
            timeBetweenMines /= mineSpedUp;
        }
    }

    private void EndMovement()
    {
        currentState = bossStates.shooting;
        shotCounter = timeBetweenShots;
        anim.SetTrigger("StopMoving");
        hitBox.SetActive(true);
    }
}
