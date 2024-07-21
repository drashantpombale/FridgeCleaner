using UnityEngine;
using UnityEngine.UI;

public class CleaningSpray : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particle;

    [SerializeField]
    private GameObject sprayImage;

    [SerializeField]
    private Slider loadingBar;

    float currentProgress;

    [SerializeField]
    private float completionTime = 3f;
    public void SprayModeOn() 
    {
        if (GameController.Instance.AreItemsShelved() && GameController.Instance.AreExpiredItemsDiscarded())
        {
            ModeController.Instance.SprayMode();
            sprayImage.gameObject.SetActive(true);
        }
    }
    
    public void SprayModeOff() 
    {
        ModeController.Instance.ResetMode();
        sprayImage.gameObject.SetActive(false);
    }

    private void Start()
    {
        ModeController.Instance.resetEvent.AddListener(SprayModeOff);
    }

    // Update is called once per frame
    void Update()
    {
        if (ModeController.Instance.currentMode == ModeController.GameMode.SprayMode)
        {
            if (Input.GetMouseButton(0) || (Input.touchCount>0 && Input.GetTouch(0).phase==TouchPhase.Began))
            {
                if (!particle.isPlaying)
                {
                    particle.Play();
                    SoundManager.Instance.PlaySFX(SoundManager.Instance.spraySound, true);
                }
                else 
                {
                    if (currentProgress < 3)
                    {
                        loadingBar.gameObject.SetActive(true);
                        loadingBar.value = (currentProgress / completionTime);
                        currentProgress += Time.deltaTime;
                        if (currentProgress >= 3)
                        {
                            GameController.Instance.SprayFinished = true;
                            GameController.Instance.IsTaskOver(GameController.GameStage.SparyingDone);
                        }
                    }
                    Vector3 screenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    sprayImage.transform.position = new Vector3(screenPos.x, screenPos.y, 0);
                }
            }
            else 
            {
                SoundManager.Instance.StopSFX();
                if (particle.isPlaying)
                {
                    particle.Stop();
                    loadingBar.gameObject.SetActive(false);
                }
            }
        }
       
    }
}
