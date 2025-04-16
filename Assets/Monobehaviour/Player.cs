using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using System.Collections;

public class Player : Character1
{
    public HitPoints hitPoints;

    public HealthBar healthBarPrefab;
    public int amount;
    HealthBar healthBar;
    public Inventory inventoryPrefab;
    Inventory inventory;

    void Start()
    {

        hitPoints.value = startingHitPoints;
        inventory = Instantiate(inventoryPrefab);
        healthBar = Instantiate(healthBarPrefab);
        healthBar.character = this;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CanBePickedUp"))
        {
            Item hitObject = collision.gameObject.GetComponent<Consumable>().item;
            if (hitObject != null)
            {
                bool shouldDisappear = false; // 1
                switch (hitObject.itemType)   // 2
                {
                    case Item.ItemType.PART: // 3
                        shouldDisappear = inventory.AddItem(hitObject);
                        shouldDisappear = true;
                        break;

                    case Item.ItemType.HEALTH: // 4
                        shouldDisappear = AdjustHitPoints(hitObject.quantity);
                        break;

                    default:
                        break;
                }

                if (shouldDisappear) // 5
                {
                    collision.gameObject.SetActive(false);
                }
            }
        }
    }

    public bool AdjustHitPoints(int amount) // 5
    {
        if(hitPoints!=null)
            Debug.Log("Current HP before; " + hitPoints.value);
        
        
        if (hitPoints.value < maxHitPoints) // 6
        {
            hitPoints.value = hitPoints.value + amount;
            print("Adjusted HP by: " + amount + ".New value: " + hitPoints.value);
            return true;
          
        }

        return false; // 10
    }

    public override IEnumerator DamageCharacter(int damage, float inteval)
    {
        while (true) 
        {
            hitPoints.value = hitPoints.value - damage;

            if (hitPoints.value <= float.Epsilon) 
            {
                KillCharacter();
                break;
            }

            if (inteval > float.Epsilon) 
            {
                yield return new WaitForSeconds(inteval);
            }
            else 
            {
                yield return null;
                break;
            }
        }
    }

    public override void KillCharacter()
    {
        base.KillCharacter();
        Destroy(healthBar.gameObject);
        Destroy(inventory.gameObject);
    }
    
    public override void ResetCharacter()
    {
        hitPoints.value = startingHitPoints;
        inventory = Instantiate(inventoryPrefab);
        healthBar = Instantiate(healthBarPrefab);
        healthBar.character = this;
    }
}