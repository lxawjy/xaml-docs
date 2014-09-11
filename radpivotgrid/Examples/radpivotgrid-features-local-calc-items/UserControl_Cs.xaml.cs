#region radpivotgrid-features-local-calc-items_1
public class MenAverageSales : CalculatedItem
{
    protected override AggregateValue GetValue(IAggregateSummaryValues aggregateSummaryValues)
    {
        AggregateValue[] aggregateValues = {
            aggregateSummaryValues.GetAggregateValue("Andrew Fuller"),
            aggregateSummaryValues.GetAggregateValue("Michael Suyama"),
            aggregateSummaryValues.GetAggregateValue("Robert King"),
            aggregateSummaryValues.GetAggregateValue("Steven Buchanan")
        };

        if (aggregateValues.ContainsError())
        {
            return AggregateValue.ErrorAggregateValue;
        }

        double average = aggregateValues.Average(av => av.ConvertOrDefault<double>());
        return new DoubleAggregateValue(average);
    }
}
#endregion

#region radpivotgrid-features-local-calc-items_2
var propertyGroupDescription = new Telerik.Pivot.Core.PropertyGroupDescription();
propertyGroupDescription.PropertyName = "Salesperson";
var calculatedItem = new MenAverageSales();
calculatedItem.GroupName = "Average Sales (Men)";
propertyGroupDescription.CalculatedItems.Add(calculatedItem);
dataProvider.ColumnGroupDescriptions.Add(propertyGroupDescription);
#endregion

#region radpivotgrid-features-local-calc-items_3
private void LocalDataSourceProvider_PrepareDescriptionForField(object sender, PrepareDescriptionForFieldEventArgs e)
{
	if (e.DescriptionType == DataProviderDescriptionType.Group && e.FieldInfo.DisplayName == "Salesperson")
	{
		var description = e.Description as Telerik.Pivot.Core.PropertyGroupDescription;

		if (description != null)
		{
			var calculatedItem = new MenAverageSales();
			calculatedItem.GroupName = "Average Sales (Men)";
			description.CalculatedItems.Add(calculatedItem);
		}
	}
}
#endregion

#region radpivotgrid-features-local-calc-items_4
var salesPersonGroupDescription = new Telerik.Pivot.Core.PropertyGroupDescription();
salesPersonGroupDescription.PropertyName = "Salesperson";
var menAvgSalesCalculatedItem = new MenAverageSales();
menAvgSalesCalculatedItem.GroupName = "Average Sales (Men)";
menAvgSalesCalculatedItem.SolveOrder = 1; 
salesPersonGroupDescription.CalculatedItems.Add(menAvgSalesCalculatedItem);

var countryGroupDescription = new Telerik.Pivot.Core.PropertyGroupDescription();
countryGroupDescription.PropertyName = "Country";
var caCalculatedItem = new CA();
caCalculatedItem.GroupName = "CA";
caCalculatedItem.SolveOrder = 2;
countryGroupDescription.CalculatedItems.Add(caCalculatedItem);

dataProvider.ColumnGroupDescriptions.Add(salesPersonGroupDescription);
dataProvider.RowGroupDescriptions.Add(countryGroupDescription);
#endregion