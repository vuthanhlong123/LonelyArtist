using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace LA.Painting.PaintLibary
{
    public class LAPaintLibaryUI : MonoBehaviour
    {
        [SerializeField] private LAPaintControl_Libary paintLibaryControl;
        [SerializeField] private RectTransform rect_SlotContainer;
        [SerializeField] private GameObject textureSlotPrefab;
        [SerializeField] private float stepValue;

        [Header("Navigation")]
        [SerializeField] private Button button_Exit;

        private List<LaPaintLibary_TextureSlot> slots = new List<LaPaintLibary_TextureSlot>();

        private void Start()
        {
            button_Exit.onClick.AddListener(OnButtonExitClicked);
        }

        private void OnButtonExitClicked()
        {
            Show(false);
        }

        private void Update()
        {
            int slotidInCenter = (int)(-rect_SlotContainer.anchoredPosition.x / stepValue);

            for(int i = slotidInCenter-1; i<=slotidInCenter+1;i++)
            {
                if(i>=0 && i <slots.Count)
                {
                    slots[i].UpdateSize(rect_SlotContainer);
                }
            }

            /*foreach (var slot in slots)
            {
                slot.UpdateSize(rect_SlotContainer);
            }*/
        }

        public void ClearLibaryUI()
        {
            for (int i = slots.Count-1; i >=0; i--)
            {
                Destroy(slots[i].gameObject);
            }

            slots.Clear();
        }

        public async void CreateTextureLibary(Texture2D texture)
        {
            await Task.Yield();

            GameObject obj = Instantiate(textureSlotPrefab, rect_SlotContainer);
            if (obj == null) return;

            LaPaintLibary_TextureSlot slot = obj.GetComponent<LaPaintLibary_TextureSlot>();
            if(slot == null) return;

            slot.SetValue(texture, slots.Count);
            slots.Add(slot);
        }

        public void Show(bool state)
        {
            gameObject.SetActive(state);
        }

        private void OnEnable()
        {
            LaPaintLibary_TextureSlot.SubmitDropPaint += LaPaintLibary_TextureSlot_SubmitDropPaint;
        }

        private void LaPaintLibary_TextureSlot_SubmitDropPaint(int slotId)
        {
            paintLibaryControl.DropTexture(slotId);
        }

        private void OnDisable()
        {
            LaPaintLibary_TextureSlot.SubmitDropPaint -= LaPaintLibary_TextureSlot_SubmitDropPaint;
        }
    }
}


