using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTypes : MonoBehaviour
{
    public StatsSaved st;
    
#if UNITY_WEBGL && !UNITY_EDITOR

    private static extern int IsMobile();
#endif
    void Start()
    {
        st = FindObjectOfType<StatsSaved>();
#if UNITY_WEBGL && !UNITY_EDITOR
        if (IsMobile() == 1)
        {
            st.controlType = ControlType.Android;
        }
        else
        {
            st.controlType = ControlType.PC;
        }
#else
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            st.controlType = ControlType.Android;
        }
        else
        {
            st.controlType = ControlType.PC;
        }
#endif
    }
   
    public void AndroidType()
    {
        st.controlType = ControlType.Android;
    }

    public void PCType()
    {
        st.controlType = ControlType.PC;
    }
}
