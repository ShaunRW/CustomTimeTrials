using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace CustomTimeTrials
{
    class TimeTrialFile
    {
        public TimeTrialData data = new TimeTrialData();
        private string fileExtension = "json";

        public void load(string timeTrialName)
        {
            string path = this.GeneratePath(timeTrialName);
            string fileContents = System.IO.File.ReadAllText(path);

            this.data = JsonConvert.DeserializeObject<TimeTrialData>(fileContents);
        }

        public void save()
        {
            string path = this.GeneratePath(this.data.displayName);
            System.IO.File.WriteAllText(path, this.toJson());
        }

        public string toJson()
        {
            string json = JsonConvert.SerializeObject(this.data, Formatting.Indented);
            return json;
        }

        private string GeneratePath(string filename)
        {
            return string.Format("scripts/TimeTrials/{0}.{1}", filename, this.fileExtension);
        }
    }
}
