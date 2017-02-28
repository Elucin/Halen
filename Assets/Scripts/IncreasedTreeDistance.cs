using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreasedTreeDistance : MonoBehaviour {

	private float distance;
	public Terrain terrain;

	void Start () {
		terrain.treeDistance = 6000;
	}
}