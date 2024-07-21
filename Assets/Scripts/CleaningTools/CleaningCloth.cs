using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CleaningCloth : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particle;

    [SerializeField]
    private GameObject clothImage;

    [SerializeField]
    private Slider loadingBar;

    float currentProgress;

    [SerializeField]
    private float completionTime = 3f;

    private void Start()
    {
        ModeController.Instance.resetEvent.AddListener(CleanModeOff);    
    }

    //To indicate that current mode is set to cleaning mode
    public void CleanModeOn()
    {
        if (GameController.Instance.AreItemsShelved() && GameController.Instance.AreExpiredItemsDiscarded())
        {
            ModeController.Instance.CleanMode();
            clothImage.gameObject.SetActive(true);
        }
    }

    public void CleanModeOff()
    {
        ModeController.Instance.ResetMode();
        clothImage.gameObject.SetActive(false);
    }


    void Update()
    {
        //Only activate if the current mode is what we require
        if (ModeController.Instance.currentMode == ModeController.GameMode.CleanMode)
        {
            if (Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                if (!particle.isPlaying)
                {
                    particle.Play();
                    SoundManager.Instance.PlaySFX(SoundManager.Instance.cleanSound, true);
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
                            GameController.Instance.CleanFinished = true;
                            GameController.Instance.IsTaskOver(GameController.GameStage.CleaningDone);
                        }
                    }
                    Vector3 screenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    clothImage.transform.position = new Vector3(screenPos.x, screenPos.y, 0);
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
