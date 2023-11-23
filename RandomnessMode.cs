using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomnessMode : MonoBehaviour
{
    MainScript MScript;
    GenerateAsyncBeat GABScript;
    private bool extraTiles;
    private GameObject[] tiles;
    private int levelProgressionRate;

    delegate void AddTiles();
    AddTiles addTiles;
    delegate void TilesForRandomTilesMode();
    TilesForRandomTilesMode instantiateTilesForRandomTilesMode;

    delegate void StartSpawningRandomTiles();
    StartSpawningRandomTiles spawningRandomTiles;

    private int addfrequenceO;
    private int additionalFrequenceOleft;
    private GameObject additionalTileP;
    private int addfrequenceP;
    private int additionalFrequencePleft;

    private GameObject additionalTileO;

    public void StartRandomness()
    {
        GABScript = FindObjectOfType<GenerateAsyncBeat>();
        MScript = FindObjectOfType<MainScript>();
        instantiateTilesForRandomTilesMode = delegate { };
        tiles = MScript.tiles;
        extraTiles = MScript.extraTiles;
        levelProgressionRate = MScript.levelProgressionRate;

        spawningRandomTiles = beginSpawningRandomTiles;

        additionalTileO = tiles[3];
        addTiles += addTileO;

        if (!extraTiles)
        {
            GABScript.frequences.Add(32);
        }
        else
        {
            GABScript.frequences.Add(32);
            GABScript.frequences.Add(32);

            additionalTileP = tiles[4];
            addTiles += addTileP;
        }

        addTiles();
    }


    public void makeRandomTiles()
    {
        instantiateTilesForRandomTilesMode();
        spawningRandomTiles();
    }


    private void beginSpawningRandomTiles()
    {
        if (GABScript.counter > 5 * levelProgressionRate && extraTiles)
        {
            spawningRandomTiles = doNothing;
            instantiateTilesForRandomTilesMode += instantiateRandomTilesWithExtra;
            return;
        }
        if (GABScript.counter > 4 * levelProgressionRate)
        {
            instantiateTilesForRandomTilesMode -= instantiateRandomTilesWithoutExtra;
            instantiateTilesForRandomTilesMode += instantiateRandomTilesWithoutExtra;
        }
    }
    private void doNothing() { }

    private void instantiateRandomTilesWithoutExtra()
    {
        if (GABScript.counter % 32 != 0 && GABScript.counter % addfrequenceO == 0)
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

    private void instantiateRandomTilesWithExtra()
    {
        if (GABScript.counter % 32 != 0 && GABScript.counter % addfrequenceP == 0)
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
    }
    private void instantiateAdditionalTileP()
    {
        Instantiate(additionalTileP);
        additionalFrequencePleft -= 1;
    }
}
