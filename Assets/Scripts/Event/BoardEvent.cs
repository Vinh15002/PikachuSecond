namespace Event
{
    public class BoardEvent
    {
        public delegate void GetDataBoardEvent(int[,] matrix);
        public static GetDataBoardEvent GetData; 
    }
}