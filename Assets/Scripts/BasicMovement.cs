using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    private Rigidbody rb;
    private float horizontalX;
    private float verticleZ;
    [SerializeField] private float speed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    { 
        verticleZ = Input.GetAxis("Vertical");
        horizontalX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector3(horizontalX * speed, rb.velocity.y, verticleZ * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Props"))
        {
            Debug.Log("Trigger");
            GameManager.instance.GetPropsDetails(other.GetComponent<Details>());
        }
    }
}
