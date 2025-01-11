namespace Api_ProgrmmingCommunity.Dto
{
    public class TaskDTO
    {
        public int Id { get; set; }

        public int? MinLevel { get; set; }

        public int? MaxLevel { get; set; }

        public DateOnly? StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public bool? IsDeleted { get; set; }
        }

}
