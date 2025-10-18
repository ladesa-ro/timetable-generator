using Google.OrTools.Sat;
using Ladesa.TimetableGenerator.Core.Application.Features.Generator.Core;
using Ladesa.TimetableGenerator.Core.Domain.ValueObjects;

namespace Ladesa.TimetableGenerator.Core.Application.Features.Generator.Constraints;

/// <summary>
///     CONSTRAINT: Teacher - no opposite turns on the same day.
/// </summary>
public abstract class ConstraintTeacherNoOppositeTurns : IGeneratorConstraint
{
    public static void Apply(GenerationContext generationContext)
    {
        var allSchedulesProposalsByTeacherIdDate =
            from scheduleProposal in generationContext.AllProposals
            group scheduleProposal by new
            {
                scheduleProposal.TeacherId,
                scheduleProposal.Date
            }
            into schedulesProposalsByTeacherIdDate
            select new
            {
                schedulesProposalsByTeacherIdDate.Key.TeacherId,
                schedulesProposalsByTeacherIdDate.Key.Date,
                Proposals = schedulesProposalsByTeacherIdDate.AsEnumerable()
            };

        foreach (var schedulesProposalsByTeacherIdDate in allSchedulesProposalsByTeacherIdDate)
        {
            if (schedulesProposalsByTeacherIdDate == null)
                continue;

            var scheduleProposals = schedulesProposalsByTeacherIdDate.Proposals.ToList();

            if (scheduleProposals.Count == 0)
                continue;

            var morningProposals = (
                from proposta in scheduleProposals
                where
                    proposta.TimeSlot.Verify(
                        new TimeSlot("00:00:00", "11:59:59")
                    )
                select proposta.ModelBoolVar
            ).ToList();

            var afternoonProposals = (
                from proposta in scheduleProposals
                where
                    proposta.TimeSlot.Verify(
                        new TimeSlot("12:00:00", "17:59:59")
                    )
                select proposta.ModelBoolVar
            ).ToList();

            var nightProposals = (
                from proposta in scheduleProposals
                where
                    proposta.TimeSlot.Verify(
                        new TimeSlot("18:00:00", "23:59:59")
                    )
                select proposta.ModelBoolVar
            ).ToList();

            /*
            Possibilidades

            | descricao            | manha | tarde | noite |
            | -------------------- | ----- | ----- | ----- |
            | nao dar aula no dia  | false | false | false |
            | dar aula so de MANHA |  true | false | false |
            |  dar aula so a tarde | false |  true | false |
            |  dar aula so a noite | false | false |  true |
            |       manha e tarde  |  true |  true | false |
            |       tarde e noite  | false |  true |  true |
            */
            if (morningProposals.Count == 0 || afternoonProposals.Count == 0 || nightProposals.Count == 0)
                continue;

            //Console.WriteLine("toppp");
            long[,] allowedArrange =
            {
                { 0, 0, 0 }, // nao dar aula no dia
                { 1, 0, 0 }, //dar aula so de MANHA
                { 0, 1, 0 }, //dar aula so a tarde
                { 0, 0, 1 }, //dar aula so a noite
                { 1, 1, 0 }, //manha e tarde
                { 0, 1, 1 } //tarde e noite
            };

            var prefix =
                $"{schedulesProposalsByTeacherIdDate.TeacherId}_{schedulesProposalsByTeacherIdDate.Date.ToString()}";

            var countMorning = generationContext.CpModel.NewIntVar(
                0,
                morningProposals.Count,
                $"{prefix}_Morning_QuantidadeAtivos"
            );
            var countAfternoon = generationContext.CpModel.NewIntVar(
                0,
                afternoonProposals.Count,
                $"{prefix}_Afternoon_QuantidadeAtivos"
            );
            var countNight = generationContext.CpModel.NewIntVar(
                0,
                nightProposals.Count,
                $"{prefix}_Night_QuantidadeAtivos"
            );

            generationContext.CpModel.Add(countMorning == LinearExpr.Sum(morningProposals));
            generationContext.CpModel.Add(countAfternoon == LinearExpr.Sum(afternoonProposals));
            generationContext.CpModel.Add(countNight == LinearExpr.Sum(nightProposals));

            var someScheduleMorning = generationContext.CpModel.NewBoolVar($"{prefix}_Morning_Ativo");
            var someScheduleAfternoon = generationContext.CpModel.NewBoolVar($"{prefix}_Afternoon_Ativo");
            var someScheduleNight = generationContext.CpModel.NewBoolVar($"{prefix}_Night_Ativo");

            generationContext.CpModel.Add(countMorning >= 1).OnlyEnforceIf(someScheduleMorning);
            generationContext.CpModel.Add(countAfternoon >= 1).OnlyEnforceIf(someScheduleAfternoon);
            generationContext.CpModel.Add(countNight >= 1).OnlyEnforceIf(someScheduleNight);

            generationContext.CpModel.Add(countMorning < 1).OnlyEnforceIf(someScheduleMorning.Not());
            generationContext.CpModel.Add(countAfternoon < 1).OnlyEnforceIf(someScheduleAfternoon.Not());
            generationContext.CpModel.Add(countNight < 1).OnlyEnforceIf(someScheduleNight.Not());

            generationContext
                .CpModel.AddAllowedAssignments([someScheduleMorning, someScheduleAfternoon, someScheduleNight])
                .AddTuples(allowedArrange);
        }
    }
}