using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BirdDatabase", menuName = "ScriptableObjects/BirdDatabase", order = 1)]
public class BirdDatabase : ScriptableObject
{
    public List<BirdData> birds; // List of all available birds.
}
