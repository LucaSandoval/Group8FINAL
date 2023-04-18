using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderController : MonoBehaviour
{
    public static bool sushiGameActive = true;
    //Game sequence info
    private int stepId;
    private float curStepTimer;
    private stepSequence currentStep;
    public stepSequence[] steps;

    [System.Serializable]
    public struct stepSequence
    {
        public Order order;
        public float delayTime;
    }

    private GameObject orderPrefab;
    public GameObject orderParent;
    private UIController uIController;

    public List<OrderObject> currentOrders;

    [System.Serializable]
    public class OrderObject
    {
        public float timer;
        public Order order;
        public GameObject thisObj;

        public OrderObject(float timer, Order order, GameObject thisObj)
        {
            this.timer = timer;
            this.order = order;
            this.thisObj = thisObj;
        }
    }

    public void Start()
    {
        uIController = GetComponent<UIController>();
        orderPrefab = Resources.Load<GameObject>("OrderPrefab");
        currentOrders = new List<OrderObject>();

        stepId = -1;
        NextStep();
    }

    public void FixedUpdate()
    {
        if (sushiGameActive && stepId < steps.Length - 1)
        {
            if (curStepTimer > 0)
            {
                curStepTimer -= Time.deltaTime;
            } else
            {
                NextStep();
            }

            for(int i = 0; i < currentOrders.Count; i++)
            {
                currentOrders[i].timer -= Time.deltaTime;
                currentOrders[i].thisObj.transform.GetChild(0).GetComponent<Slider>().value = currentOrders[i].timer;

                if(currentOrders[i].timer <= 0)
                {
                    Destroy(currentOrders[i].thisObj);
                    currentOrders.Remove(currentOrders[i]);

                    uIController.ShowResults("Order Failed !", 2f);
                }
            }
        }
    }


    void NextStep()
    {
        stepId++;
        currentStep = steps[stepId];
        curStepTimer = currentStep.delayTime;
        SpawnNewOrder(currentStep.order);
    }

    void SpawnNewOrder(Order order)
    {
        float orderTime = 20;

        GameObject newOrder = Instantiate(orderPrefab);
        newOrder.transform.SetParent(orderParent.transform, false);

        uIController.SpawnNewIngredToParent(newOrder.transform.GetChild(1), order.product, false);

        for (int i = 0; i < order.ingredients.Length; i++)
        {
            uIController.SpawnNewIngredToParent(newOrder.transform.GetChild(3), order.ingredients[i], false);
        }

        newOrder.transform.GetChild(0).GetComponent<Slider>().maxValue = orderTime;
        newOrder.transform.GetChild(0).GetComponent<Slider>().value = orderTime;


        OrderObject newOrderObj = new OrderObject(orderTime, order, newOrder);
        currentOrders.Add(newOrderObj);
    }
}
