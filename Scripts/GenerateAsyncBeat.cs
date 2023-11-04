using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class GenerateAsyncBeat : MonoBehaviour
{
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

    private List<int> frequences;
    private int counter = 1;

    private List<GameObject> mainTiles;
    private List<int> possibleFreqs;
    private int level = 1;
    private bool extraTiles;
    private int levelProgressionRate;
    private float spawnSpeed;
    private GameObject[] tiles;
    PlaySound PSScript;
    MainScript MScript;

    //  W, O, P = small || Q, space = big

    //      *************** BEAT GENERATION PART ***************
    public void start()
    {
        MScript = FindObjectOfType<MainScript>();
        spawnSpeed = MScript.SpawnSpeed;
        tiles = MScript.tiles;
        levelProgressionRate = MScript.levelProgressionRate;
        extraTiles = MScript.extraTiles;
        PSScript = FindObjectOfType<PlaySound>();
        spawningRandomTiles = beginSpawningRandomTiles;
        instantiateTilesForRandomTilesMode = doNothing;


        frequences = new List<int> { };

        additionalTileO = tiles[3];
        addTiles += addTileO;
        if (!extraTiles)
        {
            mainTiles = new List<GameObject> { tiles[2], tiles[1], tiles[3] };  //space, w, o
            possibleFreqs = new List<int> { 2, 4, 8, 16 };
            addFreq(1);
            frequences.Add(2);
            frequences.Add(32);
        }
        else {
            additionalTileP = tiles[4];
            addTiles += addTileP;
            mainTiles = new List<GameObject> { tiles[0], tiles[1], tiles[2], tiles[3], tiles[4] };
            possibleFreqs = new List<int> { 2, 4, 8, 16 };
            addFreq(1);
            frequences.Add(2);
            addFreq(1);
            frequences.Add(32);
            frequences.Add(32);
        }

        bubbleSort();
        addTiles();

    }
    private void addFreq(int min) {
        int i = Random.Range(min, possibleFreqs.Count);
        frequences.Add(possibleFreqs[i]);
        possibleFreqs.RemoveAt(i);
    }

    private void bubbleSort()
    {
        int n = frequences.Count;
        int i, j, temp;
        GameObject tempGO;
        bool swapped;
        for (i = 0; i < n - 1; i++)
        {
            swapped = false;
            for (j = 0; j < n - i - 1; j++)
            {
                if (frequences[j] > frequences[j + 1])
                {
                    temp = frequences[j];
                    tempGO = mainTiles[j];

                    frequences[j] = frequences[j + 1];
                    frequences[j + 1] = temp;

                    mainTiles[j] = mainTiles[j + 1];
                    mainTiles[j + 1] = tempGO;

                    swapped = true;
                }
            }
            if (swapped == false)
                break;
        }
    }

    //       LOOP PART 
    public void updating() {
        InvokeRepeating("spawnNextSet", 0, spawnSpeed);
        InvokeRepeating("nextLevel", levelProgressionRate, levelProgressionRate);
    }
    public void toInitValues() {
        PSScript.counter = 3;
        level = 1;
        counter = 1;
    }

    void spawnNextSet() {
        for (int i = 0; i < level; i++)
        {
            if (counter % frequences[i] == 0)
            {
                Instantiate(mainTiles[i]);
            }
        }

        instantiateTilesForRandomTilesMode();
        spawningRandomTiles();

        counter += 1;
    }

    private void beginSpawningRandomTiles() {
        if (counter > 8 * levelProgressionRate && extraTiles)
        {
            spawningRandomTiles = doNothing;
            instantiateTilesForRandomTilesMode += instantiateRandomTilesWithExtra;
        }
        if (counter > 4 * levelProgressionRate)
        {
            spawningRandomTiles = doNothing;
            instantiateTilesForRandomTilesMode += instantiateRandomTilesWithoutExtra;
        }
    }
    private void doNothing() { }

    private void instantiateRandomTilesWithoutExtra() {
        if (counter % 32 != 0 && counter % addfrequenceO == 0)
        {
            if (additionalFrequenceOleft > 0)
            {
                instantiateAdditionalTileO();
            }
            else
            {
                addTiles();
            }
        }
    }

    private void instantiateRandomTilesWithExtra()
    {
        if (counter % 32 != 0 && counter % addfrequenceP == 0)
        {
            if (additionalFrequencePleft > 0)
            {
                instantiateAdditionalTileP();
            }
            else
            {
                addTiles();
            }
        }
    }

    public void addTileO() {
        addfrequenceO = (int)Mathf.Pow(2, Random.Range(0, 4));
        additionalFrequenceOleft = Random.Range(2, 7);
    }
    private void instantiateAdditionalTileO() {
        Instantiate(additionalTileO);
        additionalFrequenceOleft -= 1;
    }

    private void addTileP(){
        addfrequenceP = (int)Mathf.Pow(2, Random.Range(0, 4));
        additionalFrequencePleft = Random.Range(2, 7);
    }
    private void instantiateAdditionalTileP(){
        Instantiate(additionalTileP);
        additionalFrequencePleft -= 1;
    }

    void nextLevel() {
        level += 1;
        PSScript.nextLevel(level);
    }
}
