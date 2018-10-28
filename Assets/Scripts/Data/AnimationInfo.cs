using UnityEngine;

[System.Serializable]
public class AnimationInfo {
	[SerializeField] private string _trigger;
	[SerializeField] private AnimationClip _clip;

	public string Trigger {
		get { return _trigger; }
	}

	public float Duration {
		get { return _clip.length; }
	}
}
