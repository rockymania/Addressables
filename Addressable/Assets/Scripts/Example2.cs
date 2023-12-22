using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

/*
 * Example2  �ϥ�AddressableŪ���ɮפ���A�i�H�t�~�x�s�_�ӡC ���ݻݭn���񪺮ɭԦA����C
 *           �o�u�O�@��²�檺�d�ҡC
 *           �ݭn �N Addressable Play Mode Script �אּ Use Existing Build ���覡�C
 *           �~�|�u�����hŪ��Bundle�C
 *           ���X�Ӫ�Bundle�ثe�]�w���O��b���a�ݡC
 *           
 *           �ثeTiger���Ϥ����L��i�Ϥ��O��b���P��Group�C
 *           ��b�@�_�N�i��y���O����L�k���񪺪��p�C
**/


public class Example2 : MonoBehaviour
{
    //��Example1 �ϥΤ@�˪��]�w
    [SerializeField]
    //�ʪ��Ӥ�
    private Image _AnimalImage;


    //�x�s AddressableŪ�����᪺�ɮסC
    List<AsyncOperationHandle> handleList = new List<AsyncOperationHandle>();

    // Start is called before the first frame update
    void Start()
    {
         //�I�sŪ���ʪ��Ϥ�
        StartCoroutine(LoadAnimal());
    }

    private IEnumerator LoadAnimal()
    {
        //���ŧi�@��Handle�h��Ū���X�Ӫ����
        AsyncOperationHandle loadAnimalHandle = Addressables.LoadAssetAsync<Texture>("Tiger");
        //����U������
        yield return loadAnimalHandle;

        //�P�_Ū�����p
        if(loadAnimalHandle.Status == AsyncOperationStatus.Succeeded)
        {
            Texture texture = (Texture)loadAnimalHandle.Result ;
            // Create a sprite from the loaded texture
            Sprite sprite = Sprite.Create((Texture2D)texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

            // Assign the sprite to the Image component
            _AnimalImage.sprite = sprite;
            //_AnimalImage.SetNativeSize();

            //Ū�����\

            handleList.Add(loadAnimalHandle);

        }
        else if( loadAnimalHandle.Status == AsyncOperationStatus.Failed)
        {
            //���~
            Debug.Log("Ū���Ϥ�����");

        }else
        {
            //��L��]
            Debug.Log(loadAnimalHandle.Status);
        }

        yield break;
    }

    public void onClearHandle()
    {
        //�N��ƨ��X�Өò����A�p�G�b�����L�{���A�S�[�J�s���i��|�y�����~�C
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
