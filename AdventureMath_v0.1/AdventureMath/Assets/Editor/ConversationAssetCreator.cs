using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


// Permite la creación de Dialogos (Conversaciones) como Assets usando la CustomAssetUtility y el Singleton
public class ConversationAssetCreator : MonoBehaviour {
    [MenuItem("Assets/Create/Dialog")]
    public static void CreateAsset()
    {
        CustomAssetUtility.CreateAsset<Dialogo>();
    }
}
