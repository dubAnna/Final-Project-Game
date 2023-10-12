/* скрипт для керування персонажем гравця в грі

Основні методи:
Start() - ініціалізує Animator та SpriteRenderer.
Update() - відповідає за рух гравця та взаємодію з ним. Цей метод обробляє рух гравця, стрибки, обробку knockback та анімації гравцяв залежності від його дій та стану.
public void KnockBack() - викликається, коли гравець отримує удар або шкоду та втсановлює лічильник "відштовхування", змінює рух гравця відповідно.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;
    public float moveSpeed;
    public Rigidbody2D theRB;
    public float jumpForce;
    public Transform groundCheckPoint;
    public LayerMask WhatIsGround;


    private bool isGrounded;
    private bool canDoubleJump;
    private Animator anim;
    private SpriteRenderer theSR;

    public float knockBackLength, knockBackForce;
    private float knockBackCounter;
    
    public bool stopInput;

    private void Awake()
    {
        instance = this;
    } 
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Pause.instance.isPaused && !stopInput)
        {

            if (knockBackCounter <= 0)
            {
                theRB.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"),theRB.velocity.y);

                isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, WhatIsGround);
                if (isGrounded )
                {
                    canDoubleJump = true;
                }
                if (Input.GetButtonUp("Jump"))
                {
                    if(isGrounded)
                    {
                        theRB.velocity = new Vector2 (theRB.velocity.x,jumpForce);
                        AudioManager.instance.PlaySFX(10);
                    }
                    else 
                    {

                        if (canDoubleJump)
                        {
                            theRB.velocity = new Vector2 (theRB.velocity.x,jumpForce);
                            canDoubleJump = false;
                            AudioManager.instance.PlaySFX(10);
                        }
                    }
                }
                if(theRB.velocity.x<0)
                {
                    theSR.flipX = true;
                }
                else if (theRB.velocity.x>0)
                {
                    theSR.flipX = false;
                }
            }else
            {
                knockBackCounter -= Time.deltaTime;
                if (!theSR.flipX)
                {
                    theRB.velocity = new Vector2(-knockBackForce, theRB.velocity.y);
                }else
                {

                    theRB.velocity = new Vector2(knockBackForce, theRB.velocity.y);
                }
            }

        }
        anim.SetFloat("moveSpeed", Mathf.Abs(theRB.velocity.x));
        anim.SetBool("isGrounded", isGrounded);
    }

    public void KnockBack()
    {
        knockBackCounter = knockBackLength;
        theRB.velocity = new Vector2(0f, knockBackForce);
        anim.SetTrigger("hurt");
    }
}
