using UnityEngine;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public int enemiesAlive = 0;

    public bool[] pagesCollected = { false, false, false, false, false, false, false, false, false, false };

    public GameObject[] pages;
    

    public int maxEnemies = 1;
    private float timer = 0;

    public GameObject boss;
    private bool bossAlive = false;

    public AudioMixer audioMixer;
    float musicVolume = 0;
    
    public GameObject bossMusic;
    float bossVolume = -20;

    void Update() 
    {
        timer += (3 * Time.deltaTime);
        if (timer > 10 && !bossAlive && maxEnemies < 30)
        {
            maxEnemies++;
            timer = 0;
        }
        if (pagesCollected[0] && pagesCollected[1] && pagesCollected[2] && pagesCollected[3] && pagesCollected[4] && pagesCollected[5] && pagesCollected[6] && pagesCollected[7] && pagesCollected[8] && pagesCollected[9] && !bossAlive) 
        {
            bossAlive = true;
            Instantiate(boss);
        }

        for (int i = 0; i < pagesCollected.Length; i++)
        {
            pages[i].SetActive(pagesCollected[i]);
        }
        if (bossAlive) 
        { 
            bossMusic.SetActive(true);
            if (bossVolume < 0) { bossVolume += (5 * Time.deltaTime); }
            if (musicVolume > -80) { musicVolume -= (5 * Time.deltaTime); }
        }
        audioMixer.SetFloat("BossVolume", bossVolume);
        audioMixer.SetFloat("MusicVolume", musicVolume);

    }
}
