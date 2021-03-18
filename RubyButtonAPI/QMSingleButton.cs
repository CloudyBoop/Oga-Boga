using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

namespace RubyButtonAPI
{
    public class QMSingleButton : QMButtonBase
    {
        public QMSingleButton(QMNestedButton btnMenu, int btnXLocation, int btnYLocation, String btnText, System.Action btnAction, String btnToolTip, Color? btnBackgroundColor = null, Color? btnTextColor = null)
        {
            btnQMLoc = btnMenu.getMenuName();
            if (btnMenu.getIsTabMenu())
                MelonLoader.MelonCoroutines.Start(WaitForTabMenu(btnXLocation, btnYLocation, btnText, btnAction, btnToolTip, btnBackgroundColor, btnTextColor));
            else
                initButton(btnXLocation, btnYLocation, btnText, btnAction, btnToolTip, btnBackgroundColor, btnTextColor);
        }

        private IEnumerator WaitForTabMenu(int btnXLocation, int btnYLocation, string btnText, Action btnAction, string btnToolTip, Color? btnBackgroundColor, Color? btnTextColor)
        {
            while (QMStuff.GetQuickMenuInstance().transform.Find(btnQMLoc) == false)
            {
                yield return null;
            }
            initButton(btnXLocation, btnYLocation, btnText, btnAction, btnToolTip, btnBackgroundColor, btnTextColor);
            yield break;
        }

        public QMSingleButton(string btnMenu, int btnXLocation, int btnYLocation, String btnText, System.Action btnAction, String btnToolTip, Color? btnBackgroundColor = null, Color? btnTextColor = null)
        {
            btnQMLoc = btnMenu;
            initButton(btnXLocation, btnYLocation, btnText, btnAction, btnToolTip, btnBackgroundColor, btnTextColor);
        }

        private void initButton(int btnXLocation, int btnYLocation, String btnText, System.Action btnAction, String btnToolTip, Color? btnBackgroundColor = null, Color? btnTextColor = null)
        {
            btnType = "SingleButton";
            button = UnityEngine.Object.Instantiate(QMStuff.SingleButtonTemplate(), QMStuff.GetQuickMenuInstance().transform.Find(btnQMLoc), true);

            initShift[0] = -1;
            initShift[1] = 0;
            setLocation(btnXLocation, btnYLocation);
            setButtonText(btnText);
            setToolTip(btnToolTip);
            setAction(btnAction);


            if (btnBackgroundColor != null)
                setBackgroundColor((Color)btnBackgroundColor);
            else
                OrigBackground = button.GetComponentInChildren<UnityEngine.UI.Image>().color;

            if (btnTextColor != null)
                setTextColor((Color)btnTextColor);
            else
                OrigText = button.GetComponentInChildren<Text>().color;

            setActive(true);
            QMButtonAPI.allSingleButtons.Add(this);
        }

        public void setButtonText(string buttonText)
        {
            button.GetComponentInChildren<Text>().text = buttonText;
        }

        public void setAction(System.Action buttonAction)
        {
            button.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
            if (buttonAction != null)
                button.GetComponent<Button>().onClick.AddListener(UnhollowerRuntimeLib.DelegateSupport.ConvertDelegate<UnityAction>(buttonAction));
        }

        public override void setBackgroundColor(Color buttonBackgroundColor, bool save = true)
        {
            //button.GetComponentInChildren<UnityEngine.UI.Image>().color = buttonBackgroundColor;
            if (save)
                OrigBackground = (Color)buttonBackgroundColor;
            //UnityEngine.UI.Image[] btnBgColorList = ((btnOn.GetComponentsInChildren<UnityEngine.UI.Image>()).Concat(btnOff.GetComponentsInChildren<UnityEngine.UI.Image>()).ToArray()).Concat(button.GetComponentsInChildren<UnityEngine.UI.Image>()).ToArray();
            //foreach (UnityEngine.UI.Image btnBackground in btnBgColorList) btnBackground.color = buttonBackgroundColor;
            button.GetComponentInChildren<UnityEngine.UI.Button>().colors = new ColorBlock()
            {
                colorMultiplier = 1f,
                disabledColor = Color.grey,
                highlightedColor = buttonBackgroundColor * 1.5f,
                normalColor = buttonBackgroundColor / 1.5f,
                pressedColor = Color.grey * 1.5f
            };
        }

        public override void setTextColor(Color buttonTextColor, bool save = true)
        {
            button.GetComponentInChildren<Text>().color = buttonTextColor;
            if (save)
                OrigText = (Color)buttonTextColor;
        }
    }
}