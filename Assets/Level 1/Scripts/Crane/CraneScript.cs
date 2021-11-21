using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneScript : MonoBehaviour

    /*ROUTES
    0 fly1
    1 fly2
    2 swoop from right
    3 swoop from left
     
    */
{
    [SerializeField]
    private Transform[] routes;
    private int routeToGo;
    private float tParam;
    private Vector2 objectPosition;
    private float speedModifier;
    private bool coroutineAllowed;
    private float time;
    private float shootTime;
    private bool canShoot;

    public GameObject featherPrefab;
    public Transform player;
    public int featherGroup;
    public float featherAngle;
    public float featherVelocity;
    public float minShootTime;
    public float maxShootTime;
    public Transform attackPosition;
    public LayerMask whatIsPlayer;
    public float attackRange;
    public float damage;

    // Start is called before the first frame updat
    void Start() {
        routeToGo = 0;
        tParam = 0f;
        speedModifier = 0.5f;
        coroutineAllowed = true;
        shootTime = Random.Range(minShootTime, maxShootTime);
        time = 0;
        canShoot = true;
    }

    // Update is called once per frame

    void Update() {
        //Follow Bezier
        if (coroutineAllowed) {
            StartCoroutine(GoByTheRoute(routeToGo));
        }

        //Shoot feathers
        time += Time.deltaTime;
        if (time >= maxShootTime) {
            time = 0;
        }
        if (time >= shootTime) {
            if (canShoot) {
                ThrowFeathers();
                shootTime = Random.Range(minShootTime, maxShootTime);
                time = 0;
            }
        }

        //Player collider check
        Collider2D[] playerToAttack = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, whatIsPlayer);
        if (playerToAttack.Length > 0) {
            PlayerStats stats = playerToAttack[0].GetComponent<PlayerStats>();
            stats.TakeDamage(10);
            Debug.Log("Player Hit by Crane");
        }
    }

    private IEnumerator GoByTheRoute(int routeNum) {

        coroutineAllowed = false;
        Vector2 p0 = routes[routeNum].GetChild(0).position;
        Vector2 p1 = routes[routeNum].GetChild(1).position;
        Vector2 p2 = routes[routeNum].GetChild(2).position;
        Vector2 p3 = routes[routeNum].GetChild(3).position;
        while (tParam < 1) {
            tParam += Time.deltaTime * speedModifier;
            objectPosition = Mathf.Pow(1 - tParam, 3) * p0 + 3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 + 3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 + Mathf.Pow(tParam, 3) * p3;
            transform.position = objectPosition;
            yield return new WaitForEndOfFrame();
        }
        tParam = 0f;

        //Set randomly the new route to do
        int newRoute = Random.Range(0, 2);
        if (routeToGo == 0 || routeToGo == 3) {
            if(newRoute == 0) {
                routeToGo = 1; //fly
                canShoot = true;
            }
            else {
                routeToGo = 2; //swoop
                canShoot = false;
            }
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (routeToGo == 1 || routeToGo == 2) {
            if (newRoute == 0) {
                routeToGo = 0; //fly
                canShoot = true;
            }
            else {
                routeToGo = 3; //swoop
                canShoot = false;
            }
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (routeToGo > routes.Length - 1) {
            routeToGo = 0;
        }

        coroutineAllowed = true;
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

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }
}
