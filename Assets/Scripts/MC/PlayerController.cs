using System;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    // Player movement
    float horizontal;
    float vertical;

    // Rotate towards cursor
    [SerializeField] private LayerMask planeLayer;
    Vector3 targetPosition;
    Vector3 direction;

    // Attack
    [Header("Attack")]
    public GameObject fireball;
    public float offset;

    [Header("Stats")]
    public float movementSpeed = 5.0f;
    public float health = 50;
    private float maxHealth;
    public float damage = 50;

    private void Start() {
        maxHealth = health;
    }

    // Influenced by framerate
    void Update() {
        CursorPos();
        Attack();
    }

    // Update is at fixed time interval
    void FixedUpdate() {
        PlayerMovement();
    }

    void Attack() {
        // TODO: Find input by name not by button
        if (Input.GetMouseButtonDown(0)) {
            Instantiate(fireball, transform.position + transform.forward * offset, transform.rotation);
         }
    }

    void PlayerMovement() {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        transform.position += new Vector3(horizontal, 0, vertical) * movementSpeed * Time.fixedDeltaTime;
    }

    void CursorPos() {
        //int layerObject = 6;
        //int layerObject = LayerMask.NameToLayer("Floor");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, planeLayer)){
            targetPosition = hitInfo.point;
            direction = targetPosition - transform.position;
            direction.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CoinController>() != null)
        {
            GameManager.Instance.AddScore(other.GetComponent<CoinController>().score);
            Destroy(other.gameObject);
        }
    }

    public void HandleDamage(float damage) {
        health -= damage;
        if (health < 0) {
            health = 0;
        }

        UIController.Instance.UpdatePlayerHealth(health, maxHealth);

        if (health <= 0.0f) {
            UIController.Instance.ShowMenu(true);
            gameObject.SetActive(false);
        }
    }

}
