using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class Preception : MonoBehaviour
{
    public float distance;

    private PostProcessingBehaviour ppEffects;
    private DepthOfFieldModel.Settings depth;
	
    // Use this for initialization
	void Start ()
    {
        ppEffects = GetComponent<PostProcessingBehaviour>();
        if(ppEffects)
        {
            depth = ppEffects.profile.depthOfField.settings;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Physics.Raycast(transform.position, transform.forward, distance))
        {
            if(ppEffects)
            {
                depth.focusDistance = 1.0f;
                ppEffects.profile.depthOfField.settings = depth;
            }
        }
        else
        {
            depth.focusDistance = 10.0f;
            ppEffects.profile.depthOfField.settings = depth;
        }
	}
}
