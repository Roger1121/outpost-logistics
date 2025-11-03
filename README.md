Aplikacja OutPost Logistics pozwala na zarządzanie flotą pojazdów oraz planowanie ich tras z uwzględnieniem cyklicznych postojów. 

Aplikacja uruchamiana jest jako kontener docker z wykorzystaniem pliku docker-compose.yml znajdującego się w katalogu głównym repozytorium.
Do uruchomienia aplikacji wymagana jest instalacja platformy docker oraz aktywne połączenie internetowe. 

Pierwsze uruchomienie aplikacji,
w zależności od prędkości połączenia internetowego, może zająć do kilku minut, gdyż aplikacja korzysta z dodatkowych kontenerów,
których obrazy muszą zostać pobrane z centralnego repozytorium Dockera. Po zakończeniu uruchamiania, aplikacja będzie dostępna pod adresem
https://localhost:8081/

Przed pierwszym uruchomieniem w przeglądarce może pojawić się ostrzeżenie dotyczące
bezpieczeństwa strony. Wynika ono z faktu, iż do celów deweloperskich użyto certyfikatu SSL bez
podpisu CA. Aby przejść do strony, należy rozwinąć zaawansowane opcje, a następnie wejść w link
„Przejdź do strony” (lub inny podobny w zależności od przeglądarki).
