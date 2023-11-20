MAUI app made in order to showcase the use of the Salling Group Holidays API for TechChapter.

Task given by TechChapter:

## Holiday Calendar
#### Background
For the Tech Chapter timeregistration we need to notify employees if they forget to do their time registration. However, holidays are days off why they do not need to fill in their hours.  

**Hint:**
Holidays are a national mater not supported by the [GregorianCalendar](https://docs.microsoft.com/en-us/dotnet/api/system.globalization.gregoriancalendar) typically used for Calendar functionality within C#. Especially the easter can cause issues, since the days will vary from one year to the next based on the moon cycle. Also Maundy Thursday is a Holiday in Denmark but not in other countries like Sweden. Salling Group, that runs a large number of supermarkets has exposed an [API](https://developer.sallinggroup.com/api-reference#apis-holidays) of Danish holidays that may help you in succeeding this task. You will need to sign up for a token before you can start the integration.


#### Userstory 1
**AS A** bookkeeper  
**I WANT** a to know if a given day is a holiday  
**SO THAT** i know wether my colleagues must do time registration or not.  

#### Accept criteria
**Scenario: Holiday**  
**GIVEN** X-mas day: December 25th 2023
**WHEN** asking if holiday  
**THEN** return true  

**Scenario: Weekday**  
**GIVEN** regular weekday: April 21st 2023
**WHEN** asking if holiday  
**THEN** return false  

#### Userstory 2
**AS A** bookkeeper
**I WANT** a to know all holidays within a time period  
**SO THAT** I can tell my colleagues in advance  

**Scenario: Easter month**  
**GIVEN** month with easter: April 2023
**WHEN** asking for all holidays  
**THEN** return Danish national holidays: Maundy thursday, Good Friday, Easter Sunday & Easter Monday)  
