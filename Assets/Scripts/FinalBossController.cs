using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossController : MonoBehaviour
{
    [SerializeField] float attackRange = 1;
    [SerializeField] Transform player;
    [SerializeField] float speed = 10f;
    [SerializeField] Transform AOEPosition;
    [SerializeField] Transform GroundSmashObject;
    [SerializeField] Transform SwingObject;
    [SerializeField] Transform BackhandObject;
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject projectile_2;
    [SerializeField] GameObject projectile_3;
    [SerializeField] Transform projectileLocation_1;
    [SerializeField] Transform projectileLocation_2;
    [SerializeField] Transform projectileLocation_3;
    [SerializeField] GameObject glowLight;
    private Animator anim;
    private bool canMove = false;
    private bool isWalking = false;
    private bool isAttacking = false;
    private bool canAttack = true;
    private bool isIdle = false;
    private bool canAOE = false;
    private float timeElapsed = 0f;
    private bool firstCheck = true;
    private bool notMovingTowardsStart = true;
    private bool glowingReady = false;
    private bool glowingFinished = true;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        SwingObject.gameObject.SetActive(false);
        BackhandObject.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        timeElapsed += Time.deltaTime;

        if (timeElapsed >= 5 && firstCheck == true)
        {
            firstCheck = false;
            canAOE = true;
            canMove = true;
        }

        if (transform.position.y != -35.97f)
        {
            transform.position = new Vector3(transform.position.x, -35.97f, transform.position.z);
        }

        float distanceToPlayer = Vector3.Distance(player.position, transform.position);
        float distanceToStart = Vector3.Distance(AOEPosition.position, transform.position);

        if(glowingReady && canAOE)
        {
            StartCoroutine(Glowing());
        }

        //ranged AOE attack
        if (canAOE && glowingFinished)
        {
            notMovingTowardsStart = true;
            if(distanceToStart >= 5)
            {
                notMovingTowardsStart = false;
                transform.LookAt(AOEPosition);
                transform.position = Vector3.MoveTowards(transform.position, AOEPosition.position, speed * 50 * Time.deltaTime);
            }
            if (isAttacking == false && canAOE && notMovingTowardsStart)
            {
                StartCoroutine(AoEAttack());
            }
        }
        else if (distanceToPlayer <= attackRange)
        {
            //for attacking
            if (isAttacking == false)
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
                        anim.SetTrigger("Idle");
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
            if (canMove)
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
    }

    IEnumerator Glowing()
    {
        canAOE = false;
        glowingReady = false;
        glowingFinished = false;
        glowLight.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        glowLight.SetActive(false);
        glowingFinished = true;
        canAOE = true;
        yield return new WaitForSeconds(7f);
        glowingReady = true;


    }
    IEnumerator AoEAttack()
    {
        transform.LookAt(player);
        glowingReady = true;
        isWalking = false;
        canAOE = false;
        canMove = false;
        canAttack = false;
        isIdle = false;
        anim.SetTrigger("throw");
        yield return new WaitForSeconds(1f);
        Instantiate(projectile, projectileLocation_1.position, projectileLocation_1.rotation);
        Instantiate(projectile_2, projectileLocation_2.position, projectileLocation_2.rotation);
        Instantiate(projectile_3, projectileLocation_3.position, projectileLocation_3.rotation);
        canMove = true;
        canAttack = true;
        yield return new WaitForSeconds(25f);
        canAOE = true;
    }
    IEnumerator Attack()
    {
        yield return null;

        isIdle = false;
        if (canAttack)
        {
            canAttack = false;
            //attack stuff
            int attackVal = Random.Range(1, 100);
                
                
            if (isAttacking == false)
            {
                isWalking = false;
                isAttacking = true;
                //10% chance for ground smash that stuns
                if (attackVal <= 10)
                {
                    //TODO: here
                    canMove = false;
                    anim.SetTrigger("Ground");
                    yield return new WaitForSeconds(.8f);
                    GroundSmashObject.gameObject.SetActive(true);
                    StartCoroutine(ResetObject("ground"));
                    StartCoroutine(ResetMove("ground"));

                    yield return new WaitForSeconds(1f);
                }
                //30% chance for heavy attack- slow attack with more damage
                else if (attackVal <= 40)
                {
                    canMove = false;
                    anim.SetTrigger("Heavy");
                    yield return new WaitForSeconds(1.25f);
                    SwingObject.gameObject.SetActive(true);
                    StartCoroutine(ResetObject("heavy"));
                    StartCoroutine(ResetMove("heavy"));
                }
                else //60% chance for normal attack - decent speed attack with average damage
                {
                    canMove = false;
                    anim.SetTrigger("Standard");
                    yield return new WaitForSeconds(.8f);
                    BackhandObject.gameObject.SetActive(true);
                    StartCoroutine(ResetObject("standard"));
                    StartCoroutine(ResetMove("standard"));
                }
            }
        }
        yield return null;
    }

    IEnumerator ResetMove(string name)
    {
        yield return null;

        if (name == "standard")
        {
            yield return new WaitForSeconds(1f);
            canMove = true;
            canAttack = true;
            isAttacking = false;
        }
        if (name == "ground")
        {
            yield return new WaitForSeconds(.33333333f);
            anim.SetTrigger("Idle");
            yield return new WaitForSeconds(1f);
            canMove = true;
            canAttack = true;
            isAttacking = false;

        }
        if (name == "heavy")
        {
            yield return new WaitForSeconds(.75f);
            canMove = true;
            canAttack = true;
            isAttacking = false;
        }

    }

    IEnumerator ResetObject(string name)
    {

        if (name == "standard")
        {
            yield return new WaitForSeconds(.1f);
            BackhandObject.gameObject.SetActive(false);

        }
        if (name == "ground")
        {
            yield return new WaitForSeconds(.1f);
            GroundSmashObject.gameObject.SetActive(false);
        }

        if(name == "heavy")
        {
            yield return new WaitForSeconds(.33333333f);
            SwingObject.gameObject.SetActive(false);
        }
    }
}
