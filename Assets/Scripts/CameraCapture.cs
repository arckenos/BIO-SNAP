using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraCapture : MonoBehaviour
{

    public OVRInput.Button shootButton;
    public GameObject Print;


    //private static ScreenshotHandler instance;
    public Camera myCamera;
    private Texture2D screenCapture;

    //PhotoCaputre
    [Header("Photo Taker")]
    [SerializeField] private Image photoDisplayArea;
    [SerializeField] private GameObject photoFrame;
    private bool viewingPhoto;

    [Header("Flash Effect")]
    [SerializeField] private GameObject cameralFlash;
    [SerializeField] private float flashTime;

    public int width = 1920;
    public int height = 1080;

    //[Header("Flash Effect")]
    //[SerializeField] private Animator fadingAnimation;

    private void Awake()
    {
        myCamera = gameObject.GetComponent<Camera>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
        myCamera.enabled = false;
        Print.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || OVRInput.GetDown(shootButton))
        {
            //TakeScreenshoot;
            if (!viewingPhoto)
            {
                Debug.Log("Screen shot desde la camara "+gameObject.name);
                StartCoroutine(CapturePhoto());
                myCamera.enabled = true;
            }
            else
            {
                RemovePhoto();
            }
        }

    }

    /*
    private void OnPostRender()
    {
        if (takeScreenShootOnNextFrame)
        {
            takeScreenShootOnNextFrame = false;
            RenderTexture renderTexture = myCamera.targetTexture;

            screenCapture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);

            StartCoroutine(CapturePhoto());

        }

    }

    private void TakeScreenShoot(int width, int height)
    {
        myCamera.targetTexture = RenderTexture.GetTemporary(width, height, 16);


        takeScreenShootOnNextFrame = true;

    }
    */

    //PhotoCaputre
    IEnumerator CapturePhoto()
    {

        screenCapture = new Texture2D(width, height, TextureFormat.RGB24, false);


        yield return new WaitForEndOfFrame();

        Rect regionToRead = new Rect(0, 0, width, height);

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
       // photoFrame.SetActive(true);
        myCamera.targetTexture = null;
        myCamera.enabled = false;
        //Do flash
        StartCoroutine(CameraFlashEffect());
        //fadingAnimation.Play("PhotoFade");
        Print.SetActive(true);
       
        Print.GetComponent<Renderer>().material.mainTexture = screenCapture;


    }

    void RemovePhoto()
    {
        viewingPhoto = false;
        photoFrame.SetActive(false);
        Print.SetActive(false);
    }
}
