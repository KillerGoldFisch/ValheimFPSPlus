using System;
using BepInEx;
using BepInEx.Configuration;
using UnityEngine;

namespace ValheimFPSPlus
{
    public class ModConfig
    {
        public static ModConfig Instance { get; private set; }

        public ConfigEntry<bool> NoSnowstorms { get; set; }
        public ConfigEntry<AnisotropicFiltering> AnisotropicFiltering { get; set; }
        public ConfigEntry<float> LodBias { get; set; }
        public ConfigEntry<int> MasterTextureLimit { get; set; }
        public ConfigEntry<int> MaximumLODLevel { get; set; }
        public ConfigEntry<int> MaxQueuedFrames { get; set; }
        public ConfigEntry<int> ParticleRaycastBudget { get; set; }
        public ConfigEntry<int> PixelLightCount { get; set; }
        public ConfigEntry<bool> RealtimeReflectionProbes { get; set; }
        public ConfigEntry<int> ShadowCascades { get; set; }
        public ConfigEntry<float> ShadowDistance { get; set; }
        public ConfigEntry<ShadowQuality> Shadows { get; set; }
        public ConfigEntry<SkinWeights> SkinWeights { get; set; }
        public ConfigEntry<bool> SoftParticles { get; set; }
        public ConfigEntry<bool> SoftVegetation { get; set; }

        public event EventHandler SettingsChanged;

        public ModConfig(BaseUnityPlugin plugin)
        {
            Instance = this;

            this.NoSnowstorms = plugin.Config.Bind<bool>(
                "General",
                "NoSnowstorms",
                true,
                "Replaces the resource-hungry snowstorm with normal snow"
            );

            this.AnisotropicFiltering = plugin.Config.Bind<AnisotropicFiltering>(
                "General",
                "AnisotropicFiltering",
                UnityEngine.AnisotropicFiltering.Disable,
                "Anisotropic filtering mode.\nValheim Default: ForceEnable\nRecommended: Disable"
            );
            this.AnisotropicFiltering.SettingChanged += Config_SettingChanged;

            this.LodBias = plugin.Config.Bind<float>(
                "General",
                "LodBias",
                1.3f,
                "Global multiplier for the LOD's switching distance.\nValheim Default: 2.0\nRecommended: 1.3"
            );
            this.LodBias.SettingChanged += Config_SettingChanged;

            this.MasterTextureLimit = plugin.Config.Bind<int>(
                "General",
                "MasterTextureLimit",
                1,
                "A texture size limit applied to all textures.\nValheim Default: 0\nRecommended: 1"
            );
            this.MasterTextureLimit.SettingChanged += Config_SettingChanged;

            this.MaximumLODLevel = plugin.Config.Bind<int>(
                "General",
                "MaximumLODLevel",
                0,
                "A maximum LOD level. All LOD groups.\nValheim Default: 0\nRecommended: 0"
            );
            this.MaximumLODLevel.SettingChanged += Config_SettingChanged;

            this.MaxQueuedFrames = plugin.Config.Bind<int>(
                "General",
                "MaxQueuedFrames",
                1,
                "Maximum number of frames queued up by graphics driver.\nValheim Default: 2\nRecommended: 1"
            );
            this.MaxQueuedFrames.SettingChanged += Config_SettingChanged;

            this.ParticleRaycastBudget = plugin.Config.Bind<int>(
                "General",
                "ParticleRaycastBudget",
                1,
                "Budget for how many ray casts can be performed per frame for approximate collision testing.\nValheim Default: 4096\nRecommended: 1"
            );
            this.ParticleRaycastBudget.SettingChanged += Config_SettingChanged;

            this.PixelLightCount = plugin.Config.Bind<int>(
                "General",
                "PixelLightCount",
                0,
                "The maximum number of pixel lights that should affect any object.\nValheim Default: 8,\nRecommended: 0"
            );
            this.PixelLightCount.SettingChanged += Config_SettingChanged;

            this.RealtimeReflectionProbes = plugin.Config.Bind<bool>(
                "General",
                "RealtimeReflectionProbes",
                false,
                "Enables realtime reflection probes.\nValheim Default: True\nRecommended: False"
            );
            this.RealtimeReflectionProbes.SettingChanged += Config_SettingChanged;

            this.ShadowCascades = plugin.Config.Bind<int>(
                "General",
                "ShadowCascades",
                0,
                "Number of cascades to use for directional light shadows.\nValheim Default: 4\nRecommended: 0"
            );
            this.ShadowCascades.SettingChanged += Config_SettingChanged;

            this.ShadowDistance = plugin.Config.Bind<float>(
                "General",
                "ShadowDistance",
                0f,
                "Shadow drawing distance.\nValheim Default: 150.0\nRecommended: 0"
            );
            this.ShadowDistance.SettingChanged += Config_SettingChanged;

            this.Shadows = plugin.Config.Bind<ShadowQuality>(
                "General",
                "Shadows",
                UnityEngine.ShadowQuality.Disable,
                "Realtime Shadows type to be used.\nValheim Default: All\nRecommended: Disable"
            );
            this.Shadows.SettingChanged += Config_SettingChanged;

            this.SkinWeights = plugin.Config.Bind<SkinWeights>(
                "General",
                "SkinWeights",
                UnityEngine.SkinWeights.OneBone,
                "The maximum number of bones per vertex that are taken into account during skinning, for all meshes in the project.\nValheim Default: FourBones\nRecommended: OneBone"
            );
            this.SkinWeights.SettingChanged += Config_SettingChanged;

            this.SoftParticles = plugin.Config.Bind<bool>(
                "General",
                "SoftParticles",
                false,
                "Should soft blending be used for particles?\nValheim Default: True\nRecommended: False"
            );
            this.SoftParticles.SettingChanged += Config_SettingChanged;

            this.SoftVegetation = plugin.Config.Bind<bool>(
                "General",
                "SoftVegetation",
                false,
                "Use a two-pass shader for the vegetation in the terrain engine.\nValheim Default: True\nRecommended: False"
            );
            this.SoftVegetation.SettingChanged += Config_SettingChanged;

        }

        private void Config_SettingChanged(object sender, System.EventArgs e) {
            SettingsChanged?.Invoke(sender, e);
        }
    }
}