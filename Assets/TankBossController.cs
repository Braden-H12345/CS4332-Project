using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBossController : MonoBehaviour
{

    [SerializeField] float attackPatternRange = 7f;
    [SerializeField] float chargeDistance = 75f;
    [SerializeField] Transform player;
    [SerializeField] float speed = 10f;
    [SerializeField] Transform KickObject;
    [SerializeField] Transform SwingObject;
    [SerializeField] Transform LeapObject;
    private Animator anim;
    private bool isWalking = false;
    private bool isAttacking = false;
    private bool canMove = true;
    private float timeElapsed = 0f;
    private bool isCharging = false;
    private bool canCharge = false;
    private bool runTrigger = false;
    private Vector3 positionAtChargeStart;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        KickObject.gameObject.SetActive(false);
        SwingObject.gameObject.SetActive(false);
        LeapObject.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        
        timeElapsed += Time.deltaTime;

        if(timeElapsed > 1.5)
        {
            canCharge = true;
        }
        if(transform.position.y != -36.97858)
        {
            transform.position = new Vector3(transform.position.x,-36.97858f, transform.position.z);
        }

        float distanceToPlayer = Vector3.Distance(player.position, transform.position);
        if(distanceToPlayer >= chargeDistance && canCharge)
        {
            if (isAttacking == false && isCharging == false)
            {
                StartCoroutine(Charge());
            }
        }
        else if (distanceToPlayer <= attackPatternRange)
        {
            if(isAttacking == false)
            {
                transform.LookAt(player);
                StartCoroutine(Attack());
            }
            
        }
        else
        {
            if(canMove)
            {
                if (isWalking == false)
                {
                    isWalking = true;
                    isAttacking = false;
                    anim.SetTrigger("Walk");
                }
                transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                transform.LookAt(player);
            }
        
        }

        if (runTrigger)
        {
            int speedMult = 5;
            if (isCharging == false)
            {
                isCharging = true;
                isAttacking = false;
                anim.SetTrigger("Run");
                positionAtChargeStart = player.position;
            }
            transform.position = Vector3.MoveTowards(transform.position, positionAtChargeStart, speed * Time.deltaTime * speedMult);
            
        }
        
    }

    
    IEnumerator Charge()
    {
        canMove = false;
        isWalking = true;
        isAttacking = true;
        //activate run trigger for set amount of time
        runTrigger = true;
        yield return new WaitForSeconds(2f);
        runTrigger = false;
        //then attack
        anim.SetTrigger("RunningAttack");
        
        //wait for animation then activate hitbox


        yield return new WaitForSeconds(.9f);
        LeapObject.gameObject.SetActive(true);
        yield return new WaitForSeconds(.333333f);
        LeapObject.gameObject.SetActive(false);

        yield return new WaitForSeconds(.45f);
        isCharging = false;
        isWalking = false;
        isAttacking = false;
        canMove = true;
        yield return new WaitForSeconds(30f);
        canCharge = false;
    }
    IEnumerator Attack()
    {
        //attack stuff
        int attackVal = //Random.Range(1, 2);
            2;

            if (isAttacking == false)
            {
            isWalking = false;
            isAttacking = true;
            if(attackVal == 1)
            {
                canMove = false;
                anim.SetTrigger("Swing");
                yield return new WaitForSeconds(1f);
                SwingObject.gameObject.SetActive(true);
                StartCoroutine(ResetObject("swing"));
                StartCoroutine(ResetMove("swing"));
            }
            if(attackVal == 2)
            {
                canMove = false;
                anim.SetTrigger("Combo");
                yield return new WaitForSeconds(5f);
                StartCoroutine(ResetMove("combo"));
            }
            }
    }

    IEnumerator ResetMove(string name)
    {
        if(name == "kick")
        {
            yield return new WaitForSeconds(1.6f);
            canMove = true;
            isAttacking = false;
        }
        if(name == "swing")
        {
            yield return new WaitForSeconds(.75f);
            canMove = true;
            isAttacking = false;
        }
        if(name == "combo")
        {
            //yield statement
            canMove = true;
            isAttacking = false;
        }
    }

    IEnumerator ResetObject(string name)
    {
        if(name == "kick")
        {
            yield return new WaitForSeconds(.333333f);
            KickObject.gameObject.SetActive(false);
        }
        if(name == "swing")
        {
            yield return new WaitForSeconds(.2f);
            SwingObject.gameObject.SetActive(false);
        }
    }
}
