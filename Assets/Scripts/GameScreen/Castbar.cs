using UnityEngine;
using HM_Utils;
using UnityEngine.UI;

public class Castbar : MonoBehaviour
{
    private SimpleTimer timer;
    public Slider castSlider;
    public Button cancelButton;
    public float castTime { get; set; }
    public bool isCasting { get; set; }

    public delegate void CastingComplete();
    public event CastingComplete OnCastingComplete;

    public delegate void CastingCanceled();
    public event CastingCanceled OnCastingCanceled;

    // Use this for initialization
    void Start()
    {
        isCasting = false;
        timer = new SimpleTimer();
        DisplayCastBar(false);
    }

    public void Cast(float _castTime)
    {
        if (!isCasting)
        {
            // Check if it's an instant cast or not
            if(_castTime > 0f)
            {
                print("Castbar: spell cast length " + _castTime);
                castTime = _castTime;
                isCasting = true;
                timer.Start();

                DisplayCastBar(true);
            }
            else
            {
                OnCastingComplete();
            }
        }
    }

    public void StopCasting(bool canceled)
    {
        print("Castbar: Cast finished");
        isCasting = false;
        timer.Stop();
        castSlider.value = 0;
        DisplayCastBar(false);

        if (canceled && OnCastingCanceled != null)
        {
            OnCastingCanceled();
        }
    }

    private void DisplayCastBar(bool enabled)
    {
        castSlider.gameObject.SetActive(enabled);
        cancelButton.gameObject.SetActive(enabled);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer != null && isCasting)
        {
            castSlider.value = (float)timer.GetElapsedTimeSecs() / castTime;

            if (timer.GetElapsedTimeSecs() >= castTime)
            {
                StopCasting(false);
                OnCastingComplete();
            }
        }
    }
}
