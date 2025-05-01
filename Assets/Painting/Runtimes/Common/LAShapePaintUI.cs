using System;
using LA.Common.UI.Runtime;
using LA.Painting.Data;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace LA.Painting.Common
{
    public class LAShapePaintUI : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] private ShapePatternDataHolder shapePatternDataHolder;
        [SerializeField] private ShapePaintMaterialHolder shapePaintMaterialDataHolder;

        [Header("Shape Control")]
        [SerializeField] private Button button_ShapeControlBoard;
        [SerializeField] private TextMeshProUGUI text_ButtonText;

        [Header("Shape Pattern")]
        [SerializeField] private GameObject container_ShapePattern;
        [SerializeField] private CustomDropdown brushOptions;

        [Header("Shape opaicty")]
        [SerializeField] private GameObject container_ShapeOpacity;
        [SerializeField] private Slider slider_BrushOpacity;
        [SerializeField] private TextMeshProUGUI text_ShapeOpacityValue;

        [Header("Shape Wire Width")]
        [SerializeField] private GameObject container_ShapeWireWidth;
        [SerializeField] private Slider slider_ShapeWireWidth;
        [SerializeField] private TextMeshProUGUI text_ShapeWireWidthValue;

        [Header("Shape Side")]
        [SerializeField] private GameObject container_ShapeSide;
        [SerializeField] private Slider slider_ShapeSide;
        [SerializeField] private TextMeshProUGUI text_ShapeSideValue;

        [Header("Shape Bound Radius")]
        [SerializeField] private GameObject container_ShapeBoundRadius;
        [SerializeField] private Slider slider_ShapeBoundRadius;
        [SerializeField] private TextMeshProUGUI text_ShapeBoundRadiusValue;

        [Header("Shape Line Width")]
        [SerializeField] private GameObject container_ShapeLineWidth;
        [SerializeField] private Slider slider_ShapeLineWidth;
        [SerializeField] private TextMeshProUGUI text_ShapeLineWidthValue;

        //Events
        public event UnityAction<Texture2D, Material> OnSubmitChangedShapePattern;
        public event UnityAction<float> OnSubmitChangedShapeOpacity;
        public event UnityAction<float> OnSubmitChangedWireWidth;
        public event UnityAction<float> OnSubmitChangedSide;
        public event UnityAction<float> OnSubmitChangedBoundRadius;
        public event UnityAction<float> OnSubmitChangedLineWidth;

        private int currentSelectedId;

        private void Start()
        {
            Init();
            AddListener();
            InitDefaultBrush();
            InitBrushPatternOption();
        }

        private void Init()
        {
            OnShapeOpacityChanged(slider_BrushOpacity);
            OnShapeWireWidthChanged(slider_ShapeWireWidth);
            OnShapeSideChanged(slider_ShapeSide);
            OnShapeBoundRadiusChanged(slider_ShapeBoundRadius);
            OnShapeLineWidthChanged(slider_ShapeLineWidth);

            container_ShapePattern.SetActive(false);
            container_ShapeOpacity.SetActive(false);
            container_ShapeWireWidth.SetActive(false);
            container_ShapeSide.SetActive(false);
            container_ShapeBoundRadius.SetActive(false);
            container_ShapeLineWidth.SetActive(false);

            text_ButtonText.text = "+";
        }

        private void AddListener()
        {
            button_ShapeControlBoard.onClick.AddListener(OnButtonBrushControlBoardClicked);
            slider_BrushOpacity.onValueChanged.AddListener(delegate { OnShapeOpacityChanged(slider_BrushOpacity); });
            slider_ShapeWireWidth.onValueChanged.AddListener(delegate { OnShapeWireWidthChanged(slider_ShapeWireWidth); });
            slider_ShapeSide.onValueChanged.AddListener(delegate { OnShapeSideChanged(slider_ShapeSide); });
            slider_ShapeBoundRadius.onValueChanged.AddListener(delegate { OnShapeBoundRadiusChanged(slider_ShapeBoundRadius); });
            slider_ShapeLineWidth.onValueChanged.AddListener(delegate { OnShapeLineWidthChanged(slider_ShapeLineWidth); });
        }

        private void OnButtonBrushControlBoardClicked()
        {
            container_ShapePattern.SetActive(!container_ShapePattern.activeSelf);
            if(container_ShapePattern.activeSelf)
                EnableShapeControllers(currentSelectedId);
            else
            {
                container_ShapeOpacity.SetActive(false);
                container_ShapeWireWidth.SetActive(false);
                container_ShapeSide.SetActive(false);
                container_ShapeBoundRadius.SetActive(false);
                container_ShapeLineWidth.SetActive(false);
            }

            text_ButtonText.text = container_ShapePattern.activeSelf ? "-" : "+";
        }

        //Enbale shape controllers follow properties used by paint material
        private void EnableShapeControllers(int id)
        {
            ShapePatternData shapeData = shapePatternDataHolder.GetByArrId(id);
            if (shapeData == null) return;

            ShapePaintMaterialData paintMaterialData = shapePaintMaterialDataHolder.FindData(shapeData.MaterialReferenceId);

            if (paintMaterialData == null) return;

            container_ShapeOpacity.SetActive(paintMaterialData.useOpacity);
            container_ShapeWireWidth.SetActive(paintMaterialData.useWireWidth);
            container_ShapeSide.SetActive(paintMaterialData.useSide);
            container_ShapeBoundRadius.SetActive(paintMaterialData.useRadius);
            container_ShapeLineWidth.SetActive(paintMaterialData.useLineWidth);
        }

        private void InitDefaultBrush()
        {
            currentSelectedId = 0;

            ShapePatternData shapePattern = shapePatternDataHolder.GetByArrId(currentSelectedId);
            if (shapePattern == null) return;

            ShapePaintMaterialData paintMaterialData = shapePaintMaterialDataHolder.FindData(shapePattern.MaterialReferenceId);

            if (paintMaterialData == null) return;

            OnSubmitChangedShapePattern?.Invoke(shapePattern.Texture, paintMaterialData.material);
        }

        private void InitBrushPatternOption()
        {
            if (shapePatternDataHolder == null) return;

            ShapePatternData[] shapePatterns = shapePatternDataHolder.ShapePatterns;

            if (shapePatterns == null) return;

            DropdownItemData[] dropdownItems = new DropdownItemData[shapePatterns.Length];

            for (int i = 0; i < dropdownItems.Length; i++)
            {
                dropdownItems[i] = new DropdownItemData();
                dropdownItems[i].sprite = shapePatterns[i].Sprite;
            }

            brushOptions.CreateDropdownItems(dropdownItems);
        }

        private void OnShapeOpacityChanged(Slider slider)
        {
            text_ShapeOpacityValue.text = (Math.Round(slider.value, 1)).ToString();

            OnSubmitChangedShapeOpacity?.Invoke(slider.value);
        }

        private void OnShapeWireWidthChanged(Slider slider)
        {
            text_ShapeWireWidthValue.text = (Math.Round(slider.value, 1)).ToString();

            OnSubmitChangedWireWidth?.Invoke(slider.value/slider.maxValue);
        }

        private void OnShapeSideChanged(Slider slider)
        {
            text_ShapeSideValue.text = (Math.Round(slider.value, 1)).ToString();

            OnSubmitChangedSide?.Invoke(slider.value);
        }

        private void OnShapeBoundRadiusChanged(Slider slider)
        {
            text_ShapeBoundRadiusValue.text = (Math.Round(slider.value, 1)).ToString();

            OnSubmitChangedBoundRadius?.Invoke(slider.value);
        }

        private void OnShapeLineWidthChanged(Slider slider)
        {
            text_ShapeLineWidthValue.text = (Math.Round(slider.value, 1)).ToString();
            OnSubmitChangedLineWidth?.Invoke(slider.value);
        }

        private void OnEnable()
        {
            brushOptions.OnDropdownClick += BrushOptions_OnDropdownClick;
            brushOptions.OnSelectionChange += BrushOptions_OnSelectionChange;
        }

        private void BrushOptions_OnDropdownClick()
        {

        }

        private void BrushOptions_OnSelectionChange(CustomDropdownItem item)
        {
            ShapePatternData shapePattern = shapePatternDataHolder.GetByArrId(item.id);
            if (shapePattern == null) return;

            ShapePaintMaterialData paintMaterialData = shapePaintMaterialDataHolder.FindData(shapePattern.MaterialReferenceId);

            if(paintMaterialData == null) return;

            currentSelectedId = item.id;
            EnableShapeControllers(currentSelectedId);

            OnSubmitChangedShapePattern?.Invoke(shapePattern.Texture, paintMaterialData.material);

            OnShapeOpacityChanged(slider_BrushOpacity);
            OnShapeWireWidthChanged(slider_ShapeWireWidth);
            OnShapeSideChanged(slider_ShapeSide);
            OnShapeBoundRadiusChanged(slider_ShapeBoundRadius);
            OnShapeLineWidthChanged(slider_ShapeBoundRadius);
        }

        private void OnDisable()
        {
            brushOptions.OnDropdownClick -= BrushOptions_OnDropdownClick;
            brushOptions.OnSelectionChange -= BrushOptions_OnSelectionChange;
        }
    }
}

