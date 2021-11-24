using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CraneScript : MonoBehaviour

    /*ROUTES
    0 fly1
    1 fly2
    2 swoop from right
    3 swoop from left
     
    */
{
    [SerializeField]
    private float tParam;
    private float speedModifier;
    private float time;
    private float shootTime;
    private bool canShoot;
    private Rigidbody2D rb;
    private Vector3 desiredHeight;
    private Vector3 groundHeight;

    public GameObject featherPrefab;
    public int featherGroup;
    public float featherAngle;
    public float featherAngleTop;
    public float featherVelocity;
    public float minShootTime;
    public float maxShootTime;
    public float featherDamage;

    public Transform player;
    public float speed;
    public float minHeight;
    public float maxHeight;

    public Transform attackPosition;
    public float attackRange;
    public LayerMask whatIsPlayer;
    public float damage;

    // Start is called before the first frame updat
    void Start() {
        tParam = 0f;
        speedModifier = 0.5f;
        shootTime = Random.Range(minShootTime, maxShootTime);
        time = 0;
        canShoot = true;
        rb = GetComponent<Rigidbody2D>();
        desiredHeight = rb.position;
        groundHeight = player.position;
    }

    // Update is called once per frame

    void Update() {
        //Shoot feathers
        time += Time.deltaTime;
        if (time >= maxShootTime) {
            time = 0;
        }
        if (time >= shootTime) {
            if (canShoot) {
                int attackType = Random.Range(0, 3);
                switch (attackType) {
                    case 0:
                        ThrowSingleFeather();
                        break;
                    case 1:
                        ThrowFeathers();
                        break;
                    case 2:
                        //ThrowFeathersFromTop();
                        break;
                    default:
                        ThrowSingleFeather();
                        break;
                }
                shootTime = Random.Range(minShootTime, maxShootTime);
                time = 0;
            }
        }

        //follow player
        rb.position = Vector2.MoveTowards(rb.position, desiredHeight, speed * Time.deltaTime);
        if (rb.position.y - desiredHeight.y <= 0.1) {
            desiredHeight = new Vector3(player.position.x, Random.Range(groundHeight.y + minHeight, groundHeight.y + maxHeight), 0);
        }
        //Player collider check
        Collider2D[] playerToAttack = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, whatIsPlayer);
        if (playerToAttack.Length > 0) {
            PlayerStats stats = playerToAttack[0].GetComponent<PlayerStats>();
            stats.TakeDamage(10);
        }
    }

    void ThrowSingleFeather() {
        GameObject feather = Instantiate(featherPrefab);
        feather.transform.position = transform.position;
        feather.transform.rotation = Quaternion.FromToRotation(feather.transform.right, player.transform.position - feather.transform.position);
        feather.GetComponent<Rigidbody2D>().velocity = feather.transform.right * featherVelocity;
    }

    void ThrowFeathers() {
        GameObject feather = Instantiate(featherPrefab);
        feather.transform.position = transform.position;
        feather.transform.rotation = Quaternion.FromToRotation(feather.transform.right, player.transform.position - feather.transform.position);
        feather.GetComponent<Rigidbody2D>().velocity = feather.transform.right * featherVelocity;
        for(int i = 0; i < featherGroup; i++) {
            feather = Instantiate(featherPrefab);
            feather.transform.position = transform.position;
            feather.transform.rotation = Quaternion.FromToRotation(feather.transform.right, player.transform.position - feather.transform.position) * Quaternion.Euler(0, 0, featherAngle * (i+1));
            feather.GetComponent<Rigidbody2D>().velocity = feather.transform.right * featherVelocity;
        }
        for (int i = 0; i < featherGroup; i++) {
            feather = Instantiate(featherPrefab);
            feather.transform.position = transform.position;
            feather.transform.rotation = Quaternion.FromToRotation(feather.transform.right, player.transform.position - feather.transform.position) * Quaternion.Euler(0, 0, -featherAngle * (i + 1));
            feather.GetComponent<Rigidbody2D>().velocity = feather.transform.right * featherVelocity;
        }
    }
    
    void ThrowFeathersFromTop()
    {
        GameObject feather = Instantiate(featherPrefab);
        feather.transform.position = transform.position;
        feather.transform.rotation = Quaternion.FromToRotation(feather.transform.right, player.transform.position - feather.transform.position);
        feather.GetComponent<Rigidbody2D>().velocity = feather.transform.right * featherVelocity;
        for (int i = 0; i < 5; i++)
        {
            feather = Instantiate(featherPrefab);
            feather.transform.position = transform.position;
            feather.transform.rotation = Quaternion.FromToRotation(feather.transform.right, player.transform.position - feather.transform.position) * Quaternion.Euler(0, 0, featherAngleTop * (i + 1));
            feather.GetComponent<Rigidbody2D>().velocity = feather.transform.right * featherVelocity;
        }
        for (int i = 0; i < 5; i++)
        {
            feather = Instantiate(featherPrefab);
            feather.transform.position = transform.position;
            feather.transform.rotation = Quaternion.FromToRotation(feather.transform.right, player.transform.position - feather.transform.position) * Quaternion.Euler(0, 0, -featherAngleTop * (i + 1));
            feather.GetComponent<Rigidbody2D>().velocity = feather.transform.right * featherVelocity;
        }
    }
    
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }
}
