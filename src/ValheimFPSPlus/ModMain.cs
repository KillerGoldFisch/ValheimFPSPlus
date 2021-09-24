using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using BepInEx;
using HarmonyLib;
using UnityEngine;


namespace ValheimFPSPlus
{
    [BepInPlugin(PluginId, "ValheimFPSPlus", "1.1.2")]
    [BepInProcess("valheim.exe")]
    public class ModMain : BaseUnityPlugin
    {
        public const string PluginId = "KillerGoldFisch.ValheimFPSPlus";

        public static ModMain Instance { get; private set; }

        private Harmony _harmony;
        private ModConfig _modConfig;

        #region EntrPoints

        public ModMain()
        {
            Instance = this;
        }

        private void Awake()
        {
            _modConfig = new ModConfig(this);
            _modConfig.SettingsChanged += (sender, args) => ApplyGraphicsSetting();
            _harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), PluginId);

            Logger.LogInfo("Original settings:");
            LogGraphicsSetting();
            ApplyGraphicsSetting();
        }

        private void OnDestroy() {
            _harmony?.UnpatchAll(PluginId);
        }
        #endregion

        private void ApplyGraphicsSetting()
        {
            if (_modConfig is null)
            {
                Logger.LogWarning("[ValheimFPSPlus] ModConfig is null");
                return;
            }

            QualitySettings.anisotropicFiltering     = _modConfig.AnisotropicFiltering.Value;
            QualitySettings.lodBias                  = _modConfig.LodBias.Value;
            QualitySettings.masterTextureLimit       = _modConfig.MasterTextureLimit.Value;
            QualitySettings.maximumLODLevel          = _modConfig.MaximumLODLevel.Value;
            QualitySettings.maxQueuedFrames          = _modConfig.MaxQueuedFrames.Value;
            QualitySettings.particleRaycastBudget    = _modConfig.ParticleRaycastBudget.Value;
            QualitySettings.pixelLightCount          = _modConfig.PixelLightCount.Value;
            QualitySettings.realtimeReflectionProbes = _modConfig.RealtimeReflectionProbes.Value;
            QualitySettings.shadowCascades           = _modConfig.ShadowCascades.Value;
            QualitySettings.shadowDistance           = _modConfig.ShadowDistance.Value;
            QualitySettings.shadows                  = _modConfig.Shadows.Value;
            QualitySettings.skinWeights              = _modConfig.SkinWeights.Value;
            QualitySettings.softParticles            = _modConfig.SoftParticles.Value;
            QualitySettings.softVegetation           = _modConfig.SoftVegetation.Value;

            Logger.LogInfo("Quality settings applied!");
        }

        private void LogGraphicsSetting()
        {
            Logger.LogInfo("QualitySettings.anisotropicFiltering = " + QualitySettings.anisotropicFiltering.ToString());
            Logger.LogInfo("QualitySettings.lodBias = " + QualitySettings.lodBias.ToString());
            Logger.LogInfo("QualitySettings.masterTextureLimit = " + QualitySettings.masterTextureLimit.ToString());
            Logger.LogInfo("QualitySettings.maximumLODLevel = " + QualitySettings.maximumLODLevel.ToString());
            Logger.LogInfo("QualitySettings.maxQueuedFrames = " + QualitySettings.maxQueuedFrames.ToString());
            Logger.LogInfo("QualitySettings.particleRaycastBudget = " + QualitySettings.particleRaycastBudget.ToString());
            Logger.LogInfo("QualitySettings.pixelLightCount = " + QualitySettings.pixelLightCount.ToString());
            Logger.LogInfo("QualitySettings.realtimeReflectionProbes = " + QualitySettings.realtimeReflectionProbes.ToString());
            Logger.LogInfo("QualitySettings.shadowCascades = " + QualitySettings.shadowCascades.ToString());
            Logger.LogInfo("QualitySettings.shadowDistance = " + QualitySettings.shadowDistance.ToString());
            Logger.LogInfo("QualitySettings.shadows = " + QualitySettings.shadows.ToString());
            Logger.LogInfo("QualitySettings.skinWeights = " + QualitySettings.skinWeights.ToString());
            Logger.LogInfo("QualitySettings.softParticles = " + QualitySettings.softParticles.ToString());
            Logger.LogInfo("QualitySettings.softVegetation = " + QualitySettings.softVegetation.ToString());
        }

        public void LogInfo(string msg) => Logger.LogInfo(msg);
    }
}
