using Google.OrTools.Sat;
using Ladesa.TimetableGenerator.Infrastructure.Solver.Constants;
using Ladesa.TimetableGenerator.Infrastructure.Solver.Generator;
using Ladesa.TimetableGenerator.Domain.Models;
using Ladesa.TimetableGenerator.Domain.Models;

namespace Ladesa.TimetableGenerator.Infrastructure.Solver.Constraints;

/// <summary>
///     CONSTRAINT: Teacher - no opposite turns on the same day.
///     Allowed combinations: none, morning only, afternoon only, night only,
///     morning+afternoon, afternoon+night. (Morning+night is forbidden.)
/// </summary>
public static class ConstraintTeacherNoOppositeTurns
{
    // Allowed shift arrangements: [morning, afternoon, night]
    private static readonly long[,] AllowedShiftArrangements =
    {
        { 0, 0, 0 }, // no classes
        { 1, 0, 0 }, // morning only
        { 0, 1, 0 }, // afternoon only
        { 0, 0, 1 }, // night only
        { 1, 1, 0 }, // morning + afternoon
        { 0, 1, 1 }  // afternoon + night
    };

    public static void Apply(GenerationContext generationContext)
    {
        var proposalsByTeacherAndDate =
            from proposal in generationContext.AllProposals
            group proposal by new { proposal.TeacherId, proposal.Date } into grouped
            select new
            {
                grouped.Key.TeacherId,
                grouped.Key.Date,
                Proposals = grouped.ToList()
            };

        foreach (var bucket in proposalsByTeacherAndDate)
        {
            if (bucket.Proposals.Count == 0)
                continue;

            var morningVars = FilterByShift(bucket.Proposals, TimeSlotConstants.MorningShift);
            var afternoonVars = FilterByShift(bucket.Proposals, TimeSlotConstants.AfternoonShift);
            var nightVars = FilterByShift(bucket.Proposals, TimeSlotConstants.NightShift);

            if (morningVars.Count == 0 || afternoonVars.Count == 0 || nightVars.Count == 0)
                continue;

            var prefix = $"{bucket.TeacherId}_{bucket.Date}";

            var shiftCounts = new[]
            {
                CreateShiftCount(generationContext, morningVars, $"{prefix}_morning"),
                CreateShiftCount(generationContext, afternoonVars, $"{prefix}_afternoon"),
                CreateShiftCount(generationContext, nightVars, $"{prefix}_night"),
            };

            var shiftActive = shiftCounts
                .Select(c => CreateShiftActiveVar(generationContext, c.countVar, c.label))
                .ToArray();

            generationContext.CpModel
                .AddAllowedAssignments(shiftActive)
                .AddTuples(AllowedShiftArrangements);
        }
    }

    private static List<BoolVar> FilterByShift(
        List<GenerationContextScheduleProposal> proposals,
        TimeSlot shift)
    {
        return proposals
            .Where(p => p.TimeSlot.Contains(shift))
            .Select(p => (BoolVar)p.ModelBoolVar)
            .ToList();
    }

    private static (IntVar countVar, string label) CreateShiftCount(
        GenerationContext context,
        List<BoolVar> shiftVars,
        string label)
    {
        var countVar = context.CpModel.NewIntVar(0, shiftVars.Count, $"{label}_count");
        context.CpModel.Add(countVar == LinearExpr.Sum(shiftVars));
        return (countVar, label);
    }

    private static BoolVar CreateShiftActiveVar(
        GenerationContext context,
        IntVar countVar,
        string label)
    {
        var activeVar = context.CpModel.NewBoolVar($"{label}_active");
        context.CpModel.Add(countVar >= 1).OnlyEnforceIf(activeVar);
        context.CpModel.Add(countVar < 1).OnlyEnforceIf(activeVar.Not());
        return activeVar;
    }
}
