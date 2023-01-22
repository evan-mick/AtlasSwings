using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ReplaceMeshWithBoxCollider : ScriptableWizard
{

	/// <summary>
	/// What is considered the highest transform
	/// </summary>
	public Transform HighestParentTransform = null; 


	[MenuItem("Edit/Optimize Colliders...")]
	static void CreateWizard()
	{
		ScriptableWizard.DisplayWizard("ReplaceMeshWithBoxCollider", typeof(ReplaceMeshWithBoxCollider), "Do it!");
	}

	/// <summary>
	/// Goes through all mesh colliders, removes the mesh collider, and adds a box collider to their highest up parent
	/// </summary>
	private void ReplaceMeshColliders()
	{

		// 1.) find all mesh colliders
		// 2.) keep going through their parents till there are not more (or reaches highest), make sure parent is not in set
		// 3.) add that parent to set 
		// 4.) add box collider to set
		// 5.) delete mesh collider
		// 6.) repeat until no mesh colliders

		MeshCollider[] colliders = FindObjectsOfType<MeshCollider>();
		HashSet<Transform> checkedParents = new HashSet<Transform>();
		

		foreach (var collider in colliders)
		{

			Transform currentParent = collider.gameObject.transform;

			// This ensures that we don't take away meshcolliders from anything with no parent (streets)
			int timesChecked = 0; 
			do
			{
				currentParent = currentParent.transform.parent;
				timesChecked++; 
			} while (currentParent != null && currentParent != HighestParentTransform);

			// Don't add if it doesn't have a parent (streets)
			if (timesChecked == 1)
				continue;


			// Remove collider
			Destroy(collider);

			// Don't add boxes to parents we've already checked
			if (checkedParents.Contains(currentParent) || currentParent.GetComponent<BoxCollider>() != null)
				continue;

			// Add box collider to parent
			checkedParents.Add(currentParent);
			currentParent.gameObject.AddComponent<BoxCollider>();
			
		}

	}
}
