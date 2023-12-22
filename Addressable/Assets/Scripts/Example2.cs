using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

/*
 * Example2  使用Addressable讀取檔案之後，可以另外儲存起來。 等待需要釋放的時候再釋放。
 *           這只是一個簡單的範例。
 *           需要 將 Addressable Play Mode Script 改為 Use Existing Build 的方式。
 *           才會真正的去讀取Bundle。
 *           產出來的Bundle目前設定都是放在本地端。
 *           
 *           目前Tiger的圖片跟其他兩張圖片是放在不同的Group。
 *           放在一起就可能造成記憶體無法釋放的狀況。
**/


public class Example2 : MonoBehaviour
{
    //跟Example1 使用一樣的設定
    [SerializeField]
    //動物照片
    private Image _AnimalImage;


    //儲存 Addressable讀取之後的檔案。
    List<AsyncOperationHandle> handleList = new List<AsyncOperationHandle>();

    // Start is called before the first frame update
    void Start()
    {
         //呼叫讀取動物圖片
        StartCoroutine(LoadAnimal());
    }

    private IEnumerator LoadAnimal()
    {
        //先宣告一個Handle去接讀取出來的資料
        AsyncOperationHandle loadAnimalHandle = Addressables.LoadAssetAsync<Texture>("Tiger");
        //直到下載完成
        yield return loadAnimalHandle;

        //判斷讀取狀況
        if(loadAnimalHandle.Status == AsyncOperationStatus.Succeeded)
        {
            Texture texture = (Texture)loadAnimalHandle.Result ;
            // Create a sprite from the loaded texture
            Sprite sprite = Sprite.Create((Texture2D)texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

            // Assign the sprite to the Image component
            _AnimalImage.sprite = sprite;
            //_AnimalImage.SetNativeSize();

            //讀取成功

            handleList.Add(loadAnimalHandle);

        }
        else if( loadAnimalHandle.Status == AsyncOperationStatus.Failed)
        {
            //錯誤
            Debug.Log("讀取圖片失敗");

        }else
        {
            //其他原因
            Debug.Log(loadAnimalHandle.Status);
        }

        yield break;
    }

    public void onClearHandle()
    {
        //將資料取出來並移除，如果在移除過程中，又加入新的可能會造成錯誤。
        foreach(AsyncOperationHandle handle in handleList)
        {
            Addressables.Release(handle);
        }

        handleList.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
