Данный проект представляет собой REST API для игры в крестики-нолики. API предоставляет возможность создавать новые игры, делать ходы и получать информацию о состоянии игр.

Стек технологий: .NET Core, Entity Framework Core, SQLite

Методы API:

 1) Начало игры
    POST /api/game/start-game - Создает новую игру и возвращает ее идентификатор.

 2) Совершение хода
    POST /api/game/make-move - Совершает ход в указанную ячейку игровой доски. Возвращает обновленное состояние игры.

 3) Получение всех игр
    GET /api/game/get-all-games - Возвращает список всех созданных игр.
    
 4) Получение игры по идентификатору
    GET /api/game/get-game-by-id?id={gameId} - Возвращает игру с указанным идентификатором.

 5) Удаление игры
    DELETE /api/game/delete-game-by-id?id={gameId} - Удаляет игру с указанным идентификатором. Возвращает сообщение об успешном удалении игры.


