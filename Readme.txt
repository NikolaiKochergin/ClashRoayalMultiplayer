Week-7

1. Немного по другому запек NavMesh. Создал иерархию объектов в Unity с названием Map, на нее повесил NavMeshSurface. Внутри этой иерархии на объекты стен повесил NavMeshModifier, который при запекании через NavMeshSurface вычитает эти объекты из поверхности.

2. Добавил реализацию машины состояний с дженериками, можно посмотреть тут https://github.com/NikolaiKochergin/EGMulty_ClashRoyal_Client/tree/week-7/Assets/Source/Scripts/GameCore/UnitLogic/States. У State все же назвал методы Enter Exit и Update, мне так привычнее.

3. На будущее для контролирования порядка инициализации игры добавил Bootstrap. Для него установил Script Execution Order -500, сразу после EventSystem. Это будет единственный скрипт, для которого мы задаем Script Execution Order.

4. Добавил скрипт Team, который собирает башни и юниты игрока и противника соответственно. Метод GetNearestTowerPosition перенес из MapInfo в Team, убрал проверку isEnemy. Добавил 2 сервиса PlayerService и EnemyService, которые хранят свои команды. Эти сервисы так же инициалзируют своих юнитов и выдают им команды протвника. Инициализация сервисов происходит в Bootstrap. Инициализация этих сервисов сейчас довольно костыльная )). Но это временно, думаю хогда будет понятно как будут спавниться юниты игрока и противнка она приобретет более нормальный вид.

В таком варианте это некоторый задел на будущую архитектуру. В дальнейшем скорее всего переработаю методы Construct и Initialize, то что они делают, когда будет понятна структура проекта. Так же в будущем планирую добавить DI.

5. Добавил интерфейс ITarget. Хранит в себе трансформ цели и радиус цели, так же добавил в него реализацию по умолчанию метода GetDistance, которая в лекцих была у Tower.

6. Немного подумал и пока что убрал GetDistance из ITarget, тк не получалось отладить переключения состояний.

7. Добавил уничтожение башен и юнитов при смерти. Добавил состояние смерти для юнитов. В AttackState в Update переделал проверку на смерть атакуемого юнита. Тк в моей реализации _target все еще мог существовать в текущем кадре, хотя его объект уже был уничтожен (По крайне мере я это так понял).

8. Добавил юнитам состояние победы, если все башни и вражеские юниты уже уничтожены.

9. Добавил юнитам и башням по слайдеру, для отображения здоровья.

10. Решил заморочиться с процессом создания FSM и сделал для нее билдер. https://github.com/NikolaiKochergin/EGMulty_ClashRoyal_Client/tree/week-7/Assets/Source/Scripts/GameCore/States

-----------------------------------------------------------------------------------------------------------------------------------------------

Week-8

1. Переделал добавление юнитов и башен в команды в соответствии с уроками. Добавил HealthBar с подпиской на изменение здоровья. Наненсение урона в Health происходит через интерфейс IDamageable. Сделал такую цепочку чтобы была возможность добавить какие то эффекты при нанесении урона или что то подобное, чтобы не пихать эту логику в модель здоровья, а она оставалась в модели юнита.

2. В рабочем проекте я скорее всего так не усложнял все и оставил бы логику смены состояний в самих состояних. Но так как это учебный проект, решил по эксперементировать с другим решением. Теперь сами состояния не знают о машине состояний. Логика переключения состояний находится в скриптах Brain https://github.com/NikolaiKochergin/EGMulty_ClashRoyal_Client/tree/week-8/Assets/Source/Scripts/GameCore/UnitLogic/AI

3. Добавил разных юнитов. Поиск и добавление юнитов реализовал по другому чем в лекциях. В моем случае юниты и башни добавляются в скрипт Team.
https://github.com/NikolaiKochergin/EGMulty_ClashRoyal_Client/blob/week-8/Assets/Source/Scripts/GameCore/Team.cs

4. Сами юниты вот такие получились.
https://github.com/NikolaiKochergin/EGMulty_ClashRoyal_Client/blob/week-8/Assets/Source/Scripts/GameCore/UnitLogic/UnitBase.cs
https://github.com/NikolaiKochergin/EGMulty_ClashRoyal_Client/blob/week-8/Assets/Source/Scripts/GameCore/UnitLogic/MeleeUnit.cs
https://github.com/NikolaiKochergin/EGMulty_ClashRoyal_Client/blob/week-8/Assets/Source/Scripts/GameCore/UnitLogic/RangeUnit.cs
https://github.com/NikolaiKochergin/EGMulty_ClashRoyal_Client/blob/week-8/Assets/Source/Scripts/GameCore/UnitLogic/GiantUnit.cs

5. Синхронизацию нанесения урона целям с анимациями сделал через триггер эвенты анимаций. Сами атаки имеют базовый класс и назначаются в инспекторе.
https://github.com/NikolaiKochergin/EGMulty_ClashRoyal_Client/blob/week-8/Assets/Source/Scripts/GameCore/UnitLogic/AttackBase.cs
https://github.com/NikolaiKochergin/EGMulty_ClashRoyal_Client/blob/week-8/Assets/Source/Scripts/GameCore/UnitLogic/MeleeAttack.cs
https://github.com/NikolaiKochergin/EGMulty_ClashRoyal_Client/blob/week-8/Assets/Source/Scripts/GameCore/UnitLogic/RangeAttack.cs

6. Добавил возможность передвижения юнитов через Transform
https://github.com/NikolaiKochergin/EGMulty_ClashRoyal_Client/blob/week-8/Assets/Source/Scripts/GameCore/UnitLogic/TransformMover.cs


