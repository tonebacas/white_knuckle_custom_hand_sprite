using System.IO;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using UnityEngine.Bindings;

// ReSharper disable InconsistentNaming
// ReSharper disable RedundantBaseQualifier

namespace custom_hand_sprite;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class CustomHandsSpriteMod : BaseUnityPlugin
{
    private static ManualLogSource _logger;
    private Harmony _harmony;

    private static ConfigEntry<bool> Enable { get; set; }

    private void Awake()
    {
        // Plugin startup logic
        _logger = base.Logger;
        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");

        Enable = Config.Bind("General", "Enable", true, "Enable loading custom hand sprites");
        Config.Save();

        _harmony = Harmony.CreateAndPatchAll(GetType(), MyPluginInfo.PLUGIN_GUID);
    }

    private void OnDestroy()
    {
        _logger.LogInfo($"Unloading plugin {MyPluginInfo.PLUGIN_GUID}");
        _harmony?.UnpatchSelf();
    }

    [HarmonyPatch(typeof(CL_Player), "Start")]
    [HarmonyPostfix]
    private static void Patch_CL_Player_Start(CL_Player __instance)
    {
        if (Enable is not { BoxedValue: true })
        {
            return;
        }

        var assetsPath = Path.Combine(Paths.PluginPath, MyPluginInfo.PLUGIN_GUID);
        assetsPath = Path.Combine(assetsPath, "assets");
        var leftHandImagePath = Path.Combine(assetsPath, "left_hand.png");
        var rightHandImagePath = Path.Combine(assetsPath, "right_hand.png");

        if (File.Exists(leftHandImagePath))
        {
            ChangeHandSprite(__instance.hands[0], leftHandImagePath);
        }

        if (File.Exists(rightHandImagePath))
        {
            ChangeHandSprite(__instance.hands[1], rightHandImagePath);
        }

        return;

        static void ChangeSpriteTexture([NotNull] ref Sprite handSprite, [NotNull] Texture2D handTexture,
            Vector2 targetPivot)
        {
            var sprite = handSprite;
            var spriteRect = sprite.rect;
            var pivot = targetPivot / spriteRect.size;
            Sprite newSprite = Sprite.Create(handTexture, spriteRect, pivot, sprite.pixelsPerUnit);
            handSprite = newSprite;
        }

        static void ChangeHandSprite([NotNull] CL_Player.Hand hand, [NotNull] string handImagePath)
        {
            byte[] handImageData = File.ReadAllBytes(handImagePath);

            Texture2D handTexture;
            {
                var tex = hand.openSprite.texture;
                int width = tex.width;
                int height = tex.height;
                bool useMipmap = tex.mipmapCount > 1;
                handTexture = new Texture2D(width, height, TextureFormat.RGBA32, useMipmap)
                {
                    filterMode = FilterMode.Point,
                    wrapMode = TextureWrapMode.Clamp
                };
                handTexture.LoadImage(handImageData);
            }

            ChangeSpriteTexture(ref hand.openSprite, handTexture, hand.openSprite.pivot);
            ChangeSpriteTexture(ref hand.normalSprite, handTexture, hand.normalSprite.pivot);
            ChangeSpriteTexture(ref hand.grabSprite, handTexture, hand.grabSprite.pivot);
        }
    }
}