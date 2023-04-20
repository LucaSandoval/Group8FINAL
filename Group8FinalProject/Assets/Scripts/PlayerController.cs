using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public List<Ingredient> heldIngredients;

    public IngredientData[] data;

    private UIController uIController;

    [System.Serializable]
    public struct IngredientData
    {
        public Sprite icon;
        public string name;
        public Ingredient ingredient;
    }

    private void Awake()
    {
        uIController = GetComponent<UIController>();
    }

    public void Start()
    {
        heldIngredients = new List<Ingredient>();
    }

    public void ClearHeldIngredients()
    {
        heldIngredients.Clear();
        uIController.GenerateHeldVisuals();
    }

    public void RemoveIngredient(Ingredient ingredient)
    {
        heldIngredients.Remove(ingredient);
        uIController.GenerateHeldVisuals();
    }

    public bool PlayerHasIngredient(Ingredient ingredient)
    {
        foreach (Ingredient i in heldIngredients)
        {
            if (i == ingredient)
            {
                return true;
            }
        }

        return false;
    }

    public void PickupIngredient(Ingredient ingredient)
    {
        if (!heldIngredients.Contains(ingredient) && heldIngredients.Count < 3)
        {
            heldIngredients.Add(ingredient);
            uIController.GenerateHeldVisuals();
        }
    }

    public string getIngredientName(Ingredient ingredient)
    {
        foreach(IngredientData d in data)
        {
            if (d.ingredient == ingredient)
            {
                return d.name;
            }
        }

        return "NULL";
    }

    public Sprite getIngredientSprite(Ingredient ingredient)
    {
        foreach (IngredientData d in data)
        {
            if (d.ingredient == ingredient)
            {
                return d.icon;
            }
        }

        return null;
    }
}

[System.Serializable]
public enum Ingredient
{
    rice,
    seaweed,
    salmon,
    salmonRoll,
    tuna,
    smokedSalmon,
    tunaRoll,
    smokedSalmonRoll,
    gear,
    unagi,
    unagiMaki
}
