# Cabana
  
Det var et sjovt lille projekt:)  
Jeg havde dog lidt problemer, ville gerne have haft lagt entity framework på og lavet routing. Men jeg har fået opfrisket mit Angular og lært en masse om Umbraco.  
  
Jeg har ikke nået alle extra punkterne.  
Første punkt, fordi der kommer en masse sikkerhed ind over som jeg ikke syntes jeg har erfaring nok til at give mig i kast med.  
Andet punkt, jeg har ikke meget erfaring med react eller vue, men jeg ved fra Angular at man skulle have bygget projektet op omkring REST apier i stedet.  
Tredie punkt, jeg har lavet lidt extra, som fx lidt sikkerhed XSS og andet.  

Jeg brugte meget tid på at prøve at sætte Entity Framework op, men det lykkedes ikke.  
Jeg brugte også en del tid på at sætte mig ind i Angular igen.  
  
opsætning:  
Da der bruges SqlSerCE, blev jeg nødt til at download og installere det for at have et DBMS. SqlServerCE kan hentes her:  
https://www.microsoft.com/en-us/download/details.aspx?id=30709  
Ellers skulle det være lige til at sætte op.  

Login backoffice:  
name - joakimjacobsen_441@hotmail.com  
pass - asdf123456  
  
Noter:  
I mine egne projekter, plejer jeg at have en seperat user, mener Umbraco bruger Identity.  
Der er måske nogle misforståelser ved brugen af sektions og trees i backoffice.  
Custom routing til backoffice api er måske ikke muligt og måske ikke nødvendig da Umbraco sørger for auto routing.  
Måske skulle Id i Movie tabellen og UmbId i MyUser tabellen have været long"bigint".  
Entity framework ville have taget sig af sql injections.  
