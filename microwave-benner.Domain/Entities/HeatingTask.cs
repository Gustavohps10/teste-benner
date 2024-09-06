namespace microwave_benner.Domain.Entities
{
    public class HeatingTask
    {
        public int id { get; private set; }
        public int time { get; private set; }
        public int power { get; private set; }
        public DateTime? startTime { get; private set; }
        public DateTime? pauseTime { get; private set; }
        public DateTime? endTime { get; private set; }
        //public int? heatingProgramId { get; private set; }

        public HeatingTask(int time, int power, DateTime? startTime = null, DateTime? pauseTime = null, DateTime? endTime = null)
        {
            this.time = time;
            this.power = power;
            this.startTime = startTime;
            this.pauseTime = pauseTime;
            this.endTime = endTime;
            //this.heatingProgramId = heatingProgramId;
        }
    }
}
