using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "CharactersDataCollection", menuName = "ScriptableObjects/CharactersDataCollection")]
public class CharacterDataCollection : ScriptableObject
{
    [SerializeField] private CharacterData[] _characterDatas;

    private Dictionary<string, AssetReferenceGameObject> _characterDataDictionary;

    private void OnValidate()
    {
        _characterDataDictionary = null;
    }
    private void InitDictionary()
    {
        _characterDataDictionary = new Dictionary<string, AssetReferenceGameObject>();

        foreach (CharacterData characterData in _characterDatas)
        {
            if (string.IsNullOrEmpty(characterData.ID))
            {
                Debug.LogError("Character ID is empty in Characters Data Dictionary");
                continue;
            }

            if (!_characterDataDictionary.ContainsKey(characterData.ID))
            {
                _characterDataDictionary.Add(characterData.ID, characterData.CharacterPrefab);
            }
            else
            {
                Debug.LogError("Duplicate ID in Characters Data Dictionary");
                continue;
            }
        }
    }

    public AssetReferenceGameObject GetCharacterPrefab(string ID)
    {
        if (_characterDataDictionary == null || _characterDataDictionary.Count == 0)
        {
            InitDictionary();
        }

        if (_characterDataDictionary.TryGetValue(ID, out AssetReferenceGameObject characterPrefab))
        {
            return characterPrefab;
        }
        else
        {
            Debug.LogError($"Prefab for {ID} not found");
        }

        return null;
    }

}
