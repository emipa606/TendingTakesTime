using System.Collections.Generic;
using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

namespace TendingTakesTime;

[HarmonyPatch(typeof(JobDriver_TendPatient), "MakeNewToils")]
public static class JobDriver_TendPatient_MakeNewToils
{
    public static IEnumerable<Toil> Postfix(IEnumerable<Toil> values, JobDriver_TendPatient __instance)
    {
        foreach (var toil in values)
        {
            if (toil.debugName is not "Wait" and not "WaitWith_NewTemp")
            {
                yield return toil;
                continue;
            }

            toil.AddPreInitAction(delegate
            {
                var actor = toil.actor;
                var medicine = (Medicine)actor.CurJob.targetB.Thing;
                var patient = __instance.job.targetA.Pawn;
                var hediffsToTend = new List<Hediff>();
                TendUtility.GetOptimalHediffsToTendWithSingleTreatment(patient, medicine != null, hediffsToTend);
                var tendMultiplier = TendingTakesTime.CalculateTendOffset(hediffsToTend, patient);
                __instance.ticksLeftThisToil = (int)(toil.defaultDuration * tendMultiplier);

                TendingTakesTime.LogMessage(
                    $"Tend-duration changed from {toil.defaultDuration} to {__instance.ticksLeftThisToil}");
            });
            yield return toil;
        }
    }
}