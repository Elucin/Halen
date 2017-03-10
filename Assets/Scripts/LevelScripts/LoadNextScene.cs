using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LoadNextScene : MonoBehaviour {
    //public static int Checkpoint = -1;
    public static int Level = 0;
    string[] LoadingTips = {
        "The Momentum Shield mitigates damage. It charges based on how fast you're moving and drains as you take damage or stand still, so keep moving!",
        "Sharp can absorb energy from the enemy's system when he destroys them. If you kill an enemy with a Dash or Melee attack, it will recharge your ammo!",
        "If you get up close and personal with an enemy, you can use Sharp to execute a quick Melee attack by pressing the Shoot button.",
        "Chargers are heavily armored and hard to kill. If you can open the weak point on their back, however, they become vulnerable.",
        "Projectiles can block each other! You can use this to your advantage, but so can your enemies.",
        "Brawlers are weak and predictable, but be careful! They can sneak up on you if you ignore them.",
        "Snipers have a long range, but a low fire rate. Stay moving and use cover to avoid them.",
        "Floaters are not a threat by themselves, but if you leave them alone for long enough they can create a large hazard area. Take them out early!",
        "Minefields are a dangerous place for you, but you can make them a dangerous place for your enemies!",
        "If you kill an enemy with your Dash attack, it resets instantly! Use this to quickly dispatch another enemy, or get out of harm's way.",
        "Your Scattershot doesn't do much damage but it can stun enemies, rendering them temporarily helpless while you finish them off.",
        "Creativity is rewarded! Find different ways to kill enemies to earn additional points."
    };

	// Use this for initialization
	void Start () {
		UnityEngine.SceneManagement.SceneManager.LoadSceneAsync (Level);
        GameObject.Find("Tip").GetComponent<Text>().text = "TIP: " + LoadingTips[Random.Range(0, LoadingTips.Length)];
	}
}
