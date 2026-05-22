# Copilot Instructions for RimWorld Mod Project: Tending Takes Time

## Mod Overview and Purpose

**Mod Name:** Tending Takes Time

**Description:**  
This mod introduces a dynamic system to the tending process of injuries in RimWorld, where the time taken to treat injuries varies based on multiple factors. The aim is to provide a more realistic medical experience by differentiating the tending times for simple injuries and complex injuries that previously took the same amount of time.

## Key Features and Systems

- **Dynamic Tending Times:**  
  - Increased tend time for:
    - Heavy or massive bleeding
    - Life-threatening injuries
    - Internal injuries
    - Missing body parts
  - Decreased tend time for:
    - Low or no bleeding
    - Permanent injuries
    - External injuries

- **Mod Settings:**  
  - Users can customize their experience by enabling or disabling different factors that affect tending times. Also, they can tweak the degrees by which each factor increases or decreases the tending times.

- **Technical Implementation:**  
  - Adds an `init-action` to the `waiting-toil` of the `tending-job`, modifying its duration based on the hediffs being treated.

## Coding Patterns and Conventions

- **Naming Conventions:**  
  - Use PascalCase for class names.
  - Methods and internal scopes should follow camelCase where applicable.

- **Class Structures:**  
  - Public classes typically contain static methods if they don't maintain any state.
  - Internal classes are used for mod settings and configurations to manage accessibility within the assembly.

- **Method Composition:**  
  - Ensure methods serve single responsibilities for clarity and maintainability.
  - Maintain private methods for repeated logic to ensure DRY principles.

## XML Integration

- Use XML to define mod settings that players can customize.
- Ensure that the XML configuration is well-documented and easy to parse for custom modifications.

## Harmony Patching

- **Purpose:**  
  - Used to modify core game behavior without altering the original code. Perfect for injecting new logic into existing methods.
  
- **Implementation:**  
  - Create Harmony patches to hook into RimWorld’s existing `JobDriver_TendPatient` methods.
  - Apply prefix and postfix patches where needed to modify method behavior or continue existing logic flow.

- **Best Practices:**  
  - Avoid conflicts by ensuring Harmony patches are modular and minimally invasive.
  - Comment patches adequately to convey the intent and logic to other developers.

## Suggestions for Copilot

- **Contextual Awareness:**  
  - When generating code for this project, focus on factors increasing or decreasing tending times, based on the injury’s characteristics.
  - Suggest code snippets for managing XML-based configuration files to allow settings toggling and adjustments.

- **Code Recommendations:**  
  - Recommend Harmony patch templates for common use cases such as prefix or postfix methods.
  - Provide examples for refactoring repetitive logic into helper methods for better readability.

- **Testing and Debugging Aids:**  
  - Suggest unit tests for critical methods, especially regarding calculations of tend times.
  - Offer debugging statements to trace the execution flow during injury processing.

By following these instructions, Copilot can assist in further development and maintainance of the 'Tending Takes Time' mod, ensuring code quality and enhancing features with ease.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.
