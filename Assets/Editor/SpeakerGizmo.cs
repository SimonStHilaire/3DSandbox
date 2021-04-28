using UnityEditor;
using UnityEngine;

public class SpeakerGizmo
{
    [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected, typeof(Speaker))]
    public static void DrawIcon(Speaker obj, GizmoType type)
    {
        if (obj.IsPLaying())
            Gizmos.DrawIcon(obj.transform.position, "music_icon_on");
        else
            Gizmos.DrawIcon(obj.transform.position, "music_icon_off");

        if((type & GizmoType.NonSelected) == GizmoType.NonSelected)
            Gizmos.DrawWireSphere(obj.transform.position, obj.GetComponent<AudioSource>().maxDistance);
    }
}
