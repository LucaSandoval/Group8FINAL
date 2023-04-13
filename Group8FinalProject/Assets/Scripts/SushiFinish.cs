using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SushiFinish : Interactable
{
    public override void Update()
    {
        base.Update();

        canSeeVisual = playerController.heldIngredients.Count > 0;
    }

    public override void Interact()
    {
        playerController.ClearHeldIngredients();
    }
}
