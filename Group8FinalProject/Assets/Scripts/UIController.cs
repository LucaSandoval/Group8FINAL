using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject heldIngredientParent;

    private PlayerController playerController;
    private List<GameObject> visualIngredients;
    private GameObject ingredientVisualsPrefab;

    public GameObject interactMenu;
    public Text interactText;

    public Slider progressSlider;
    private float progMax = 1;
    private float curProg;
    private bool fillingProg;

    public List<Interactable> inRangeItems;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        ingredientVisualsPrefab = Resources.Load<GameObject>("IngredientPrefab");
        visualIngredients = new List<GameObject>();
        inRangeItems = new List<Interactable>();

        progressSlider.maxValue = progMax;
        curProg = 0;
    }

    private void Update()
    {
        if (inRangeItems.Count > 0)
        {
            interactMenu.SetActive(true);
            interactText.text = inRangeItems[0].description;
        } else
        {
            interactMenu.SetActive(false);
        }

        if (Input.GetKey(KeyCode.E) && inRangeItems.Count > 0)
        {
            if (inRangeItems[0].canSeeVisual)
            {
                progressSlider.gameObject.SetActive(true);
                fillingProg = true;
            }           
        } else
        {
            fillingProg = false;
            progressSlider.gameObject.SetActive(false);
        }

        if (curProg > progMax)
        {
            inRangeItems[0].Interact();
            curProg = 0;
        }

        progressSlider.value = curProg;
    }

    private void FixedUpdate()
    {
        if (fillingProg)
        {
            curProg += Time.deltaTime;
        } else
        {
            curProg = 0;
        }

        
    }

    public void GenerateHeldVisuals()
    {
        foreach(GameObject g in visualIngredients)
        {
            Destroy(g);
        }

        visualIngredients.Clear();

        for(int i = 0; i < playerController.heldIngredients.Count; i++)
        {
            SpawnNewIngredToParent(heldIngredientParent.transform, playerController.heldIngredients[i], true);
        }
    }

    public void SpawnNewIngredToParent(Transform parent, Ingredient ingredient, bool forInven)
    {
        GameObject newVisual = Instantiate(ingredientVisualsPrefab);

        newVisual.GetComponent<Image>().sprite = playerController.getIngredientSprite(ingredient);
        newVisual.transform.GetChild(0).GetComponent<Text>().text =
            playerController.getIngredientName(ingredient);

        newVisual.transform.SetParent(parent.transform, false);

        if (forInven)
        {
            visualIngredients.Add(newVisual);
        }        
    }
}
