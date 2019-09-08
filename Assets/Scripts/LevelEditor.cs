using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Random = UnityEngine.Random;

public class LevelEditor : MonoBehaviour
{
	public float curDistance;
	public float minDistance = 8;
	public GameObject[] objects;
	public Stack<GameObject> lastObjects = new Stack<GameObject>();

    private void Update()
    {
		var mousePos = Input.mousePosition;

		RaycastHit hit;
		Physics.Raycast(GameManager.gm.cam.camera.ScreenPointToRay(Input.mousePosition), out hit);
		var hitPos = hit.point;
		hitPos.y = -0.5f;

		var lastObject = lastObjects.Count > 0 ? lastObjects.Peek() : null;
		curDistance = lastObject && hit.collider ? (lastObject.transform.position - hitPos).magnitude : minDistance;

		if (hit.collider && (Input.GetMouseButtonDown(1) || Input.GetMouseButton(1) && curDistance >= minDistance))
		{
			var obj = objects.OrderBy(g => Guid.NewGuid()).FirstOrDefault(o => o != lastObject);
			lastObjects.Push(Instantiate(obj, new Vector3(hitPos.x, 0, hitPos.z), Quaternion.Euler(new Vector3(0, Random.Range(0, 360f), 0))));
		}

		if (Input.GetKeyDown(KeyCode.Z))
		{
			Destroy(lastObjects.Pop());
		}
    }
}