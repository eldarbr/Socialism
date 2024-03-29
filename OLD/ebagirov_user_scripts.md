# Эльдар Багиров - "Socialism Simulator"
# Пользовательские сценарии

### Группа: 10И4
### Электронная почта: eldar@bagirov.ru


### [ Сценарий 1 - Навигация в главном меню ]

1. При запуске приложения пользователь попадает в главное меню
1. Режим игры выбирается из списка:
	1. Если пользователь выбирает режим стандартного социализма и нажимает кнопку старта, начинается игра в режиме стандартного социализма (Сценарий 3)
	1. Если пользователь выбирает режим демократического социализма и нажимает кнопку старта, начинается игра в режиме демократического социализма (Сценарий 3)
1. В главном меню можно произвести настройку звуков:
	1. Есть возможность выключить по умолчанию включенные звуковые эффекты
1. В главном меню можно посмотреть свой рекорд, а также поделиться им по нажатию (Сценарий 5)

### [ Сценарий 2 - Обучение ]

1. Обучение проходит внутри игрового интерфейса
1. Во время обучения игровое время остановлено, возможности пользователя взаимодействовать с интерфейсом ограничены
1. Обучение начинается автоматически, если пользователь нажал "играть" впервые
1. Интерактивные таблички указывают на нужный элемент интерфейса
1. Краткий текст описывает его предназначение
1. После прочтения текста пользователь нажимает на кнопку для продолжения
1. После введения во все элементы интерфейса и детали геймплея обучение завершается автоматически

### [ Сценарий 3 - Игра в режиме стандартного социализма ]

1. Пользователь попадает в игровой интерфейс
	1. Если игра запускается впервые, автоматически начнется обучение (Сценарий 2)
	1. Иначе игра продолжится в нормальном режиме
1. Внимание пользователя сосредоточено на трех шкалах - показателях социалистического государства (деньги, уровень жизни граждан, лояльность избирателей), которым управляет игрок. В начале игры показатели заполнены максимально. С течением времени показатели стремятся к нулю
1. Пользователь должен предпринимать действия, чтобы увеличить показатели:
	1. игроку предоставляется выбор из трех категорий действий. Категории разделяют возможные действия по максимальному влиянию на каждый показатель (действия из категории 1 больше всего влияют на показатель 1; однако все действия могут влиять на все показатели)
1. В процессе игры пользователю показывается
	1. количество "внимания" - игровой "валюты", которая увеличивается каждый игровой день и уменьшается с каждым предпринятым действием
	1. игровой счет - количество игровых дней, в течение которых пользователь смог продержаться
1. Пользователь может поставить игру на паузу (Сценарий 5)
1. Игра заканчивается, если хотя бы один из игровых показателей равен нулю
1. Пользователю показывается экран проигрыша с пояснением, что произошло (какой из показателей иссяк)
1. На экране проигрыша показывается счет. Он выделяется, если это рекорд. По нажатию на счет пользователь может поделиться им

### [ Сценарий 4 - Игра в режиме демократического социализма ]

1. Пользователь попадает в игровой интерфейс
	1. Если игра запускается впервые, автоматически начнется обучение (Сценарий 2)
	1. Иначе игра продолжится в нормальном режиме
1. Внимание пользователя сосредоточено на трех шкалах - показателях социалистического государства с подчеркнутой демократичностью (деньги, уровень жизни граждан, лояльность избирателей), которым управляет игрок. В начале игры показатели заполнены максимально. С течением времени показатели стремятся к нулю
1. Пользователь должен предпринимать действия, чтобы увеличить показатели:
	1. игроку предоставляется выбор из трех категорий действий. Категории разделяют возможные действия по максимальному влиянию на каждый показатель (действия из категории 1 больше всего влияют на показатель 1; однако все действия могут влиять на все показатели)
1. В процессе игры пользователю показывается
	1. количество "внимания" - игровой "валюты", которая увеличивается каждый игровой день и уменьшается с каждым предпринятым действием
	1. игровой счет - количество игровых дней, в течение которых пользователь смог продержаться
1. Пользователь может поставить игру на паузу (Сценарий 5)
1. Игра заканчивается, если хотя бы один из игровых показателей равен нулю
1. Пользователю показывается экран проигрыша с пояснением, что произошло (какой из показателей иссяк)
1. На экране проигрыша показывается счет. Он выделяется, если это рекорд. По нажатию на счет пользователь может поделиться им

### [ Сценарий 5 - Игра в режиме коммунизма ]

1. Пользователь попадает в игровой интерфейс
	1. Если игра запускается впервые, автоматически начнется обучение (Сценарий 2)
	1. Иначе игра продолжится в нормальном режиме
1. Внимание пользователя сосредоточено на трех шкалах - показателях коммунистического государства (деньги, уровень жизни граждан, лояльность избирателей), которым управляет игрок. В начале игры показатели заполнены максимально. С течением времени показатели стремятся к нулю
1. Пользователь должен предпринимать действия, чтобы увеличить показатели:
	1. игроку предоставляется выбор из трех категорий действий. Категории разделяют возможные действия по максимальному влиянию на каждый показатель (действия из категории 1 больше всего влияют на показатель 1; однако все действия могут влиять на все показатели)
1. В процессе игры пользователю показывается
	1. количество "внимания" - игровой "валюты", которая увеличивается каждый игровой день и уменьшается с каждым предпринятым действием
	1. игровой счет - количество игровых дней, в течение которых пользователь смог продержаться
1. Пользователь может поставить игру на паузу (Сценарий 5)
1. Игра заканчивается, если хотя бы один из игровых показателей равен нулю
1. Пользователю показывается экран проигрыша с пояснением, что произошло (какой из показателей иссяк)
1. На экране проигрыша показывается счет. Он выделяется, если это рекорд. По нажатию на счет пользователь может поделиться им

### [ Сценарий 6 - Навигация в меню паузы ]

1. Меню паузы можно открыть с помощью специального элемента интерфейса - кнопки
1. Меню паузы предоставляет возможность временно остановить игру
1. Из паузы можно запустить туториал прямо во время игры:
	1. Это не повлияет на прогресс
	1. Игра продолжится сразу же после окончания обучения
1. Из паузы пользователь может выйти в главное меню, то есть закончить игру
1. Меню паузы не закрывает интерфейс полностью, позволяет игроку прочитать описание действия и обдумать решение дольше

### [ Сценарий 7 - Опция поделиться рекордом ]

1. Если во время игры пользователь побил свой рекорд, приложение предложит поделиться им
1. Пользователь может отказаться, тогда предложение закроется
1. Поделиться рекордом можно из главного меню:
	1. Для этого нужно нажать на область отображения рекорда в меню
	1. Выбрать рекорд из определенного режима игры, который будет отправлен
1. Если пользователь Решил поделиться рекордом, открывается стандартное для системы окно Поделиться (Android Sharesheet)
1. Внутри этого окна пользователь выбирает приложение, в которое будет отправлено сообщение с рекордом, а тажке адресата
