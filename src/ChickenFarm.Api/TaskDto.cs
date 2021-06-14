using System;

namespace ChickenFarm.Api
{
    public class TaskDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Temperature { get; set; }
    }
}