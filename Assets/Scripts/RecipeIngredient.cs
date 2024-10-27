using System;
using UnityEngine;

[Serializable]
public class RecipeIngredient
{
    [field: SerializeField] public ItemData ItemData { get; private set; }
    [field: SerializeField] public int Quantity { get; private set; }
}