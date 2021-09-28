using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class FishManager
{
    
    /// <summary>
    /// This script takes the information from the CSV file with all of the fish
    /// information and creates a list of the Fish class with an instance for
    /// each fish in the csv file!
    /// </summary>
    
    public static List<Fish> allFish = new List<Fish>();

    //Run this when we hit play always
    [RuntimeInitializeOnLoadMethod]
    public static void InitFishManager() {
        //Access the csv file that contains fish data
        var textAsset = Resources.Load<TextAsset>("CSV/Fish");
        
        //Split the text asset up into each line
        var splitData = textAsset.text.Split('\n');
        foreach (var line in splitData) { //Loop through the array we made of each line
            var lineData = line.Split(','); //Split each line up at the comma
            if (lineData[0] != "Fish Name") { //Fish Name is contained on the first row, so we ignore that
                var newFish = new Fish(lineData[0], lineData[1], Int32.Parse(lineData[2]), Int32.Parse(lineData[3])); //We then create a fish instance with the info we have read
                allFish.Add(newFish); //Add the new fish to the list
            }
        }
    }

    public static Fish GetRandomFish() {
        //Picks out a random fish from the list
        return allFish[Random.Range(0, allFish.Count)];
    }

    public static Fish GetRandomFishWeighted() {
        //Picks out a random fish using spoke weights
        var totalSpoke = 0;
        foreach (var fish in allFish) {
            totalSpoke += fish.spokeWeight;
        }
        var valueChosen = Random.Range(0, totalSpoke);
        foreach (var fish in allFish) {
            if (valueChosen < fish.spokeWeight) {
                return fish;
            } else {
                valueChosen -= fish.spokeWeight;
            }
        }
        return null;
    }
}
