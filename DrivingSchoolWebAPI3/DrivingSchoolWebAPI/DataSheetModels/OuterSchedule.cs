namespace DrivingSchoolWebAPI.DataSheetModels
{
    public class OuterSchedule
    {
        public Dictionary<int, DateOfClass> Dates { get; set; } = new Dictionary<int, DateOfClass>();
        public Dictionary<int, TimeOfClass> Times { get; set; } = new Dictionary<int, TimeOfClass>();
        public Dictionary<(int, int), OuterClassData> OuterClasses { get; set; } = new Dictionary<(int, int), OuterClassData> ();

        public OuterSchedule(Dictionary<int, DateOfClass> dates, Dictionary<int, TimeOfClass> times)
        {
            Dates = dates;
            Times = times;
        }

        public OuterClassData this[int date, int time]
        {
            get => OuterClasses[(date, time)];
            set => OuterClasses[(date, time)] = value;
        }
        public OuterClassData this[DateOfClass date, TimeOfClass time]
        {
            get
            {
                if (!Dates.ContainsValue(date) || !Times.ContainsValue(time))
                    return null;
                var d = Dates.Where(x => x.Value == date).First().Key;
                var t = Times.Where(x => x.Value == time).First().Key;
                return OuterClasses[(d, t)];
            }
            set
            {
                if (!Dates.ContainsValue(date) || !Times.ContainsValue(time))
                    return;
                var d = Dates.Where(x => x.Value == date).First().Key;
                var t = Times.Where(x => x.Value == time).First().Key;
                OuterClasses[(d, t)] = value;
            }
        }
    }
}
