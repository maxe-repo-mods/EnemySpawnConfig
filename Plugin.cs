using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;

namespace EnemySpawnConfig;

[BepInPlugin(PluginGuid, PluginName, PluginVersion)]
public class Plugin : BaseUnityPlugin
{
    private const string PluginGuid = "maxenterme.EnemySpawnConfig";
    private const string PluginName = "EnemySpawnConfig";
    private const string PluginVersion = "1.0.4";

    internal static Plugin Instance { get; private set; } = null!;
    internal new static ManualLogSource Logger => Instance._logger;
    private ManualLogSource _logger => base.Logger;

    internal static ConfigEntry<int> SpawnMultiplier = null!;

    internal static ConfigEntry<int> Difficulty1Multiplier = null!;
    internal static ConfigEntry<int> Difficulty2Multiplier = null!;
    internal static ConfigEntry<int> Difficulty3Multiplier = null!;

    internal static ConfigEntry<bool> DisableAllEnemies = null!;

    private void Awake()
    {
        Instance = this;

        SpawnMultiplier = Config.Bind("General", "SpawnMultiplier", 100,
            new ConfigDescription("Global multiplier for enemy group counts (100 = 100%, 0 = no enemies)", new AcceptableValueRange<int>(0, 500)));

        Difficulty1Multiplier = Config.Bind("PerDifficulty", "Difficulty1Multiplier", 100,
            new ConfigDescription("Multiplier for Difficulty 1 (easiest) enemy groups (100 = 100%)", new AcceptableValueRange<int>(0, 500)));
        Difficulty2Multiplier = Config.Bind("PerDifficulty", "Difficulty2Multiplier", 100,
            new ConfigDescription("Multiplier for Difficulty 2 (medium) enemy groups (100 = 100%)", new AcceptableValueRange<int>(0, 500)));
        Difficulty3Multiplier = Config.Bind("PerDifficulty", "Difficulty3Multiplier", 100,
            new ConfigDescription("Multiplier for Difficulty 3 (hardest) enemy groups (100 = 100%)", new AcceptableValueRange<int>(0, 500)));

        DisableAllEnemies = Config.Bind("General", "DisableAllEnemies", false,
            "Set to true to disable all enemy spawning (peaceful mode).");

        new Harmony(PluginGuid).PatchAll(typeof(Plugin).Assembly);
        Logger.LogInfo($"{PluginName} v{PluginVersion} loaded!");
    }
}
