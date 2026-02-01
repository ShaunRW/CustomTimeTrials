using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;

namespace CustomTimeTrials.RecordData
{
    class RecordsFile
    {
        private static readonly object fileLock = new object();
        
        public RecordData recordData = new RecordData();
        readonly string fileExtension = "json";

        public void load()
        {
            string path = this.GeneratePath();
            try
            {
                if (!System.IO.File.Exists(path))
                {
                    // Create file with minimal JSON
                    System.IO.File.WriteAllText(path, "{\"records\": {}}");
                }
                string fileContents = System.IO.File.ReadAllText(path);
                this.recordData = Newtonsoft.Json.JsonConvert.DeserializeObject<RecordData>(fileContents);
            }
            catch (Exception ex)
            {
                UI.Notify($"Error loading record data: {ex.Message}");
            }
        }

        public CurrentRecord getRaceRecord(string raceName, int lapCount)
        {
            if (this.recordData.records.ContainsKey(raceName))
            {
                if (lapCount > -1 && this.recordData.records[raceName].fastestTimes.ContainsKey(lapCount))
                {
                    return new CurrentRecord()
                    {
                        fastestTime = this.recordData.records[raceName].fastestTimes[lapCount].fastestTime,
                        fastestLapTime = this.recordData.records[raceName].fastestLapTime,
                    };
                }
            }
            return new CurrentRecord()
            {
                fastestTime = float.MaxValue,
                fastestLapTime = float.MaxValue,
            };
        }

        public void updateRecord(string raceName, int lapCount, float fastestTime, float fastestLapTime)
        {
            if (!this.recordData.records.ContainsKey(raceName))
            {
                this.recordData.records[raceName] = new RaceRecords()
                {
                    fastestLapTime = float.MaxValue,
                    fastestTimes = new Dictionary<int, LapRaceRecords>(),
                };
            }

            var raceRecord = this.recordData.records[raceName];

            if (lapCount > -1)
            {
                if (!raceRecord.fastestTimes.ContainsKey(lapCount))
                {
                    raceRecord.fastestTimes[lapCount] = new LapRaceRecords()
                    {
                        fastestTime = float.MaxValue,
                    };
                }

                var lapRecord = raceRecord.fastestTimes[lapCount];
                lapRecord.fastestTime = fastestTime;
                raceRecord.fastestTimes[lapCount] = lapRecord;
            }

            raceRecord.fastestLapTime = fastestLapTime;
            this.recordData.records[raceName] = raceRecord;

            this.save();
        }

        private void save()
        {
            lock (fileLock)
            {
                string path = this.GeneratePath();
                try
                {
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(
                        this.recordData,
                        Newtonsoft.Json.Formatting.Indented);

                    using (var fs = new System.IO.FileStream(
                        path,
                        System.IO.FileMode.Create,
                        System.IO.FileAccess.Write,
                        System.IO.FileShare.ReadWrite))
                    using (var writer = new System.IO.StreamWriter(fs))
                    {
                        writer.Write(json);
                    }
                }
                catch (Exception ex)
                {
                    UI.Notify($"Error saving record data: {ex.Message}");
                }
            }
        }

        private string GeneratePath()
        {
            string filename = "records";
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string targetDir = System.IO.Path.Combine(documentsPath, "Rockstar Games", "GTAV-CustomTimeTrials");
            if (!System.IO.Directory.Exists(targetDir))
            {
                System.IO.Directory.CreateDirectory(targetDir);
            }
            return System.IO.Path.Combine(targetDir, string.Format("{0}.{1}", filename, this.fileExtension));
        }
    }
}

