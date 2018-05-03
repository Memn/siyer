using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public interface LoadableHelper 
{
    // Convention is by name loading
    void Load(Selectable tab);

}