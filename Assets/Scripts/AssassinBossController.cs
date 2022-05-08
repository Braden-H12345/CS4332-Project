using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinBossController : MonoBehaviour
{

    [SerializeField] float attackRange = 1;
    [SerializeField] Transform player;
    [SerializeField] float speed = 10f;
    [SerializeField] float teleportDistance = 30f;
    [SerializeField] GameObject visualsToDisable;
    [SerializeField] Transform DOTObject;
    [SerializeField] Transform SwingObject;
    [SerializeField] ParticleSystem flameParticles;
    [SerializeField] ParticleSystem teleportParticles;
    [SerializeField] Transform BackhandObject;
    [SerializeField] AudioClip teleportSound;
    [SerializeField] AudioClip igniteSound;
    private Animator anim;
    private bool canMove = true;
    private bool isWalking = false;
    private bool isAttacking = false;
    private bool canAttack = true;
    private bool isIdle = false;
    private bool canTeleport = false;
    private float timeElapsed = 0f;
    private bool firstCheck = true;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        DOTObject.gameObject.SetActive(false);
        SwingObject.gameObject.SetActive(false);
        BackhandObject.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        timeElapsed += Time.deltaTime;

        if(timeElapsed >= 2 && firstCheck == true)
        {
            firstCheck = false;
            canTeleport = true;
        }

        if (transform.position.y != -35.97f)
        {
            transform.position = new Vector3(transform.position.x, -35.97f, transform.position.z);
        }

        float distanceToPlayer = Vector3.Distance(player.position, transform.position);
        if (distanceToPlayer >= teleportDistance && canTeleport)
        {
            if (isAttacking == false && canTeleport) //teleporrt condition or something else
            {
                StartCoroutine(Teleport(distanceToPlayer));
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


    IEnumerator Teleport(float distance)
    {
        //anim.SetTrigger("Jump");
        canTeleport = false;
        isWalking = false;
        isAttacking = false;
        canMove = false;
        AudioHelper.PlayClip2D(teleportSound, 1f);
        teleportParticles.Play();
        visualsToDisable.SetActive(false);

        yield return new WaitForSeconds(1.5f);

        transform.position = (transform.position + player.position) / 3;
        if (transform.position.y != -35.97f)
        {
            transform.position = new Vector3(transform.position.x, -35.97f, transform.position.z);
        }

        teleportParticles.Stop();
        visualsToDisable.SetActive(true);
        canMove = true;
        yield return new WaitForSeconds(10f);
        canTeleport = true;
    }
    IEnumerator Attack()
    {
        yield return null;

        isIdle = false;
        if (canAttack)
        {
            canAttack = false;
            //attack stuff
            int attackVal = Random.Range(1, 30);
            if (isAttacking == false)
            {
                isWalking = false;
                isAttacking = true;
                if (attackVal <= 10)
                {
                    canMove = false;
                    anim.SetTrigger("Attack");
                    yield return new WaitForSeconds(.8f);
                    SwingObject.gameObject.SetActive(true);
                    StartCoroutine(ResetObject("swing"));
                    StartCoroutine(ResetMove("swing"));
                    anim.SetTrigger("Idle");
                    yield return new WaitForSeconds(1f);
                }
                else if (attackVal <= 20)
                {
                    canMove = false;
                    AudioHelper.PlayClip2D(igniteSound, 1f);
                    anim.SetTrigger("Ignite");
                    yield return new WaitForSeconds(.5f);
                    flameParticles.Play();
                    yield return new WaitForSeconds(.75f);
                    anim.SetTrigger("Combo");
                    yield return new WaitForSeconds(.25f);
                    DOTObject.gameObject.SetActive(true);
                    yield return new WaitForSeconds(3.5f);
                    anim.SetTrigger("Idle");
                    StartCoroutine(ResetObject("combo"));
                    StartCoroutine(ResetMove("combo"));
                }
                else
                {
                    
                    canMove = false;
                    anim.SetTrigger("Backhand");
                    yield return new WaitForSeconds(1f);
                    BackhandObject.gameObject.SetActive(true);
                    StartCoroutine(ResetObject("kick"));
                    StartCoroutine(ResetMove("kick"));
                }
            }
        }
        yield return null;
    }

    IEnumerator ResetMove(string name)
    {
        yield return null;

        if (name == "kick")
        {
            yield return new WaitForSeconds(2.011111117f);
            canMove = true;
            canAttack = true;
            isAttacking = false;
        }
        if (name == "swing")
        {
            yield return new WaitForSeconds(.33333333f);
            canMove = true;
            canAttack = true;
            isAttacking = false;
            
        }
        if (name == "combo")
        {
            yield return new WaitForSeconds(.75f);
            canMove = true;
            canAttack = true;
            isAttacking = false;
        }

    }

    IEnumerator ResetObject(string name)
    {

        if (name == "kick")
        {
            yield return new WaitForSeconds(.25f);
            BackhandObject.gameObject.SetActive(false);

        }
        if (name == "swing")
        {
            yield return new WaitForSeconds(.1f);
            SwingObject.gameObject.SetActive(false);
        }
        if (name == "combo")
        {
            flameParticles.Stop();
            DOTObject.gameObject.SetActive(false);
        }

    }
}
    

