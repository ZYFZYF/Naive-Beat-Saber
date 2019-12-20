using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    private float speed;
    // Use this for initialization
    void Start()
    {
        speed = Generate.Z_MOVE_SPEED;
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(-Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision enter");
    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Trigger enter");
        Debug.Log(collider.ToString());
    }
}
