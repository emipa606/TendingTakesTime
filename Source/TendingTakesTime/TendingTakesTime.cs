using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using Verse;

namespace TendingTakesTime;

[StaticConstructorOnStartup]
public static class TendingTakesTime
{
    static TendingTakesTime()
    {
        new Harmony("Mlie.TendingTakesTime").PatchAll(Assembly.GetExecutingAssembly());
    }

    public static float CalculateTendOffset(List<Hediff> hediffsToTend, Pawn patient)
    {
        var tendMultiplier = 1f;
        switch (patient.health.hediffSet.BleedRateTotal)
        {
            case 0f when TendingTakesTimeMod.instance.Settings.LowBleeding:
                tendMultiplier *= TendingTakesTimeMod.instance.Settings.LargeDecrease;
                LogMessage("No bleeding, large decrease to tend time");
                break;
            case < 0.25f when TendingTakesTimeMod.instance.Settings.LowBleeding:
                tendMultiplier *= TendingTakesTimeMod.instance.Settings.SmallDecrease;
                LogMessage("Minimal bleeding, decreased tend time");
                break;
            case > 1.25f when TendingTakesTimeMod.instance.Settings.HeavyBleeding:
                tendMultiplier *= TendingTakesTimeMod.instance.Settings.LargeIncrease;
                LogMessage("Massive bleeding, large increase to tend time");
                break;
            case > 0.75f when TendingTakesTimeMod.instance.Settings.HeavyBleeding:
                tendMultiplier *= TendingTakesTimeMod.instance.Settings.SmallIncrease;
                LogMessage("Heavy bleeding, increased tend time");
                break;
        }

        foreach (var hediff in hediffsToTend)
        {
            if (TendingTakesTimeMod.instance.Settings.LifeThreatening && hediff.IsCurrentlyLifeThreatening)
            {
                tendMultiplier *= TendingTakesTimeMod.instance.Settings.SmallIncrease;
                LogMessage($"{hediff} is life threatening, increased tend time");
            }

            if (TendingTakesTimeMod.instance.Settings.Permanent && hediff.IsPermanent() &&
                hediff is not Hediff_MissingPart)
            {
                tendMultiplier *= TendingTakesTimeMod.instance.Settings.SmallDecrease;
                LogMessage($"{hediff} is permanent injury, decreased tend time");
            }

            if (hediff.Part != null)
            {
                if (TendingTakesTimeMod.instance.Settings.Internal && hediff.Part.depth == BodyPartDepth.Inside)
                {
                    tendMultiplier *= TendingTakesTimeMod.instance.Settings.LargeIncrease;
                    LogMessage($"{hediff} affects internal bodypart, large increase to tend time");
                }

                if (TendingTakesTimeMod.instance.Settings.External && hediff.Part.depth == BodyPartDepth.Outside &&
                    hediff is not Hediff_MissingPart)
                {
                    tendMultiplier *= TendingTakesTimeMod.instance.Settings.SmallDecrease;
                    LogMessage($"{hediff} affects external bodypart, decreased tend time");
                }
            }

            if (TendingTakesTimeMod.instance.Settings.Missing && hediff is Hediff_MissingPart)
            {
                tendMultiplier *= TendingTakesTimeMod.instance.Settings.LargeIncrease;
                LogMessage($"{hediff} is missing bodypart, large increase to tend time");
            }
        }

        return tendMultiplier;
    }

    public static void LogMessage(string message)
    {
        if (TendingTakesTimeMod.instance.Settings.VerboseLogging)
        {
            Log.Message($"[TendingTakesTime]: {message}");
        }
    }
}