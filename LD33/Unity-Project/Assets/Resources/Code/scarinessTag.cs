using UnityEngine;
using System.Collections;

public class scarinessTag : MonoBehaviour {

	Monster parent;
	public TextMesh mesh;

	// Use this for initialization
	void Start () {
		parent = transform.parent.gameObject.GetComponent<Monster>();
	}
	
	// Update is called once per frame
	void Update () {
		mesh.text = parent.scarinessLvl.ToString();
	}
}
