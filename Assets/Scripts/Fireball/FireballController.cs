using UnityEngine;

public class FireballController : MonoBehaviour {

    [Header("Stats")]
    public float speed;
    public int damage = 4;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void FixedUpdate() {
        Movement();
    }

    void Movement() {
        transform.position += transform.forward * Time.deltaTime * speed;
    }

    // PUBLIC FUNCTIONS
    void OnTriggerEnter(Collider collision) {
        EnemyController enemyController = collision.gameObject.GetComponent<EnemyController>();

        if (enemyController != null) {
            enemyController.HandleDamage(damage);
            gameObject.SetActive(false);
        }
    }
}
