using System.Collections.Generic;
using UnityEngine;

namespace RubyButtonAPI
{
    //Credits:
    //Emilia - Porting it to MelonLoader and helping to keep the git updated
    //Tritn - Helping to keep the git updated

    //This adds a couple of new functions compared to the old one, however,
    //like the last one, I will not be providing any support as I will
    //personally not be using melonloader/unhollower in the near future.

    //Look here for a useful example guide:
    //https://github.com/DubyaDude/RubyButtonAPI/blob/master/RubyButtonAPI_Old.cs

    public static class QMButtonAPI
    {
        //REPLACE THIS STRING SO YOUR MENU DOESNT COLLIDE WITH OTHER MENUS
        public static string identifier = "Oga Boga";
        public static Color mBackground = Color.red;
        public static Color mForeground = Color.white;
        public static Color bBackground = Color.red;
        public static Color bForeground = Color.yellow;
        public static List<QMSingleButton> allSingleButtons = new List<QMSingleButton>();
        public static List<QMToggleButton> allToggleButtons = new List<QMToggleButton>();
        public static List<QMNestedButton> allNestedButtons = new List<QMNestedButton>();
    }
}