using System.ComponentModel;

namespace Workers {
    internal enum Command {
        [Description("Отобразить всех записанных сотрудников")]
        GetAllWorkers,
        [Description("Поиск сотрудника по Id")]
        GetWorkerById,
        [Description("Отобразить сотрудников, которые были записаны в заданном диапазоне дат")]
        GetWorkersByDateRange,
        [Description("Удалить сотрудника по Id")]
        DeleteWorker,
        [Description("Добавить сотрудника")]
        AddWorker,
        [Description("Список всех команд")]
        Help
    }
}
