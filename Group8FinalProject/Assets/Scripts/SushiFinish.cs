using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SushiFinish : Interactable
{
    public override void Update()
    {
        base.Update();

        canSeeVisual = playerController.heldIngredients.Count > 0 && canMakeAnyOrder() != null;
    }

    public override void Interact()
    {         
        OrderController.OrderObject completedOrder = canMakeAnyOrder();
        
        playerController.ClearHeldIngredients();
        playerController.PickupIngredient(completedOrder.order.product);
    }


    public OrderController.OrderObject canMakeAnyOrder()
    {
        for(int i = 0; i < orderController.currentOrders.Count; i++)
        {
            if (IngredientMatch(orderController.currentOrders[i].order.ingredients, playerController.heldIngredients))
            {
                return orderController.currentOrders[i];
            }
        }

        return null;
    }

    public bool IngredientMatch(Ingredient[] recipeIngreds, List<Ingredient> invenIngreds)
    {
        int count = 0;
        for(int i = 0; i < recipeIngreds.Length; i++)
        {
            if(invenIngreds.Contains(recipeIngreds[i]))
            {
                count++;
            }
        }

        return count == recipeIngreds.Length;
    }
}
