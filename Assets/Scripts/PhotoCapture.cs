using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoCapture : MonoBehaviour
{

    [Header("Photo Taker")]
    [SerializeField] private Image photoDisplayArea;
    [SerializeField] private GameObject photoFrame;
    private bool viewingPhoto;

    [Header("Flash Effect")]
    [SerializeField] private GameObject cameralFlash;
    [SerializeField] private float flashTime;

    [Header("Flash Effect")]
    [SerializeField] private Animator fadingAnimation;



    private Texture2D screenCapture;


    // Start is called before the first frame update
    void Start()
    {
        screenCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //TakeScreenshoot;
            if (!viewingPhoto)
            {
                StartCoroutine(CapturePhoto());
            }
            else
            {
                RemovePhoto();
            }
        }
        
    }


    IEnumerator CapturePhoto()
    {
        yield return new WaitForEndOfFrame();

        Rect regionToRead = new Rect(0, 0, Screen.width, Screen.height);

        screenCapture.ReadPixels(regionToRead, 0, 0, false);
        screenCapture.Apply();
        ShowPhoto();
    }

    IEnumerator CameraFlashEffect()
    {
        //Play some audio
        cameralFlash.SetActive(true);
        yield return new WaitForSeconds(flashTime);
        cameralFlash.SetActive(false);
    }

    void ShowPhoto()
    {
        viewingPhoto = true; 
        Sprite photoSprite = Sprite.Create(
            screenCapture, //Texture
            new Rect(0.0f, 0.0f, screenCapture.width, screenCapture.height), //Rect
            new Vector2(0.5f, 0.5f), //Pivot
            100f //Pixels per Unit
            );

        photoDisplayArea.sprite = photoSprite;
        photoFrame.SetActive(true);
        //Do flash
        StartCoroutine(CameraFlashEffect());
        fadingAnimation.Play("PhotoFade");

    }

    void RemovePhoto()
    {
        viewingPhoto = false;
        photoFrame.SetActive(false);

    }
}
