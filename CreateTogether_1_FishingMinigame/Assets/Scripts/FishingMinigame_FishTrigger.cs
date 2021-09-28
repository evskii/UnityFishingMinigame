using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingMinigame_FishTrigger : MonoBehaviour
{
   /// <summary>
   /// We use this to detect whether or not the fish is in the catching bar
   /// and sends that info to the FishingMinigame.cs script.
   /// </summary>
   
   public bool beingCaught = false;
   private FishingMinigame minigameController;

   private void Start() {
      minigameController = FindObjectOfType<FishingMinigame>();
   }

   private void OnTriggerEnter2D(Collider2D other) {
      if (minigameController.reelingFish) {
         if (other.CompareTag("CatchingBar") && !beingCaught) {
            beingCaught = true;
            minigameController.FishInBar();
         }
      }
   }

   private void OnTriggerExit2D(Collider2D other) {
      if (other.CompareTag("CatchingBar") && beingCaught) {
         beingCaught = false;
         minigameController.FishOutOfBar();
      }
   }
}
