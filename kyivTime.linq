<Query Kind="Statements" />

var kyivTime = new DateTime(2022, 5, 27, 16, 50, 0, 0, DateTimeKind.Local);
var kyivOffset = new DateTimeOffset(kyivTime, TimeSpan.FromHours(3));
Console.WriteLine(kyivTime);
Console.WriteLine(kyivOffset);

var dtfi = Thread.CurrentThread.CurrentCulture.DateTimeFormat;
// In Universal sortable date time format:
Console.WriteLine(dtfi.UniversalSortableDateTimePattern);
Console.WriteLine(kyivTime.ToString(dtfi.UniversalSortableDateTimePattern));
Console.WriteLine(kyivOffset.ToString(dtfi.UniversalSortableDateTimePattern));