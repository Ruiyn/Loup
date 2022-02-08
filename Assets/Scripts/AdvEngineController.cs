using System;
using System.Collections;
using UnityEngine;
using Utage;
using UtageExtensions;

public class AdvEngineController : MonoBehaviour
{
    public AdvEngine AdvEngine { get { return advEngine; } }
    [SerializeField]
    protected AdvEngine advEngine;

    public bool IsPlaying { get; private set; }

    float defaultSpeed = -1;

    public void JumpScenario(string label)
    {
        StartCoroutine(JumpScenarioAsync(label, null));
    }

    public void JumpScenario(string label, Action onComplete)
    {
        StartCoroutine(JumpScenarioAsync(label, onComplete));
    }

    IEnumerator JumpScenarioAsync(string label, Action onComplete)
    {
        IsPlaying = true;
        AdvEngine.JumpScenario(label);
        while (!AdvEngine.IsEndOrPauseScenario)
        {
            IsPlaying = false;
            yield return null;
        }
        if (onComplete != null) onComplete();
    }

    public void JumpScenario(string label, Action onComplete, Action onFailed)
    {
        JumpScenario(label, null, onComplete, onFailed);
    }

    public void JumpScenario(string label, Action onStart, Action onComplete, Action onFailed)
    {
        if (string.IsNullOrEmpty(label))
        {
            if (onFailed != null) onFailed();
            Debug.LogErrorFormat("Line 49");
            return;
        }
        if (label[0] == '*')
        {
            label = label.Substring(1);
        }
        if (AdvEngine.DataManager.FindScenarioData(label) == null)
        {
            if (onFailed != null) onFailed();
            Debug.LogErrorFormat("{0}Line 59", label);
            return;
        }

        if (onStart != null) onStart();
        JumpScenario(
            label,
            onComplete);
    }

    public void ChangeMessageSpeed(float speed)
    {
        if (defaultSpeed < 0)
        {
            defaultSpeed = AdvEngine.Config.MessageSpeed;
        }
        AdvEngine.Config.MessageSpeed = speed;
    }

    public void ResetMessageSpeed()
    {
        if (defaultSpeed >= 0)
        {
            AdvEngine.Config.MessageSpeed = defaultSpeed;
        }
    }
}