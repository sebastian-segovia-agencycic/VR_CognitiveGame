using UnityEngine;

public class InteractableButton : MonoBehaviour
{
    public bool doOnce, cloneButton, doOnceClone;
    public Spawner spawner;

    public void ButtonReciver()
    {
        if (cloneButton)
        {
            spawner.SetBurstingEnabled(true);
            spawner.StartBurst();
        }
        else
        {
            if (doOnce)
            {
                doOnce = false;
                GeneratorGame.Instance.CalculateFinalGame();
            }
        }
    }

    public void ButtonReciverOff()
    {
        if (cloneButton)
        {
            spawner.SetBurstingEnabled(false);
            spawner.StartBurst();
        }
    }
}
