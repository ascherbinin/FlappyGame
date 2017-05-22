using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusPool : MonoBehaviour
{
    public GameObject gemBonusPrefab;                                 //The column game object.
    public GameObject coinBonusPrefab;                                 //The column game object.
    public float spawnRate = 3f;                                    //How quickly columns spawn.
    public float bonusYMin = -2f;                                   //Minimum y value of the column position.
    public float bonusYMax = 4.5f;         //Maximum y value of the column position.

    private List<GameObject> bonusesList = new List<GameObject>();
    private GameObject goBaseBonus;
    private float spawnXPosition = 12f;
    private float timeSinceLastSpawned;
    private const float GEM_CHANCE = 0.1f;

    void Start()
    {
        timeSinceLastSpawned = 0f;
        goBaseBonus = new GameObject();
        goBaseBonus.name = "Bonuses";
    }

    void OnEnable()
    {
        //EventManager.StartListening("StartGame", GenerateColumns);
    }

    void OnDisable()
    {
        //EventManager.StopListening("StartGame", GenerateColumns);
    }

    //This spawns columns as long as the game is not over.
    void Update()
    {
        timeSinceLastSpawned += Time.deltaTime;

        if (GameControl.instance.gameOver == false && timeSinceLastSpawned >= spawnRate && GameControl.instance.GameState == State.Play)
        {
            timeSinceLastSpawned = 0f;

            //Set a random y position for the column
            float spawnYPosition = Random.Range(bonusYMin, bonusYMax);

            var nextSpawnPos = new Vector2(spawnXPosition, spawnYPosition);
            var bonus = Instantiate(coinBonusPrefab, nextSpawnPos, Quaternion.identity);
            bonus.transform.parent = goBaseBonus.transform;
            bonusesList.Add(bonus);
        }
    }

    public void GenerateColumns()
    {
        // to do
    }
}
