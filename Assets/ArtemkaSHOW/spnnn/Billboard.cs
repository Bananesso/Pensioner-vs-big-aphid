using UnityEngine;

[ExecuteInEditMode, RequireComponent(typeof(SpriteRenderer))]
public class Billboard : MonoBehaviour
{
	private const float UpdateFrequency = 300;

	private float time;

	[Header("Billboard Settings")]
    [SerializeField] private bool doNotOptimize;
    [SerializeField] private bool billboardX;

	private float waitTime;

	private new Transform transform;

	private void Start()
	{
		GetComponent<SpriteRenderer>().flipX = false;
		
		transform = base.transform;
	}

	private void OnBecameVisible()
	{
		var cameraCurrent = Camera.current;

		try
		{
			if(billboardX) transform.rotation = Quaternion.Euler(cameraCurrent.transform.rotation.eulerAngles.x, cameraCurrent.transform.rotation.eulerAngles.y, 0f);
			else
				transform.rotation = Quaternion.Euler(0f, cameraCurrent.transform.rotation.eulerAngles.y, 0f);	
		} catch {}
	}

	private void OnWillRenderObject()
	{
		var cameraCurrent = Camera.current;

		time += Time.deltaTime;
		
		if (time < waitTime && !doNotOptimize) return;
		time = 0;
		
		try
		{
			if(!doNotOptimize) waitTime = Vector3.Distance(transform.position, cameraCurrent.transform.position) / UpdateFrequency;

			if(billboardX) transform.rotation = Quaternion.Euler(Camera.current.transform.rotation.eulerAngles.x, cameraCurrent.transform.rotation.eulerAngles.y, 0f);
			else
			transform.rotation = Quaternion.Euler(0f, cameraCurrent.transform.rotation.eulerAngles.y, 0f);
		}
		catch
		{
		}
	}
}