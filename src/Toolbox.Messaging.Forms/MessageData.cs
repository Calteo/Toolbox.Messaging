namespace Toolbox.Messaging.Forms
{
    [Serializable]
    public class MessageData
    {
        public MessageData()
        {
            MagicNumber = new Random().Next(9999);
        }

        public int MagicNumber { get; }
        public int ResposeNumber { get; set; }
    }
}