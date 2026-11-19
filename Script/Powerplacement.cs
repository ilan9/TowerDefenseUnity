using UnityEngine;
using UnityEngine.InputSystem;

public class TowerPlacement : MonoBehaviour
{
    [SerializeField] private SpriteRenderer rangeSprite;
    [SerializeField] private CircleCollider2D rangeCollider;
    [SerializeField] private Color gray;
    [SerializeField] private Color red;

    public bool isPlacing = true;
    
    // Variable déduite de son utilisation dans le reste du code
    private bool isRestricted; 
    private Tower tower;

    void Awake()
    {
        tower = GetComponent<Tower>();
        rangeCollider.enabled = false;
    }

    void Update()
    {
        if(isPlacing)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            
            transform.position = mousePosition;

            if(Mouse.current.leftButton.wasPressedThisFrame && !isRestricted && tower.cost <= Player.main.money)
            {
                Player.main.money -= tower.cost;
                rangeCollider.enabled = true;
                isPlacing = false;
                GetComponent<TowerPlacement>().enabled = false;
            }

            if(isRestricted)
            {
                rangeSprite.color = red;
            }
            else
            {
                rangeSprite.color = gray;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Restricted" || collision.gameObject.tag == "Tower" && isPlacing)
        {
            isRestricted = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Restricted" || collision.gameObject.tag == "Tower" && isPlacing)
        {
            isRestricted = false;
        }
    }
}
