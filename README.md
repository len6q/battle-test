# Игра про боксеров
В основе игры лежит сражение между двумя игроками. В начале сражения 
игрокам устанавливается равное количество очков здоровья (HP).

Сражение состоит из нескольких раундов, каждый из которых состоит из
двух фаз: фазы планирования и фазы битвы.

Если по завершению фазы битвы HP какого-либо из игроков становится
меньше или равно нулю, то сражение завершается. Игрок с нулевым или
отрицательным значением HP считается проигравшим, а его оппонент
победившим.

Если по завершению фазы битвы HP обоих игроков становится меньше или
равно нулю, то сражение завершается, а результатом игры считается “ничья”.

Если по завершению фазы битвы HP обоих игроков являются
положительными, то сражение продолжается и начинается следующий по
счету раунд.

[_Видео геймплея_](https://youtu.be/-2Ksx1selcQ)

**В проекте реализованы:**

_1. Zenject;_

_2. Машина состояний (State Pattern)._
