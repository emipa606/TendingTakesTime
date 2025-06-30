using Mlie;
using UnityEngine;
using Verse;

namespace TendingTakesTime;

[StaticConstructorOnStartup]
internal class TendingTakesTimeMod : Mod
{
    /// <summary>
    ///     The instance of the settings to be read by the mod
    /// </summary>
    public static TendingTakesTimeMod Instance;

    private static string currentVersion;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="content"></param>
    public TendingTakesTimeMod(ModContentPack content) : base(content)
    {
        Instance = this;
        Settings = GetSettings<TendingTakesTimeSettings>();
        currentVersion = VersionFromManifest.GetVersionFromModMetaData(content.ModMetaData);
    }

    /// <summary>
    ///     The instance-settings for the mod
    /// </summary>
    internal TendingTakesTimeSettings Settings { get; }

    /// <summary>
    ///     The title for the mod-settings
    /// </summary>
    /// <returns></returns>
    public override string SettingsCategory()
    {
        return "Tending Takes Time";
    }

    /// <summary>
    ///     The settings-window
    ///     For more info: https://rimworldwiki.com/wiki/Modding_Tutorials/ModSettings
    /// </summary>
    /// <param name="rect"></param>
    public override void DoSettingsWindowContents(Rect rect)
    {
        var listingStandard = new Listing_Standard();
        listingStandard.Begin(rect);
        listingStandard.Label("TTT.Increases".Translate(), tooltip: "TTT.IncreasesTT".Translate());
        listingStandard.CheckboxLabeled("TTT.HeavyBleeding".Translate(), ref Settings.HeavyBleeding,
            "TTT.HeavyBleedingTT".Translate());
        listingStandard.CheckboxLabeled("TTT.LifeThreatening".Translate(), ref Settings.LifeThreatening,
            "TTT.LifeThreateningTT".Translate());
        listingStandard.CheckboxLabeled("TTT.Internal".Translate(), ref Settings.Internal,
            "TTT.InternalTT".Translate());
        listingStandard.CheckboxLabeled("TTT.Missing".Translate(), ref Settings.Missing,
            "TTT.MissingTT".Translate());
        listingStandard.Gap();
        listingStandard.Label("TTT.Decreases".Translate(), tooltip: "TTT.DecreasesTT".Translate());
        listingStandard.CheckboxLabeled("TTT.LowBleeding".Translate(), ref Settings.LowBleeding,
            "TTT.LowBleedingTT".Translate());
        listingStandard.CheckboxLabeled("TTT.Permanent".Translate(), ref Settings.Permanent,
            "TTT.PermanentTT".Translate());
        listingStandard.CheckboxLabeled("TTT.External".Translate(), ref Settings.External,
            "TTT.ExternalTT".Translate());
        listingStandard.Gap();
        listingStandard.GapLine();

        listingStandard.Label("TTT.Multipliers".Translate(), tooltip: "TTT.MultipliersTT".Translate());
        Settings.LargeDecrease = listingStandard.SliderLabeled(
            "TTT.LargeDecrease".Translate((1f - Settings.LargeDecrease).ToStringPercent()), Settings.LargeDecrease,
            0.1f,
            Settings.SmallDecrease);
        Settings.SmallDecrease = listingStandard.SliderLabeled(
            "TTT.SmallDecrease".Translate((1f - Settings.SmallDecrease).ToStringPercent()), Settings.SmallDecrease,
            Settings.LargeDecrease, 1f);
        Settings.SmallIncrease = listingStandard.SliderLabeled(
            "TTT.SmallIncrease".Translate((Settings.SmallIncrease - 1f).ToStringPercent()), Settings.SmallIncrease, 1f,
            Settings.LargeIncrease);
        Settings.LargeIncrease = listingStandard.SliderLabeled(
            "TTT.LargeIncrease".Translate((Settings.LargeIncrease - 1f).ToStringPercent()), Settings.LargeIncrease,
            Settings.SmallIncrease, 2f);
        listingStandard.Gap();

        listingStandard.GapLine();
        if (listingStandard.ButtonText("Reset".Translate(), widthPct: 0.25f))
        {
            Settings.Reset();
        }

        listingStandard.CheckboxLabeled("TTT.VerboseLogging".Translate(), ref Settings.VerboseLogging,
            "TTT.VerboseLoggingTT".Translate());
        if (currentVersion != null)
        {
            listingStandard.Gap();
            GUI.contentColor = Color.gray;
            listingStandard.Label("TTT.CurrentModVersion".Translate(currentVersion));
            GUI.contentColor = Color.white;
        }

        listingStandard.End();
    }
}