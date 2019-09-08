using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public Camera camera;
	private Vector3 defaultPos;
	private Vector3 step;

	private void Awake()
	{
		camera = GetComponent<Camera>();
		GameManager.gm.cam = this;
	}

	private void Start()
	{
		defaultPos = transform.localPosition;
		step = defaultPos / GameManager.gm.pc.scale;
	}

	public void ZoomOut ()
	{
		StartCoroutine(AnimateZoomOut());
	}

	private IEnumerator AnimateZoomOut()
	{
		Vector3 startPos = transform.localPosition;
		Vector3 targetPos = step * GameManager.gm.pc.scale;

		var t = 0f;
		while (t < .2f)
		{
			transform.localPosition = Vector3.Lerp(startPos, targetPos, t);
			t += Time.deltaTime;
			yield return null;
		}
	}
}