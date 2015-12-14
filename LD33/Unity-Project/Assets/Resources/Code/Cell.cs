//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------	
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	public class Cell
	{
		static Object cellPrefab = Resources.Load ("Prefabs/Cell");
		//GameObject cellGameObject;
		int height;
		public int x;
		public int y;
		public int z;
		public List<Cell> neighbors = new List<Cell>();
		
		public Cell (int x, int z, int height, GameObject parent) {
			this.x = x;
			this.z = z;
			this.height = height;
			this.y = this.height / 2;
			for (int y = 0; y < this.height; y ++) {
				GameObject cube = GameObject.Instantiate(cellPrefab, new Vector3(this.x, y, this.z), Quaternion.identity) as GameObject;
				cube.transform.parent = parent.transform;
			}
		}

		public List<Cell> pathTo (int x, int z) {

			List<PathSegment> open = new List<PathSegment> () {new PathSegment(this)};
			
			PathSegment path = open [0];

			if (x != this.x + Game.map.getHalfWidth () && y != this.y + Game.map.getHalfHeight ()) {
				List<Cell> visited = new List<Cell> ();

				bool cont = true;

				int count = 0;



				while (open.Count > 0 && cont) {

					PathSegment checkSegment = open [0];
					Cell checkCell = checkSegment.getCurrent ();
					foreach (Cell neighbor in checkCell.neighbors) {
						if (!visited.Contains (neighbor)) {
							PathSegment nSegment = new PathSegment (neighbor, checkSegment);

							if (neighbor.x + Game.map.getHalfWidth () == x && neighbor.z + Game.map.getHalfHeight () == z) {
								Debug.Log ("Found a match");
								path = nSegment;
								cont = false;
								break;
							}
							open.Add (nSegment);
						}
					}

					visited.Add (checkCell);
					open.RemoveAt (0);
				}
			}

			List<Cell> pathList = new List<Cell> ();

			while (path != null) {
				Debug.Log ("Updating path");
				pathList.Add(path.getCurrent());
				path = path.getPrevious();
			}

			return pathList;
		}

		public void addNeighbor(Cell neighbor) {
			if (!neighbors.Contains(neighbor)) {
				neighbors.Add (neighbor);
				neighbor.addNeighbor(this);
			}
		}
	}
}

