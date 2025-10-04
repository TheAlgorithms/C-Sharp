using System;
using System.Collections.Generic;
using Algorithms.Problems.JobScheduling;

namespace Algorithms.Tests.Problems.JobScheduling;

public class IntervalSchedulingSolverTests
{
    [Test]
    public void Schedule_ReturnsEmpty_WhenNoJobs()
    {
        var result = IntervalSchedulingSolver.Schedule(new List<Job>());
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void Schedule_ReturnsSingleJob_WhenOnlyOneJob()
    {
        var jobs = new List<Job> { new Job(1, 3) };
        var result = IntervalSchedulingSolver.Schedule(jobs);
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result[0], Is.EqualTo(jobs[0]));
    }

    [Test]
    public void Schedule_ThrowsArgumentNullException_WhenJobsIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => IntervalSchedulingSolver.Schedule(jobs: null!));
    }

    [Test]
    public void Schedule_SelectsJobsWithEqualEndTime()
    {
        var jobs = new List<Job>
        {
            new Job(1, 4),
            new Job(2, 4),
            new Job(4, 6)
        };
        var result = IntervalSchedulingSolver.Schedule(jobs);
        Assert.That(result, Has.Count.EqualTo(2));
        Assert.That(result, Does.Contain(new Job(1, 4)));
        Assert.That(result, Does.Contain(new Job(4, 6)));
    }

    [Test]
    public void Schedule_SelectsJobStartingAtLastEnd()
    {
        var jobs = new List<Job>
        {
            new Job(1, 3),
            new Job(3, 5),
            new Job(5, 7)
        };
        var result = IntervalSchedulingSolver.Schedule(jobs);
        Assert.That(result, Has.Count.EqualTo(3));
        Assert.That(result[0], Is.EqualTo(jobs[0]));
        Assert.That(result[1], Is.EqualTo(jobs[1]));
        Assert.That(result[2], Is.EqualTo(jobs[2]));
    }

    [Test]
    public void Schedule_HandlesJobsWithNegativeTimes()
    {
        var jobs = new List<Job>
        {
            new Job(-5, -3),
            new Job(-2, 1),
            new Job(0, 2)
        };
        var result = IntervalSchedulingSolver.Schedule(jobs);
        Assert.That(result, Has.Count.EqualTo(2));
        Assert.That(result, Does.Contain(new Job(-5, -3)));
        Assert.That(result, Does.Contain(new Job(-2, 1)));
    }

    [Test]
    public void Schedule_SelectsNonOverlappingJobs()
    {
        var jobs = new List<Job>
        {
            new Job(1, 4),
            new Job(3, 5),
            new Job(0, 6),
            new Job(5, 7),
            new Job(8, 9),
            new Job(5, 9)
        };
        var result = IntervalSchedulingSolver.Schedule(jobs);
        // Expected: (1,4), (5,7), (8,9)
        Assert.That(result, Has.Count.EqualTo(3));
        Assert.That(result, Does.Contain(new Job(1, 4)));
        Assert.That(result, Does.Contain(new Job(5, 7)));
        Assert.That(result, Does.Contain(new Job(8, 9)));
    }

    [Test]
    public void Schedule_HandlesFullyOverlappingJobs()
    {
        var jobs = new List<Job>
        {
            new Job(1, 5),
            new Job(2, 6),
            new Job(3, 7)
        };
        var result = IntervalSchedulingSolver.Schedule(jobs);
        // Only one job can be selected
        Assert.That(result, Has.Count.EqualTo(1));
    }
}
