namespace KhoThoMVP.DTOs
{
    public class WorkerJobTypeDto
    {
        public int WorkerJobTypeId { get; set; }
        public int WorkerId { get; set; }
        public int JobTypeId { get; set; }
        public JobTypeDto? JobType { get; set; }
        public WorkerDto? Worker { get; set; }
    }
}
