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
        [SerializeField] private BrushPatternDataHolder brushPatternDataHolder;

        [Header("Brush pattern")]
        [SerializeField] private CustomDropdown brushOptions;

        [Header("Brush Control")]
        [SerializeField] private Button button_BrushControlBoard;
        [SerializeField] private TextMeshProUGUI text_ButtonText;
        [SerializeField] private GameObject container_BrushControl;
        [SerializeField] private GameObject container_brushPattern;

        [Header("Brush opaicty")]
        [SerializeField] private Slider slider_BrushOpacity;
        [SerializeField] private TextMeshProUGUI text_BrushOpacityValue;

        //Events
        public event UnityAction<Texture2D> OnSubmitChangedBrushPattern;
        public event UnityAction<float> OnSubmitChangedBrushOpacity;

        private void Start()
        {
            Init();
            AddListener();
            InitDefaultBrush();
            InitBrushPatternOption();
        }

        private void Init()
        {
            OnBrushOpacityChanged(slider_BrushOpacity);

            container_BrushControl.SetActive(false);
            container_brushPattern.SetActive(false);

            text_ButtonText.text = "+";
        }

        private void AddListener()
        {
            button_BrushControlBoard.onClick.AddListener(OnButtonBrushControlBoardClicked);
            slider_BrushOpacity.onValueChanged.AddListener(delegate { OnBrushOpacityChanged(slider_BrushOpacity); });
        }

        private void OnButtonBrushControlBoardClicked()
        {
            container_BrushControl.SetActive(!container_BrushControl.activeSelf);
            container_brushPattern.SetActive(!container_brushPattern.activeSelf);

            text_ButtonText.text = container_BrushControl.activeSelf ? "-" : "+";
        }

        private void InitDefaultBrush()
        {
            BrushPatternData brushPattern = brushPatternDataHolder.GetByArrId(0);

            if (brushPattern != null)
                OnSubmitChangedBrushPattern?.Invoke(brushPattern.BrushTexture);
        }

        private void InitBrushPatternOption()
        {
            if (brushPatternDataHolder == null) return;

            BrushPatternData[] brushPatterns = brushPatternDataHolder.BrushPatterns;

            if (brushPatterns == null) return;

            DropdownItemData[] dropdownItems = new DropdownItemData[brushPatterns.Length];

            for (int i = 0; i < dropdownItems.Length; i++)
            {
                dropdownItems[i] = new DropdownItemData();
                dropdownItems[i].sprite = brushPatterns[i].BrushSprite;
            }

            brushOptions.CreateDropdownItems(dropdownItems);
        }

        private void OnBrushOpacityChanged(Slider slider)
        {
            text_BrushOpacityValue.text = (Math.Round(slider.value, 1)).ToString();

            OnSubmitChangedBrushOpacity?.Invoke(slider.value);
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
            BrushPatternData brushPattern = brushPatternDataHolder.GetByArrId(item.id);

            if (brushPattern != null)
                OnSubmitChangedBrushPattern?.Invoke(brushPattern.BrushTexture);
        }

        private void OnDisable()
        {
            brushOptions.OnDropdownClick -= BrushOptions_OnDropdownClick;
            brushOptions.OnSelectionChange -= BrushOptions_OnSelectionChange;
        }
    }
}

