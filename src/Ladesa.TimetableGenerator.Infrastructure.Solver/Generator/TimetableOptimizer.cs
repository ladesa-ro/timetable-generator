using Google.OrTools.Sat;
using Ladesa.TimetableGenerator.Application.UseCases.GenerateTimetable;
using Ladesa.TimetableGenerator.Domain.Models.Schedule;
using Ladesa.TimetableGenerator.Domain.Models.TimetableGrid;
using LinearExpr = Google.OrTools.Sat.LinearExpr;

namespace Ladesa.TimetableGenerator.Infrastructure.Solver.Generator;

/// <summary>
///     Configures the objective function for the CP solver to maximize
///     schedule quality based on preferences and continuity with previous timetables.
/// </summary>
internal class TimetableOptimizer : ITimetableOptimizer
{
    public void OptimizeResult(
        GenerationContext context,
        long? scoreLimit = null)
    {
        var generationContext = context;
        var qualityScore = LinearExpr.NewBuilder();

        AddBasicScheduleScore(qualityScore, generationContext);

        var previousTimetableGrid = generationContext.GenerateTimetableCommand.PreviousTimetableGrid;
        if (previousTimetableGrid is not null)
            AddPreviousTimetableBonus(qualityScore, generationContext, previousTimetableGrid);

        if (scoreLimit != null)
            generationContext.CpModel.Add(qualityScore <= generationContext.CpModel.NewConstant((long)scoreLimit));

        generationContext.CpModel.Maximize(qualityScore);
        generationContext.Score = qualityScore;
    }

    private static void AddBasicScheduleScore(LinearExprBuilder qualityScore, GenerationContext context)
    {
        foreach (var proposal in context.AllProposals)
            qualityScore.AddTerm((IntVar)proposal.ModelBoolVar, 1);
    }

    private static void AddPreviousTimetableBonus(
        LinearExprBuilder qualityScore,
        GenerationContext context,
        TimetableGrid previousTimetableGrid)
    {
        var request = context.GenerateTimetableCommand;

        foreach (var previousSchedule in previousTimetableGrid.Schedules)
        {
            var matchingProposals = context.AllProposals
                .Where(p => p.GroupId == previousSchedule.GroupId
                         && p.DiaryId == previousSchedule.DiaryId
                         && p.TeacherId == previousSchedule.TeacherId)
                .ToArray();

            AddScoreTerms(qualityScore, matchingProposals,
                p => p.Date.DayOfWeek == previousSchedule.Date.DayOfWeek,
                request.BoostSameDayOfWeekOnly);

            AddScoreTerms(qualityScore, matchingProposals,
                p => p.TimeSlot == previousSchedule.TimeSlot,
                request.BoostSameTimeSlotOnly);

            AddScoreTerms(qualityScore, matchingProposals,
                p => p.Date.DayOfWeek == previousSchedule.Date.DayOfWeek
                  && p.TimeSlot == previousSchedule.TimeSlot,
                request.BoostSameDayOfWeekAndTimeSlot);

            AddDayDistanceBonus(qualityScore, matchingProposals, previousSchedule, request);
            AddTimeSlotDistanceBonus(qualityScore, matchingProposals, previousSchedule, request);
        }
    }

    private static void AddScoreTerms(
        LinearExprBuilder qualityScore,
        GenerationContextScheduleProposal[] proposals,
        Func<GenerationContextScheduleProposal, bool> filter,
        long boost)
    {
        foreach (var proposal in proposals.Where(filter))
            qualityScore.AddTerm((IntVar)proposal.ModelBoolVar, boost);
    }

    private static void AddDayDistanceBonus(
        LinearExprBuilder qualityScore,
        GenerationContextScheduleProposal[] matchingProposals,
        Schedule previousSchedule,
        GenerateTimetableCommand timetableCommand)
    {
        foreach (var group in matchingProposals.GroupBy(p => p.Date.DayOfWeek))
        {
            var distance = Math.Abs((int)group.Key - (int)previousSchedule.Date.DayOfWeek);
            var score = (7 - distance) * timetableCommand.BoostLesserDistanceFromDayOfWeek;

            foreach (var proposal in group)
                qualityScore.AddTerm((IntVar)proposal.ModelBoolVar, score);
        }
    }

    private static void AddTimeSlotDistanceBonus(
        LinearExprBuilder qualityScore,
        GenerationContextScheduleProposal[] matchingProposals,
        Schedule previousSchedule,
        GenerateTimetableCommand timetableCommand)
    {
        foreach (var group in matchingProposals.GroupBy(p => p.TimeSlot))
        {
            var distanceMinutes = previousSchedule.TimeSlot.Distance(group.Key).TotalMinutes;
            var score = (long)((-distanceMinutes) * timetableCommand.BoostLesserDistanceFromTimeSlot);

            foreach (var proposal in group)
                qualityScore.AddTerm((IntVar)proposal.ModelBoolVar, score);
        }
    }
}
