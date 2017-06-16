using UnityEngine;
using System.Collections;


public class Timer : MonoBehaviour
{

    // Use this for initialization
    public int startingTime;
    public PlayerController[] players;
    public float timeRemaining;
    public UnityEngine.UI.Text countDown;
    public bool playersLost = false;
    public Animator playerAnim;
    public Animator playerAnim2;
    public Animator playerAnim3;
    public Animator playerAnim4;
    void Start()
    {
        timeRemaining = startingTime;
    }

    // Update is called once per frame
    void Update()
    { 
        if (!Pauser.paused && !WinDebug.gameOver)

    
        timeRemaining -= Time.deltaTime;

        countDown.text = Mathf.FloorToInt(timeRemaining / 60.0f).ToString() + ":" + Mathf.FloorToInt(timeRemaining % 60).ToString("D2");

        if (timeRemaining <= 0)
        {
            countDown.text = "Times Up!";
            timeRemaining = 0;
            playersLost = true;
            StartCoroutine(Coroutine());
            playerAnim.SetBool("IsLoser", true);
            playerAnim.SetBool("IsRunning", false);
            playerAnim.SetBool("IsWalking", false);
            playerAnim.SetBool("IsJumping", false);
            playerAnim.SetBool("IsPainting", false);

            playerAnim2.SetBool("IsLoser", true);
            playerAnim2.SetBool("IsRunning", false);
            playerAnim2.SetBool("IsWalking", false);
            playerAnim2.SetBool("IsJumping", false);
            playerAnim2.SetBool("IsPainting", false);

            playerAnim3.SetBool("IsLoser", true);
            playerAnim3.SetBool("IsRunning", false);
            playerAnim3.SetBool("IsWalking", false);
            playerAnim3.SetBool("IsJumping", false);
            playerAnim3.SetBool("IsPainting", false);

            playerAnim4.SetBool("IsLoser", true);
            playerAnim4.SetBool("IsRunning", false);
            playerAnim4.SetBool("IsWalking", false);
            playerAnim4.SetBool("IsJumping", false);
            playerAnim4.SetBool("IsPainting", false);
            Debug.Log("Draw");
        }
    }
    IEnumerator Coroutine()
    {
        PlayerIcon.StopWebcam();
        Time.timeScale = 0.6f;
        yield return new WaitForSeconds(3);
        Time.timeScale = 1;
        Pauser.SaveMaterials();
        Application.LoadLevel("DrawFinishScreen");
    }
}
