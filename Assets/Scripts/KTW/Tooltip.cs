using UnityEngine;
using System.Collections;

public class Tooltip : PropertyAttribute {

    public string _tip;

    public Tooltip(string tip ) {
        _tip = tip;
    }
}
