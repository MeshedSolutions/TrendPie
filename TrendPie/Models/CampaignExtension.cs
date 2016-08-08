using System.Globalization;

namespace TrendPie.Models
{
    public partial class Campaign
    {
        public string StartDay { get; set; }
        public string StartMonth { get; set; }
        public string StartYear { get; set; }
        public string EndDay { get; set; }
        public string EndMonth { get; set; }
        public string EndYear { get; set; }
        public string EndMonthAbbreviation
        {
            get
            {
                string abbreviation = "";

                if (EndDate != null)
                {
                    switch (EndDate.Value.Month)
                    {
                        case 1:
                            abbreviation = "Jan";
                            break;
                        case 2:
                            abbreviation = "Feb";
                            break;
                        case 3:
                            abbreviation = "Mar";
                            break;
                        case 4:
                            abbreviation = "Apr";
                            break;
                        case 5:
                            abbreviation = "May";
                            break;
                        case 6:
                            abbreviation = "Jun";
                            break;
                        case 7:
                            abbreviation = "Jul";
                            break;
                        case 8:
                            abbreviation = "Aug";
                            break;
                        case 9:
                            abbreviation = "Sep";
                            break;
                        case 10:
                            abbreviation = "Oct";
                            break;
                        case 11:
                            abbreviation = "Nov";
                            break;
                        case 12:
                            abbreviation = "Dec";
                            break;
                    }
                }

                return abbreviation;
            }
        }
        public string ShortStartDate
        {
            get
            {
                string date = string.Empty;

                if (StartDate != null) date = StartDate.Value.ToShortDateString();

                return date;
            }
        }
        public string ShortEndDate
        {
            get
            {
                string date = string.Empty;

                if (EndDate != null) date = EndDate.Value.ToShortDateString();

                return date;
            }
        }
    }
}