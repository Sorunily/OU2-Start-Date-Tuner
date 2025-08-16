using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Rosa.StartDateTuner
{
    [BepInPlugin("ou2.startdatetuner", "OU2 Start Date Tuner", "1.0.0")]
    public class StartDateTuner : BaseUnityPlugin
    {
        public static ConfigEntry<float> StartYear;

        void Awake()
        {
            StartYear = Config.Bind("StartDate", "Year", 2100f, "Starting year for new worlds.");
            new Harmony(Info.Metadata.GUID).PatchAll();
            Logger.LogInfo($"Start Date Tuner loaded. Year={StartYear.Value}");
        }

        [HarmonyPatch]
        static class PatchSimulationCtor
        {
            static IEnumerable<MethodBase> TargetMethods()
            {
                var simType = AccessTools.TypeByName("OU2.Engine.World.Simulation");
                if (simType == null) yield break;

                foreach (var c in simType.GetConstructors(BindingFlags.Instance |
                                                          BindingFlags.Public |
                                                          BindingFlags.NonPublic))
                    yield return c;
            }

            static void Postfix(object __instance)
            {
                try
                {
                    var t = __instance.GetType();

                    var prop = t.GetProperty("Date", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    var set = prop?.GetSetMethod(true);
                    if (set != null)
                    {
                        set.Invoke(__instance, new object[] { StartYear.Value });
                        return;
                    }

                    var backing = AccessTools.Field(t, "<Date>k__BackingField");
                    if (backing != null)
                        backing.SetValue(__instance, StartYear.Value);
                }
                catch (Exception e)
                {
                    BepInEx.Logging.Logger.CreateLogSource("StartDateTuner")
                        .LogError($"Failed to set start year: {e}");
                }
            }
        }
    }
}