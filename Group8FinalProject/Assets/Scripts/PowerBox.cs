using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBox : Interactable
{
    public override void Update()
    {
        base.Update();

        canSeeVisual = playerController.PlayerHasIngredient(Ingredient.steelBeam) && OrderController.sushiGameActive;
    }

    public override void Interact()
    {
        soundPlayer.PlaySound("Sparks");
        playerController.RemoveIngredient(Ingredient.steelBeam);
        OrderController.sushiGameActive = false;
        sequenceController.ScarySequence();
        sequenceController.lockedDoor.SetActive(false);
        sequenceController.unlockedDoor.SetActive(true);
    }
}
