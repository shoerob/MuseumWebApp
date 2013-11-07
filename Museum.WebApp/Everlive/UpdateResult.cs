
namespace EverLive
{
    internal class UpdateResult
    {
        public string ModifiedAt { get; set; }
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
