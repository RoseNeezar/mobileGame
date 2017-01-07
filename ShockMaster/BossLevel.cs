using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class BossLevel : MonoBehaviour {

    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }
    public GameObject levelOver;
    public GameObject effect;
   // public GameObject wavesUI;
    public Wave[] waves;
    private int nextWave = 0;

    public int NextWave
    {
        get { return nextWave + 1; }
    }

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    public float WaveCountdown
    {
        get { return waveCountdown; }
    }

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;

    public SpawnState State
    {
        get { return state; }
    }
    //things need tweaking
    public string levelToLoad;
    ScoreManager chromosomes;
    public string levelTag;
    protected string currentLevela;
    protected int worldIndex;
    protected int levelIndex;
    //reference to star images
    private GameObject star1;
    private GameObject star2;
    private GameObject star3;
    private GameObject star4;
    public GameObject controlsUI;
    public PlayerControl player;
  //  public GameObject waveUI;
    //reference to next button
    private GameObject buttonNext;
    protected string currentLevel;
    ShockHealth shock;


    public bool isLevelComplete;
    //timer text reference
    //public Text timerText;
    //time passed since start of level
    //protected float totalTime = 0f;


    void Start()
    {
        shock = FindObjectOfType<ShockHealth>();
        // Time.timeScale = 1f;
        //set the level complete to false on start of level
        isLevelComplete = false;
        //get the star images
        star1 = GameObject.Find("star1");
        star2 = GameObject.Find("star2");
        star3 = GameObject.Find("star3");
        star4 = GameObject.Find("star4");
        //get the next button
        buttonNext = GameObject.Find("Next");
        //disable the image component of all the star images
        star1.GetComponent<Image>().enabled = false;
        star2.GetComponent<Image>().enabled = false;
        star3.GetComponent<Image>().enabled = false;
        star4.GetComponent<Image>().enabled = false;
        //disable the next button
        buttonNext.SetActive(false);
        //save the current level name
        currentLevel = Application.loadedLevelName;
        waveCountdown = timeBetweenWaves;
    }

    void Update()
    {

        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive() && shock.isDead)
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING && !isLevelComplete && shock.enemyHealth < 100)//&& !(nextWave +1 > waves.Length-1))
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }
    public void LoadLevel()
    {
        //1 leveltag means unlock
        PlayerPrefs.SetInt(levelTag, 1);
        Application.LoadLevel(levelToLoad);
    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completed!");

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            controlsUI.SetActive(false);
            // player.gameObject.SetActive(false);
            isLevelComplete = true;
            buttonNext.SetActive(true);
          //  waveUI.SetActive(false);

            if (ScoreManager.score > 0)
            {
                star4.GetComponent<Image>().enabled = true;
                UnlockLevels(4);
            }
            
            Debug.Log("ALL WAVES COMPLETE! Looping...");
        }
        else
        {

            nextWave++;
        }
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave: " + _wave.name);
        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        if (_enemy == null && spawnPoints == null)
        {
            return;
            Debug.Log("No enemy needed");
        }
        else
        {
            Debug.Log("Spawning Enemy: " + _enemy.name);

            Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(_enemy, _sp.position, _sp.rotation);
            Instantiate(effect, _sp.position, _sp.rotation);
        }
    }
    
    
    public void OnClickButton()
    {
        //load the World1 level 
        Application.LoadLevel("World1");

    }


    protected void UnlockLevels(int stars)
    {
        for (int i = 0; i < LockLevel.worlds; i++)
        {
            for (int j = 1; j < LockLevel.levels; j++)
            {
                if (currentLevel == "Level" + (i + 1).ToString() + "." + j.ToString())
                {
                    worldIndex = (i + 1);
                    levelIndex = (j + 1);
                    PlayerPrefs.SetInt("level" + worldIndex.ToString() + ":" + levelIndex.ToString(), 1);
                    //check if the current stars value is less than the new value
                    if (PlayerPrefs.GetInt("level" + worldIndex.ToString() + ":" + j.ToString() + "stars") < stars)
                        //overwrite the stars value with the new value obtained
                        PlayerPrefs.SetInt("level" + worldIndex.ToString() + ":" + j.ToString() + "stars", stars);
                }
            }
        }

    }


}
