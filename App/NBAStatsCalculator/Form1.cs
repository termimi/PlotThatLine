namespace NBAStatsCalculator
{
    public partial class Form1 : Form
    {
        public List<double> nbPoints = new List<double>
        {
            75, 
            85, 
            110,
            95, 
            98, 
            75, 
            83
        };
        public DateTime dateTest = new DateTime(2024, 9, 6);
        public Form1()
        {
            InitializeComponent();
            Graph graph1 = new Graph("equipe1");
            graph1.createGraph(this,nbPoints);

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
