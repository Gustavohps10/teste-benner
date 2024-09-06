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

        public void Resume()
        {
            if (!pauseTime.HasValue)
                throw new InvalidOperationException("A tarefa não está pausada e não pode ser retomada.");

            if (IsFinished())
                throw new InvalidOperationException("A tarefa já foi concluída e não pode ser retomada.");

            TimeSpan pausedDuration = DateTime.Now - pauseTime.Value;
            startTime = startTime.Value.Add(pausedDuration);

            pauseTime = null;
        }

        public void AddTime()
        {
            if (this.startTime.HasValue && !this.endTime.HasValue)
            {
                // Adiciona o tempo extra se o aquecimento está em execução
                this.time += 30;
            }
            else
            {
                throw new InvalidOperationException("O aquecimento não está em execução.");
            }
        }

        //Pegar tempo restante
        public int GetRemainingTime()
        {

            if (!startTime.HasValue)
                return time;

            int elapsedSeconds = 0;

            if (pauseTime.HasValue)
            {
                elapsedSeconds = (int)(pauseTime.Value - startTime.Value).TotalSeconds;
            }
            else
            {
                elapsedSeconds = (int)(DateTime.UtcNow - startTime.Value).TotalSeconds;
            }

            if (endTime.HasValue)
                return 0;

            int remainingTime = time - elapsedSeconds;

            return Math.Max(0, remainingTime);
        }



        public bool IsRunning()
        {
            if (!startTime.HasValue)
                return false;

            if (pauseTime.HasValue)
                return false;

            return GetRemainingTime() > 0;
        }

        
        public bool IsFinished()
        {
            var remainingtime = GetRemainingTime();
            return endTime.HasValue || remainingtime <= 0;
        }

        public bool IsPaused()
        {
            if (!startTime.HasValue)
                return false;

            return pauseTime.HasValue;
        }
    }
}
