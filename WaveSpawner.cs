using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
using UnityEngine.UI;
public class WaveSpawner : MonoBehaviour {

    public enum SpawnState { SPAWNING, WAITING, COUNTING };
    LifeManager lifeSystem;
    [SerializeField]
    string gameID = "1232146";
    [System.Serializable]
    public class Wave
    {     
        public string name;
        public Transform enemy;
        public Transform enemy1;
        public Transform enemy2;
        public Transform enemy3;
        public Transform enemy11;//blob
        public Transform enemy12;//blob
        public int count;
        public float rate;
    }
    public GameObject levelOver;
    public GameObject effect;
    public GameObject wavesUI;
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
    public GameObject waveUI;
    //reference to next button
    private GameObject buttonNext;
    protected string currentLevel;
    public Transform[] turretLocation;
    public Transform[] FlyingSpawn;
    
   
    public bool isLevelComplete;
    //timer text reference
    //public Text timerText;
    //time passed since start of level
    //protected float totalTime = 0f;
    void Awake()
    {
        Advertisement.Initialize(gameID, true);
    }

    void Start()
    {
        lifeSystem = FindObjectOfType<LifeManager>();
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

        if (spawnPoints.Length == 0 && turretLocation.Length == 0)
        {
            Debug.LogError("No spawn points referenced.");
        }

        waveCountdown = timeBetweenWaves;
    }

    void Update()
    {

        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
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
            if (state != SpawnState.SPAWNING && !isLevelComplete )//&& !(nextWave +1 > waves.Length-1))
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
            waveUI.SetActive(false);
          
            if (ScoreManager.score > 3000)
            {
                star4.GetComponent<Image>().enabled = true;
                UnlockLevels(4);
            }
            if (ScoreManager.score >2000 && ScoreManager.score <3000)
            {
                star3.GetComponent<Image>().enabled = true;
                UnlockLevels(3);
            }
            if (ScoreManager.score < 2000 && ScoreManager.score > 1000 )
            {
                star2.GetComponent<Image>().enabled = true;
                UnlockLevels(2);
            }
            if (ScoreManager.score <= 1000)
            {
                star1.GetComponent<Image>().enabled = true;
                UnlockLevels(1);
            }
            // if(ScoreManager.score <= 50)
            //edit STAR CANVAS HERE
            // gameObject.SetActive(false);
            // wavesUI.SetActive(false);
            //// levelOver.SetActive(true);
            //Application.LoadLevel(levelToLoad);
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
            SpawnEnemy(_wave.enemy);//blob
            SpawnEnemy(_wave.enemy11);
            SpawnEnemy(_wave.enemy12);
            SpawnEnemy1(_wave.enemy1);//guardian
            SpawnEnemy2(_wave.enemy2);//turret
            SpawnEnemy3(_wave.enemy3);//flying
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
    void SpawnEnemy11(Transform _enemy11)
    {
        if (_enemy11 == null && spawnPoints == null)
        {
            return;
            Debug.Log("No enemy needed");
        }
        else
        {
            Debug.Log("Spawning Enemy: " + _enemy11.name);

            Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(_enemy11, _sp.position, _sp.rotation);
            Instantiate(effect, _sp.position, _sp.rotation);
        }
    }
    void SpawnEnemy12(Transform _enemy12)
    {
        if (_enemy12 == null && spawnPoints == null)
        {
            return;
            Debug.Log("No enemy needed");
        }
        else
        {
            Debug.Log("Spawning Enemy: " + _enemy12.name);

            Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(_enemy12, _sp.position, _sp.rotation);
            Instantiate(effect, _sp.position, _sp.rotation);
        }
    }
    void SpawnEnemy1(Transform _enemy1)
    {
        if (_enemy1 == null && spawnPoints ==null)
        {
            return;
            Debug.Log("No enemy needed");
        }
        else
        {
            Debug.Log("Spawning Enemy: " + _enemy1.name);

            Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(_enemy1, _sp.position, _sp.rotation);
            Instantiate(effect, _sp.position, _sp.rotation);
        }
    }
    void SpawnEnemy2(Transform _enemy2)
    {
         
        if (_enemy2 == null && turretLocation == null)
        {
            return;
            Debug.Log("No enemy needed");
        }
        else
        {
            Debug.Log("Spawning Enemy: " + _enemy2.name);

            Transform _sp1 = turretLocation[Random.Range(0, turretLocation.Length)];
            Instantiate(_enemy2, _sp1.position, _sp1.rotation);
            Instantiate(effect, _sp1.position, _sp1.rotation);
        }
    }
    void SpawnEnemy3(Transform _enemy3)
    {
        
        if (_enemy3 == null && FlyingSpawn == null)
        {
            Debug.Log("No enemy needed");
            return;
        }
        else
        {
            Debug.Log("Spawning Enemy: " + _enemy3.name);

            Transform _sp1 = FlyingSpawn[Random.Range(0, FlyingSpawn.Length)];
            Instantiate(_enemy3, _sp1.position, _sp1.rotation);
            Instantiate(effect, _sp1.position, _sp1.rotation);
        }
    }
    public void OnClickButton()
    {
        //load the World1 level 
        //put AD MOB
        //  AdManager.Instance.ShowVideo();
        ShowAd();
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
    public void ShowAd()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show("rewardedVideo", new ShowOptions() { resultCallback = HandleAdResult });
        }
    }
    private void HandleAdResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                lifeSystem.startingLife = 2;
                break;
            case ShowResult.Skipped:
                Debug.Log("Player not watch");
                break;
            case ShowResult.Failed:
                Debug.Log("Player no internet?");
                break;
        }
    }



}
