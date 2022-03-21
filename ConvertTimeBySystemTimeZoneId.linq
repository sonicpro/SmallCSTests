<Query Kind="Statements" />

//var tzinfos = TimeZoneInfo.GetSystemTimeZones();
//foreach (var tzi in tzinfos)
//{
//	Console.WriteLine(tzi.Id);
//}

var delgadosTime = new DateTimeOffset(2022, 3, 21, 11, 30, 0, TimeSpan.FromHours(-5));
var meetingStart = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(delgadosTime, "FLE Standard Time");
Console.WriteLine($"Meeting is starting at {meetingStart:t}");