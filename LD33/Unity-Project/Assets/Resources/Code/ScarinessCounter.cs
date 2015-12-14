using UnityEngine;
using System.Collections;

using AssemblyCSharp;

public class ScarinessCounter : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		guiText.text = "Scariness: " + Game.player.getScariness();
	}
}
