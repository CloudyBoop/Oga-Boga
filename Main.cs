using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using Photon.Realtime;
using RubyButtonAPI;
using MelonLoader;

[assembly: MelonLoader.MelonGame("VRChat", "VRChat")]
[assembly: MelonLoader.MelonInfo(typeof(Oga_Boga.Main), "Oga Boga", "♥\t♥\t♥ KEK", "KeafyIsHere, WTFBlaze, Spacers.VIP")]

namespace Oga_Boga
{
    public class Main : MelonMod
    {
        /*
         
            Blaze's Changes:
            - Updated the mod to MelonLoader v0.4.0.
            - Updated the Harmony patching to Harmony X
            - Added a label at the bottom of QM That shows if you are invisible or not.
         */
        /*
             Spacers.VIP Changes:
             - Updated To MelonLoader v0.4.3
             - Fixed the VRCUiManager error making it so you cant join lobbys and you will be stuck in a loop
         */


        private static bool _goInvisble = false;
        const string Icon = "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";
        private static QMToggleButton _invisbleToggleButton;
        private GameObject InvisLabel;
        private Text InvisTXT;
        
        public override void OnApplicationStart()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("This mod only makes it so masterclient can see you. \nYes you can just not send out event 202 but to be safe I made it so only masterclient can see you\nOnly made this because most my friends do scuffed ways of doing invisble and they wanted something safe");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Just a note. There are clients & mods that are able to detect you being in the lobby. \nSo while nobody can see you in the social menu or see you in the lobby, you are still able to be seen in playerlists if the mod creators look for it. ~ Blaze");
            Console.ResetColor();
            MelonCoroutines.Start(WaitForUI());
            try
            {
                HarmonyLib.Harmony.CreateAndPatchAll(typeof(Main), "♥-BlazeWasHere-LMAO-KEK-♥");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("OGA BOGA SUCCESSFULLY PATCHED!");
                Console.ResetColor();
            }
            catch 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("OGA BOGA FAILED TO PATCH!");
                Console.ResetColor();
            }
        }

        [HarmonyPatch(typeof(Photon.Realtime.LoadBalancingClient), "Method_Public_Virtual_New_Boolean_Byte_Object_RaiseEventOptions_SendOptions_0"), HarmonyPrefix]
        private static bool oof(byte __0, RaiseEventOptions __2) 
        {
            switch (__0)
            {
                case 202:
                    __2.field_Public_ReceiverGroup_0 = _goInvisble ? ReceiverGroup.MasterClient : ReceiverGroup.Others;
                    _goInvisble = false;
                    _invisbleToggleButton.setToggleState(false, false);
                    break;
            }
            return true;
        }

        private IEnumerator WaitForUI()
        {
            while (VRCUiManager.field_Private_Static_VRCUiManager_0 == null)
                yield return null;
            VRCUI();
        }

        public void VRCUI()
        {
            QMNestedButton TabButton = new QMNestedButton("Hentai", Icon);
            _invisbleToggleButton = new QMToggleButton(TabButton, 1, 0, "Safe Invisble On", delegate () { _goInvisble = true; }, "Safe Invisble Off", delegate () { _goInvisble = false; }, "Toggle for joining worlds either Invisble or Visble", Color.cyan, Color.white);
            new QMSingleButton(TabButton, 2, 0, "Join World\n(Clipboard)", delegate ()
            {
                _goInvisble = true;
                string ImGay = GUIUtility.systemCopyBuffer;
                new PortalInternal().Method_Private_Void_String_String_PDM_0(ImGay.Split(':')[0], ImGay.Split(':')[1]);
            }, "Tries to join world from clipboard");

            InvisLabel = GameObject.Instantiate(QMStuff.GetQuickMenuInstance().transform.Find("QuickMenu_NewElements/_InfoBar/EarlyAccessText").gameObject, QMStuff.GetQuickMenuInstance().transform.Find("QuickMenu_NewElements/_InfoBar/"));
            InvisLabel.name = "Oga Boga Indicator";
            InvisLabel.transform.localScale = new Vector3(1, 1, 1);
            InvisLabel.GetComponent<RectTransform>().anchoredPosition = new Vector2(780, -230);
            InvisTXT = InvisLabel.GetComponent<Text>();
            InvisTXT.fontStyle = FontStyle.Bold;
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (InvisLabel == null) return;

            if (_goInvisble && InvisLabel != null)
            {
                InvisTXT.color = Color.red;
                InvisTXT.text = "YOU ARE CURRENTLY INVISIBLE!";
            }
            else
            {
                InvisTXT.color = Color.green;
                InvisTXT.text = "YOU ARE CURRENTLY SHOWN!";
            }
        }
    }
}
