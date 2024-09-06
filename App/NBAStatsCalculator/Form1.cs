namespace NBAStatsCalculator
{
    public partial class Form1 : Form
    {
        public  double[] dayOfWeek = {1, 2, 3, 4, 5, 6, 7 };
        public int[] nbPoints = { 10, 20, 30, 40, 50, 60, 70 };
        public DateTime dateTest = new DateTime(2024, 9, 6);
        public Form1()
        {
            InitializeComponent();
            Graph graph1 = new Graph("equipe1");
            graph1.createGraph(this,nbPoints,dayOfWeek);

            int daysOfWeek = ConvertDateToDayOfWeekNumber(dateTest);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public int ConvertDateToDayOfWeekNumber(DateTime dateOfDay)
        {
            int dayOfWeek = (int)dateOfDay.DayOfWeek;
            return dayOfWeek;

        }

    }
}
