# BtcSignal api backend
<h3>PL</h3>
Btc signal - aplikacja internetowa, służąca do komunikowania użytkownika sms’em o przekroczeniu wcześniej ustawionego progu cenowego Bitcoina.</br></br>

Technologie backend: </br>
-.Net Core 3.1 </br>
-MS Sql Server</br>
-Entity Framework</br>
-Quartz.Net</br>
-wzorzec projektowy ‚Clean Architecture’.</br>

Technologie frontend:</br>
-React</br>

Cechy aplikacji: </br>
-logowanie oraz rejestracja użytkownika za pomocą JWT Token</br>
-obsługa wysyłania emaili do potwierdzenia konta podczas rejestracji oraz przy każdym logowaniu (serwis zewnętrzny SendGrid)</br>
-enpointy Rest API w aplikacji umożliwiające zapisywanie, usuwanie, logowanie itd. ustawionych progów cenowych</br>
-harmonogram zadań Quartz.Net, w którym będzie zawarta logika sprawdzania obecnego kursu bitcoina i wysyłająca powiadomienia do zewnetrznego serwisu sms (w trakcie realizacji)</br>

<h3>ENG</h3>

Btc signal - an internet application used to notify the user via SMS about exceeding the pre-set Bitcoin price threshold.</br>

Backend technologies:</br>
-.Net Core 3.1</br>
-MS Sql Server</br>
-Entity Framework</br>
-Quartz.Net</br>
-"Clean Architecture" design pattern.</br>

Technologie frontend:</br>
-React</br>

Features of the app:</br>
-logging and user registration with JWT Token</br>
-support for sending emails to confirm the account during registration and each time you log in (external SendGrid service)</br>
-enpoints Rest API in the application that allow saving, deleting, logging, etc. set price thresholds</br>
-Quartz.Net task schedule, which will contain the logic of checking the current bitcoin rate and sending notifications to an external sms service (in progress)
