using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHolder : MonoBehaviour {

    static CharacterHolder singleton;

    public PlayerStats player1;
    public PlayerStats player2;
    public PlayerStats player3;
    public PlayerStats player4;

    public ControlScheme keyboard1;
    public ControlScheme keyboard2;
    public ControlScheme keyboard3;
    public ControlScheme keyboard4;

    public static ControlScheme[] keyboardControls;

    [System.Serializable]
    public class PlayerStats
    {
        public bool active;
        public bool usingKeyboard;
        public int keyboardID;
        public int joypadID;
        [Tooltip("0: Blue, 1: Red, 2: Yellow, 3: Green")]
        public int colorID = -1;
    }

    [System.Serializable]
    public class ControlScheme
    {
        public KeyCode jump;
        public KeyCode down;
        public KeyCode left;
        public KeyCode right;
        public KeyCode paint;
        public KeyCode back;
    }


	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(this);
		if (singleton == null)
        {
            singleton = this;
            keyboardControls = new ControlScheme[4]
            {
                keyboard1,
                keyboard2,
                keyboard3,
                keyboard4
            };
        }
        else if (singleton != this)
        {
            Destroy(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    public void SetPlayerStats(int _playerNumber, bool _active, int _controlSchemeID, int _colorID)
    {
        switch (_playerNumber)
        {
            case 1:
                player1.active = _active;
                player1.keyboardID = _controlSchemeID;
                player1.colorID = _colorID;
                break;
            case 2:
                player2.active = _active;
                player2.keyboardID = _controlSchemeID;
                player2.colorID = _colorID;
                break;
            case 3:
                player3.active = _active;
                player3.keyboardID = _controlSchemeID;
                player3.colorID = _colorID;
                break;
            case 4:
                player4.active = _active;
                player4.keyboardID = _controlSchemeID;
                player4.colorID = _colorID;
                break;
            default:
                Debug.Log("Invalid player number, check the Player Boxes");
                break;
        }
    }
}
