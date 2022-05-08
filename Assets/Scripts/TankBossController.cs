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
    [SerializeField] Transform ComboObject;
    private Animator anim;
    private bool isWalking = false;
    private bool isAttacking = false;
    private bool canMove = true;
    private bool canAttack = true;
    private float timeElapsed = 0f;
    private bool isCharging = false;
    private bool canCharge = false;
    private bool runTrigger = false;
    private bool isIdle = false;
    private bool firstCheck = true;
    private Vector3 positionAtChargeStart;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        KickObject.gameObject.SetActive(false);
        SwingObject.gameObject.SetActive(false);
        LeapObject.gameObject.SetActive(false);
        ComboObject.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        timeElapsed += Time.deltaTime;

        if(timeElapsed > 1.5 && firstCheck)
        {
            firstCheck = false;
            canCharge = true;
        }
        if(transform.position.y != -36.97858f)
        {
            transform.position = new Vector3(transform.position.x, -36.97858f, transform.position.z);
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
                Vector3 dirFromBossToPlayer = (player.transform.position - transform.position).normalized;

                float dotproduct = Vector3.Dot(dirFromBossToPlayer, transform.forward);
                if (dotproduct > .9)
                {
                    canAttack = true;
                    StartCoroutine(Attack());
                }
                else
                {
                    if (isIdle == false)
                    {
                        anim.SetTrigger("idle");
                        isIdle = true;
                    }
                    canAttack = false;
                    Vector3 targetDirection = player.position - transform.position;

                    float singleStep = 5 * Time.deltaTime;

                    Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

                    transform.rotation = Quaternion.LookRotation(newDir);
                }
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
        canCharge = false;
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
        canCharge = true;
    }
    IEnumerator Attack()
    {
        isIdle = false;
        if (canAttack)
        {
            canAttack = false;
            //attack stuff
            int attackVal = Random.Range(1, 50);

            if (isAttacking == false)
            {
                isWalking = false;
                isAttacking = true;
                if (attackVal % 2 == 0)
                {
                    canMove = false;
                    anim.SetTrigger("Swing");
                    yield return new WaitForSeconds(1f);
                    SwingObject.gameObject.SetActive(true);
                    StartCoroutine(ResetObject("swing"));
                    StartCoroutine(ResetMove("swing"));
                }
                if (attackVal % 2 != 0)
                {
                    canMove = false;
                    anim.SetTrigger("Combo");
                    yield return new WaitForSeconds(2.7f);
                    ComboObject.gameObject.SetActive(true);
                    yield return new WaitForSeconds(.2f);
                    ComboObject.gameObject.SetActive(false);
                    yield return new WaitForSeconds(.9f);
                    ComboObject.gameObject.SetActive(true);
                    StartCoroutine(ResetObject("combo"));
                    StartCoroutine(ResetMove("combo"));
                }
            }
        }
        yield return null;
    }

    IEnumerator ResetMove(string name)
    {
        if(name == "kick")
        {
            yield return new WaitForSeconds(1.6f);
            canMove = true;
            canAttack = true;
            isAttacking = false;
        }
        if(name == "swing")
        {
            yield return new WaitForSeconds(.75f);
            canMove = true;
            canAttack = true;
            isAttacking = false;
        }
        if(name == "combo")
        {
            yield return new WaitForSeconds(.5f);
            canMove = true;
            canAttack = true;
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
        if(name == "combo")
        {
            yield return new WaitForSeconds(.3f);
            ComboObject.gameObject.SetActive(false);
        }
    }
}
