using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	//public float speed;
	public TextMeshProUGUI countText;
	//public GameObject winTextObject;

	public AudioClip crystalSound;
	public AudioSource audioS;
	public AudioSource mainAudioS;
	public AudioClip secondMusic;
	private Rigidbody rb;
	private int count;
	public TextMeshProUGUI barrierText;
	public TextMeshProUGUI startText;
	public TextMeshProUGUI MissionText;
	public TextMeshProUGUI EndText;
	public AudioMixerSnapshot part1Snapshot;
	public AudioMixerSnapshot part2Snapshot;
	GameObject barrier2;
	GameObject firstPerson;

	void Start()
	{

		rb = GetComponent<Rigidbody>();

		barrier2 = GameObject.FindGameObjectWithTag("Barrier2");
		firstPerson = GameObject.FindGameObjectWithTag("person");
		count = 0;
		startText.text = "Hmm..what's that glowing thing up ahead?";
		SetCountText();
		
	


		//winTextObject.SetActive(false);
	}

	void OnTriggerEnter(Collider other)
	{

		if (other.gameObject.CompareTag("Crystal"))
		{
			other.gameObject.SetActive(false);
			audioS.PlayOneShot(crystalSound);
			startText.text = "";
			startText.text = "It's a crystal..continue on the path and see if you can collect more!";
			Destroy(startText, 3);

			count = count + 1;


			SetCountText();
		}
		else if (other.gameObject.CompareTag("Barrier"))
        {
            if (count < 6)
            {
				other.gameObject.SetActive(true);
				barrierText.text = "You cannot cross this path yet...are you missing something? Hint: You need 6 crystals to pass.";
			}
            else
            {
				barrierText.text = "Congratulations on finding all crystals...so far...";
				other.gameObject.SetActive(false);
				barrier2.SetActive(false);
				Destroy(barrierText, 3);

			}
        }
		else if (other.gameObject.CompareTag("secondPartBarrier"))
        {
			mainAudioS.clip = secondMusic;
			mainAudioS.Play();
		}
		else if (other.gameObject.CompareTag("missionTrigger"))
        {
			MissionText.text = "Once you think you've found all the crystals, head to the wizards house in the mountains past the village to turn them in.";
			Destroy(MissionText, 5);
        }
		else if (other.gameObject.CompareTag("endTrigger"))
        {
			EndText.text = "End of Game Stats\nCrystals Found: " + count.ToString() + " out of 21";
			Destroy(countText, 1);
			Invoke("reloadScene", 5);
		}
	}

	void SetCountText()
	{
		countText.text = "Crystals Found: " + count.ToString();
	}

	void reloadScene()
    {
		SceneManager.LoadScene("Module3Scene");
	}
}
