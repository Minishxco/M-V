using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImageAnimation : MonoBehaviour {

	public Sprite[] sprites;
	public float Seconds = 0.1f;
	public bool loop = true;

	private int index = 0;
	private Image image;
	private float frame = 0;

	void Awake() {
		image = GetComponent<Image>();
	}

	void Update() {
		if (!loop && index == sprites.Length) return;
		frame += Time.deltaTime; //frame ++;
		if (frame <= Seconds) return;
		image.sprite = sprites [index];
		frame = 0;
		index ++;
		if (index >= sprites.Length) {
			if (loop)
			{
				index = 0;
			}
		}
	}
}