using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookAt : MonoBehaviour
{
    public bool distHide;
    private GameObject player;
    private GameObject text;
    private SequenceController sequenceController;
    void Start()
    {
        sequenceController = GameObject.FindGameObjectWithTag("GameController").GetComponent<SequenceController>();
        player = GameObject.FindGameObjectWithTag("Player");
        if(distHide)
        {
            if (transform.GetChild(0).gameObject.activeSelf)
            {
                text = transform.GetChild(0).gameObject;
            } else
            {
                text = transform.GetChild(1).gameObject;
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookAtPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(lookAtPos);

        if (distHide)
        {
            if (sequenceController.scaryLighting.activeSelf == false)
            {
                text.SetActive(!(Vector3.Distance(player.transform.position, transform.position) < 2f));
            } else
            {
                text.SetActive(false);
            }            
        }
    }
}
