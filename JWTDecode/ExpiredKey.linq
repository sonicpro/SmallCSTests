<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.ComponentModel.TypeConverter.dll</Reference>
  <Namespace>System.ComponentModel</Namespace>
</Query>

void Main()
{
//	var letterMBase64Url = "TQ";
//	Console.WriteLine(Base64UrlToBase64(letterMBase64Url));
//
//	var lettersMaBase64Url = "TWE";
//	Console.WriteLine(Base64UrlToBase64(lettersMaBase64Url));
//
//
//	var lettersManBase64Url = "TWFu";
//	Console.WriteLine(Base64UrlToBase64(lettersManBase64Url));
	
	var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJTdHVkZW50UG9ydGZvbGlvSWQiOiIxNjA5NDY5NyIsIlVzZXJOYW1lIjoidGVzdC1uaXhvbiIsIlVzZXJUeXBlIjoiU3R1ZGVudCIsIlNlY3VyaXR5U3RhbXAiOiIyYTQ5Y2JjZi1jOTM4LTQxMGEtYjQzMi1jYWY5YjUxY2YzNDMiLCJUb2tlblR5cGUiOiJBdXRoVG9rZW4iLCJVc2VyQWNjb3VudElkIjoiNDUzMzE5IiwiVGVzdGVyIjoidGVzdC1uaXhvbiIsIm5iZiI6MTU3OTI3MTU3MCwiZXhwIjoxNTc5MjczMzcwLCJpYXQiOjE1NzkyNzE1NzAsImlzcyI6InhlbGxvLndvcmxkIiwiYXVkIjoiNDNhMThkZWYyNzEwNDM3Yzg2NjExYWJhYTE1MTcwMWQifQ.F1FeqfaSBBh0l_bfYcjT4i34V6_90X4n4TJCzXIAoMg";
	var payload = token.Split('.')[1];
	byte[] textAsBytes = System.Convert.FromBase64String(Base64UrlToBase64(payload));
	Console.WriteLine(Encoding.UTF8.GetString(textAsBytes));
}

// Define other methods and classes here
private static string Base64UrlToBase64(string base64Url)
{
	// During conversion from Base64 to Base64Url the padding characters ('=') are dropped.
	// Valid paddings are either '=' or '==' and never '==='. Reason: to get '===' padding we must have 18 zero bits at the end of the byte array that is being converted to Base 64 padded with zeroes so
	// that numberOfBits % 24 == 0.
	// It means that the byte array length in bits modulo 24 (which is LCM of 8 and 6) must be equal 6. It is never true, because the byte array length in bits is multiple of 8,
	// and the multiple of eight modulo 24 is either 0, or 8, or 16, and never 6.
	if ((base64Url.Length % 4) == 1)
	{
		throw new ArgumentException("The input is not a Base64Url-encoded string.");
	}
	// Deduce how many 6-bit charaters should be added to get the total number of bits divisible by 8.
	var missingBase64Characters = base64Url.Length % 4;
	var padding = "==".Substring(0, missingBase64Characters == 0 ? 0 : 4 - missingBase64Characters);
	return base64Url.Replace('-', '+').Replace('_', '/') + padding;
}

// ------------------- Encoding examples ( the rule - always pad the bits that represent Base64 digits with zeroes to be divisible by 24) -----------------------------------------
//      Example 1 - encoding 16 bits ('Ma' in ACSII) to Base64
//    M (77)          |     a (97)   
// 0 1 0 0  1 1   0 1 | 0 1 1 0   0 0 0 1
// Grouped by six bits:
// 0 1 0 0  1 1 | 0 1   0 1 1 0 | 0 0 0 1
// Padded by zeroes to be divisible by 24:
// 0 1 0 0  1 1 | 0 1   0 1 1 0 | 0 0 0 1 0 0 | 0 0 0 0 0 0
// Represented by Base64 digits: (see https://base64.guru/learn/base64-characters for the character table)
//      T       |       W       |     E       |      =

//      Example 2 - encoding 8 bits ('M' in ASCII) to Base64
//    M (77)
// 0 1 0 0  1 1   0 1
// Grouped by six bits:
// 0 1 0 0  1 1 | 0 1
// Padded by zeroes to be divisible by 24:
// 0 1 0 0  1 1 | 0 1   0 0 0 0 | 0 0 0 0 0 0 | 0 0 0 0 0 0
// Reporesented by Base64 digits:
//      T       |       Q       |     =       |      =