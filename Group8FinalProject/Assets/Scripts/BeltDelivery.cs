using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltDelivery : Interactable
{
    public override void Update()
    {
        base.Update();

        canSeeVisual = playerController.heldIngredients.Count > 0 && NeededSushiInInven() != null;
    }

    public override void Interact()
    {
        OrderController.OrderObject completedOrder = NeededSushiInInven();
        playerController.RemoveIngredient(completedOrder.order.product);
        Destroy(completedOrder.thisObj);
        orderController.currentOrders.Remove(completedOrder);
    }

    private OrderController.OrderObject NeededSushiInInven()
    {
        for(int i = 0; i < playerController.heldIngredients.Count; i++)
        {
            for(int x = 0; x < orderController.currentOrders.Count; x++)
            {
                if (playerController.heldIngredients[i] == orderController.currentOrders[x].order.product)
                {
                    return orderController.currentOrders[x];
                }
            }
        }

        return null;
    }
}
