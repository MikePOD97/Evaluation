using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CustomShaderGUI : ShaderGUI
{
    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
        foreach(MaterialProperty property in properties)
        {
            materialEditor.ShaderProperty(property, property.displayName);
        }
    }
}
