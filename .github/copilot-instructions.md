# GitHub Copilot Instructions for Tending Takes Time

## Mod Overview and Purpose

The Tending Takes Time mod for RimWorld introduces variability in the time it takes for a doctor to tend to an injury. The mod aims to provide a more realistic and dynamic medical experience by adjusting the duration based on the severity and nature of the injury. This mod enhances gameplay by making medical care more challenging and rewarding.

## Key Features and Systems

### Features
- **Variable Tend Time**: The time it takes to tend to an injury varies based on factors such as bleeding severity, injury type, and body part status.
- **Mod Settings**: Players can customize the mod settings to enable or disable specific factors that influence tend time and adjust the increase and decrease values.
  
### Systems
- **Hediff Factors**: The mod calculates tend time based on hediff factors such as bleeding, injury type (internal vs. external), and body part status (missing body part).
- **Settings Menu**: The mod provides a settings menu to allow players to configure the behavior of the tend time adjustments.

## Coding Patterns and Conventions

### C# Coding Patterns
- **Mod Structure**: The mod follows a structured C# codebase with clear separation of concerns.
  - `TendingTakesTimeMod.cs`: Contains the main mod class with methods for settings window and mod settings.
  - `TendingTakesTime.cs`: Handles the calculation of tend time offsets.
  - `TendingTakesTimeSettings.cs`: Manages mod settings and data persistence.
  - `JobDriver_TendPatient_MakeNewToils.cs`: Applies the tend time calculation to the tending job.
  
- **Harmony Patching**: The mod uses Harmony patching to modify the behavior of the tending job.
  
- **Settings Management**: The mod uses the RimWorld settings framework to provide a settings menu and handle setting persistence.

### XML Integration
- **XML Defs**: The mod does not currently use any XML definitions. However, XML integration is possible if the mod needs to define new content or modify existing content.

## Harmony Patching

The Tending Takes Time mod uses Harmony patching to modify the behavior of the tending job. Specifically, it applies a postfix patch to the `JobDriver_TendPatient_MakeNewToils` class to adjust the duration of the tending job based on the calculated tend time offset.

### Example Harmony Patch

csharp
using HarmonyLib;
using RimWorld;
using Verse;

namespace TendingTakesTime
{
    [HarmonyPatch(typeof(JobDriver_TendPatient_MakeNewToils), "MakeNewToils")]
    public static class Patch_JobDriver_TendPatient_MakeNewToils
    {
        public static void Postfix(ref JobDriver_TendPatient __instance)
        {
            // Calculate the tend time offset based on the patient's injuries
            float tendOffset = TendingTakesTime.CalculateTendOffset(__instance.pawn);

            // Adjust the duration of the tending job
            __instance.job.duration += (int)tendOffset;
        }
    }
}


## Suggestions for Copilot

### General Suggestions
- **Code Snippets**: Provide code snippets for creating Harmony patches and modifying RimWorld jobs.
- **XML Integration**: Provide examples of how to integrate XML definitions for new content or to modify existing content.
- **Settings Management**: Provide examples of how to create and manage mod settings using the RimWorld settings framework.
- **Debugging and Logging**: Provide examples of how to add debugging and logging to help with troubleshooting and development.

### Specific Suggestions
- **Harmony Patching**: Provide examples of how to create and apply Harmony patches for common RimWorld job modifications.
- **Mod Settings**: Provide examples of how to create and manage mod settings with the RimWorld settings framework.
- **XML Defs**: Provide examples of how to integrate XML definitions for new content or to modify existing content.

By following these guidelines, GitHub Copilot can assist developers in creating high-quality RimWorld mods with minimal effort.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

