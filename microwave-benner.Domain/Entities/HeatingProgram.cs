

using System.Xml.Linq;

namespace microwave_benner.Domain.Entities
{
    public class HeatingProgram
    {
        public int id { get; private set; }
        public string name { get; private set; }
        public string food { get; private set; }
        public int time { get; private set; }
        public int power { get; private set; }
        public string? instructions { get; private set; }
        public bool custom { get; private set; }

        public HeatingProgram(string name, string food, int time, int power, string? instructions = null, bool custom = false)
        {
            this.name = name;
            this.food = food;
            this.time = time;
            this.power = power;
            this.instructions = instructions;
            this.custom = custom;
        }

        public HeatingProgram(int id, string name, string food, int time, int power, string? instructions = null, bool custom = false)
        {
            this.id = id;
            this.name = name;
            this.food = food;
            this.time = time;
            this.power = power;
            this.instructions = instructions;
            this.custom = custom;
        }
    }
}
