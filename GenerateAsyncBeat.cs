using System.Collections.Generic;
using UnityEngine;

public class GenerateAsyncBeat : MonoBehaviour
{
	delegate void ModeTaskInStart();
	ModeTaskInStart modeTaskStart;

	private delegate void SpawnNextSetDelegate();
	private SpawnNextSetDelegate spawnNextSet;

	private delegate void nextLevelDelegate(int level);
	private nextLevelDelegate nextLevel;

	public delegate void checkMistakesDelegate();
	public checkMistakesDelegate checkMistakes;

	public List<int> frequences;
	public int counter = 1;

	private List<GameObject> mainTiles;
	private List<int> possibleFreqs;
	public int level = 1;
	private bool extraTiles;
	private int levelProgressionRate;
	private float spawnSpeed;
	private GameObject[] tiles;
	PlaySound PSScript;
	MainScript MScript;
	Darken DScript;

	BlackoutGMode BOMScript;
	RandomnessMode RMScript;

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

		BOMScript = FindObjectOfType<BlackoutGMode>();
		RMScript = FindObjectOfType<RandomnessMode>();
		DScript = FindObjectOfType<Darken>();


		frequences = new List<int> { };

		if (!extraTiles)
		{
			mainTiles = new List<GameObject> { tiles[2], tiles[1], tiles[3] };  //space, w, o
			possibleFreqs = new List<int> {4, 8, 16};
			addFreq();
			frequences.Add(2);
		}
		else {

			mainTiles = new List<GameObject> { tiles[0], tiles[1], tiles[2], tiles[3], tiles[4] };
			possibleFreqs = new List<int> {4, 4, 8, 8, 16};
			addFreq();
			frequences.Add(2);
			addFreq();
		}


		if (PlayerPrefs.GetInt("gm") == 0)
		{
			modeTaskStart = RMScript.StartRandomness;
			spawnNextSet = spawnNextSetRandomnessMode;
			nextLevel = PSScript.nextLevelRandomness;
		}
		else
		{
			modeTaskStart = BOMScript.startBlackout;
			spawnNextSet = spawnNextSetBlackoutMode;
			nextLevel = PSScript.nextLevelBlackout;
		}

		modeTaskStart();

		bubbleSort();
	}

	public void addFreq() {
		int i = Random.Range(0, possibleFreqs.Count);
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
		InvokeRepeating("InvokeSpawnNextSet", 0, spawnSpeed);
		InvokeRepeating("levelUp", levelProgressionRate, levelProgressionRate);
	}


	private void spawnNextSetRandomnessMode()
	{
		for (int i = 0; i < level; i++)
		{
			if (counter % frequences[i] == 0)
			{
				Instantiate(mainTiles[i]);
			}
		}
		RMScript.makeRandomTiles();
		counter += 1;
	}

	private void InvokeSpawnNextSet()
	{
		spawnNextSet?.Invoke();
	}

	private void spawnNextSetBlackoutMode()
	{
		for (int i = 0; i < level; i++)
		{
			if (counter % frequences[i] == 0)
			{
				Instantiate(mainTiles[i]);
			}
		}
		counter += 1;
		checkMistakes();
	}

    public void startCheckingForMistakes()
    {
        Debug.Log("Check Started");
        checkMistakes = checkMistakesWhenAllTiles;
    }

    private void checkMistakesWhenAllTiles() {
        if (PSScript.missedTilesNumber >= 4)
        {
			Debug.Log("BAD");
            PSScript.missedTilesNumber = 0;
            DScript.lightsOn();
        }
        if (PSScript.missedTilesNumber <= -3)
        {
            Debug.Log("GOOD");
            PSScript.missedTilesNumber = 0;
            DScript.lightsOff();
        }

    }

	void levelUp()
	{
		level += 1;
		nextLevel(level);
	}

	public void toInitValues()
	{
		PSScript.counter = 3;
		level = 1;
		counter = 1;
		PSScript.score = 0;
		PSScript.maxScore = 0;
		PSScript.missedTilesNumber = 0;
	}
}
