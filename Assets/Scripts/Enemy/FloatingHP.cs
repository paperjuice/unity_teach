using UnityEngine;

public class FloatingHP : MonoBehaviour
{
    private void Start() {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.forward = Camera.main.transform.forward;
    }
}
