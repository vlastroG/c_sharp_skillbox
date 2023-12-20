using Workers.Entities;

namespace Workers.Repositories {
    internal class Repository {
        private const string _path = "./workers.txt";
        private int _lastId;


        public Repository() {
            if (File.Exists(_path)) {
                using (StreamReader reader = new StreamReader(_path)) {
                    string? prevLine = null;
                    string? line;
                    while ((line = reader.ReadLine()) != null) {
                        prevLine = line;
                    }
                    if (int.TryParse(prevLine?.Split('#').FirstOrDefault(), out int result)) {
                        _lastId = result >= 0 ? result : 0;
                    }
                }
            } else {
                _lastId = 0;
            }
        }


        public Worker[] GetAllWorkers() {
            if (File.Exists(_path)) {
                Worker[] workers = new Worker[10];
                using (StreamReader reader = new StreamReader(_path)) {
                    string? line;
                    int i = 0;
                    while ((line = reader.ReadLine()) != null) {
                        try {
                            if (workers.Length <= i) {
                                Array.Resize(ref workers, workers.Length * 2);
                            }
                            workers[i++] = Worker.ConvertFromString(line);
                        } catch (ArgumentException) {
                            continue;
                        }
                    }
                    Array.Resize(ref workers, i);
                }
                return workers;
            } else {
                return Array.Empty<Worker>();
            }
        }

        public Worker GetWorkerById(int id) {
            return GetAllWorkers().First(w => w.Id == id);
        }

        public void DeleteWorker(int id) {
            var workers = GetAllWorkers().Where(worker => worker.Id != id).ToArray();
            using (StreamWriter writer = new StreamWriter(_path, false)) {
                _lastId = 0;
                for (int i = 0; i < workers.Length; i++) {
                    workers[i].Id = ++_lastId;
                    writer.WriteLine(workers[i].ToString());
                }
            }
        }

        public void AddWorker(Worker worker) {
            using (StreamWriter writer = new StreamWriter(_path, true)) {
                worker.Id = ++_lastId;
                writer.WriteLine(worker.ToString());
            }
        }

        public Worker[] GetWorkersBetweenTwoDates(DateTime dateFrom, DateTime dateTo) {
            return GetAllWorkers()
                .Where(worker => dateFrom <= worker.RecordDate && worker.RecordDate <= dateTo)
                .ToArray();
        }
    }
}
