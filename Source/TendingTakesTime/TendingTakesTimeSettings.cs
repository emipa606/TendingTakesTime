using Verse;

namespace TendingTakesTime;

/// <summary>
///     Definition of the settings for the mod
/// </summary>
internal class TendingTakesTimeSettings : ModSettings
{
    public bool External = true;
    public bool HeavyBleeding = true;
    public bool Internal = true;
    public float LargeDecrease = 0.5f;
    public float LargeIncrease = 1.5f;
    public bool LifeThreatening = true;
    public bool LowBleeding = true;
    public bool Missing = true;
    public bool Permanent = true;
    public float SmallDecrease = 0.75f;
    public float SmallIncrease = 1.25f;
    public bool VerboseLogging;

    /// <summary>
    ///     Saving and loading the values
    /// </summary>
    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref HeavyBleeding, "HeavyBleeding", true);
        Scribe_Values.Look(ref LowBleeding, "LowBleeding", true);
        Scribe_Values.Look(ref Permanent, "Permanent", true);
        Scribe_Values.Look(ref Missing, "Missing", true);
        Scribe_Values.Look(ref LifeThreatening, "LifeThreatening", true);
        Scribe_Values.Look(ref Internal, "Internal", true);
        Scribe_Values.Look(ref External, "External", true);
        Scribe_Values.Look(ref LargeDecrease, "LargeDecrease", 0.5f);
        Scribe_Values.Look(ref SmallDecrease, "SmallDecrease", 0.75f);
        Scribe_Values.Look(ref SmallIncrease, "SmallIncrease", 1.25f);
        Scribe_Values.Look(ref LargeIncrease, "LargeIncrease", 1.5f);
        Scribe_Values.Look(ref VerboseLogging, "VerboseLogging");
    }

    public void Reset()
    {
        HeavyBleeding = true;
        LowBleeding = true;
        Permanent = true;
        Missing = true;
        LifeThreatening = true;
        Internal = true;
        External = true;
        LargeDecrease = 0.5f;
        SmallDecrease = 0.75f;
        SmallIncrease = 1.25f;
        LargeIncrease = 1.5f;
        VerboseLogging = false;
    }
}