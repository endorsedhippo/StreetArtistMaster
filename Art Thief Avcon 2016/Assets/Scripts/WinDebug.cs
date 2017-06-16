using UnityEngine;
using System.Collections;

public class WinDebug : MonoBehaviour {

    int playerNumber;
    public PlayerController[] players;
    public Material[] winMaterials;
    public static bool gameOver = false;

    void Start()
    {
        gameOver = false;
    }
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player")
		{
            gameOver = true;
			Debug.Log ("Player wins");
            playerNumber = col.GetComponent<PlayerController>().playerNumber;
			StartCoroutine (Coroutine ());
            GetComponent<MeshRenderer>().material = winMaterials[playerNumber - 1];

            foreach(PlayerController p in players)
            {
                if (p.playerNumber == playerNumber)
                    p.SetAsWinner();
                else
                    p.SetAsLoser();
            }
		}
	}

	IEnumerator Coroutine()
	{
        PlayerIcon.StopWebcam();
		Time.timeScale = 0.6f;
		yield return new WaitForSeconds (3);
		Time.timeScale = 1;
        Pauser.SaveMaterials();
		Application.LoadLevel ("P" + playerNumber + "_FinishScreen");
	}


}
