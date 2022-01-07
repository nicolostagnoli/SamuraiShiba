using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CraneMovement : BossMovement {

    private float timeToHeal;
    private float timeToShoot;
    private float randomShootTime;
    private bool canShoot;
    private Rigidbody2D rb;
    private Vector3 desiredPosition;
    private float _speed;
    private int currentMovement;
    private bool canMakeNewMove;
    private bool isHealing;

    private Animator anim;

    //feather attack
    public GameObject featherPrefab;
    public float featherAngle;
    public float featherAngleTop;
    public float featherVelocity;
    public float minShootTime;
    public float maxShootTime;
    public float featherDamage;
    public int featherGroup;
    private bool featherDarkMode;

    public Transform player;
    public Transform ground;
    public Transform healPos;
    public Transform featherAttackPos1;

    //fly parameters
    public float normalSpeed;
    public float swoopSpeed;
    public float minHeight;
    public float maxHeight;

    //attack collider
    public Transform attackPosition;
    public float attackRange;
    public LayerMask whatIsPlayer;
    public float damage;
    private bool canAttack;

    //health
    public float healTreshold;
    public float timeBetweenHealings;
    private BossHealth craneHealth;

    void Awake() {
        canMakeNewMove = true;
        randomShootTime = Random.Range(minShootTime, maxShootTime);
        timeToShoot = 0;
        timeToHeal = 0;
        canShoot = true;
        rb = GetComponent<Rigidbody2D>();
        desiredPosition = featherAttackPos1.position;
        _speed = normalSpeed;
        currentMovement = 0;
        anim = GetComponent<Animator>();
        craneHealth = GetComponent<BossHealth>();
        canAttack = false;
    }

    void Update() {
        if (isHealing) {
            craneHealth.SetHealth(craneHealth.GetHealth() + 2f * Time.deltaTime);
        }

        if (player != null)
        {
            timeToShoot += Time.deltaTime;
            timeToHeal += Time.deltaTime;

            //Shoot feathers
            if (timeToShoot >= maxShootTime)
            {
                timeToShoot = 0;
            }

            if (timeToShoot >= randomShootTime)
            {
                if (canShoot)
                {
                    int attackType = Random.Range(0, 2);
                    switch (attackType)
                    {
                        case 0:
                            ThrowSingleFeather();
                            break;
                        case 1:
                            anim.SetTrigger("FeatherAttack");
                            ThrowFeathers(featherGroup);
                            break;
                        default:
                            ThrowSingleFeather();
                            break;
                    }

                    randomShootTime = Random.Range(minShootTime, maxShootTime);
                    timeToShoot = 0;
                }
            }

            //face right direction
            if (transform.position.x > player.position.x)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }

            //follow desired position
            rb.position = Vector2.MoveTowards(rb.position, desiredPosition, _speed * Time.deltaTime);

            //when desired position is reached
            if (Vector2.Distance(rb.position, desiredPosition) <= 0.001)
            {

                //based on previuos desired position, do something
                switch (currentMovement)
                {
                    case 2: //make attack from top
                        anim.SetTrigger("FeatherAttackFromTop");
                        break;
                    case 3: //heal
                        StartHealing();
                        anim.SetBool("Heal", true);
                        break;
                    default: break;
                }

                //make new move
                if (canMakeNewMove)
                {
                    currentMovement = Random.Range(0, 3);
                    switch (currentMovement)
                    {
                        case 0: //random fly
                            desiredPosition = new Vector3(player.position.x,
                                Random.Range(ground.position.y + minHeight, ground.position.y + maxHeight), 0);
                            _speed = normalSpeed;
                            break;
                        case 1: //swoop attack
                            desiredPosition = player.position;
                            _speed = swoopSpeed;
                            break;
                        case 2: //attack from top
                            desiredPosition = featherAttackPos1.position;
                            break;
                        default: //random fly
                            desiredPosition = new Vector3(player.position.x,
                                Random.Range(ground.position.y + minHeight, ground.position.y + maxHeight), 0);
                            _speed = normalSpeed;
                            break;
                    }

                    //after new movement, check if healing should override
                    if (craneHealth.GetHealth() < healTreshold && timeToHeal > timeBetweenHealings)
                    {
                        desiredPosition = healPos.position;
                        timeToHeal = 0;
                    }
                }


            }

            //Player collider check
            if (canAttack) {
                Collider2D[] playerToAttack =
                    Physics2D.OverlapCircleAll(attackPosition.position, attackRange, whatIsPlayer);
                if (playerToAttack.Length > 0) {
                    PlayerStats stats = playerToAttack[0].GetComponent<PlayerStats>();
                    stats.TakeDamage(damage);
                }
            }
        }
    }

    void ThrowSingleFeather() {
        GameObject feather = Instantiate(featherPrefab);
        if(featherDarkMode )feather.GetComponentInChildren<DarkParticleEffect>().activateDarkMode();
        feather.GetComponent<BossProjectyle>().SetDamage(featherDamage);
        feather.transform.position = transform.position;
        feather.transform.rotation = Quaternion.FromToRotation(feather.transform.right, player.transform.position - feather.transform.position);
        feather.GetComponent<Rigidbody2D>().velocity = feather.transform.right * featherVelocity;
    }

    void ThrowFeathers(int featherGroup) {
        GameObject feather = Instantiate(featherPrefab);
        if(featherDarkMode )feather.GetComponentInChildren<DarkParticleEffect>().activateDarkMode();
        feather.GetComponent<BossProjectyle>().SetDamage(featherDamage);
        feather.transform.position = transform.position;
        feather.transform.rotation = Quaternion.FromToRotation(feather.transform.right, player.transform.position - feather.transform.position);
        feather.GetComponent<Rigidbody2D>().velocity = feather.transform.right * featherVelocity;
        for(int i = 0; i < featherGroup; i++) {
            feather = Instantiate(featherPrefab);
            if(featherDarkMode )feather.GetComponentInChildren<DarkParticleEffect>().activateDarkMode();
            feather.GetComponent<BossProjectyle>().SetDamage(featherDamage);
            feather.transform.position = transform.position;
            feather.transform.rotation = Quaternion.FromToRotation(feather.transform.right, player.transform.position - feather.transform.position) * Quaternion.Euler(0, 0, featherAngle * (i+1));
            feather.GetComponent<Rigidbody2D>().velocity = feather.transform.right * featherVelocity;
        }
        for (int i = 0; i < featherGroup; i++) {
            feather = Instantiate(featherPrefab);
            if(featherDarkMode )feather.GetComponentInChildren<DarkParticleEffect>().activateDarkMode();
            feather.GetComponent<BossProjectyle>().SetDamage(featherDamage);
            feather.transform.position = transform.position;
            feather.transform.rotation = Quaternion.FromToRotation(feather.transform.right, player.transform.position - feather.transform.position) * Quaternion.Euler(0, 0, -featherAngle * (i + 1));
            feather.GetComponent<Rigidbody2D>().velocity = feather.transform.right * featherVelocity;
        }

    }

    public void FeatherAttack() {
        if (!featherDarkMode)
            ThrowFeathers(1);
        else
            ThrowFeathers(2);
    }

    public void StartHealing() {
        canMakeNewMove = false;
        canShoot = false;
        isHealing = true;
    }

    public void OnHealingFinished() {
        canMakeNewMove = true;
        canShoot = true;
        isHealing = false;
        anim.SetBool("Heal", false);
    }

    public void EnableAttackCollider() {
        canAttack = true;
    }

    public void DisableAttackCollider() {
        canAttack = false;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }

    public override void TriggerDarkMode(float damage, float speed, float projectileDamage)
    {
        this.damage = damage;
        _speed = speed;
        this.featherDamage = projectileDamage;
        featherDarkMode = true;
        featherPrefab.GetComponentInChildren<DarkParticleEffect>().activateDarkMode();
        featherGroup = 3;
    }
}
