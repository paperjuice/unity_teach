using System.Collections.Specialized;
using System.Threading;
using UnityEngine;

public class CameraControllerScript : MonoBehaviour {

    public Transform player;
    public float moveSpeed;
    public Vector3 offset;
    public float followDistance;
    public Quaternion rotation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        
    }

    // Update is called once per frame
    void FixedUpdate() {
        Vector3 pos = Vector3.Lerp(transform.position, player.position + offset + -transform.forward * followDistance, moveSpeed * Time.fixedDeltaTime);
       
        transform.position = pos;
        transform.rotation = rotation;
    }
}
