using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientDepo : Interactable
{
    public Ingredient ingredient;

    public override void Update()
    {
        base.Update();

        canSeeVisual = !playerController.PlayerHasIngredient(ingredient);
    }

    public override void Interact()
    {
        playerController.PickupIngredient(ingredient);
    }
}
