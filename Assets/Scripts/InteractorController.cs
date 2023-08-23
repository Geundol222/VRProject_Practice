using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.UI;

public class InteractorController : MonoBehaviour
{
    [Header("Interactor")]
    [SerializeField] XRDirectInteractor directInteractor;
    [SerializeField] XRRayInteractor rayInteractor;
    [SerializeField] XRRayInteractor teleportInteractor;

    [Header("Input")]
    [SerializeField] InputActionReference teleportModeActive;

    [Header("Locomotion")]
    [SerializeField] List<LocomotionProvider> locomotionProviders;

    private void Awake()
    {
        if (rayInteractor != null)
        {
            rayInteractor.gameObject.SetActive(true);
        }

        if (teleportInteractor != null)
        {
            teleportInteractor.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        if (rayInteractor != null)
        {
            rayInteractor.selectEntered.AddListener(OnRaySelectEnter);
            rayInteractor.selectExited.AddListener(OnRaySelectExit);
            rayInteractor.uiHoverEntered.AddListener(OnUIHoverEnter);
            rayInteractor.uiHoverExited.AddListener(OnUIHoverExit);
        }

        if (teleportInteractor != null)
        {
            InputAction teleportAction = teleportModeActive.action;
            teleportAction.performed += TeleportModeStart;
            teleportAction.canceled += TeleportModeEnd;
        }
    }

    private void OnDisable()
    {
        if (rayInteractor != null)
        {
            rayInteractor.selectEntered.RemoveListener(OnRaySelectEnter);
            rayInteractor.selectExited.RemoveListener(OnRaySelectExit);
            rayInteractor.uiHoverEntered.RemoveListener(OnUIHoverEnter);
            rayInteractor.uiHoverExited.RemoveListener(OnUIHoverExit);
        }

        if (teleportModeActive != null)
        {
            InputAction teleportAction = teleportModeActive.action;
            teleportAction.performed -= TeleportModeStart;
            teleportAction.canceled -= TeleportModeEnd;
        }
    }

    private void OnRaySelectEnter(SelectEnterEventArgs args)
    {
        foreach (var provider in locomotionProviders)
        {
            provider.gameObject.SetActive(false);
        }
    }

    private void OnRaySelectExit(SelectExitEventArgs args)
    {
        foreach (var provider in locomotionProviders)
        {
            provider.gameObject.SetActive(true);
        }
    }

    private void OnUIHoverEnter(UIHoverEventArgs args)
    {
        foreach (var provider in locomotionProviders)
        {
            provider.gameObject.SetActive(false);
        }
    }

    private void OnUIHoverExit(UIHoverEventArgs args)
    {
        foreach (var provider in locomotionProviders)
        {
            provider.gameObject.SetActive(true);
        }
    }

    private void TeleportModeStart(InputAction.CallbackContext context)
    {
        if (rayInteractor != null)
            rayInteractor.gameObject.SetActive(false);
        if (teleportInteractor != null)
            teleportInteractor.gameObject.SetActive(true);
    }

    private void TeleportModeEnd(InputAction.CallbackContext context)
    {
        if (rayInteractor != null)
            rayInteractor.gameObject.SetActive(true);
        if (teleportInteractor != null)
            StartCoroutine(DelayOneFrame());
    }

    IEnumerator DelayOneFrame()
    {
        yield return new WaitForEndOfFrame();
        teleportInteractor.gameObject.SetActive(false);
    }
}
