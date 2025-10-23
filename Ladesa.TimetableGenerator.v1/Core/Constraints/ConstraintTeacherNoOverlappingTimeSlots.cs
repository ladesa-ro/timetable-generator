using Ladesa.TimetableGenerator.v1.Core.Generator;
using Ladesa.TimetableGenerator.v1.Core.Domain;

namespace Ladesa.TimetableGenerator.v1.Core.Constraints;

/// <summary>
/// Constraint: For the same teacher and date, no overlapping time slots may be scheduled.
/// </summary>
public abstract class ConstraintTeacherNoOverlappingTimeSlots : IGeneratorConstraint
{
    public static void Apply(GenerationContext generationContext)
    {
        var proposalsByDateAndTeacher = from p in generationContext.AllProposals
            group p by new { p.Date, p.TeacherId } into g
            select new { g.Key.Date, g.Key.TeacherId, Proposals = g.AsEnumerable().ToArray() };

        foreach (var bucket in proposalsByDateAndTeacher)
        {
            var proposals = bucket.Proposals;
            for (int i = 0; i < proposals.Length; i++)
            {
                for (int j = i + 1; j < proposals.Length; j++)
                {
                    if (Overlap(proposals[i].TimeSlot, proposals[j].TimeSlot, bucket.Date))
                    {
                        var a = proposals[i].ModelBoolVar;
                        var b = proposals[j].ModelBoolVar;
                        generationContext.CpModel.AddAtMostOne(new[] { a, b });
                    }
                }
            }
        }
    }

    private static bool Overlap(TimeSlot a, TimeSlot b, DateOnly date)
    {
        var (aStart, aEnd) = a.GetDateTimeRange(date);
        var (bStart, bEnd) = b.GetDateTimeRange(date);
        return aStart < bEnd && aEnd > bStart;
    }
}
