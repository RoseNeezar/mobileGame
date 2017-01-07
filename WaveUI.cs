using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class WaveUI : MonoBehaviour {


    [SerializeField]
    WaveSpawner spawner;
    [SerializeField]
    Animator waveAnim;
    [SerializeField]
    Text waveCountdownText;
    [SerializeField]
    Text waveCountText;

    private WaveSpawner.SpawnState previousState;
    WaveSpawner waves;
	// Use this for initialization
	void Start ()
    {
	    if(spawner == null)
        {
            Debug.LogError("no spawner referenced");
            this.enabled = false;
        }
        if (spawner == null)
        {
            Debug.LogError("no waveAnim referenced");
            this.enabled = false;
        }
        if (spawner == null)
        {
            Debug.LogError("no waveCountdownText referenced");
            this.enabled = false;
        }
        if (spawner == null)
        {
            Debug.LogError("no waveCountText referenced");
            this.enabled = false;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        //if (!waves.isLevelComplete)
            switch (spawner.State )
            {
                case WaveSpawner.SpawnState.COUNTING:
                    UpdateCountingUI();
                    break;
                case WaveSpawner.SpawnState.SPAWNING:
                    UpdateSpawningUI();
                    break;
            }
            previousState = spawner.State;
        
	}

    void UpdateCountingUI()
    {
        if (previousState != WaveSpawner.SpawnState.COUNTING)
        {
            waveAnim.SetBool("WaveIncoming", false);
                waveAnim.SetBool("WaveCountdown", true);
                Debug.Log("Counting");

        }
        waveCountdownText.text = ((int)spawner.WaveCountdown).ToString();   
    }

    void UpdateSpawningUI()
    {
        if (previousState != WaveSpawner.SpawnState.SPAWNING)
        {
            waveAnim.SetBool("WaveCountdown", false);
            waveAnim.SetBool("WaveIncoming", true);
            waveCountText.text = spawner.NextWave.ToString();
            Debug.Log("Spawning");
        }   
    }
}
