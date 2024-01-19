

using System;
using System.Security.Cryptography.X509Certificates;

namespace MyApp 
{
    internal class Program
    {
        static void Main(string[] args)
        {
    
            string fileName =@"\TestData.csv";
            DisplayTopScorers(fileName);
        }
        private static void DisplayTopScorers(string filename)
        {
            string whole_file = System.IO.File.ReadAllText(Directory.GetCurrentDirectory() + filename);
            whole_file = whole_file.Replace('\n', '\r');
            string[] lines = whole_file.Split(new char[] { '\r' },
                StringSplitOptions.RemoveEmptyEntries);
            List<LearnerDetails> topScorers = new List<LearnerDetails>();
            foreach (string line in lines.Where(x => x != lines[0]).ToList())
            {
                var leanerDetail = line.Split(',');
                if (Int32.TryParse(leanerDetail[2], out int mark))
                {
                    if (topScorers.Count == 0)
                    {
                        topScorers.Add(new LearnerDetails()
                        {
                            FullName = leanerDetail[0] + " " + leanerDetail[1],
                            Mark = mark
                        });
                    }
                    else if (!topScorers.Any(x => x.Mark > mark))
                    {
                        topScorers = topScorers.Where(x => x.Mark == mark).ToList();
                        topScorers.Add(new LearnerDetails()
                        {
                            FullName = leanerDetail[0] + " " + leanerDetail[1],
                            Mark = mark
                        });
                    }
                }
                else
                {
                    Console.WriteLine("file could not be parsed.");
                    return;
                }
              

            }
            topScorers.OrderBy(a =>a.FullName).ToList().ForEach(x =>
            {
                Console.WriteLine(x.FullName);
            });

        }
    }
}
