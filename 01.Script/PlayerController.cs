using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    Rigidbody2D rigidbody;
    public Text collectedText;
    public static int collectedAMount=0;

    public GameObject bulletPrefab;
    public float bulletSpeed;
    private float lastFire;
    public float fireDelay;

    void Start() {
        rigidbody=GetComponent<Rigidbody2D>();
    }

    void Update() {
        fireDelay = GameController.FireRate;
        speed = GameController.MoveSpeed;
        
        float horizontal =Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float shootHor = Input.GetAxis("ShootHorizontal");
        float shootVer = Input.GetAxis("ShootVertical");
        if((shootHor!=0||shootVer!=0)&&Time.time >lastFire+fireDelay){
            Shoot(shootHor,shootVer);
            lastFire=Time.time;
        }
        rigidbody.velocity = new Vector3(horizontal * speed,vertical*speed,0);
        collectedText.text = "Item Collected : " + collectedAMount;
    }

    void Shoot(float x,float y){
        GameObject bullet = Instantiate(bulletPrefab,transform.position, transform.rotation)as GameObject;
        bullet.AddComponent<Rigidbody2D>().gravityScale=0;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
            (x<0)?Mathf.Floor(x)*bulletSpeed : Mathf.Ceil(x) * bulletSpeed,
            (y<0)?Mathf.Floor(y)*bulletSpeed : Mathf.Ceil(y) * bulletSpeed,
            0
        );
    }
}
 