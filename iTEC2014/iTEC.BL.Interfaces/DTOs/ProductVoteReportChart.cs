using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTEC.BL.Interfaces.DTOs
{
    public class ReportChart
    {
        public ChartType Chart { get; set; }
        public Title Title { get; set; }
        public Title Subtitle { get; set; }
        public XAxis XAxis { get; set; }
        public YAxis YAxis { get; set; }
        public Tooltip Tooltip { get; set; }
        public ChartSeries[] Series { get; set; }
    }

    public class AreasplineReportChart : ReportChart
    {
        public AreasplineReportChart()
        {
            Chart = new ChartType() { Type = "areaspline" };
        }
    }
    
    public class ProductVoteReportChart : AreasplineReportChart
    {
        public void Generate(VotedProductDTO[] products)
        {
            Title = new Title() { Text = "Votes on products" };
            Subtitle = new Title() { Text = "Current session" };
            XAxis = new XAxis() { Categories = products.Select(x => x.Name).ToArray() };
            YAxis = new YAxis() { Title = new Title() { Text = "Vote number" } };
            Tooltip = new Tooltip() { Shared = true, ValueSuffix = "votes" };
            Series = new ChartSeries[] {
                new ChartSeries() { Name = "Current", Data = products.Select(x => x.Points).ToArray() }
            };
        }
    }

    public class ChartSeries
    {
        public String Name { get; set; }
        public Int32[] Data { get; set; }
    }

    public class ChartType
    {
        public string Type { get; set; }
    }

    public class Title
    {
        public string Text { get; set; }
    }

    public class XAxis
    {
        public String[] Categories { get; set; }
    }

    public class Tooltip
    {
        public bool Shared { get; set; }
        public string ValueSuffix { get; set; }
    }

    public class YAxis
    {
        public Title Title { get; set; }
    }
}
