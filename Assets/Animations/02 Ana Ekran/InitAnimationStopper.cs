using UnityEngine;

public class InitAnimationStopper : MonoBehaviour {

	private void CullCompletely()
	{
		GetComponent<Animator>().cullingMode = AnimatorCullingMode.CullCompletely;
	}
}
