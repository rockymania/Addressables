using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class Example : MonoBehaviour
{
    [SerializeField]
    //動物照片
    private Image _AnimalImage;

    [SerializeField]
    //讀取第幾位動物
    private int _LoadIndex = 0;

    [SerializeField]
    private bool _LoadAnimal = false;

    /// <summary>
    /// 讀取檔案
    /// </summary>
    private void LoadAsset(int loadIndex)
    {
        switch(loadIndex)
        {
            case 0:
                //CallBack   //讀取AddressableName
                Addressables.LoadAssetAsync<Texture>("deer").Completed += Load_Complete =>
                {
                    Texture texture = Load_Complete.Result;
                    // Create a sprite from the loaded texture
                    Sprite sprite = Sprite.Create((Texture2D)texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

                    // Assign the sprite to the Image component
                    _AnimalImage.sprite = sprite;
                    _AnimalImage.SetNativeSize();
                };
                break;
            case 1:
                //設定CallBack   讀取Label
                Addressables.LoadAssetAsync<Texture>("HH").Completed += CallBack_Load_Complete;
                break;
            case 2://讀取Resources底下的檔案
                Addressables.LoadAssetAsync<Texture>("Image/Tiger.png").Completed += Load_Complete =>
                {
                    Texture texture = Load_Complete.Result;
                    // Create a sprite from the loaded texture
                    Sprite sprite = Sprite.Create((Texture2D)texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

                    // Assign the sprite to the Image component
                    _AnimalImage.sprite = sprite;
                    _AnimalImage.SetNativeSize();
                };
                break;
        }
    }

    private void CallBack_Load_Complete(AsyncOperationHandle<Texture> load_Complete)
    {
        Texture texture = load_Complete.Result;
        // Create a sprite from the loaded texture
        Sprite sprite = Sprite.Create((Texture2D)texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

        // Assign the sprite to the Image component
        _AnimalImage.sprite = sprite;
        _AnimalImage.SetNativeSize();
    }

    // Update is called once per frame
    void Update()
    {
        if(_LoadAnimal)
        {
            _LoadAnimal = false;
            LoadAsset(_LoadIndex);
        }
    }
}
