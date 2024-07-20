using UnityEngine;
using UnityEngine.UI;

public class CleaningSpray : MonoSingletonGeneric<CleaningSpray>
{
    [SerializeField]
    private ParticleSystem particle;

    [SerializeField]
    private Texture2D sprayCursor;

    [SerializeField]
    private GameObject sprayImage; 

    [HideInInspector]
    public bool sprayMode = false;
    public void SprayModeOn() 
    {
        sprayMode = true;
        sprayImage.gameObject.SetActive(true);
    }
    
    public void SprayModeOff() 
    {
        sprayMode = false;
        sprayImage.gameObject.SetActive(false);
    }

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (sprayMode)
        {
            if (Input.GetMouseButton(0) || Input.GetTouch(0).phase==TouchPhase.Began)
            {
                if (!particle.isPlaying)
                {
                    particle.Play();
                    Debug.Log("Play play [play");
                }
                else 
                {
                    Vector3 screenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    sprayImage.transform.position = new Vector3(screenPos.x, screenPos.y, 0);
                }
            }
            else 
            {
                if (particle.isPlaying)
                {
                    particle.Stop();
                    Debug.Log("Stop Stop [Stop");
                }
            }
        }
       
    }
}
