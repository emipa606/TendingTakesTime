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
    public static TendingTakesTimeMod instance;

    private static string currentVersion;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="content"></param>
    public TendingTakesTimeMod(ModContentPack content) : base(content)
    {
        instance = this;
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
        var listing_Standard = new Listing_Standard();
        listing_Standard.Begin(rect);
        listing_Standard.Label("TTT.Increases".Translate(), tooltip: "TTT.IncreasesTT".Translate());
        listing_Standard.CheckboxLabeled("TTT.HeavyBleeding".Translate(), ref Settings.HeavyBleeding,
            "TTT.HeavyBleedingTT".Translate());
        listing_Standard.CheckboxLabeled("TTT.LifeThreatening".Translate(), ref Settings.LifeThreatening,
            "TTT.LifeThreateningTT".Translate());
        listing_Standard.CheckboxLabeled("TTT.Internal".Translate(), ref Settings.Internal,
            "TTT.InternalTT".Translate());
        listing_Standard.CheckboxLabeled("TTT.Missing".Translate(), ref Settings.Missing,
            "TTT.MissingTT".Translate());
        listing_Standard.Gap();
        listing_Standard.Label("TTT.Decreases".Translate(), tooltip: "TTT.DecreasesTT".Translate());
        listing_Standard.CheckboxLabeled("TTT.LowBleeding".Translate(), ref Settings.LowBleeding,
            "TTT.LowBleedingTT".Translate());
        listing_Standard.CheckboxLabeled("TTT.Permanent".Translate(), ref Settings.Permanent,
            "TTT.PermanentTT".Translate());
        listing_Standard.CheckboxLabeled("TTT.External".Translate(), ref Settings.External,
            "TTT.ExternalTT".Translate());
        listing_Standard.Gap();
        listing_Standard.GapLine();

        listing_Standard.Label("TTT.Multipliers".Translate(), tooltip: "TTT.MultipliersTT".Translate());
        Settings.LargeDecrease = listing_Standard.SliderLabeled(
            "TTT.LargeDecrease".Translate((1f - Settings.LargeDecrease).ToStringPercent()), Settings.LargeDecrease,
            0.1f,
            Settings.SmallDecrease);
        Settings.SmallDecrease = listing_Standard.SliderLabeled(
            "TTT.SmallDecrease".Translate((1f - Settings.SmallDecrease).ToStringPercent()), Settings.SmallDecrease,
            Settings.LargeDecrease, 1f);
        Settings.SmallIncrease = listing_Standard.SliderLabeled(
            "TTT.SmallIncrease".Translate((Settings.SmallIncrease - 1f).ToStringPercent()), Settings.SmallIncrease, 1f,
            Settings.LargeIncrease);
        Settings.LargeIncrease = listing_Standard.SliderLabeled(
            "TTT.LargeIncrease".Translate((Settings.LargeIncrease - 1f).ToStringPercent()), Settings.LargeIncrease,
            Settings.SmallIncrease, 2f);
        listing_Standard.Gap();

        listing_Standard.GapLine();
        if (listing_Standard.ButtonText("Reset".Translate(), widthPct: 0.25f))
        {
            Settings.Reset();
        }

        listing_Standard.CheckboxLabeled("TTT.VerboseLogging".Translate(), ref Settings.VerboseLogging,
            "TTT.VerboseLoggingTT".Translate());
        if (currentVersion != null)
        {
            listing_Standard.Gap();
            GUI.contentColor = Color.gray;
            listing_Standard.Label("TTT.CurrentModVersion".Translate(currentVersion));
            GUI.contentColor = Color.white;
        }

        listing_Standard.End();
    }
}