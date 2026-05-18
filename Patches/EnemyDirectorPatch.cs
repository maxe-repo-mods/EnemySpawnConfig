using System;
using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;

namespace EnemySpawnConfig.Patches;

[HarmonyPatch]
public static class EnemyDirectorPatch
{
    [HarmonyPatch(typeof(EnemyDirector), nameof(EnemyDirector.AmountSetup))]
    [HarmonyPostfix]
    private static void AmountSetup_Postfix(EnemyDirector __instance)
    {
        try
        {
            if (!SemiFunc.IsMasterClientOrSingleplayer()) return;

            if (Plugin.DisableAllEnemies.Value)
            {
                __instance.totalAmount = 0;
                __instance.amountCurve1Value = 0;
                __instance.amountCurve2Value = 0;
                __instance.amountCurve3Value = 0;
                __instance.enemyList.Clear();
                Plugin.Logger.LogInfo("EnemySpawnConfig: all enemies disabled.");
                return;
            }

            float globalMult = Plugin.SpawnMultiplier.Value / 100f;
            float d1Mult = Plugin.Difficulty1Multiplier.Value / 100f;
            float d2Mult = Plugin.Difficulty2Multiplier.Value / 100f;
            float d3Mult = Plugin.Difficulty3Multiplier.Value / 100f;

            int origD1 = __instance.amountCurve1Value;
            int origD2 = __instance.amountCurve2Value;
            int origD3 = __instance.amountCurve3Value;
            int origTotal = __instance.totalAmount;
            int origListCount = __instance.enemyList.Count;

            // Scale budget values
            __instance.amountCurve1Value = Mathf.RoundToInt(origD1 * d1Mult * globalMult);
            __instance.amountCurve2Value = Mathf.RoundToInt(origD2 * d2Mult * globalMult);
            __instance.amountCurve3Value = Mathf.RoundToInt(origD3 * d3Mult * globalMult);
            int newTotal = __instance.amountCurve1Value + __instance.amountCurve2Value + __instance.amountCurve3Value;
            __instance.totalAmount = newTotal;

            // Expand or shrink enemyList to match new totalAmount
            if (newTotal > origListCount && origListCount > 0)
            {
                for (int i = origListCount; i < newTotal; i++)
                    __instance.enemyList.Add(__instance.enemyList[i % origListCount]);
            }
            else if (newTotal < origListCount && newTotal >= 0)
            {
                __instance.enemyList.RemoveRange(newTotal, origListCount - newTotal);
            }

            Plugin.Logger.LogDebug($"EnemySpawnConfig: total {origTotal} -> {newTotal}, list {origListCount} -> {__instance.enemyList.Count} " +
                $"(d1={origD1}->{__instance.amountCurve1Value}, d2={origD2}->{__instance.amountCurve2Value}, d3={origD3}->{__instance.amountCurve3Value})");
        }
        catch (Exception e)
        {
            Plugin.Logger.LogError($"EnemySpawnConfig AmountSetup error: {e}");
        }
    }
}
