<Query Kind="Statements">
  <Namespace>System.Net</Namespace>
</Query>

string clientId = "4ba3367db1b0415f8005160bde4bdde9";
string clientSecret = "H94K8tkvcIzb1PWo9vRl5kybej8ma8meUQJ_QCMITeXJfzukc65bMKVYV-_u7fLo";
var credentialsByteArray = Encoding.ASCII.GetBytes($"{clientId}:{clientSecret}");
Console.WriteLine(Convert.ToBase64String(credentialsByteArray));