using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Item{
  public string name;
  public string description;
  public Sprite itemImage;
}

public class CollectionController : MonoBehaviour
{
  
    public Item item;
    public float healthChange;
    public float moveSpeedChange;
    public float attackSpeedChange;
    public float bulletSizeChange;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite=item.itemImage;
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
    }

   
    private void OnTriggerEnter2D(Collider2D collider) {
       if(collider.tag=="Player"){
          PlayerController.collectedAMount++;
          GameController.HealPlayer(healthChange);
          GameController.MoveSpeedChange(moveSpeedChange);
          GameController.FireRateChange(attackSpeedChange);
          GameController.BulletSizeChange(bulletSizeChange);
          GameController.instance.UpdateCollectedItems(this);
          Destroy(gameObject);
      }
    }

    
}
