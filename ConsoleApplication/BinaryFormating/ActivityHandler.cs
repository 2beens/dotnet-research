using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication.BinaryFormating
{
    public class ActivityHandler
    {
        private const string ActivityStateFileName = "AdjustIOActivityState";
        private const string ActivityStateName = "Activity state";
        private const string AttributionFileName = "AdjustAttribution";
        private const string AttributionName = "Attribution";

        public void WriteActivityStateInternal()
        {
            ActivityState activityState = new ActivityState(1, "a", "b");

            Util.SerializeToFileAsync(
                fileName: ActivityStateFileName,
                objectWriter: ActivityState.SerializeToStream,
                input: activityState,
                objectName: ActivityStateName)
                .Wait();
        }

        public void ReadActivityState()
        {
            ActivityState activityState = Util.DeserializeFromFileAsync(ActivityStateFileName,
                ActivityState.DeserializeFromStream, //deserialize function from Stream to ActivityState
                () => null, //default value in case of error
                ActivityStateName) // activity state name
                .Result;
        }
    }

}
