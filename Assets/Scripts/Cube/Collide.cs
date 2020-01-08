using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Collide : MonoBehaviour
{
    public AudioClip collideSound;
    private Renderer renderer;
    private bool isCollided = false;
    private Meta meta;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        meta = GetComponent<Meta>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (isCollided) return;
        // Debug.Log(name + " is collided!");
        // Debug.Log(meta.color);//color 和 direction的值的含义参见README.md
        // Debug.Log(meta.direction);
        bool valideCollision = false;
        // TODO 这里是物理上的碰撞，还需要检测逻辑上的碰撞是否合法
        // TODO 这里要做物体的切开动画
        ContactPoint contact = collision.contacts[0];
        Vector3 direction = contact.normal;
        Collider mycollider = collision.collider;
        Vector3 hitpoint = contact.point;
        Vector3 core = this.transform.position;
        Vector3 difference = hitpoint - core;
        float eps = 0.00001f;
        if (mycollider.name == "LaserSwordPrefab_left" || mycollider.name == "LaserSwordPrefab_right")
        {
            if (this.meta.direction == 0)
            {
                if (difference.x>0) {
                    valideCollision = true;
                }
            }
            else if (this.meta.direction == 1)
            {
                if (difference.x>0 && difference.y>0) valideCollision = true;
            }
            else if (this.meta.direction == 2)
            {
                if (difference.y>0) valideCollision = true;
            }
            else if (this.meta.direction == 3)
            {
                if (difference.x<0 && difference.y>0) valideCollision = true;
            }
            else if (this.meta.direction == 4)
            {
                if (difference.x<0) valideCollision = true;
            }
            else if (this.meta.direction == 5)
            {
                if (difference.x<0 && difference.y<0) valideCollision = true;
            }
            else if (this.meta.direction == 6)
            {
                if (difference.y<0) valideCollision = true;
            }
            else if (this.meta.direction == 7)
            {
                if (difference.y<0 && difference.x>0) valideCollision = true;
            }
            else if (this.meta.direction == -1)
            {
                valideCollision = true;
            }
        }
        Debug.Log(name + " is collided!");
        Debug.Log(direction.x + "," + direction.y);
        Debug.Log("judge result is " + valideCollision);
        // Debug.Log(name + " is collided!");
        // Debug.Log(direction.x + ","+direction.y);
        if (valideCollision)
        {
            isCollided = true;
            AudioSource.PlayClipAtPoint(collideSound, transform.position);
            // Debug.Log("destroy cube "+this.name);
            Destroy(this.gameObject);
        }
    }


    // void OnTriggerEnter(Collider collider)
    // {
    //     //如果被碰撞过了，就不管接下来的碰撞
    //     if (isCollided) return;
    //     Debug.Log(name + " is collided!");
    //     Debug.Log(meta.color);//color 和 direction的值的含义参见README.md
    //     Debug.Log(meta.direction);
    //     bool valideCollision = true;
    //     // TODO 这里是物理上的碰撞，还需要检测逻辑上的碰撞是否合法
    //     // TODO 这里要做物体的切开动画
    //     if (valideCollision)
    //     {
    //         isCollided = true;
    //         AudioSource.PlayClipAtPoint(collideSound, transform.position);
    //         renderer.enabled = false;
    //     }
    // }
}
