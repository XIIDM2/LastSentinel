using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

public class Factory
{
    [Inject] private readonly CharacterDataCollection _charactersDataCollection;

    private HashSet<GameObject> _characters = new HashSet<GameObject>();

    public async UniTask<GameObject> CreateCharacter(string characterID)
    {
        AssetReferenceGameObject assetReferencePrefab = _charactersDataCollection.GetCharacterPrefab(characterID);

        if (assetReferencePrefab == null)
        {
            Debug.LogError("Prefab is null in factory");
            return null;
        }

        GameObject characterInstance = await assetReferencePrefab.InstantiateAsync().ToUniTask();


        _characters.Add(characterInstance);


        return characterInstance;
    }

    public void ReleaseAllCharacters()
    {
        foreach (GameObject character in _characters)
        {
            Addressables.ReleaseInstance(character);
        }

        _characters.Clear();
    }

    public void ReleaseCharacterInstance(GameObject character)
    {
        if (_characters.Remove(character))
        {
            Addressables.ReleaseInstance(character);
        }
        else
        {
            Debug.LogError("Error in clearing Addressable's assets in Factory");
        }
    }
}
