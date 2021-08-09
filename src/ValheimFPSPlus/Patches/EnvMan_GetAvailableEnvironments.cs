using System.Collections.Generic;
using System.Linq;
using HarmonyLib;

namespace ValheimFPSPlus.Patches
{
    [HarmonyPatch(typeof(EnvMan), "GetAvailableEnvironments")]
    public class EnvMan_GetAvailableEnvironments
    {
        static void Postfix(ref List<EnvEntry> __result)
        {
            if (!ModConfig.Instance?.NoSnowstorms?.Value ?? false)
                return;

            if (__result is null)
                return;

            if (!ContainsSnowStorm(__result))
                return;

            //ModMain.Instance.LogInfo("Old __result: " + __result.Count);

            __result = __result.Where(e => !SnowStorms.Contains(e.m_environment)).ToList();

            //ModMain.Instance.LogInfo("New __result: " + __result.Count);
        }

        private static bool ContainsSnowStorm(IEnumerable<EnvEntry> env)
        {
            foreach (var entry in env)
                if (SnowStorms.Contains(entry.m_environment))
                    return true;

            return false;
        }

        private static readonly string Snow = "Snow";

        private static readonly string[] SnowStorms = new string[]
        {
            "Twilight_SnowStorm",
            "SnowStorm",
            "Moder",
        };
    }
}