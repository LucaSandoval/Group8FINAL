using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public string description;

    protected float range = 1.7f;
    private GameObject player;
    private UIController uiController;
    protected PlayerController playerController;

    private bool inRange;

    protected bool canInteract;

    [HideInInspector]
    public bool canSeeVisual;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        uiController = GameObject.Find("GameController").GetComponent<UIController>();
        playerController = GameObject.Find("GameController").GetComponent<PlayerController>();
        canInteract = true;
        canSeeVisual = true;
    }
    
    public virtual void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= range && canSeeVisual)
        {
            inRange = true;
            if (!uiController.inRangeItems.Contains(this))
            {
                uiController.inRangeItems.Add(this);
            }
        } else
        {
            inRange = false;
            if (uiController.inRangeItems.Contains(this))
            {
                uiController.inRangeItems.Remove(this);
            }
        }
    }

    public abstract void Interact();

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
