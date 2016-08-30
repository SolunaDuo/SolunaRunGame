using UnityEngine;
using System.ComponentModel;
using System.Diagnostics;
using UnityEditor;
using System.Collections;
using Debug = System.Diagnostics.Debug;

[CustomPropertyDrawer(typeof(Tooltip))]
public class TooltipDraw : PropertyDrawer {
    public override void OnGUI( Rect position, SerializedProperty property, GUIContent label ) {
        Tooltip tooltipAttribute = attribute as Tooltip;

        if( property.propertyType == SerializedPropertyType.AnimationCurve ) {
            property.animationCurveValue = EditorGUI.CurveField( position, new GUIContent( label.text, tooltipAttribute._tip ), property.animationCurveValue );
        }

        if( property.propertyType == SerializedPropertyType.Boolean ) {
            property.boolValue = EditorGUI.Toggle( position, new GUIContent( label.text, tooltipAttribute._tip ), property.boolValue );
        }

        if( property.propertyType == SerializedPropertyType.Bounds ) {
            property.boundsValue = EditorGUI.BoundsField( position, new GUIContent( label.text, tooltipAttribute._tip ), property.boundsValue );
        }

        if( property.propertyType == SerializedPropertyType.Color ) {
            property.colorValue = EditorGUI.ColorField( position, new GUIContent( label.text, tooltipAttribute._tip ),
                property.colorValue );
        }

        if( property.propertyType == SerializedPropertyType.Float ) {
            property.floatValue = EditorGUI.FloatField( position,
                new GUIContent( label.text, tooltipAttribute._tip ), property.floatValue );
        }

        if( property.propertyType == SerializedPropertyType.Integer ) {
            property.intValue = EditorGUI.IntField( position, new GUIContent( label.text, tooltipAttribute._tip ), property.intValue );
        }

        if( property.propertyType == SerializedPropertyType.Rect ) {
            property.rectValue = EditorGUI.RectField( position, new GUIContent( label.text, tooltipAttribute._tip ),
                property.rectValue );
        }

        if( property.propertyType == SerializedPropertyType.String ) {
            property.stringValue = EditorGUI.TextField( position,
                new GUIContent( label.text, tooltipAttribute._tip ), property.stringValue );
        }
    }
}
