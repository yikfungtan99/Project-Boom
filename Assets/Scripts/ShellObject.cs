using UnityEngine;

[CreateAssetMenu(fileName = "Shell", menuName = "ScriptableObject/Shell")]
public class ShellObject : ScriptableObject
{
    [Tooltip("shell to be fire")]
    public GameObject shell;

    [Tooltip("maximum amount reloadable into a chamber")]
    public int maxLoadable;
}
