using System;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [Serializable]
    public class DialogueLine
    {
        [field: SerializeField] public string SpeakerName { get; private set; }
        [field: SerializeField, TextArea(3, 10)] public string Text { get; private set; }
    }
    
    [CreateAssetMenu(fileName = "Dialogue", menuName = "Winter Universe/Dialogue/New Dialogue")]
    public class DialogueConfig : BasicInfoConfig
    {
        [field: SerializeField] public List<DialogueLine> Lines { get; private set; } = new List<DialogueLine>();
    }
} 