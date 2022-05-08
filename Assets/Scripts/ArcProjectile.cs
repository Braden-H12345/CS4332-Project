using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcProjectile : MonoBehaviour
{
    [SerializeField] GameObject sphereVisuals;
    [SerializeField] GameObject explosionsVisuals;
    [SerializeField] float explosionRadius = 7;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] GameObject visuals;
    [SerializeField] AudioClip chargeSound;
    [SerializeField] AudioClip explosionSound;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool notExploding = true;
    bool isGrounded;
    Coroutine currentArea;
    private float physicsTimeElapsed = 0f;
    // Start is called before the first frame update
    void Start()
    {
        ArcPattern();
        
    }

    public bool getGrounded()
    {
        return isGrounded;
    }

    // Update is called once per frame
    void Update()
    {
        if(physicsTimeElapsed > 5)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        }

        if (isGrounded && notExploding)
        {
            if (currentArea != null)
            {
                StopCoroutine(currentArea);
            }
            currentArea = StartCoroutine(StartAreaAttack());
        }
    }
    private void FixedUpdate()
    {
        if(physicsTimeElapsed <= 5)
        {
            physicsTimeElapsed += Time.deltaTime;
        }
       
    }

    private void ArcPattern()
    {
        
    }

    IEnumerator StartAreaAttack()
    {
        notExploding = false;
        GameObject sphere;
        GameObject sphere1;

        AudioHelper.PlayClip2D(chargeSound, 1f);
            for (int i = 0; i < 5; i++)
            {
                sphere1 = Instantiate(sphereVisuals, transform.position, transform.rotation);
                yield return new WaitForSeconds(.1f);
                Destroy(sphere1);
                yield return new WaitForSeconds(.1f);
            }
            sphere = Instantiate(explosionsVisuals, transform.position, transform.rotation);

            Collider[] hit = Physics.OverlapSphere(transform.position, explosionRadius, enemyLayer);
            foreach (Collider player in hit)
            {
                if (player != null)
                {
                    if (player.gameObject.GetComponent<PlayerMovement>())
                    {
                        Health playerDmg = player.gameObject.GetComponent<Health>();
                        playerDmg.takeDamage(100);
                    }
                }
            }


        visuals.SetActive(false);
        AudioHelper.PlayClip2D(explosionSound, 1f);
        yield return new WaitForSeconds(1f);
        Destroy(sphere);
        Destroy(this.gameObject);
    }
}
