using System;
using System.IO;
using System.Reflection;
using BepInEx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace RoR2BedrockEdition
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    public class RoR2_BedrockEdition : BaseUnityPlugin
    {
        public const string PluginGUID = PluginAuthor + "." + PluginName;
        public const string PluginAuthor = "Yesterdave&Ascellayn";
        public const string PluginName = "RoR2BedrockEdition";
        public const string PluginVersion = "1.0.0";
        public void Awake()
        {
            Log.Init(Logger);
            
            string replacePath = "RoR2BedrockEdition.Resources.title.png";

            On.RoR2.UI.MainMenu.MainMenuController.Awake += (_, self) =>
            {
                try
                {
                    var images = self.titleMenuScreen.GetComponentsInChildren<Image>();
                    foreach (var logo in images)
                    {
                        if (logo.transform.name == "LogoImage")
                        {
                            byte[] bytes = ReadBytes(replacePath);
                            logo.sprite.texture.LoadImage(bytes);
                            return;
                        }
                    }
                }
                catch (Exception e)
                {
                    Log.LogError("Error replacing title: " + e.Message);
                }
            };
        }
        
        private static byte[] ReadBytes(string path)
        {
            using var assetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
            using BinaryReader reader = new BinaryReader(assetStream);
            
            return reader.ReadBytes((int)assetStream.Length);
        }
    }
}
