using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Problems.JobScheduling;

/// <summary>
/// Implements the greedy algorithm for Interval Scheduling.
/// Finds the maximum set of non-overlapping jobs.
/// </summary>
public static class IntervalSchedulingSolver
{
    /// <summary>
    /// Returns the maximal set of non-overlapping jobs.
    /// </summary>
    /// <param name="jobs">List of jobs to schedule.</param>
    /// <returns>List of selected jobs (maximal set).</returns>
    public static List<Job> Schedule(IEnumerable<Job> jobs)
    {
        if (jobs == null)
        {
            throw new ArgumentNullException(nameof(jobs));
        }

        // Sort jobs by their end time (earliest finish first)
        var sortedJobs = jobs.OrderBy(j => j.End).ToList();
        var result = new List<Job>();
        int lastEnd = int.MinValue;

        foreach (var job in sortedJobs)
        {
            // If the job starts after the last selected job ends, select it
            if (job.Start >= lastEnd)
            {
                result.Add(job);
                lastEnd = job.End;
            }
        }

        return result;
    }
}
