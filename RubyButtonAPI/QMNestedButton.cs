using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Net;

namespace RubyButtonAPI
{
    public class QMNestedButton
    {
        protected QMSingleButton mainButton;
        protected QMSingleButton backButton;
        protected string menuName;
        protected string btnQMLoc;
        protected string btnType;
        protected bool IsTabMenu;

        public QMNestedButton(QMNestedButton btnMenu, int btnXLocation, int btnYLocation, String btnText, String btnToolTip, Color? btnBackgroundColor = null, Color? btnTextColor = null, Color? backbtnBackgroundColor = null, Color? backbtnTextColor = null)
        {
            IsTabMenu = false;
            btnQMLoc = btnMenu.getMenuName();
            InitButtonOriginal(btnXLocation, btnYLocation, btnText, btnToolTip, btnBackgroundColor, btnTextColor, backbtnBackgroundColor, backbtnTextColor);
        }

        public QMNestedButton(string btnMenu, int btnXLocation, int btnYLocation, String btnText, String btnToolTip, Color? btnBackgroundColor = null, Color? btnTextColor = null, Color? backbtnBackgroundColor = null, Color? backbtnTextColor = null)
        {
            IsTabMenu = false;
            btnQMLoc = btnMenu;
            InitButtonOriginal(btnXLocation, btnYLocation, btnText, btnToolTip, btnBackgroundColor, btnTextColor, backbtnBackgroundColor, backbtnTextColor);
        }
        public QMNestedButton(string Name, string Base64Image = null, string imageurl = null, byte[] imagebytes = null)
        {
            IsTabMenu = true;
            MelonLoader.MelonCoroutines.Start(InitButtonNew(Name, Base64Image, imageurl, imagebytes));
        }
        private IEnumerator InitButtonNew(string Name, string base64image, string ImageURL, byte[] imagebytes) 
        {
            menuName = QMButtonAPI.identifier + "_Custom_Menu_" + Name;
            while (MonoBehaviourPublicObCoGaCoObCoObCoUnique.prop_MonoBehaviourPublicObCoGaCoObCoObCoUnique_0 == null || MonoBehaviourPublicObCoGaCoObCoObCoUnique.prop_MonoBehaviourPublicObCoGaCoObCoObCoUnique_0.field_Public_ArrayOf_GameObject_0 == null)
            {
                yield return null;
            }
            byte[] imagedata = null;
            if (base64image != null && ImageURL == null && imagebytes == null)
            {
                imagedata = Convert.FromBase64String(base64image);
            }
            else if (ImageURL != null && base64image == null && imagebytes == null)
            {
                WebClient wc = new WebClient();
                imagedata = wc.DownloadData(ImageURL);
            }
            else if (imagebytes != null && ImageURL == null && base64image == null)
            {
                imagedata = imagebytes;
            }
            else 
            {
                MelonLoader.MelonLogger.Error("Well Shit someone is dumb");
                yield break;
            }
            Texture2D icontexture = new Texture2D(2, 2, TextureFormat.ARGB32, false);
            ImageConversion.LoadImage(icontexture, imagedata);


            Transform menu = UnityEngine.Object.Instantiate<Transform>(QMStuff.NestedMenuTemplate(), QMStuff.GetQuickMenuInstance().transform);
            menu.name = menuName;

            var TabButton = GameObject.Instantiate(GameObject.Find("UserInterface/QuickMenu/QuickModeTabs/NotificationsTab"), GameObject.Find("UserInterface/QuickMenu/QuickModeTabs").transform);
            TabButton.name = QMButtonAPI.identifier +"_"+ Name;

            GameObject.Destroy(TabButton.transform.Find("Badge"));

            TabButton.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
            TabButton.GetComponent<Button>().onClick.AddListener((Action) delegate() 
            {
                QMStuff.ShowQuickmenuPage(menuName);
            });

            TabButton.transform.Find("Icon").GetComponent<Image>().sprite = new Sprite();
            TabButton.transform.Find("Icon").GetComponent<Image>().material = new Material(TabButton.transform.Find("Icon").GetComponent<Image>().material)
            {
                mainTexture = icontexture
            };
            Il2CppSystem.Collections.IEnumerator enumerator = menu.transform.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Il2CppSystem.Object obj = enumerator.Current;
                Transform btnEnum = obj.Cast<Transform>();
                if (btnEnum != null)
                {
                    UnityEngine.Object.Destroy(btnEnum.gameObject);
                }
            }
            QMButtonAPI.allNestedButtons.Add(this);
            backButton = new QMSingleButton(this, 5, 2, "Back", () => { QMStuff.ShowQuickmenuPage("ShortcutMenu"); }, "Go Back", Color.cyan, Color.yellow);
            yield break;
        }

        public void InitButtonOriginal(int btnXLocation, int btnYLocation, String btnText, String btnToolTip, Color? btnBackgroundColor = null, Color? btnTextColor = null, Color? backbtnBackgroundColor = null, Color? backbtnTextColor = null)
        {
            btnType = "NestedButton";

            Transform menu = UnityEngine.Object.Instantiate<Transform>(QMStuff.NestedMenuTemplate(), QMStuff.GetQuickMenuInstance().transform);
            menuName = QMButtonAPI.identifier + btnQMLoc + "_" + btnXLocation + "_" + btnYLocation;
            menu.name = menuName;

            mainButton = new QMSingleButton(btnQMLoc, btnXLocation, btnYLocation, btnText, () => { QMStuff.ShowQuickmenuPage(menuName); }, btnToolTip, btnBackgroundColor, btnTextColor);

            Il2CppSystem.Collections.IEnumerator enumerator = menu.transform.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Il2CppSystem.Object obj = enumerator.Current;
                Transform btnEnum = obj.Cast<Transform>();
                if (btnEnum != null)
                {
                    UnityEngine.Object.Destroy(btnEnum.gameObject);
                }
            }

            if (backbtnTextColor == null)
            {
                backbtnTextColor = Color.yellow;
            }
            QMButtonAPI.allNestedButtons.Add(this);
            backButton = new QMSingleButton(this, 5, 2, "Back", () => { QMStuff.ShowQuickmenuPage(btnQMLoc); }, "Go Back", backbtnBackgroundColor, backbtnTextColor);
        }

        public string getMenuName()
        {
            return menuName;
        }

        public bool getIsTabMenu() 
        {
            return IsTabMenu;
        }
        public QMSingleButton getMainButton()
        {
            return mainButton;
        }

        public QMSingleButton getBackButton()
        {
            return backButton;
        }

        public void DestroyMe()
        {
            mainButton.DestroyMe();
            backButton.DestroyMe();
        }
    }
}