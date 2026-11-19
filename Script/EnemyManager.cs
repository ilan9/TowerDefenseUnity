using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager main;

    public Transform spawnpoint;
    public Transform[] checkpoints;

    [SerializeField] private GameObject zombie;
    [SerializeField] private GameObject fastZombie;
    [SerializeField] private GameObject tankZombie;

    [SerializeField] private int wave = 1;
    [SerializeField] private float spawndelaymin = 0.5f;
    [SerializeField] private float spawndelaymax = 1.5f;
    [SerializeField] private int enemyCount = 6;

    private bool wavedone = false;
    [SerializeField] private float zombieRate = 0.5f;
    [SerializeField] private float fastZombieRate = 0.4f;
    [SerializeField] private float tankZombieRate = 0.1f;
    [SerializeField] private TextMeshProUGUI nextWave;
    private bool waveover = false;

    private List<GameObject> waveset = new List<GameObject>();
    private int enemyLeft;

    private int zombieCount;
    private int fastZombieCount;
    private int tankZombieCount;

    void Awake()
    {
        main = this;
        foreach (var check in checkpoints)
        {
            check.gameObject.SetActive(false);
        }
    }

    void Start()
    {
        zombieCount = Mathf.RoundToInt(enemyCount * zombieRate);
        fastZombieCount = Mathf.RoundToInt(enemyCount * fastZombieRate);
        tankZombieCount = Mathf.RoundToInt(enemyCount * tankZombieRate);

        enemyLeft = zombieCount + fastZombieCount + tankZombieCount;
        enemyCount = enemyLeft;
        SetWave();
    }

    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        if ((wavedone && enemies.Length == 0) || waveover)
        {
            waveover = false;
            wave ++;
            wavedone = false;
            enemyCount += wave ;
            Player.main.money += 50 + 10*wave;
            SetWave();
        }
        int gain = 50 + 10*(wave+1);
        nextWave.text = "Prochaine vague + "+ gain.ToString()+ "Money";
    }

    private void SetWave()
    {
        zombieCount = Mathf.RoundToInt(enemyCount * zombieRate);
        fastZombieCount = Mathf.RoundToInt(enemyCount * fastZombieRate);
        tankZombieCount = Mathf.RoundToInt(enemyCount * tankZombieRate);

        waveset = new List<GameObject>();

        for (int i = 0; i < zombieCount; i++)
        {
            waveset.Add(zombie);
        }

        for (int i = 0; i < fastZombieCount; i++)
        {
            waveset.Add(fastZombie);
        }

        for (int i = 0; i < tankZombieCount; i++)
        {
            waveset.Add(tankZombie);
        }

        for (int i = waveset.Count - 1; i > 0; i--)
        {
        int randomIndex = Random.Range(0, i + 1);
        GameObject temporary = waveset[i];
        waveset[i] = waveset[randomIndex];
        waveset[randomIndex] = temporary;
        } // On parcourt la liste depuis la fin et et on échange au hasard avec un élément

        StartCoroutine(spawn());
    }

    IEnumerator spawn()
    {
        for (int i = 0;i < waveset.Count;i++)
        {
            Instantiate(waveset[i],spawnpoint.position,Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(spawndelaymin,spawndelaymax));
        }
        wavedone = true;
    }
    public void nextVague()
    {
        waveover = true;
        
    }

}