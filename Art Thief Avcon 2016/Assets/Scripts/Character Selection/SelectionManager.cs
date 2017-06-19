using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour {

    public PlayerBox[] playerBoxes;
    public Image[] keyboardIcons;
    public bool[] controlSchemesInUse;
    public bool[] colorsInUse;
    CharacterHolder info;

    // Use this for initialization
    void Start ()
    {
        controlSchemesInUse = new bool[4];
        colorsInUse = new bool[4];
        info = GameObject.FindObjectOfType<CharacterHolder>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //TODO: Assign control scheme to player box

        //Set true or not
		for(int i = 0; i < 4; i++)
        {
            //Input checks
            if (Input.GetKeyDown(CharacterHolder.keyboardControls[i].jump))
            {
                //Assign keyboard controls to a player slot
                if (!controlSchemesInUse[i])
                {
                    int slot = NextAvailableSlot();
                    if (slot != 4)
                    {
                        controlSchemesInUse[i] = true;
                        playerBoxes[slot].active = true;
                        playerBoxes[slot].usingKeyboard = true;
                        playerBoxes[slot].controlSchemeID = i;
                        playerBoxes[slot].colorID = NextAvailableColor();
                        colorsInUse[NextAvailableColor()] = true;
                    }
                }

            }
            else if (Input.GetKeyDown(CharacterHolder.keyboardControls[i].paint))
            {
                PlayerBox box = null;
                foreach (PlayerBox temp in playerBoxes)
                {
                    if (temp.active && temp.controlSchemeID == i)
                    {
                        box = temp;
                    }
                }
                if (box != null)
                {
                    colorsInUse[box.colorID] = false;
                    box.colorID = -1;
                    box.usingKeyboard = false;
                    box.active = false;
                }
                controlSchemesInUse[i] = false;
            }
        }

        foreach(Image img in keyboardIcons)
        {
            img.color = Color.white;
        }
        foreach(PlayerBox box in playerBoxes)
        {
            if (box.active)
            {
                keyboardIcons[box.controlSchemeID].color = Color.grey;
            }
        }
	}

    public int NewColorID(int currentID, bool up)
    {
        colorsInUse[currentID] = false;
        int tempID = currentID;
        do
        {
            if (up)
            {
                if (tempID >= 3)
                {
                    tempID = 0;
                }
                else
                {
                    tempID = tempID + 1;
                }
            }
            else
            {
                if (tempID <= 0)
                {
                    tempID = 3;
                }
                else
                {
                    tempID = tempID - 1;
                }
            }

        } while (colorsInUse[tempID]);

        return tempID;

    }

    int NextAvailableColor()
    {
        for (int i = 0; i < 4; i++)
        {
            if (!colorsInUse[i])
                return i;
        }

        return 4;
    }

    int NextAvailableSlot()
    {
        for (int i = 0; i < 4; i++)
        {
            if (!playerBoxes[i].active)
                return i;
        }

        return 4;
    }
}
