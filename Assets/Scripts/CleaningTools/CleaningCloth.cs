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

    private float completionTime = 3f;

    private void Start()
    {
        ModeController.Instance.resetEvent.AddListener(CleanModeOff);    
    }

    public void CleanModeOn()
    {
        ModeController.Instance.CleanMode();
        clothImage.gameObject.SetActive(true);
    }

    public void CleanModeOff()
    {
        ModeController.Instance.ResetMode();
        clothImage.gameObject.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        if (ModeController.Instance.currentMode == ModeController.GameMode.CleanMode)
        {
            if (Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                if (!particle.isPlaying)
                {
                    particle.Play();
                }
                else
                {
                    if (currentProgress < 3)
                    {
                        loadingBar.gameObject.SetActive(true);
                        loadingBar.value = (currentProgress / 3);
                        currentProgress += Time.deltaTime;
                        if (currentProgress >= 3)
                            GameController.Instance.CleanFinished = true;
                    }
                    Vector3 screenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    clothImage.transform.position = new Vector3(screenPos.x, screenPos.y, 0);
                }
            }
            else
            {
                if (particle.isPlaying)
                {
                    particle.Stop();
                    loadingBar.gameObject.SetActive(false);
                }
            }
        }

    }
}
