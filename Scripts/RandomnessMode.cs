using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomnessMode : MonoBehaviour
{
    MainScript MScript;
    private bool extraTiles;
    private GameObject[] tiles;
    private int levelProgressionRate;

    delegate void AddTiles();
    AddTiles addTiles;
    delegate void TilesForRandomTilesMode(int counter);
    TilesForRandomTilesMode instantiateTilesForRandomTilesMode;

    delegate void StartSpawningRandomTiles(int counter);
    StartSpawningRandomTiles spawningRandomTiles;

    private int addfrequenceO;
    private int additionalFrequenceOleft;
    private GameObject additionalTileP;
    private int addfrequenceP;
    private int additionalFrequencePleft;

    private GameObject additionalTileO;

    public void StartRandomness(List<int> frequences)
    {
        MScript = FindObjectOfType<MainScript>();
        instantiateTilesForRandomTilesMode = delegate {};
        tiles = MScript.tiles;
        extraTiles = MScript.extraTiles;
        levelProgressionRate = MScript.levelProgressionRate;

        spawningRandomTiles = beginSpawningRandomTiles;

        additionalTileO = tiles[3];
        addTiles += addTileO;

        if (!extraTiles)
        {
            frequences.Add(32);
        }
        else {
            frequences.Add(32);
            frequences.Add(32);

            additionalTileP = tiles[4];
            addTiles += addTileP;
        }

        addTiles();
    }


    public void makeRandomTiles(int counter) {
        instantiateTilesForRandomTilesMode(counter);
        spawningRandomTiles(counter);
    }


    private void beginSpawningRandomTiles(int counter)
    {
        if (counter > 5 * levelProgressionRate && extraTiles)
        {
            spawningRandomTiles = doNothing;
            instantiateTilesForRandomTilesMode += instantiateRandomTilesWithExtra;
            return;
        }
        if (counter > 4 * levelProgressionRate)
        {
            instantiateTilesForRandomTilesMode -= instantiateRandomTilesWithoutExtra;
            instantiateTilesForRandomTilesMode += instantiateRandomTilesWithoutExtra;
        }
    }
    private void doNothing(int counter) { }

    private void instantiateRandomTilesWithoutExtra(int counter)
    {
        if (counter % 32 != 0 && counter % addfrequenceO == 0)
        {
            if (additionalFrequenceOleft > 0)
            {
                instantiateAdditionalTileO();
            }
            else
            {
                addTileO();
            }
        }
    }

    private void instantiateRandomTilesWithExtra(int counter)
    {
        if (counter % 32 != 0 && counter % addfrequenceP == 0)
        {
            if (additionalFrequencePleft > 0)
            {
                instantiateAdditionalTileP();
            }
            else
            {
                addTileP();
            }
        }
    }

    public void addTileO()
    {
        addfrequenceO = (int)Mathf.Pow(2, Random.Range(0, 4));
        additionalFrequenceOleft = Random.Range(2, 7);
        //Debug.Log($"Tile O added. addfrequenceO:{addfrequenceO}; additionalFrequenceOleft:{additionalFrequenceOleft}");
    }
    private void instantiateAdditionalTileO()
    {
        Instantiate(additionalTileO);
        additionalFrequenceOleft -= 1;
    }

    private void addTileP()
    {
        addfrequenceP = (int)Mathf.Pow(2, Random.Range(0, 4));
        additionalFrequencePleft = Random.Range(2, 7);
        //Debug.Log($"Tile P added. addfrequenceP:{addfrequenceP}; additionalFrequencePleft:{additionalFrequencePleft}");
    }
    private void instantiateAdditionalTileP()
    {
        Instantiate(additionalTileP);
        additionalFrequencePleft -= 1;
    }
}
