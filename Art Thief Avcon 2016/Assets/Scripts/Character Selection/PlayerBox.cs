using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBox : MonoBehaviour {

    public bool active;
    public bool usingKeyboard;
    public int playerNumber;
    public int controlSchemeID;
    public int colorID;
    [Tooltip("Please don't change this")]
    public Sprite[] colorSprites;
    public Sprite greySprite;
    static SelectionManager manager;

    CharacterHolder.PlayerStats controls;
    CharacterHolder info;
    public Image characterIcon;

	// Use this for initialization
	void Start ()
    {
        info = GameObject.FindObjectOfType<CharacterHolder>();
        if (manager == null)
        {
            manager = GameObject.FindObjectOfType<SelectionManager>();
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        info.SetPlayerStats(playerNumber, active, controlSchemeID, colorID);

        if (active && usingKeyboard && controlSchemeID >= 0 && controlSchemeID <= 3)
        {
            KeyboardChecks();
        }
        if (active)
        {
            characterIcon.sprite = colorSprites[colorID];
            manager.colorsInUse[colorID] = true;
        }
        else
        {
            characterIcon.sprite = greySprite;
            colorID = -1;
        }
	}

    void KeyboardChecks()
    {
        if (Input.GetKeyDown(CharacterHolder.keyboardControls[controlSchemeID].jump))
        {
            manager.colorsInUse[colorID] = false;
            colorID = manager.NewColorID(colorID, true);
        }
        else if (Input.GetKeyDown(CharacterHolder.keyboardControls[controlSchemeID].down))
        {
            manager.colorsInUse[colorID] = false;
            colorID = manager.NewColorID(colorID, false);
        }
    }
}
