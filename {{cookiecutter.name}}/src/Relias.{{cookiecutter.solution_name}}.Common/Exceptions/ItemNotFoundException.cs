namespace Relias.UserProfile.Common.Exceptions
{
    /// <summary>
    /// Captures item details when one is not found
    /// </summary>
    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException(string itemId) : base($"Item {itemId} not found")
        {
            // NOP
        }
    }
}
