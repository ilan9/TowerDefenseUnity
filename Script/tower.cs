using System.Collections;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float range = 8f;
    public int damage = 25;
    public float fireRate = 1f;

    public GameObject target;
    private float cooldown = 0f;
    [Header("Effects")]
    [SerializeField] GameObject fireeffect;
    public int cost = 50;

    void Start()
    {

    }

    void Update()
    {
        if (target)
        {
            transform.right = target.transform.position - transform.position;
            if (cooldown >= fireRate)
            {
                
                target.GetComponent<Enemy>().damage(damage);
                cooldown = 0f;
                StartCoroutine(FireEffect());
            }
            else
            {
                cooldown += 1 * Time.deltaTime;
            }
            
        }
    }

    IEnumerator FireEffect()
    {
        fireeffect.SetActive(true);
        yield return new WaitForSeconds(fireRate/3);
        fireeffect.SetActive(false);
    }
}