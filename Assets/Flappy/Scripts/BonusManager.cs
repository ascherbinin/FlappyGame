using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager: MonoBehaviour
{
    public GameObject gemBonusPrefab;                                 //The column game object.
    public GameObject coinBonusPrefab;                                 //The column game object.
    public float spawnRate = 3f;                                    //How quickly columns spawn.
    public float bonusYMin = -0.5f;                                   //Minimum y value of the column position.
    public float bonusYMax = 4f;         //Maximum y value of the column position.

    private List<GameObject> bonusesList = new List<GameObject>();
    private GameObject goBaseBonus;
    private float spawnXPosition = 22f;
    private float timeSinceLastSpawned;
    private const float GEM_CHANCE = 1f;
    private const float MID_Y = 1.7f;

    void Start()
    {
        timeSinceLastSpawned = 0f;
        goBaseBonus = new GameObject();
        goBaseBonus.name = "Bonuses";
    }

    //This spawns columns as long as the game is not over.
    void Update()
    {
        if (GameControl.instance.GameState == State.Play)
        {
            timeSinceLastSpawned += Time.deltaTime;
            if (GameControl.instance.gameOver == false && timeSinceLastSpawned >= spawnRate)
            {

                timeSinceLastSpawned = 0f;

                //Set a random y position for the column
                float spawnYPositionTop = Random.Range(MID_Y + 0.5F, bonusYMax);
                float spawnYPositionBot = Random.Range(bonusYMin, MID_Y - 0.5F);
                bool isGem = Random.Range(0, 10) > GEM_CHANCE;
                var nextSpawnPosTop = new Vector2(spawnXPosition, spawnYPositionTop);
                GameObject bonusTop = isGem ? Instantiate(coinBonusPrefab, nextSpawnPosTop, Quaternion.identity) : Instantiate(gemBonusPrefab, nextSpawnPosTop, Quaternion.identity);
                bonusTop.transform.parent = goBaseBonus.transform;
                bonusesList.Add(bonusTop);

                var nextSpawnPosBot = new Vector2(spawnXPosition, spawnYPositionBot);
                GameObject bonusBot = isGem ? Instantiate(coinBonusPrefab, nextSpawnPosBot, Quaternion.identity) : Instantiate(gemBonusPrefab, nextSpawnPosBot, Quaternion.identity);
                bonusBot.transform.parent = goBaseBonus.transform;
                bonusesList.Add(bonusBot);
            }
        }
    }

}
