using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LA.Painting
{
    public class LAPaintingColorPicker : MonoBehaviour
    {
        [Header("Manager")]
        [SerializeField] private LAPaintingManager paintingManager;

        [SerializeField] private float currentHue;
        [SerializeField] private float currentSat;
        [SerializeField] private float currentVal;

        [SerializeField] private RawImage hueImage;
        [SerializeField] private RawImage satValImage;
        [SerializeField] private RawImage outputImage;

        [SerializeField] private Slider hueSlider;

        [Header("Hex Code")]
        [SerializeField] private TMP_InputField hexInputField;
        [SerializeField] private Button button_hexAccept;

        private Texture2D hueTexture, svTexture, outputTexture;

        private void Start()
        {
            Init();
            AddListener();
        }

        private void Init()
        {
            CreateHueImage();
            CreateSVImage();
            CreateOutputImage();
            UpdateOutputImage();
        }

        private void AddListener()
        {
            hueSlider.onValueChanged.AddListener(delegate { UpdateSVImage(); });
            button_hexAccept.onClick.AddListener(OnTexInput);
        }

        private void CreateHueImage()
        {
            hueTexture = new Texture2D(1, 16);
            hueTexture.wrapMode = TextureWrapMode.Clamp;
            hueTexture.name = "HueTexture";

            for (int i = 0; i < hueTexture.height; i++)
            {
                hueTexture.SetPixel(0, i, Color.HSVToRGB(i / (float)hueTexture.height, 1, 1));
            }

            hueTexture.Apply();
            currentHue = 0;

            hueImage.texture = hueTexture;
        }

        private void CreateSVImage()
        {
            svTexture = new Texture2D(16, 16);
            svTexture.wrapMode = TextureWrapMode.Clamp;
            svTexture.name = "SatValTexture";

            for(int y=0; y <svTexture.height;y++)
            {
                for (int x=0; x <svTexture.width;x++)
                {
                    svTexture.SetPixel(x, y, Color.HSVToRGB(currentHue, (float)x / svTexture.width, (float)y / svTexture.height));
                }

                svTexture.Apply();
                currentSat = 0;
                currentVal = 0;

                satValImage.texture = svTexture;
            }
        }

        private void CreateOutputImage()
        {
            outputTexture = new Texture2D (1, 16);
            outputTexture.wrapMode = TextureWrapMode.Clamp;
            outputTexture.name = "OutputTexture";

            Color currentColor = Color.HSVToRGB(currentHue, currentSat, currentVal);

            for(int i=0;i<outputTexture.height;i++)
            {
                outputTexture.SetPixel(0,i,currentColor);
            }

            outputTexture.Apply();
            outputImage.texture = outputTexture;
        }

        private void UpdateOutputImage()
        {
            Color currentColor = Color.HSVToRGB (currentHue, currentSat, currentVal);

            for(int i=0;i<outputTexture.height;i++)
            {
                outputTexture.SetPixel (0,i,currentColor);
            }

            outputTexture.Apply();

            hexInputField.text = ColorUtility.ToHtmlStringRGB (currentColor);

            paintingManager.UpdateBrushColor(currentColor);
        }

        public void SetSV(float S, float V)
        {
            currentSat = S; currentVal = V;

            UpdateOutputImage();
        }

        public void UpdateSVImage()
        {
            currentHue = hueSlider.value;

            for (int y = 0; y < svTexture.height; y++)
            {
                for (int x = 0; x < svTexture.width; x++)
                {
                    svTexture.SetPixel(x, y, Color.HSVToRGB(currentHue, (float)x / svTexture.width, (float)y / svTexture.height));
                }
            }

            svTexture.Apply();
            UpdateOutputImage();
        }

        public void OnTexInput()
        {
            if(hexInputField.text.Length < 6)  return;

            Color newColor;

            if(ColorUtility.TryParseHtmlString("#"+hexInputField.text, out newColor))
                Color.RGBToHSV(newColor, out currentHue, out currentSat, out currentVal);

            hueSlider.value = currentHue;

            hexInputField.text = "";

            UpdateOutputImage();
        }
    }
}


