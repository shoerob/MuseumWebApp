
namespace EverLive
{
    internal class DeleteResult
    {
        public int Result { get; set; }
        public bool Success
        {
            get
            {
                return Result >= 1;
            }
        }
    }
}
