using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController: MonoBehaviour
{
    // Generic
    private GameObject player;
    private PlayerController playerController;
    private Vector3 initialPosition;
    public GameObject coin;
    public Image uiHpBar;

    [Header("Stats")]
    public float health = 20;
    public float damage = 5;
    public float movementSpeed = 10;
    private float maxHealth;

    [Header("Attack")]
    public float attackCooldown = 1.0f;
    private float cooldown;
    private bool isPerformingAttack = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        maxHealth = health;
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();

        cooldown = attackCooldown;
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate() {
        MoveTowardsPlayer();
    }

    void Update() {
        IncreaseDifficultyByTime();
        PerformAttack();
    }

    void MoveTowardsPlayer() {
        if (Vector3.Distance(this.transform.position, player.transform.position) > 2.0f) {
            transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, movementSpeed * Time.fixedDeltaTime);
        }
        transform.LookAt(player.transform.position); 
    }

    void PerformAttack() {
        if(Vector3.Distance(this.transform.position, player.transform.position) < 3.0f && player.gameObject.activeInHierarchy) {
            isPerformingAttack = true;
        }

        if(isPerformingAttack) {
            cooldown -= Time.deltaTime;
        }

        if (cooldown < 0.0f)
        {
            if (Vector3.Distance(this.transform.position, player.transform.position) < 3.0f) {
                Debug.Log("DMG");
                playerController.HandleDamage(damage); 
            }
            cooldown = attackCooldown;
            isPerformingAttack = false;
        }
    }

    void IncreaseDifficultyByTime()
    {
        damage += 0.05f * Time.deltaTime;
        movementSpeed += 0.05f * Time.deltaTime;

    }

    public void HandleDamage(int damage) {
        health -= damage;
        if (health < 0.0f) { health = 0.0f; }
        float ratio = health / maxHealth;
        uiHpBar.transform.localScale = new Vector3(ratio, 1, 1);

        if (health <= 0.0f) {
            Instantiate(coin, transform.position, new Quaternion(270,0,0,0));

            uiHpBar.transform.localScale = new Vector3(1, 1, 1);
            transform.position = initialPosition;
            health = maxHealth;
        }
    }
}






