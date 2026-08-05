using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MySample
{
    //Cube Color를 흰색에서 빨간색으로 바꾸기
    //메테리얼 바꿔치기로 컬러 바꾸기
    //직접 메테리얼의 컬러를 빨간색으로 바꾸기
    public class MaterialTest : MonoBehaviour
    {
        #region Variables

        //참조
        public Renderer renderer;
        
        //인풋
        public InputActionReference jumpAction;
        
        public Material damagedMaterial;
        private Material originMaterial;
        
        //Material의 속성값을 관리하는 개체
        private MaterialPropertyBlock materialPropertyBlock;
        #endregion
        
        #region Unity Event Methods

        private void Awake()
        {
            renderer = GetComponent<Renderer>();
            
            //MaterialPropertyBlock 객체 생성
            materialPropertyBlock = new MaterialPropertyBlock();
        }

        private void Start()
        {
            //초기화
            //originMaterial = renderer.material;
        }

        private void OnEnable()
        {
            jumpAction.action.Enable();
        }

        void OnDisable()
        {
            jumpAction.action.Disable();
        }

        private void Update()
        {
            //스페이스바를 누르면 큐브 컬러 변경
            if (jumpAction.action.WasPressedThisFrame())
            {
                //ChangeMaterial();
                //ChangeMaterialColor();
                //Debug.Log("Jump Pressed");
                ChangeSharedMaterialColor();
            }
        }

        #endregion
        
        #region Custom Method
        //메테리얼 바꿔치기
        private void ChangeMaterial()
        {
            renderer.material = damagedMaterial;
        }

        private void ResetMaterial()
        {
            renderer.material = originMaterial;
        }
        
        //직접 메테리얼의 컬러를 빨간색으로 바꾸기
        private void ChangeMaterialColor()
        {
            //renderer.material.SetColor("_BaseColor", Color.red);
            renderer.sharedMaterial.SetColor("_BaseColor", Color.red);
        }
        
        //해당 오브젝트만 컬러 변경, 배칭 깨지 않고
        //materialPropertyBlock을 이용하여 sharedMaterial의 컬러를 변경하기
        private void ChangeSharedMaterialColor()
        {
            materialPropertyBlock.SetColor("_Color", Color.red);
            renderer.SetPropertyBlock(materialPropertyBlock);
        }
        #endregion
    }
}