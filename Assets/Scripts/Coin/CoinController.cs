using UnityEngine;

public class CoinController : MonoBehaviour
{

    public int score = 5;
    public float yForce = 10.0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float x = Random.Range(-100f, 100f);
        float z = Random.Range(-100f, -100f);
        Vector3 randomForce = new Vector3(x, yForce, z);

        Destroy(gameObject, 5);

        GetComponent<Rigidbody>().AddForce(randomForce, ForceMode.Impulse);

        // Random rotate
        transform.Rotate(0, 0, Random.Range(0, 360));
    }
}
