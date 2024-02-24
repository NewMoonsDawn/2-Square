using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRButton : MonoBehaviour
{
    public Transform visualTarget;
    public Vector3 localAxis;
    public float followTreshold = 45;

    private bool freeze;
    private bool following;
    private Vector3 offset;
    private Transform pokeAttachTransform;
    private XRBaseInteractable interactable;

    void Start()
    {
        interactable = GetComponent<XRBaseInteractable>();
        interactable.hoverEntered.AddListener(Follow);
        interactable.hoverExited.AddListener(ResetVisual);
        interactable.selectEntered.AddListener(Freeze);
    }

    public void Follow(BaseInteractionEventArgs hover)
    {
        var interactor = hover.interactorObject;
           
        pokeAttachTransform = interactor.transform;
        offset = visualTarget.position - pokeAttachTransform.position;

        float pokeAngle = Vector3.Angle(offset, visualTarget.TransformDirection(localAxis));

        if (pokeAngle < followTreshold)
        {
            freeze = false;
            following = true;
        }
    }

    public void ResetVisual(BaseInteractionEventArgs hover)
    {
        following = false;
        freeze = false;
    }

    public void Freeze(BaseInteractionEventArgs hover)
    {
        freeze = true;
    }

    void Update()
    {
        if (freeze) return;

        if (following)
        {
            Vector3 localTargetPosition = visualTarget.InverseTransformPoint(pokeAttachTransform.position + offset);
            Vector3 constrainedLocalTargetPosition = Vector3.Project(localTargetPosition, localAxis);
            visualTarget.position = visualTarget.TransformPoint(constrainedLocalTargetPosition);
        }
        else
        {
            visualTarget.localPosition = Vector3.Lerp(visualTarget.localPosition, Vector3.up * .5f, .2f);
        }
    }
}
