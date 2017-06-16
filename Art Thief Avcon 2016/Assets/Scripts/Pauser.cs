using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Pauser : MonoBehaviour
{
    public static bool paused;

    static Pauser p;
    static Material[] iconMaterials;
    static PlayerIcon[] playerIcons;
    static GameObject pauseText;

    static float timeUntilPlay;
    static bool waitTime = false;

    // Use this for initialization
    void Start()
    {
        paused = true;
        GetPlayerIcons();
        SetIconMaterials();
        pauseText = GameObject.FindGameObjectWithTag("Pause Text");
        if (p == null)
        {
            p = this;
        }
        else
        {
            Destroy(p.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (paused)
        {
            if (CheckIfReady() && !waitTime)
            {
                timeUntilPlay = Time.time + 3;
                waitTime = true;
            }

            if (!CheckIfReady() && waitTime == true)
            {
                waitTime = false;
                paused = true;
                foreach (PlayerIcon p in playerIcons)
                {
                    p.SetReady(false);
                }
            }
            if (waitTime && timeUntilPlay <= Time.time)
            {
                paused = false;
                timeUntilPlay = 0;
            }
        }

        if (paused)
            pauseText.SetActive(true);
        else
            pauseText.SetActive(false);
    }

    void GetPlayerIcons()
    {
        PlayerIcon[] go = GameObject.FindObjectsOfType<PlayerIcon>();
        if (go != null)
        {
            playerIcons = new PlayerIcon[go.Length];

            foreach (PlayerIcon g in go)
            {
                playerIcons[g.GetPlayerNumber() - 1] = g;
            }
        }
    }

    void SetIconMaterials()
    {
        if (iconMaterials != null)
        {
            for (int i = 0; i < playerIcons.Length; i++)
                playerIcons[i].GetComponent<Image>().material = iconMaterials[i];
        }
    }

    public static void SaveMaterials()
    {
        iconMaterials = new Material[playerIcons.Length];
        for (int i = 0; i < playerIcons.Length; i++)
        {
            iconMaterials[i] = playerIcons[i].GetComponent<Image>().material;
        }
    }

    bool CheckIfReady()
    {
        foreach (PlayerIcon p in playerIcons)
        {
            if (!p.IsReady())
                return false;
        }
        return true;
    }

    public static void ResetTimer()
    {
        timeUntilPlay = Time.time + 3;
    }
}
