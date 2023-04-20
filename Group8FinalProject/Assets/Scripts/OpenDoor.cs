using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : Interactable
{
    public override void Update()
    {
        base.Update();

        canSeeVisual = true;
    }
        
    public override void Interact()
    {
        soundPlayer.PlaySound("Locked");
        uiController.ShowResults("Door Locked", 1f);
    }
}
