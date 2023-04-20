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

    private int failSequence;

    [System.Serializable]
    public struct stepSequence
    {
        public Order order;
        public float delayTime;
        public bool requiredFail;
    }

    private GameObject orderPrefab;
    public GameObject orderParent;
    private UIController uIController;
    private SoundPlayer soundPlayer;
    private SequenceController sequenceController;

    public List<OrderObject> currentOrders;

    [System.Serializable]
    public class OrderObject
    {
        public float timer;
        public Order order;
        public GameObject thisObj;
        public bool requiredFail;

        public OrderObject(float timer, Order order, GameObject thisObj, bool requiredFail)
        {
            this.timer = timer;
            this.order = order;
            this.thisObj = thisObj;
            this.requiredFail = requiredFail;
        }
    }

    public void Start()
    {
        sequenceController = GetComponent<SequenceController>();
        uIController = GetComponent<UIController>();
        orderPrefab = Resources.Load<GameObject>("OrderPrefab");
        soundPlayer = GameObject.FindGameObjectWithTag("SoundController").GetComponent<SoundPlayer>();
        currentOrders = new List<OrderObject>();

        failSequence = -1;
        stepId = -1; //-1 is defalt
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

                    if (currentOrders[i].requiredFail)
                    {
                        NextFail();
                    }

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
        SpawnNewOrder(currentStep);
    }

    void SpawnNewOrder(stepSequence step)
    {
        float orderTime;
        soundPlayer.PlaySound("Bell");
        if(step.requiredFail)
        {
            orderTime = 25;
        } else
        {
            orderTime = 50;
        }

        GameObject newOrder = Instantiate(orderPrefab);
        newOrder.transform.SetParent(orderParent.transform, false);

        uIController.SpawnNewIngredToParent(newOrder.transform.GetChild(1), step.order.product, false);

        for (int i = 0; i < step.order.ingredients.Length; i++)
        {
            uIController.SpawnNewIngredToParent(newOrder.transform.GetChild(3), step.order.ingredients[i], false);
        }

        newOrder.transform.GetChild(0).GetComponent<Slider>().maxValue = orderTime;
        newOrder.transform.GetChild(0).GetComponent<Slider>().value = orderTime;


        OrderObject newOrderObj = new OrderObject(orderTime, step.order, newOrder, step.requiredFail);
        currentOrders.Add(newOrderObj);
    }

    public void NextFail()
    {
        failSequence++;

        switch(failSequence)
        {
            case 0:
                //light flicker 
                StartCoroutine(fail1());
                break;
            case 1:
                //lights out 
                StartCoroutine(fail2());
                break;
            case 2:
                //stuff breaks 
                StartCoroutine(fail3());
                break;
        }
    }

    public IEnumerator fail1()
    {
        sushiGameActive = false;
        soundPlayer.PlaySound("Static");
        soundPlayer.PauseSound("SushiMusic");
        sequenceController.normalLighting.SetActive(false);
        sequenceController.scaryLighting.SetActive(true);
        yield return new WaitForSecondsRealtime(0.1f);
        sequenceController.normalLighting.SetActive(true);
        sequenceController.scaryLighting.SetActive(false);
        yield return new WaitForSecondsRealtime(1f);
        sequenceController.normalLighting.SetActive(false);
        sequenceController.scaryLighting.SetActive(true);
        yield return new WaitForSecondsRealtime(0.1f);
        sequenceController.normalLighting.SetActive(true);
        sequenceController.scaryLighting.SetActive(false);
        yield return new WaitForSecondsRealtime(0.1f);
        sequenceController.normalLighting.SetActive(false);
        sequenceController.scaryLighting.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        sequenceController.normalLighting.SetActive(true);
        sequenceController.scaryLighting.SetActive(false);
        yield return new WaitForSecondsRealtime(0.1f);
        soundPlayer.PauseSound("Static");
        soundPlayer.PlaySound("SushiMusic");
        sushiGameActive = true;
    }

    public IEnumerator fail2()
    {
        sushiGameActive = false;
        sequenceController.ScarySequence();
        yield return new WaitForSecondsRealtime(3f);
        sequenceController.SushiSequence();
        sushiGameActive = true;
    }

    public IEnumerator fail3()
    {
        sushiGameActive = false;
        soundPlayer.PlaySound("Crash");
        sequenceController.ScarySequence();
        sequenceController.rubble.SetActive(true);
        sequenceController.nonHiddenStuff.SetActive(false);
        sequenceController.hiddenstuff.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        sequenceController.SushiSequence();
        sushiGameActive = true;
    }
}
