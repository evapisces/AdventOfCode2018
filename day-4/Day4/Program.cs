using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day4
{
    public class Program
    {
        public static List<ActivityLog> ActivityLogs { get; set; }
        public static List<GuardActivity> GuardActivities { get; set; }
        public static void Main(string[] args)
        {
            var testLines = File.ReadAllLines("../../../test-input.txt");
            var lines = File.ReadAllLines("../../../puzzle-input.txt");
            ActivityLogs = new List<ActivityLog>();
            GuardActivities = new List<GuardActivity>();

            foreach (var line in lines)
            {
                var date = Convert.ToDateTime(line.Substring(0, line.IndexOf("]")).Replace("[", ""));
                var activity = line.Substring(line.IndexOf("]") + 2);

                ActivityLogs.Add(new ActivityLog
                {
                    ActivityDate = date,
                    Activity = activity
                });

                ActivityLogs.Sort((x, y) => x.ActivityDate.CompareTo(y.ActivityDate));
            }

            SetupGuardActivities(ActivityLogs);

            PartA();

            PartB();

            Console.Read();
        }

        private static void SetupGuardActivities(List<ActivityLog> activityLog)
        {
            var startMinute = 0;
            var guardId = 0;
            foreach (var log in activityLog)
            {
                if (log.Activity.Contains("Guard"))
                {
                    guardId = Convert.ToInt32(log.Activity.Split(" ")[1].Replace("#", ""));

                    if (!GuardActivities.Any(g => g.GuardId == guardId && g.Date.Date == log.ActivityDate.Date))
                    {
                        GuardActivities.Add(new GuardActivity
                        {
                            GuardId = guardId,
                            Date = log.ActivityDate.Date,
                            Activities = Enumerable.Repeat('.', 60).ToArray()
                        });
                    }
                }
                else
                {
                    if (log.Activity.Contains("falls"))
                    {
                        startMinute = log.ActivityDate.Minute;
                    }
                    else if (log.Activity.Contains("wakes"))
                    {
                        var endMinute = log.ActivityDate.Minute - 1;

                        var guardActivity = GuardActivities.FirstOrDefault(g => g.GuardId == guardId && g.Date.Date == log.ActivityDate.Date);

                        if (guardActivity == null)
                        {
                            GuardActivities.Add(new GuardActivity
                            {
                                GuardId = guardId,
                                Date = log.ActivityDate.Date,
                                Activities = Enumerable.Repeat('.', 60).ToArray()
                            });

                            guardActivity = GuardActivities.FirstOrDefault(g => g.GuardId == guardId && g.Date.Date == log.ActivityDate.Date);
                        }

                        for (int i = startMinute; i <= endMinute; i++)
                        {
                            guardActivity.Activities[i] = '#';
                        }
                    }
                }
            }
        }

        public static void PartA()
        {
            foreach (var guardActivity in GuardActivities)
            {
                guardActivity.SleepCount = guardActivity.Activities.Count(a => a == '#');
            }

            var mostMinutesSleptGuard = GuardActivities.GroupBy(g => g.GuardId).Select(g => new
            {
                Key = g.Key,
                MinutesSlept = g.Sum(s => s.SleepCount)
            }).OrderByDescending(g => g.MinutesSlept).First().Key;

            var mostMinutesSleptGuardActivities = GuardActivities.Where(g => g.GuardId == mostMinutesSleptGuard).Select(g => g.Activities).ToList();

            var index = 0;
            var minuteCountDict = new Dictionary<int, int>();

            for (int i = 0; i < 60; i++)
            {
                minuteCountDict.Add(i, 0);
            }

            foreach (var mostMinutesSleptGuardActivity in mostMinutesSleptGuardActivities)
            {
                for (var i = 0; i < mostMinutesSleptGuardActivity.Length; i++)
                {
                    if (mostMinutesSleptGuardActivity[i] == '#')
                    {
                        minuteCountDict[i]++;
                    }
                }
            }

            var answer = mostMinutesSleptGuard * minuteCountDict.OrderByDescending(d => d.Value).First().Key;

            Console.WriteLine("Answer = " + answer);
        }

        public static void PartB()
        {
            var activityByMinuteList = new List<ActivityByMinute>();

            foreach (var guardActivity in GuardActivities)
            {
                for (int i = 0; i < guardActivity.Activities.Length; i++)
                {
                    if (guardActivity.Activities[i] == '#')
                    {
                        activityByMinuteList.Add(new ActivityByMinute
                        {
                            GuardId = guardActivity.GuardId,
                            Minute = i
                        });
                    }
                }
            }

            var orderedMinuteActivity = activityByMinuteList.GroupBy(a => a.GuardId).Select(a => new
            {
                GuardId = a.Key,
                MostFrequentMinute = a.ToList().Select(b => b .Minute).ToList().GroupBy(b => b).Select(b => new
                {
                    Minute = b.Key,
                    Count = b.Count()
                }).OrderByDescending(c => c.Count).First()
            }).ToList().OrderByDescending(d => d.MostFrequentMinute.Count).First();

            var answer = orderedMinuteActivity.GuardId * orderedMinuteActivity.MostFrequentMinute.Minute;

            Console.WriteLine($"Answer = {answer}");
        }
    }

    public class ActivityLog
    {
        public DateTime ActivityDate { get; set; }
        public string Activity { get; set; }
    }

    public class GuardActivity
    {
        public int GuardId { get; set; }
        public DateTime Date { get; set; }
        public char[] Activities { get; set; }
        public int SleepCount { get; set; }
    }

    public class ActivityByMinute
    {
        public int Minute { get; set; }
        public int GuardId { get; set; }
    }
}
