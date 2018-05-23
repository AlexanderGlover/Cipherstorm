using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static Collider[] FindComponentsInChildrenWithTag<Collider>(this GameObject parent, string tag, bool forceActive = false) where Collider : Component
    {
        if (parent == null) { throw new System.ArgumentNullException(); }
        if (string.IsNullOrEmpty(tag) == true) { throw new System.ArgumentNullException(); }
        List<Collider> list = new List<Collider>(parent.GetComponentsInChildren<Collider>(forceActive));
        if (list.Count == 0) { return null; }

        for (int i = list.Count - 1; i >= 0; i--)
        {
            if (list[i].CompareTag(tag) == false)
            {
                list.RemoveAt(i);
            }
        }
        return list.ToArray();
    }
}