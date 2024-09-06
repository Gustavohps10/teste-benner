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

        // Inicio Rápido
        private const int defaultTime = 30; // Segundos
        private const int defaultPower = 10;

        public HeatingTask(int time = defaultTime, int power = defaultPower)
        {
            this.time = time > 0 ? time : defaultTime;
            this.power = power > 0 ? power : defaultPower;
        }

        public HeatingTask(int id, int time, int power, DateTime? startTime = null, DateTime? pauseTime = null, DateTime? endTime = null)
            : this(time, power)
        {
            this.id = id;
            this.startTime = startTime;
            this.pauseTime = pauseTime;
            this.endTime = endTime;
        }

        public void Start()
        {
            if (this.startTime.HasValue)
            {
                throw new InvalidOperationException("O aquecimento já foi iniciado.");
            }
            this.startTime = DateTime.UtcNow;
        }

        public void Pause()
        {
            if (!this.startTime.HasValue)
            {
                throw new InvalidOperationException("O aquecimento não foi iniciado.");
            }
            if (this.pauseTime.HasValue)
            {
                throw new InvalidOperationException("O aquecimento já foi pausado.");
            }
            this.pauseTime = DateTime.UtcNow;
        }

        public void Continue()
        {
            if (!this.pauseTime.HasValue)
            {
                throw new InvalidOperationException("O aquecimento não está pausado.");
            }
            if (this.endTime.HasValue)
            {
                throw new InvalidOperationException("O aquecimento já foi finalizado.");
            }
            this.pauseTime = null;
        }

        public void Interrupt()
        {
            if (!this.startTime.HasValue)
            {
                throw new InvalidOperationException("O aquecimento não foi iniciado.");
            }
            if (this.endTime.HasValue)
            {
                throw new InvalidOperationException("O aquecimento já foi finalizado.");
            }
            this.endTime = DateTime.UtcNow;
        }
    }
}
